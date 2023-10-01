using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireScripst : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool isRight;


    // Start is called before the first frame update
    void Start()
    {
        if (!isRight)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        Destroy(gameObject, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        // xử lý chuyển động
        // xử lý quay hình
        if (isRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
    }

    public void SetDirection(bool isRight)
    {
        this.isRight = isRight;
    }
}
