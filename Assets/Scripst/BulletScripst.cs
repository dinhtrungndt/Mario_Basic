using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripst : MonoBehaviour
{

    [SerializeField]
    private new Rigidbody2D rigidbody2D;

    [SerializeField]
    private float speed;

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
        if (collision.gameObject.CompareTag("Nen_gach"))
        {
            speed *= 0.9f;
            rigidbody2D.velocity = new Vector2(speed, speed);
        }
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    
}
