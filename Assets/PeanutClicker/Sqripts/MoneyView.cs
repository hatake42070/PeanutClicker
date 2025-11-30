using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text moneyText;

    void Start()
    {
        // イベントを購読する
        GameManager.Instance.OnMoneyChanged += UpdateDisplay;
        GameManager.Instance.OnMoneyChanged += UpdateDisplay;
    }

    void OnDestroy()
    {
        // 購読解除（メモリリーク防止）
        if (GameManager.Instance != null)
            GameManager.Instance.OnMoneyChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(BigInteger currentMoney)
    {
        moneyText.text = currentMoney.ToString("C0");
    }
}