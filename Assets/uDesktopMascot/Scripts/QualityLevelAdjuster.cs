using Unity.Logging;
using UnityEngine;

namespace uDesktopMascot
{
    /// <summary>
    ///     品質レベルを調整するクラス
    /// </summary>
    public static class QualityLevelAdjuster
    {
        /// <summary>
        ///     品質レベルを調整
        /// </summary>
        /// <returns></returns>
        public static int AdjustQualityLevel()
        {
            int qualityLevel = 0; // デフォルトの品質レベルを最低に設定

            // デバイス情報の取得とデフォルト値の設定
            int processorFrequency = SystemInfo.processorFrequency > 0 ? SystemInfo.processorFrequency : 2000; // MHz
            int processorCount = SystemInfo.processorCount > 0 ? SystemInfo.processorCount : 2;
            int systemMemorySize = SystemInfo.systemMemorySize > 0 ? SystemInfo.systemMemorySize : 4000; // MB
            int graphicsMemorySize = SystemInfo.graphicsMemorySize > 0 ? SystemInfo.graphicsMemorySize : 512; // MB
            int graphicsShaderLevel = SystemInfo.graphicsShaderLevel > 0 ? SystemInfo.graphicsShaderLevel : 30;

            // CPUの評価
            if (processorFrequency >= 3000 && processorCount >= 4)
            {
                qualityLevel += 1;
            }

            // メモリの評価
            if (systemMemorySize >= 8000)
            {
                qualityLevel += 1;
            }

            // GPUの評価
            if (graphicsMemorySize >= 2048 && graphicsShaderLevel >= 35)
            {
                qualityLevel += 1;
            }

            // 品質レベルの上限を設定
            qualityLevel = Mathf.Clamp(qualityLevel, 0, QualitySettings.names.Length - 1);

            // 品質レベルを設定
            QualitySettings.SetQualityLevel(qualityLevel, true);

            // 必要に応じて品質レベルを返す
            return qualityLevel;
        }
    }
}