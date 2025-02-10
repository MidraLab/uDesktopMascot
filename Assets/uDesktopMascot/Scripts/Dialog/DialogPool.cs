using UnityEngine.Pool;

namespace uDesktopMascot
{
    /// <summary>
    /// ダイアログプール
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DialogPool<T> : IDialogPool where T : DialogBase
    {
        /// <summary>
        /// ダイアログのプール
        /// </summary>
        private readonly ObjectPool<T> _pool;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pool"></param>
        public DialogPool(ObjectPool<T> pool)
        {
            _pool = pool;
        }

        /// <summary>
        /// ダイアログを取得
        /// </summary>
        /// <returns></returns>
        public DialogBase Get()
        {
            return _pool.Get();
        }

        /// <summary>
        /// ダイアログを解放
        /// </summary>
        /// <param name="dialog"></param>
        public void Release(DialogBase dialog)
        {
            _pool.Release((T)dialog);
        }
    }
}