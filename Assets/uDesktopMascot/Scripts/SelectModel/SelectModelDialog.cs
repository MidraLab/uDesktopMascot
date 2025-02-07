using System.IO;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.Logging;

namespace uDesktopMascot
{
    /// <summary>
    /// モデルの追加と選択ダイアログ
    /// </summary>
    public class SelectModelDialog : DialogBase
    {
        /// <summary>
        /// ModelInfoのPrefab
        /// </summary>
        [SerializeField] private ModelInfo modelInfoPrefab;

        /// <summary>
        /// ScrollViewのContent
        /// </summary>
        [SerializeField] private Transform contentTransform;

        /// <summary>
        /// 現在ロード中または表示中のモデル
        /// </summary>
        private ModelInfo _currentModel;

        private CancellationTokenSource _cancellationTokenSource;

        private protected override void Awake()
        {
            base.Awake();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void Start()
        {
            await AddDefaultModelList();
            // モデルリストをロード
            LoadModelListAsync().Forget();
        }

        /// <summary>
        /// モデルリストを非同期でロードし、表示する
        /// </summary>
        private async UniTaskVoid LoadModelListAsync()
        {
            // StreamingAssetsフォルダ内のVRMファイルを取得
            string streamingAssetsPath = Application.streamingAssetsPath;
            string[] vrmFiles = Directory.GetFiles(streamingAssetsPath, "*.vrm", SearchOption.AllDirectories);

            foreach (string vrmFile in vrmFiles)
            {
                // ファイルパスをキャプチャ
                string filePath = vrmFile;

                // 非同期でメタデータを読み込むタスクを開始
                var loadMetaTask = LoadVRM.LoadVrmMetaAsync(filePath);

                // メインスレッドでModelInfoアイテムを生成
                await UniTask.SwitchToMainThread();

                var item = Instantiate(modelInfoPrefab, contentTransform);

                item.Initialize("モデル情報を取得中...", null, () => OnModelSelected(item, vrmFile).Forget());

                // 他の処理を続行し、メタデータの読み込みを待つ
                var (modelName, thumbnail) = await loadMetaTask;

                // メインスレッドでUIを更新
                await UniTask.SwitchToMainThread();

                // モデル情報を更新
                item.UpdateModelInfo(modelName, thumbnail);

                // 各ファイルの処理間で待機して、他の処理を行えるようにする
                await UniTask.Yield();
            }
        }

        /// <summary>
        /// デフォルトのモデルリストを追加
        /// </summary>
        private async UniTask AddDefaultModelList()
        {
            // デフォルトのモデルリストを追加
            await UniTask.SwitchToMainThread();
            var item = Instantiate(modelInfoPrefab, contentTransform);
            item.Initialize(CharacterManager.Instance.CurrentVrmInfo.ModelName,
                CharacterManager.Instance.CurrentVrmInfo.ThumbnailTexture,
                () => OnModelSelected(item).Forget());
            _currentModel = item;
            _currentModel.SetSelected(true);
        }

        /// <summary>
        /// モデルが選択されたときの処理
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <param name="path">選択されたモデルのパス</param>
        private async UniTaskVoid OnModelSelected(ModelInfo modelInfo, string path = null)
        {
            modelInfo.SetSelected(true);
            _currentModel?.SetSelected(false);
            _currentModel = modelInfo;
            // 既存のモデルがある場合は削除
            if (_currentModel != null)
            {
                Destroy(_currentModel);
            }

            LoadedVRMInfo model;
            // 指定されたモデルをロード
            if (path == null)
            {
                var defaultModel = await LoadVRM.LoadDefaultModel();
                
                model = new LoadedVRMInfo(defaultModel.gameObject, defaultModel.Vrm.Meta.Name, defaultModel.Vrm.Meta.Thumbnail);
            } else
            {
                model = await LoadVRM.LoadModelAsync(path, _cancellationTokenSource.Token);
            }

            if (model != null)
            {
                // CharacterManagerにモデルを渡す
                CharacterManager.Instance.OnModelLoaded(model.Model, true);
            } else
            {
                Log.Error($"Failed to load Model: {path}");
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}