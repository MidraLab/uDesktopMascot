using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Logging;
using UnityEngine;
using UnityEngine.InputSystem;

namespace uDesktopMascot
{
    /// <summary>
    /// モデルのモーションを制御するクラス
    /// </summary>
    public class CharacterController : MonoBehaviour
    {
        /// <summary>
        /// モデルのアニメーター
        /// </summary>
        private Animator _modelAnimator;

        /// <summary>
        /// モデルのゲームオブジェクト
        /// </summary>
        private GameObject _model;

        /// <summary>
        /// メインカメラ
        /// </summary>
        private Camera _mainCamera;

        /// <summary>
        /// キャンセルトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// 初期化済みかどうか
        /// </summary>
        private bool _isInitialized = false;

        /// <summary>
        /// Input Systemのアクション
        /// </summary>
        private UDMInputActions _inputActions;

        /// <summary>
        /// マウスがドラッグ中かどうか
        /// </summary>
        private bool _isDragging = false;

        /// <summary>
        ///     ドラッグ対象のオブジェクトがモデルかどうか
        /// </summary>
        private bool _isDraggingModel;

        /// <summary>
        ///     終了処理中かどうか
        /// </summary>
        private bool _isQuitting;

        /// <summary>
        ///     ドラッグ開始位置
        /// </summary>
        private Vector2 _startDragPosition;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _cancellationTokenSource = new CancellationTokenSource();

            // InputActionsのインスタンスを作成
            _inputActions = new UDMInputActions();
        }

        private void OnEnable()
        {
            // 入力アクションを有効化
            _inputActions.Enable();

            // イベントの購読
            _inputActions.UI.Click.started += OnClickStarted;
            _inputActions.UI.Click.canceled += OnClickCanceled;

            _inputActions.UI.Hold.performed += OnHoldPerformed;

            Application.wantsToQuit += OnWantsToQuit;
        }

        private void OnDisable()
        {
            // 入力アクションを無効化
            _inputActions.Disable();

            // イベントの購読解除
            _inputActions.UI.Click.started -= OnClickStarted;
            _inputActions.UI.Click.canceled -= OnClickCanceled;

            _inputActions.UI.Hold.performed -= OnHoldPerformed;

            Application.wantsToQuit -= OnWantsToQuit;
        }

        private void Start()
        {
            InitModel().Forget();
            VoiceController.Instance.PlayStartVoiceAsync(_cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        /// モデルの初期化
        /// </summary>
        private async UniTaskVoid InitModel()
        {
            try
            {
                _model = await LoadVRM.LoadModel(_cancellationTokenSource.Token);
                _modelAnimator = _model.GetComponent<Animator>();
                LoadVRM.UpdateAnimationController(_modelAnimator);

                // モデルにColliderを追加（既にある場合は不要）
                LoadVRM.AddCollidersToModel(_model);

                _isInitialized = true;
            } catch (Exception e)
            {
                Log.Error($"モデルの初期化中にエラーが発生しました: {e.Message}");
            }
        }

        private void Update()
        {
            if (!_isInitialized)
            {
                return;
            }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

            // デスクトップ上のマウスカーソル座標を取得
            var cursorPos = MouseUtility.GetCursorPosition();

            // エクスプローラーウィンドウの位置を取得
            var explorerWindows = ExplorerWindowDetector.GetExplorerWindows();

            bool isNearExplorerTop = false;

            foreach (var window in explorerWindows)
            {
                // ウィンドウの矩形情報を取得
                var rect = window.rect;

                // DPIスケールを取得
                float dpiScale = ExplorerWindowDetector.GetDPIScale();

                // ウィンドウの座標をDPIスケールで割る
                rect.left = (int)(rect.left / dpiScale);
                rect.top = (int)(rect.top / dpiScale);
                rect.right = (int)(rect.right / dpiScale);
                rect.bottom = (int)(rect.bottom / dpiScale);

                // マウスがウィンドウの上部付近にいるか判定（例：ウィンドウの上端から50ピクセル以内）
                if (cursorPos.x >= rect.left && cursorPos.x <= rect.right)
                {
                    if (cursorPos.y >= rect.top - 50 && cursorPos.y <= rect.top + 50)
                    {
                        isNearExplorerTop = true;
                        break;
                    }
                }
            }
#endif

            // モーションを切り替える
            if (_isDragging && _isDraggingModel)
            {
                // ドラッグ中はハンギングモーション（ぶら下がりモーション）
                _modelAnimator.SetBool(Const.IsSitting, false);
                _modelAnimator.SetBool(Const.IsDragging, true);
            } else
            {
                _modelAnimator.SetBool(Const.IsDragging, false);
                // 座りモーションまたは立ちモーションに切り替え
                _modelAnimator.SetBool(Const.IsSitting, false);
            }
        }

        /// <summary>
        ///     ドラッグが行われたときの処理
        /// </summary>
        /// <param name="context"></param>
        private void OnHoldPerformed(InputAction.CallbackContext context)
        {
            _isDragging = !_isDragging;

            // マウス位置を取得
            var mousePosition = _inputActions.UI.Point.ReadValue<Vector2>();

            // マウス位置からレイを飛ばす
            var ray = _mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform == _model.transform || hit.transform.IsChildOf(_model.transform))
                {
                    // モデルがクリックされた
                    _isDraggingModel = true;
                    VoiceController.Instance.PlayHoldVoice();
                } else
                {
                    _isDraggingModel = false;
                }
            } else
            {
                _isDraggingModel = false;
            }
        }

        /// <summary>
        ///     クリックが押されたときの処理
        /// </summary>
        /// <param name="context"></param>
        private void OnClickStarted(InputAction.CallbackContext context)
        {
            VoiceController.Instance.PlayClickVoice();
        }

        /// <summary>
        /// クリックが離されたときの処理
        /// </summary>
        /// <param name="context"></param>
        private void OnClickCanceled(InputAction.CallbackContext context)
        {
            _isDragging = false;

            // アニメーターのパラメータをリセット
            _modelAnimator.SetBool(Const.IsDragging, false);
            Log.Debug("Click終了");
        }

        /// <summary>
        ///     アプリケーションが終了するときの処理
        /// </summary>
        /// <returns></returns>
        private bool OnWantsToQuit()
        {
            if (_isQuitting)
            {
                return true;
            }

            _isQuitting = true;
            HandleApplicationQuit(_cancellationTokenSource.Token).Forget();
            return false;
        }


        /// <summary>
        ///     アプリケーションが終了するときの処理
        /// </summary>
        /// <param name="cancellationToken"></param>
        private async UniTaskVoid HandleApplicationQuit(CancellationToken cancellationToken)
        {
#if UNITY_EDITOR
            // エディタの場合、再生モードを停止
            // ref: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/EditorApplication.html
            // いろいろできそうだが、とりあえず再生モードを停止するだけにしておく
            Log.Debug("アプリケーション終了ボイスを流します。Editorの場合はボイスの再生はしません");
            await UniTask.CompletedTask;
#else
            await VoiceController.Instance.PlayEndVoiceAsync(cancellationToken);
            // ビルド後のアプリケーションでは通常の終了処理
            Application.Quit();
#endif
        }


        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}