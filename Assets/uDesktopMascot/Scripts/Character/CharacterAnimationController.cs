using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace uDesktopMascot
{
    /// <summary>
    /// キャラクターのアニメーションを制御し、ブレンドとループ再生をサポートするクラス
    /// </summary>
    public class CharacterAnimationController : IDisposable
    {
        /// <summary>
        /// キャラクターの PlayableGraph
        /// </summary>
        private PlayableGraph _characterPlayableGraph;
        
        /// <summary>
        /// アニメーションクリップを再生するミキサー
        /// </summary>
        private readonly AnimationMixerPlayable _mixer;

        private AnimationClipPlayable _currentClipPlayable;
        private AnimationClipPlayable _nextClipPlayable;

        private bool _isBlending;
        private float _blendDuration;
        private float _blendStartTime;

        /// <summary>
        /// 現在再生中のアニメーションクリップ
        /// </summary>
        public AnimationClip CurrentAnimationClip { get; private set; }

        /// <summary>
        /// プレイアブル出力
        /// </summary>
        private AnimationPlayableOutput _playableOutput;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="animator"></param>
        public CharacterAnimationController(Animator animator)
        {
            _characterPlayableGraph = PlayableGraph.Create("CharacterGraph");

            // 2つの入力を持つミキサーを作成
            _mixer = AnimationMixerPlayable.Create(_characterPlayableGraph, 2);

            // プレイアブル出力を作成
            _playableOutput = AnimationPlayableOutput.Create(_characterPlayableGraph, "Animation", animator);
            // playableOutput.SetSourcePlayable(_mixer);

            // グラフを再生
            // _characterPlayableGraph.Play();
        }

        /// <summary>
        /// 初期のアニメーションクリップを設定して再生します。
        /// </summary>
        /// <param name="initialClip">初期アニメーションクリップ</param>
        public void SetInitialAnimation(AnimationClip initialClip)
        {
            if (initialClip == null)
            {
                return;
            }

            // 初期クリップのPlayableを作成
            _currentClipPlayable = AnimationClipPlayable.Create(_characterPlayableGraph, initialClip);
            
            _playableOutput.SetSourcePlayable(_currentClipPlayable);
            _characterPlayableGraph.Play();

            // ミキサーに接続
            // _mixer.ConnectInput(0, _currentClipPlayable, 0);
            // _mixer.SetInputWeight(0, 1f);

            CurrentAnimationClip = initialClip;
        }

        /// <summary>
        /// 指定したアニメーションに、指定した時間でブレンドして再生します。
        /// </summary>
        /// <param name="nextAnimationClip">次のアニメーションクリップ</param>
        /// <param name="blendDuration">ブレンド時間（秒単位）</param>
        public void PlayAnimation(AnimationClip nextAnimationClip, float blendDuration)
        {
            if (nextAnimationClip == null)
            {
                // 次のアニメーションが指定されていない場合は何もしない
                return;
            }

            // 次のクリップのPlayableを作成
            _nextClipPlayable = AnimationClipPlayable.Create(_characterPlayableGraph, nextAnimationClip);

            // 次のクリップをミキサーに接続
            _characterPlayableGraph.Connect(_nextClipPlayable, 0, _mixer, 1);
            _mixer.SetInputWeight(1, 0f); // 次のクリップの初期ウェイトを0に設定

            // ブレンドを開始
            _blendDuration = blendDuration;
            _blendStartTime = (float)_characterPlayableGraph.GetRootPlayable(0).GetTime();
            _isBlending = true;

            // 現在のアニメーションクリップ参照を更新
            CurrentAnimationClip = nextAnimationClip;
        }

        /// <summary>
        /// アニメーションのブレンドを時間経過に応じて更新します。
        /// </summary>
        public void Update()
        {
            if (_isBlending)
            {
                // ブレンド開始からの経過時間を計算
                float currentTime = (float)_characterPlayableGraph.GetRootPlayable(0).GetTime();
                float elapsed = currentTime - _blendStartTime;

                // ブレンドウェイトを計算
                float weight = Mathf.Clamp01(elapsed / _blendDuration);

                // ミキサーのウェイトを更新
                _mixer.SetInputWeight(0, 1f - weight);
                _mixer.SetInputWeight(1, weight);

                if (weight >= 1f)
                {
                    // ブレンド完了
                    _isBlending = false;

                    // 前のクリップを切断して破棄
                    if (_currentClipPlayable.IsValid())
                    {
                        _characterPlayableGraph.Disconnect(_mixer, 0);
                        _currentClipPlayable.Destroy();
                    }

                    // 次のクリップを現在のクリップとして更新
                    _currentClipPlayable = _nextClipPlayable;
                    _nextClipPlayable = default(AnimationClipPlayable);

                    // ミキサーの入力を更新
                    _mixer.ConnectInput(0, _currentClipPlayable, 0);
                    _mixer.SetInputWeight(0, 1f);

                    // 入力1を切断
                    _mixer.DisconnectInput(1);
                }
            }
        }

        public void Dispose()
        {
            _characterPlayableGraph.Destroy();
        }
    }
}