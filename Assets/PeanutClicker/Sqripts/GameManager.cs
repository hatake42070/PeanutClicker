using UnityEngine;
using System;
using System.Numerics;

/// <summary>
/// ゲーム全体の管理を行うマネージャークラス
/// </summary>
public class GameManager : MonoBehaviour
{
    // シングルトン化（簡易的）してどこからでもアクセスしやすくする
    public static GameManager Instance { get; private set; }

    public BigInteger Money { get; private set; }

    // お金が変わったことを通知するイベント
    public event Action<BigInteger> OnMoneyChanged;

    void Awake()
    {
        Instance = this;
    }

    public void AddMoney(BigInteger amount)
    {
        Money += amount;
        // 登録されているリスナー（UIなど）に通知を送る
        OnMoneyChanged?.Invoke(Money);
    }
}