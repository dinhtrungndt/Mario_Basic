using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Unity.VisualScripting.Member;

public class AskScripst : MonoBehaviour
{
    [SerializeField]
    private float speed;

    //biên
    [SerializeField]
    private float bounce;

    // vị trí ban đầu
    private Vector2 originalPosition;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private Sprite sprite01;

    [SerializeField]
    private Sprite sprite02;

    [SerializeField]
    private Sprite sprite03;

    [SerializeField]
    private Sprite sprite04;

    // chặn nhảy lên quá 1 lần
    [SerializeField]
    private Sprite empty;
    private bool isSecret;



    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        isSecret = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // đi lên
    IEnumerator GoUp()
    {
        while (true)
        {
            // (4, 5, 0) ----> (4, 7, 0))
            Vector2 currentPosition = transform.position;
            currentPosition.y += speed * Time.deltaTime;
            // nếu đi quá giới hạn , dừng lại
            if (currentPosition.y >= originalPosition.y + bounce)
            {
                break;
            }
            transform.position = currentPosition;
            yield return null;
        }
        StartCoroutine(GoDown());
    }

    // đi xuống
    IEnumerator GoDown()
    {
        while(true)
        {
            Vector2 currentPosition = transform.position;
            currentPosition.y -= speed * Time.deltaTime;
            // nếu đi quá vị trí ban đầu , dừng lại
            if (currentPosition.y <= originalPosition.y)
            {
                break;
            }
            transform.position = currentPosition;
            yield return null;
        }
        // tắt animator
        GetComponent<Animator>().enabled = false;
        // biến hình thành empty
        GetComponent<SpriteRenderer>().sprite = empty;
        // tạo vật phẩm
        int random = Random.Range(0, 4);
        var list = new List<Sprite> { sprite01, sprite02, sprite03, sprite04};
        item.GetComponent<SpriteRenderer>().sprite = list[random];
        GameObject oneItem = Instantiate(item);
        oneItem.transform.position = originalPosition;

        // di chuyển vật phẩm chạy lên
        StartCoroutine(ItemGoUp(oneItem));
    }

    IEnumerator ItemGoUp(GameObject oneItem)
    {
        while (true)
        {
            Vector2 currentPosition = oneItem.transform.position;
            currentPosition.y += speed * Time.deltaTime;
            // nếu đi quá giới hạn , dừng lại
            if (currentPosition.y >= originalPosition.y + bounce)
            {
                break;
            }
            oneItem.transform.position = currentPosition;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x; // directionX > 0: phải, else: trái
            float directionY = direction.y; // directionY > 1: trên, else: dưới
            if (directionY > 0 && isSecret == true)
            {
                isSecret = false;
                StartCoroutine(GoUp());
            }
        }
    }

}
