using UnityEngine;

public class Peanut : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public void Launch(Vector2 force)
    {
        if (_rigidbody2D != null)
        {
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            _rigidbody2D.AddTorque(Random.Range(-0.5f, 0.5f), ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        // 画面外に落ちたら消える
        if (transform.position.y < -10) // 余裕を持って-10くらいに
        {
            Destroy(gameObject);
        }
    }
}