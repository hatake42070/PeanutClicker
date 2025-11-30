using UnityEngine;
using System.Collections;

public class AutoClickController : MonoBehaviour
{
    private Coroutine autoClickCoroutine;
    private float currentInterval = -1f;

    void Start()
    {
        // Modelのデータ変更を監視
        GameModel.Instance.OnDataChanged += CheckAutoClickStatus;
    }

    void CheckAutoClickStatus()
    {
        var model = GameModel.Instance;

        // オートクリックが有効で、まだコルーチンが動いていない、または速度が変わった場合
        if (model.IsAutoClickActive)
        {
            // 間隔が変わっていたら再起動
            if (currentInterval != model.AutoClickInterval)
            {
                currentInterval = model.AutoClickInterval;
                if (autoClickCoroutine != null) StopCoroutine(autoClickCoroutine);
                autoClickCoroutine = StartCoroutine(AutoClickLoop());
            }
        }
    }

    IEnumerator AutoClickLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentInterval);
            // Modelにお金を増やすよう命令
            GameModel.Instance.EarnMoney();

            // ※ここで必要ならピーナッツを生成する処理を呼ぶことも可能
        }
    }
}