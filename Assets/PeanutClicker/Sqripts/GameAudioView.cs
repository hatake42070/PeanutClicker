using UnityEngine;

public class GameAudioView : MonoBehaviour
{
    [Header("Clips")]
    public AudioClip clickSound;
    public AudioClip upgradeSuccessSound;
    public AudioClip bgmMain;

    void Start()
    {
        // 1. BGM再生
        AudioManager.Instance.PlayBGM(bgmMain);

        // 2. Modelのイベントを購読したいが...
        // 今のGameModelは「全部まとめて OnDataChanged」なので、
        // 「アップグレードした瞬間」だけを検知するのが少し難しいです。

        // そのため、ボタン側に直接音を設定するか、
        // Modelに「OnUpgradeSuccess」のような専用イベントを追加するのが理想です。
    }

    // このメソッドを、ピーナッツの「OnMouseDown」や
    // アップグレードボタンの「OnClick」に登録する
    public void OnClickPeanut()
    {
        AudioManager.Instance.PlaySE(clickSound);
    }

    public void OnBuyUpgrade()
    {
        AudioManager.Instance.PlaySE(upgradeSuccessSound);
    }
}