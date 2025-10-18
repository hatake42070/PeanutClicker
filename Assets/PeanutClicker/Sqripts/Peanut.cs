using UnityEngine;

public class Peanut : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

     // 外部から力を加えるための公開メソッド
    public void Launch(Vector2 force)
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            // ここに回転を加える処理を追加
            _rigidbody2D.AddTorque(Random.Range(-0.5f, 0.5f), ForceMode2D.Impulse);
            // Debug.Log(_rigidbody2D.angularVelocity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }

    }
}
