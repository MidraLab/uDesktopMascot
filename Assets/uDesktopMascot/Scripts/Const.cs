using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///     定数クラス
    /// </summary>
    public class Const
    {
        /// <summary>
        ///     座るアニメーションハッシュ
        /// </summary>
        public static readonly int IsSitting = Animator.StringToHash("IsSitting");

        /// <summary>
        ///     キャラクタードラッグ中のアニメーションハッシュ
        /// </summary>
        public static readonly int IsDragging = Animator.StringToHash("IsHanding");

        /// <summary>
        ///     アイドルサブアニメーション番号のアニメーションハッシュ
        /// </summary>
        public static readonly int RandomAnimePattern = Animator.StringToHash("RandomAnimePattern");

        // ドラッグを開始するための閾値
        public const float DragThreshold = 5f;
    }
}