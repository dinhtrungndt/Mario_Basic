using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombasScripst : MonoBehaviour
{
    // biên di chuyển trái phải
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;

    // tốc độ di chuyển
    [SerializeField]
    private float speed;

    // hướng di chuyển
    [SerializeField]
    private bool isOnRight;

    private bool isShot = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float positionX = transform.position.x;
        //float positionX = transform.localPosition.x;

        if (positionX <= leftBound)
        {
            isOnRight = true;
        }
        else if (positionX >= rightBound)
        {
            isOnRight = false;
        }
   
        if (isOnRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("taggggggggggggggg: " + collision.gameObject.tag
            //+ ">>>>>>>: " + collision.gameObject.CompareTag("Bullet"));
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("bang bang bang");
            Destroy(gameObject);
        }
        if (!isShot)
        {
            // Goombas bị bắn
            isShot = true;
            // Thực hiện các hành động liên quan đến việc bị bắn, ví dụ như biến mất
        }
    }
}
