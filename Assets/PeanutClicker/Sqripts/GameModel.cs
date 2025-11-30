using UnityEngine;
using System;
using System.Numerics; // BigInteger用

public class GameModel : MonoBehaviour
{
    public static GameModel Instance { get; private set; }

    // ==================================================
    // 1. インスペクター設定用 (SerializeField)
    // ==================================================
    [Header("Initial Settings")]
    [SerializeField] private string initialMoney = "0"; // BigIntegerは文字列で定義
    [SerializeField] private string initialPeanutPrice = "1";

    // intやfloatはそのままSerializeFieldにすればインスペクターで編集可能
    [SerializeField] private int _peanutsPerClick = 1;
    [SerializeField] private float _autoClickInterval = 10.0f;
    [SerializeField] private BigInteger _autoClickUpgradeCost = 100; // ※注意: BigIntegerはこれだけでは表示されません（後述）

    [Header("Debug (Play Mode Only)")]
    [Tooltip("ここに数字を入れてApplyチェックを入れるとお金が書き換わります")]
    [SerializeField] private string debugSetMoneyAmount;
    [SerializeField] private bool applyDebugMoney; // チェックを入れた瞬間に適用

    // ==================================================
    // 2. 公開プロパティ (外部からはこれを見る)
    // ==================================================

    // 変数を返すだけにすることで、インスペクターの値と連動させる
    public BigInteger Money { get; private set; }
    public BigInteger PeanutPrice { get; private set; }

    // そのまま変数を返す（ラムダ式）
    public int PeanutsPerClick => _peanutsPerClick;
    public float AutoClickInterval => _autoClickInterval;

    // 他のパラメータ
    public BigInteger PriceUpgradeCost { get; private set; } = 100;
    public BigInteger AutoClickUpgradeCost { get; private set; } = 100;

    public bool IsAutoClickActive { get; private set; } = false;
    public int AutoClickLevel { get; private set; } = 0;
    public const int MaxAutoClickLevel = 10;

    public event Action OnDataChanged;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        // 文字列からBigIntegerへ変換して初期化
        Money = BigInteger.Parse(initialMoney);
        PeanutPrice = BigInteger.Parse(initialPeanutPrice);

        NotifyChanged();
    }

    // デバッグ用：ゲーム中にインスペクターを監視する
    void Update()
    {
        // デバッグ機能：チェックが入ったら入力された金額を適用する
        if (applyDebugMoney)
        {
            if (BigInteger.TryParse(debugSetMoneyAmount, out BigInteger result))
            {
                Money = result;
                NotifyChanged();
                Debug.Log($"[Debug] Money changed to {Money}");
            }
            else
            {
                Debug.LogWarning("[Debug] Invalid number format");
            }
            applyDebugMoney = false; // チェックを自動で外す
        }
    }

    // --- ロジック ---

    public void EarnMoney()
    {
        // プロパティではなく変数を直接参照しても良いが、統一感のためプロパティ経由でもOK
        BigInteger amount = PeanutPrice * PeanutsPerClick;
        Money += amount;
        NotifyChanged();
    }

    public void TryBuyPriceUpgrade()
    {
        if (Money >= PriceUpgradeCost)
        {
            Money -= PriceUpgradeCost;
            PeanutPrice++;
            PriceUpgradeCost += 100;
            NotifyChanged();
        }
    }

    public void TryBuyAutoClickUpgrade()
    {
        if (AutoClickLevel < MaxAutoClickLevel && Money >= AutoClickUpgradeCost)
        {
            Money -= AutoClickUpgradeCost;

            if (!IsAutoClickActive)
            {
                IsAutoClickActive = true;
            }
            else
            {
                // インスペクターで設定した変数を書き換える
                _autoClickInterval *= 0.9f;
            }

            AutoClickUpgradeCost += 150;
            AutoClickLevel++;
            NotifyChanged();
        }
    }

    private void NotifyChanged()
    {
        OnDataChanged?.Invoke();
    }
}