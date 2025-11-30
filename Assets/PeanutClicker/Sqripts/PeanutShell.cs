using UnityEngine;

public class PeanutShell : MonoBehaviour
{
    [SerializeField]
    private Peanut peanutPrefab;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // クリックされた時の処理
    private void OnMouseDown()
    {
        HandleInteraction();
    }

    // マウスオーバー時の右クリック処理（元の仕様維持）
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        // 1. Modelにお金を増やすよう命令
        GameModel.Instance.EarnMoney();

        // 2. 演出（アニメーションと生成）
        animator.SetTrigger("Squish");
        SpawnPeanuts();
    }

    private void SpawnPeanuts()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        spawnPosition.z = 0;

        // Modelから一度に出す数を取得
        int count = GameModel.Instance.PeanutsPerClick;

        for (int i = 0; i < count; i++)
        {
            Peanut newPeanut = Instantiate(peanutPrefab, spawnPosition, Quaternion.identity);

            // ランダムな方向に飛ばす
            Vector2 forceDirection = new Vector2(Random.Range(-0.5f, 0.5f), 1.7f).normalized * Random.Range(3f, 7f);
            newPeanut.Launch(forceDirection);
        }
    }
}