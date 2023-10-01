using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VienDanScripts : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private float speed;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = new Vector2(speed, 0);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("cuc_gach (4)"))
        {
            speed *= 0.9f;
            rigidbody2D.velocity = new Vector2(speed, speed);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Screen playerScript = collision.gameObject.GetComponent<Player_Screen>();
            if (playerScript != null)
            {
                Destroy(gameObject);
            }
            Destroy(gameObject); // Hủy đối tượng đạn
        }
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;

    }


}
