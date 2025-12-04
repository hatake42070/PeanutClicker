using UnityEngine;

/// <summary>
/// ピーナッツシェルをたたいたときの処理
/// </summary>
public class PeanutShell : MonoBehaviour
{
    [SerializeField]
    private Peanut peanutPrefab;
    [SerializeField]
    private GameAudioView gameAudioView;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

     /// <summary>
     /// オーバー時のクリック処理
     /// </summary>
    void OnMouseOver()
    {
        // 「左クリック(0)」 または 「右クリック(1)」 が押された瞬間なら実行
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        // Modelにお金を増やすよう命令
        GameModel.Instance.EarnMoney();
        // SE再生
        if (gameAudioView != null)
        {
            gameAudioView.OnClickPeanut();
        }

        // 演出（アニメーションと生成）
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