using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace uDesktopMascot
{
    /// <summary>
    ///    モデル情報
    /// </summary>
    public class ModelInfo : MonoBehaviour
    {
        /// <summary>
        /// モデル名を表示するテキスト
        /// </summary>
        [SerializeField] private TextMeshProUGUI modelNameText;

        /// <summary>
        /// モデル選択ボタン
        /// </summary>
        [SerializeField] private Button selectButton;

        /// <summary>
        /// 背景イメージ
        /// </summary>
        [SerializeField] private Image backgroundFrameImage;

        /// <summary>
        /// サムネイルイメージ
        /// </summary>
        [SerializeField] RawImage thumbnailImage;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize(string modelName, Texture2D thumbnail, UnityEngine.Events.UnityAction onClickAction)
        {
            // モデル名を設定
            modelNameText.text = modelName;

            // サムネイルを設定
            thumbnailImage.texture = thumbnail;

            // ボタンのクリックイベントを設定
            selectButton.onClick.AddListener(onClickAction);
        }

        /// <summary>
        /// 選択状態を設定
        /// </summary>
        /// <param name="isSelected"></param>
        public void SetSelected(bool isSelected)
        {
            // 背景色を変更
            backgroundFrameImage.enabled = isSelected;
        }

        // モデル情報を更新するメソッドを追加
        public void UpdateModelInfo(string modelName, Texture2D thumbnail)
        {
            modelNameText.text = modelName;
            thumbnailImage.texture = thumbnail;
        }

        private void OnDestroy()
        {
            // イベントリスナーを解除
            selectButton.onClick.RemoveAllListeners();
        }
    }
}