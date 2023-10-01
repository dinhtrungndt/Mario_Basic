using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossScripst : MonoBehaviour
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

    // máu của Boss
    [SerializeField]
    private int health;
    private int nowHealth;
    public HealthBarScripst healthBarScripst;

    //viên đạn
    [SerializeField]
    private GameObject bullet;

    // thời gian cho phép bắn sau mỗi lần bắn
    [SerializeField]
    private float fireRate;

    // biến đếm thời gian
    private float timeCounter;

    // Mạng ban đầu của Boss
    [SerializeField]
    private int initialLives;

    // số mạng
    [SerializeField]
    private TextMeshProUGUI livesText;
    private int livesNumber;
    private MangScripts mangScript;
    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 0.5f;
        timeCounter = fireRate * Time.deltaTime;

        health = 100;
        nowHealth = health;

        healthBarScripst.SetMaxHealth(health);
        // Khởi tạo số mạng
        livesNumber = initialLives;
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

        // tăng biến đếm thời gian lên
        timeCounter -= Time.deltaTime;

        // nếu đã đủ thời gian cho phép bắn, bắn đạn và reset lại biến đếm thời gian
        if (timeCounter < 0)
        {
            BossFire();
            timeCounter = fireRate;
        }



    }

    private void UpdateLivesText()
    {
        // Cập nhật số mạng hiển thị trên giao diện
        livesText.text = livesNumber.ToString();
    }

    //Boss bắn đạn
    private void BossFire()
    {
        // tạo viện đạn
        GameObject _bullet = Instantiate(bullet);
        //xác định vị trí viên đạn
        _bullet.transform.position = new Vector3(
            transform.position.x + (isOnRight ? 0.8f : -0.8f),
            transform.position.y,
            transform.position.z
            );
        _bullet.GetComponent<BossFireScripst>().SetDirection(isOnRight);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Trừ máu nếu còn Boss bị trừ máu
            if (nowHealth > 0)
            {
                nowHealth -= 5;
                healthBarScripst.SetHealth(nowHealth, !isOnRight);
            }

            // Kiểm tra xem máu hiện tại có nhỏ hơn hoặc bằng 0 hay không
            if (nowHealth <= 0)
            {
                Destroy(gameObject);
            }

            // Trừ số mạng khi Boss bị bắn trúng
            DecreaseLives();
        }
    }
    private void DecreaseLives()
    {
        int decreaseAmount = 5; // Số mạng bị giảm khi Boss bị bắn trúng
        livesNumber -= decreaseAmount; // Trừ số mạng từ biến livesNumber
        UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

        if (livesNumber <= 0)
        {
            // Xử lý khi số mạng hết, ví dụ: kết thúc trò chơi
            // ...
        }
    }
}