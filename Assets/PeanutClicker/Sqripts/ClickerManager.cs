using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using System.Collections;
//using UnityEngine.UIElements;

public class ClickerManager : MonoBehaviour
{
    public string initialMoneyString;
    // 1クリックで生成できるピーナッツの数
    public int basePeanutCount;
    // ピーナッツ1個の価格
    public BigInteger basePeanutPrice;

    private BigInteger money = 0;
    private BigInteger totalClicks = 0;

    public Text moneyText;
    public Text clickCountText;
    // ピーナッツ価格上昇
    public Button PeanutPriceUpgradeButton;
    private BigInteger peanutPriceUpgradeCost;
    public Text PeanutPriceButtonText;
    // オートクリック
    public Button AutoClickUpgradeButton;
    private BigInteger autoClickUpgradeCost;
    public Text AutoClickPriceButtonText;
    public float ClickDelay = 10.0f;
    // オートクリック実行中かどうか(初めてアップグレード時に使用)
    private bool isAutoClickActive = false;
    // 実行中のオートクリックコルーチンを保存しておくための変数
    private Coroutine autoClickCoroutine;
    // アップグレードできる回数を制限
    private int autoClickUpdateCount;

    // 初期化
    void Start()
    {
        basePeanutCount = 1;
        basePeanutPrice = 1;

        basePeanutPrice = BigInteger.Parse(initialMoneyString);

        peanutPriceUpgradeCost = 100;
        PeanutPriceButtonText.text = "100";
        autoClickUpgradeCost = 100;
        AutoClickPriceButtonText.text = "100";
        autoClickUpdateCount = 0;
        UpdateUI();
    }

    // クリック可能なオブジェクトから呼び出されるメソッド
    public void HandleClick()
    {
        // totalClicks++;
        // BigInteger peanutsEarned = (BigInteger)basePeanutCount;
        // BigInteger moneyEarned = peanutsEarned * (BigInteger)basePeanutPrice;
        BigInteger moneyEarned = (BigInteger)basePeanutCount * basePeanutPrice;

        money += moneyEarned;

        // Debug.Log("クリックで獲得した金額: " + moneyEarned);

        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = money.ToString("C0");
        // clickCountText.text = "総クリック数: " + totalClicks;
        // オートクリックアップグレード回数を10回までに制限
        PeanutPriceUpgradeButton.interactable = (money >= peanutPriceUpgradeCost);
        if (autoClickUpdateCount < 10)
            {
                // 通常通り、所持金とコストを比較してボタンの有効/無効を決める
                AutoClickUpgradeButton.interactable = (money >= autoClickUpgradeCost);
            }
            else // 上限に達している場合
            {
                // 強制的にボタンを無効化する
                AutoClickUpgradeButton.interactable = false;
                // ボタンのテキストを「MAX」に変更して、これ以上強化できないことを示す
                AutoClickPriceButtonText.text = "MAX";
            }
    }

    public void PeanutPriceUpgradeButtonClick()
    {
        //念のため、お金が足りているか再度チェック
        if (money >= peanutPriceUpgradeCost)
        {
            // 1. コストを支払う
            money -= peanutPriceUpgradeCost;

            // 2. 次のコストを300に設定する
            peanutPriceUpgradeCost += 100;
            PeanutPriceButtonText.text = peanutPriceUpgradeCost.ToString();
            basePeanutPrice++;

            // 3. UIを更新する
            //    (お金が減り、ボタンが非アクティブになる)
            UpdateUI();

            Debug.Log("アップグレード成功！ 次のコストは " + peanutPriceUpgradeCost);
        }
    }
    


    public void AutoClickUpgradeButtonClick()
    {
        // 1. お金が足りているか最初にチェック
        if (money >= autoClickUpgradeCost)
        {
            // 2. コストを支払う
            money -= autoClickUpgradeCost;

            // 3. 実行中のオートクリックがあれば、一度停止する
            if (autoClickCoroutine != null)
            {
                StopCoroutine(autoClickCoroutine);
            }

            // 4. アップグレード処理（初回かどうかで分岐）
            if (!isAutoClickActive)
            {
                // 初回購入の場合
                isAutoClickActive = true;
                Debug.Log("オートクリック機能を解放！");
            }
            else
            {
                // 2回目以降の購入の場合
                ClickDelay *= 0.9f; // 間隔を短くする
                Debug.Log("オートクリック速度アップ！ 次の間隔: " + ClickDelay + "秒");
            }

            // 5. 更新されたClickDelayの値を使って、コルーチンを【常に】開始（または再開）する
            autoClickCoroutine = StartCoroutine(AutoClick());

            // 6. 次のコストを更新
            autoClickUpgradeCost += 150;
            AutoClickPriceButtonText.text = autoClickUpgradeCost.ToString();
            autoClickUpdateCount++;
            Debug.Log("アップグレードカウント" + autoClickUpdateCount);
            UpdateUI();
        }
    }
    public IEnumerator AutoClick()
    {
        // 無限ループ
        while (true)
        {
            // 現在のClickDelayの値だけ待機する
            yield return new WaitForSeconds(ClickDelay);

            // クリック処理を実行
            HandleClick();
        }
    }
}