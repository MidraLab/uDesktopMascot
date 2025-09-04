using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///     シーンにまたがってデータを保持するクラスのベースクラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        ///     シングルトンのインスタンス
        /// </summary>
        private static T _instance;

        /// <summary>
        ///     シングルトンのインスタンス
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        Log.Warning(typeof(T) + "SingletonMonoBehaviour is nothing");
                    } else
                    {
                        // ルートGameObjectでない場合は、親をルートに移動してからDontDestroyOnLoadを適用
                        if (_instance.transform.parent != null)
                        {
                            _instance.transform.SetParent(null);
                        }
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        private protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                // ルートGameObjectでない場合は、親をルートに移動してからDontDestroyOnLoadを適用
                if (transform.parent != null)
                {
                    transform.SetParent(null);
                }
                DontDestroyOnLoad(gameObject);
            } else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}