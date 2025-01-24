using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CharacterManager : MonoBehaviour
    {
        /// <summary>
        /// モデルのアニメーター
        /// </summary>
        private Animator _modelAnimator;

        /// <summary>
        /// モデルのデフォルトアニメーションクリップ
        /// </summary>
        [SerializeField] private List<AnimationClip> _defaultAnimationClip;

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
        /// キャラクターのアニメーションコントローラ
        /// </summary>
        private CharacterAnimationController _characterAnimationController;

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
            // InitAnimation();
            VoiceController.Instance.PlayStartVoiceAsync(_cancellationTokenSource.Token).Forget();
        }

        /// <summary>
        /// アニメーションの初期化
        /// </summary>
        private void InitAnimation()
        {
            _characterAnimationController = new CharacterAnimationController(_modelAnimator);
            var initAnimation = _defaultAnimationClip.FirstOrDefault(x => x.name == "idle");
            if(initAnimation == null)
            {
                Log.Error("デフォルトのアニメーションクリップが見つかりませんでした。");
                return;
            }
            _characterAnimationController.SetInitialAnimation(initAnimation);
        }

        /// <summary>
        /// モデルの初期化
        /// </summary>
        private async UniTaskVoid InitModel()
        {
            try
            {
                _model = await LoadCharacterModel.LoadModel(_cancellationTokenSource.Token);
                
                await UniTask.SwitchToMainThread();
                
                // モデルの初期調節
                OnModelLoaded(_model);
                
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

            // モデルのスクリーン座標を取得
            var modelScreenPos = ScreenUtility.GetModelScreenPosition(_mainCamera, _model.transform);

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

                // モデルがウィンドウの上部付近にいるか判定（例：ウィンドウの上端から50ピクセル以内）
                if (modelScreenPos.x >= rect.left && modelScreenPos.x <= rect.right)
                {
                    if (modelScreenPos.y >= rect.top - 50 && modelScreenPos.y <= rect.top + 50)
                    {
                        isNearExplorerTop = true;
                        break;
                    }
                }
            }
#endif

            // _characterAnimationController.Update();
            
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
        /// 初期ロードのモデルの表示後の調節
        /// </summary>
        /// <param name="model"></param>
        private void OnModelLoaded(GameObject model)
        {
            // モデルを包む空のゲームオブジェクトを作成
            GameObject modelContainer = new GameObject("ModelContainer");

            // モデルを子オブジェクトに設定
            model.transform.SetParent(modelContainer.transform, false);
            
            // モデルコンテナをカメラの前方に配置
            Vector3 cameraPosition = _mainCamera.transform.position;
            Vector3 cameraForward = _mainCamera.transform.forward;
            float distanceFromCamera = 2.0f; // 距離を調整
            modelContainer.transform.position = cameraPosition + cameraForward * distanceFromCamera;

            // モデルコンテナをカメラの方向に向ける
            modelContainer.transform.LookAt(cameraPosition, Vector3.up);
            
            var characterApplicationSettings = ApplicationSettings.Instance.Character;

            // モデルのスケールを調整（必要に応じて変更）
            modelContainer.transform.localScale = Vector3.one * characterApplicationSettings.Scale;

            // モデルコンテナの相対位置を設定
            modelContainer.transform.position += new Vector3(characterApplicationSettings.PositionX, characterApplicationSettings.PositionY, characterApplicationSettings.PositionZ);
            
            // モデルコンテナの相対回転を設定
            var currentRotation = modelContainer.transform.rotation.eulerAngles;
            modelContainer.transform.rotation = Quaternion.Euler(currentRotation.x + characterApplicationSettings.RotationX, currentRotation.y + characterApplicationSettings.RotationY, currentRotation.z + characterApplicationSettings.RotationZ);
            
            Log.Info("キャラクター設定: スケール {0}, 位置 {1}, 回転 {2}", characterApplicationSettings.Scale, modelContainer.transform.position, modelContainer.transform.rotation.eulerAngles);

            // モデルコンテナをフィールドに保持
            _model = modelContainer;

            // アニメータの取得と設定
            _modelAnimator = _model.GetComponentInChildren<Animator>();
            
            if(_modelAnimator == null)
            {
                Log.Debug("Animatorが見つからなかったため、新しく追加します。");
                _modelAnimator = model.AddComponent<Animator>();
            }

            // モデルからAvatarを取得して設定
            var avatar = CreateAvatarFromModel(model);
            if (avatar != null)
            {
                _modelAnimator.avatar = avatar;
                Log.Info("モデルからAvatarを生成し、Animatorに設定しました。");
            } else
            {
                Log.Warning("モデルからAvatarを生成できませんでした。アニメーションが正しく再生されない可能性があります。");
            }
            
            // アニメーションコントローラーを設定
            LoadVRM.UpdateAnimationController(_modelAnimator);

            _isInitialized = true;
        }
        
        /// <summary>
        /// モデルからAvatarを生成します。
        /// </summary>
        /// <param name="model">モデルのGameObject</param>
        /// <returns>生成されたAvatar</returns>
        private Avatar CreateAvatarFromModel(GameObject model)
        {
            // モデル内のHumanDescriptionを取得する
            var animator = model.GetComponent<Animator>();
            if (animator != null && animator.avatar != null)
            {
                // 既存のAvatarがある場合はそれを返す
                return animator.avatar;
            }

            // SkinnedMeshRenderer から HumanDescription を取得
            var smr = model.GetComponentInChildren<SkinnedMeshRenderer>();
            if (smr != null && smr.sharedMesh != null)
            {
                // Humanoid アバターを自動生成

                var avatar = AvatarBuilder.BuildGenericAvatar(model, "");
                if (avatar != null)
                {
                    avatar.name = model.name + "_Avatar";
                    return avatar;
                } else
                {
                    Log.Warning("Humanoid アバターの生成に失敗しました。");
                }
            } else
            {
                Log.Warning("SkinnedMeshRenderer が見つからなかったため、Avatar を生成できませんでした。");
            }

            return null;
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


        /// <summary>
        ///    オブジェクトが破棄されるときの処理
        /// </summary>
        private void OnDestroy()
        {
            _characterAnimationController?.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}