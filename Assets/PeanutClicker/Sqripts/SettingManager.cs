using UnityEngine.Audio; // AudioMixerを使うために必要
using UnityEngine;

namespace Assets.PeanutClicker.Sqripts
{
    public class SettingManager : MonoBehaviour
    {
        // シングルトンインスタンス
        public static SettingManager Instance { get; private set; }
        [SerializeField] private AudioMixer mainMixer; // InspectorでAudioMixerアセットを設定
        // 現在の音量（デフォルトは1.0 = Max）
        public float BgmVolume { get; private set; } = 0.4f;
        public float SeVolume { get; private set; } = 0.4f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーン遷移しても破壊されないようにする場合
            }
            else
            {
                Destroy(gameObject); // 二重生成防止
            }
        }
        void Start()
        {
            // Start()はAwake()の後に呼ばれるため、Mixerが初期化された後になる
            SetBgmVolume(BgmVolume);
            SetSeVolume(SeVolume);
        }

        // Update is called once per frame
        void Update()
        {

        }
        /// <summary>
        /// BGM音量を設定する（スライダーから呼ばれる）
        /// </summary>
        public void SetBgmVolume(float volume_0_to_1)
        {
            // スライダーから読み込む0~1の値
            BgmVolume = volume_0_to_1;

            // 0以下ならミュート、それ以外は「Aカーブ」計算
            float volume_dB;

            if (volume_0_to_1 <= 0)
            {
                volume_dB = -80f; // 完全無音
            }
            else
            {
                // Mathf.Pow(値, 2.0f) で「2乗」
                // これにより、スライダーの左側の変化が緩やかになり、自然な操作感になります。
                // ※もっと緩やかにしたい場合は 3.0f などに増やしてください
                volume_dB = Mathf.Log10(Mathf.Pow(volume_0_to_1, 2.0f)) * 20f;
            }

            // -80dBを下回らないように制限（念のため）
            volume_dB = Mathf.Clamp(volume_dB, -80f, 0f);

            // Mixerの "BGM" という名前のパラメータを変更
            mainMixer.SetFloat("BGM", volume_dB);
        }
        /// <summary>
        /// SE音量を設定する（スライダーから呼ばれる）
        /// </summary>
        public void SetSeVolume(float volume_0_to_1)
        {
            SeVolume = volume_0_to_1;

            // 0以下ならミュート、それ以外は「Aカーブ」計算
            float volume_dB;

            if (volume_0_to_1 <= 0)
            {
                volume_dB = -80f; // 完全無音
            }
            else
            {
                // Mathf.Pow(値, 2.0f) で「2乗」
                // これにより、スライダーの左側の変化が緩やかになり、自然な操作感になります。
                // ※もっと緩やかにしたい場合は 3.0f などに増やしてください
                volume_dB = Mathf.Log10(Mathf.Pow(volume_0_to_1, 2.0f)) * 20f;
            }

            // -80dBを下回らないように制限（念のため）
            volume_dB = Mathf.Clamp(volume_dB, -80f, 0f);

            // Mixerの "SE" という名前のパラメータを変更
            mainMixer.SetFloat("SE", volume_dB);
        }
    }
}