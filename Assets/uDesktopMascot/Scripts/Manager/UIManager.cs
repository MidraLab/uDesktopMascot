using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Unity.Logging;

namespace uDesktopMascot
{
    /// <summary>
    /// UI管理クラス
    /// </summary>
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        /// <summary>
        /// ダイアログスタック
        /// </summary>
        private readonly Stack<DialogBase> _dialogStack = new Stack<DialogBase>();

        /// <summary>
        /// ダイアログのプール（ダイアログの種類ごとに ObjectPool で管理）
        /// </summary>
        private readonly Dictionary<string, object> _dialogPool = new Dictionary<string, object>();

        /// <summary>
        /// ダイアログのプレハブパスを取得する
        /// </summary>
        /// <param name="dialogueName"></param>
        /// <returns></returns>
        private string GetDialogPath(string dialogueName)
        {
            return Path.Combine("UI/Dialog",dialogueName);
        }

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <typeparam name="T">ダイアログの型</typeparam>
        /// <param name="dialogName">Resourcesフォルダからのプレハブのパス</param>
        /// <param name="setup">ダイアログのセットアップアクション</param>
        /// <param name="onClose"></param>
        /// <returns>表示したダイアログのインスタンス</returns>
        public T PushDialog<T>(string dialogName, Action<T> setup = null,Action onClose = null) where T : DialogBase
        {
            ObjectPool<T> pool;

            // ダイアログのプールを取得または作成
            if (!_dialogPool.TryGetValue(GetDialogPath(dialogName), out var poolObj))
            {
                // 新しいプールを作成
                pool = new ObjectPool<T>(
                    createFunc: () =>
                    {
                        // プレハブをロードしてダイアログを生成
                        GameObject dialogPrefab = Resources.Load<GameObject>(GetDialogPath(dialogName));
                        if (dialogPrefab == null)
                        {
                            Log.Error($"ダイアログのプレハブが見つかりませんでした。パス: {GetDialogPath(dialogName)}");
                            return null;
                        }

                        GameObject dialogObject = Instantiate(dialogPrefab, transform);
                        T dialogInstance = dialogObject.GetComponent<T>();
                        if (dialogInstance == null)
                        {
                            Log.Error($"ダイアログにコンポーネント{typeof(T)}が見つかりません。");
                            Destroy(dialogObject);
                            return null;
                        }

                        // ダイアログのプレハブパスを設定
                        dialogInstance.PrefabPath = GetDialogPath(dialogName);

                        // 非アクティブ化
                        dialogInstance.gameObject.SetActive(false);

                        return dialogInstance;
                    },
                    actionOnGet: (dialogInstance) =>
                    {
                        // アクティブ化
                        dialogInstance.gameObject.SetActive(true);
                    },
                    actionOnRelease: (dialogInstance) =>
                    {
                        // リセットして非アクティブ化
                        dialogInstance.Reset();
                        dialogInstance.gameObject.SetActive(false);
                    },
                    actionOnDestroy: (dialogInstance) =>
                    {
                        // 破棄
                        Destroy(dialogInstance.gameObject);
                    },
                    collectionCheck: false,
                    defaultCapacity: 10,
                    maxSize: 100
                );

                _dialogPool.Add(GetDialogPath(dialogName), pool);
            }
            else
            {
                pool = poolObj as ObjectPool<T>;
            }

            // ダイアログを取得
            if(pool == null)
            {
                Log.Error($"ダイアログのプールが見つかりませんでした。パス: {dialogName}");
                return null;
            }
            
            T dialog = pool.Get();
            if (dialog == null)
            {
                Log.Error($"ダイアログの生成に失敗しました。パス: {dialogName}");
                return null;
            }

            // ダイアログの初期化（必要に応じて）
            dialog.Initialize();

            // ダイアログのセットアップ
            setup?.Invoke(dialog);
            
            // 閉じるボタンのクリックイベントを設定
            dialog.OnClose = onClose;

            // スタックに追加
            _dialogStack.Push(dialog);

            return dialog;
        }
        
        /// <summary>
        /// ダイアログの型を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetDialogOfType<T>() where T : DialogBase
        {
            foreach (var dialog in _dialogStack)
            {
                if (dialog is T typedDialog)
                {
                    return typedDialog;
                }
            }
            return null;
        }

        /// <summary>
        /// ダイアログを閉じる
        /// </summary>
        public async UniTask PopDialogAsync()
        {
            if (_dialogStack.Count > 0)
            {
                DialogBase dialog = _dialogStack.Pop();

                // ダイアログの非表示
                await dialog.HideAsync();

                // プレハブパスを取得
                string prefabPath = dialog.PrefabPath;

                // ダイアログをプールに戻す
                if (_dialogPool.TryGetValue(prefabPath, out var poolObj))
                {
                    var pool = poolObj as ObjectPool<DialogBase>;
                    if (pool == null)
                    {
                        Log.Error($"ダイアログのプールが見つかりませんでした。パス: {prefabPath}");
                        return;
                    }
                    
                    pool.Release(dialog);
                }
                else
                {
                    // プールがない場合は破棄
                    Destroy(dialog.gameObject);
                }
            }
            else
            {
                Log.Warning("ダイアログスタックが空です。");
            }
        }
    }
}