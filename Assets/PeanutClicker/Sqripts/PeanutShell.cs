using UnityEngine;
using UnityEngine.UI;

public class PeanutShell : MonoBehaviour
{

    [SerializeField]
    private ClickerManager clickerManager;

    [SerializeField]
    private Peanut peanutPrefab;
    
    // アニメーター
    private Animator animator;

    void Start()
    {
        // ClickCountText.text = "0";
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        // count++;
        // Debug.Log("左クリック:" + count);
        // UpdateClickCountText();
        clickerManager.HandleClick();
        animator.SetTrigger("Squish");
        SpawnPeanut();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // count++;
            // Debug.Log("右クリック：" + count);
            // UpdateClickCountText();
            clickerManager.HandleClick();
            animator.SetTrigger("Squish");
            SpawnPeanut();
        }
    }

    // private void UpdateClickCountText()
    // {
    //     ClickCountText.text = count.ToString();
    // }

    private void SpawnPeanut()
    {
        // マウスのスクリーン座標を取得
        Vector3 mousePosition = Input.mousePosition;
        // マウス座標をワールド座標に変換
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // 2DゲームなのでZ座標を0に設定
        spawnPosition.z = 0;

        // クリックで生成されるピーナッツの数を取得
        int peanutsToSpawn = clickerManager.basePeanutCount;

        for (int i = 0; i < peanutsToSpawn; i++)
        {
            // ピーナッツを生成
            Peanut newPeanut = Instantiate(peanutPrefab, spawnPosition, Quaternion.identity);
            Vector3 offset = new Vector3(0f, 2.5f, 0f);
            // Peanut newPeanut = Instantiate(peanutPrefab, transform.position + offset, Quaternion.identity);
            
            // 生成されたオブジェクトからPeanutスクリプトを取得
            Peanut peanutComponent = newPeanut.GetComponent<Peanut>();

            // 力を計算
            // Vector2 forceDirection = Quaternion.Euler(0, 0, Random.Range(-35.0f, 35.0f)) * Vector2.up *  5;
            // Vector2 forceDirection = Quaternion.Euler(0, 0, Random.Range(-35.0f, 35.0f)) * Vector2.up *  Random.Range(5.0f, 10.0f);
            // X座標を-1fから1fの間でランダムにし、Y座標を1fに固定
            Vector2 forceDirection = new Vector2(Random.Range(-0.5f, 0.5f), 1.7f).normalized * Random.Range(3f, 7f);

            // PeanutスクリプトのLaunchメソッドを呼び出し、力を加える
            if (peanutComponent != null)
            {
                peanutComponent.Launch(forceDirection);
            }
        }
    }
}