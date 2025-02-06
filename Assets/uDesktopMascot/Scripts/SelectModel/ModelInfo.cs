using System;
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
        /// 初期化
        /// </summary>
        public void Initialize(string modelName, UnityEngine.Events.UnityAction onClickAction)
        {
            // モデル名を設定
            modelNameText.text = modelName;

            // ボタンのクリックイベントを設定
            selectButton.onClick.AddListener(onClickAction);
        }

        private void OnDestroy()
        {
            // イベントリスナーを解除
            selectButton.onClick.RemoveAllListeners();
        }
    }
}