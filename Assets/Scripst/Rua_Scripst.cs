using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rua_Scripst : MonoBehaviour
{
    // biên di chuyển trái phải
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;

    // tốc độ di chuyển
    [SerializeField]
    private float speed;

    // hướng mặt
    [SerializeField]
    private bool isOnRight;


    void Start()
    {
        
    }

    
    void Update()
    {
        float positionX = transform.position.x;
        //float positionX = transform.localPosition.x;
        //float localPX = transform.localPosition.x;
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
            Vector3 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("taggggggggggggggg: " + collision.gameObject.tag
            //+ ">>>>>>>: " + collision.gameObject.CompareTag("Bullet"));
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("bang bang bang");
            Destroy(gameObject);
        }
    }
}
