using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScripst : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float bounce;

    // vi tri ban dau cua flower
    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(GoUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // bông chạy lên
    IEnumerator GoUp()
    {
        while (true)
        {
            // (4, 5, 0) ----> (4, 7, 0))
            Vector2 currentPosition = transform.localPosition;
            currentPosition.y += speed * Time.deltaTime;
            // nếu đi quá giới hạn , dừng lại
            if(currentPosition.y >= originalPosition.y + bounce)
            {
                break;
            }
            transform.localPosition = currentPosition;
            yield return null;
        }
        StartCoroutine(GoDown());
    }

    // bông chạy xuống
    IEnumerator GoDown()
    {
        // biến dừng chuyển động
        bool isStop = false;
        while (!isStop)
        {
            yield return new WaitForSeconds(2);
            isStop = true;

        }
        while (true)
        {
            // (4, 7, 0) ----> (4, 5, 0))
            Vector2 currentPosition = transform.localPosition;
            currentPosition.y -= speed * Time.deltaTime;
            // nếu đi quá vị trí ban đầu , dừng lại
            if (currentPosition.y <= originalPosition.y )
            {
                break;
            }
            transform.localPosition = currentPosition;
            yield return null;
        }
        StartCoroutine(GoUp());
    }

}
