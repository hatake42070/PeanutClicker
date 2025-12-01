using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmAudioSource; // BGM再生プレイヤー
    [SerializeField] private AudioSource seAudioSource;  // SE再生プレイヤー
    
    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを保持
        }
        else
        {
            Destroy(gameObject); // 既に存在する場合は破棄
        }
    }
    // BGMを再生するメソッド
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return; // クリップがnullなら何もしない
        if (bgmAudioSource.clip == clip) return; // 同じBGMなら再生しない
        bgmAudioSource.clip = clip; // BGMを設定
        bgmAudioSource.loop = true; 
        bgmAudioSource.Play(); // BGMを再生
    }
    // SEを再生するメソッド
    public void PlaySE(AudioClip clip)
    {
        if (clip != null)
        {
            // PlayOneShotで、音を重ねて再生する
            seAudioSource.PlayOneShot(clip); // 曲をセットして再生まで一度に
        }
    }
}
