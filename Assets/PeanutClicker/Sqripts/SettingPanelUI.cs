using UnityEngine.UI;
using UnityEngine;

namespace Assets.PeanutClicker.Sqripts
{
    public class SettingPanelUI : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider seSlider;
        //[SerializeField] private AudioClip clickSE; // InspectorでSEを設定
        //[SerializeField] private Button saveButton;

        void Start()
        {
            // 起動時に、SettingsManagerが保持している現在の音量値をスライダーに反映
            bgmSlider.value = SettingManager.Instance.BgmVolume;
            seSlider.value = SettingManager.Instance.SeVolume;

            // スライダーが操作されたら、SettingsManagerの値を更新
            bgmSlider.onValueChanged.AddListener(SettingManager.Instance.SetBgmVolume);
            seSlider.onValueChanged.AddListener(SettingManager.Instance.SetSeVolume);

            //saveButton.onClick.AddListener(() =>
            //{
            //    AudioManager.Instance.PlaySE(clickSE);
            //    SaveManager.Instance.SaveGame();
            //});
        }
    }
}