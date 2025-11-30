using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [Header("UI References")]
    public Text moneyText;

    [Header("Price Upgrade UI")]
    public Button priceUpgradeButton;
    public Text priceUpgradeCostText;

    [Header("Auto Click UI")]
    public Button autoClickButton;
    public Text autoClickCostText;

    void Start()
    {
        // Modelの存在チェック
        if (GameModel.Instance != null)
        {
            // 1. データ変更の監視登録
            GameModel.Instance.OnDataChanged += UpdateUI;

            // -------------------------------------------------------
            // 2. ボタンのクリックイベントをここで自動登録 (★ここが追加点)
            // -------------------------------------------------------

            // 単価アップグレードボタンに機能を追加
            // "() => 関数()" という書き方（ラムダ式）で書くのが一般的です
            priceUpgradeButton.onClick.AddListener(() => GameModel.Instance.TryBuyPriceUpgrade());

            // オートクリックボタンに機能を追加
            autoClickButton.onClick.AddListener(() => GameModel.Instance.TryBuyAutoClickUpgrade());

            // -------------------------------------------------------

            // 3. 初回更新
            UpdateUI();
        }
    }

    void OnDestroy()
    {
        if (GameModel.Instance != null)
        {
            GameModel.Instance.OnDataChanged -= UpdateUI;
        }

        // 念のためボタンの登録も解除しておく（必須ではありませんが丁寧な実装です）
        if (priceUpgradeButton != null) priceUpgradeButton.onClick.RemoveAllListeners();
        if (autoClickButton != null) autoClickButton.onClick.RemoveAllListeners();
    }

    private void UpdateUI()
    {
        var model = GameModel.Instance;

        // お金表示
        moneyText.text = model.Money.ToString("C0");

        // 単価アップグレードボタン
        priceUpgradeCostText.text = model.PriceUpgradeCost.ToString();
        priceUpgradeButton.interactable = (model.Money >= model.PriceUpgradeCost);

        // オートクリックボタン
        if (model.AutoClickLevel >= 10)
        {
            autoClickCostText.text = "MAX";
            autoClickButton.interactable = false;
        }
        else
        {
            autoClickCostText.text = model.AutoClickUpgradeCost.ToString();
            autoClickButton.interactable = (model.Money >= model.AutoClickUpgradeCost);
        }
    }
}