using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Player_Screen : MonoBehaviour
{
    // tốc độ di chuyển của nv
    private float movingSpeed;
    
    //hướng mặt ban đầu của Mario
    private bool isRight;

    //animation
    private Animator animator;
    private float speed;
    private bool isOnFloor;

    //nhạc
    [SerializeField]
    private AudioClip Clip;
    private AudioSource source;

    [SerializeField]
    private AudioClip coinClip;

    // coin text:
    [SerializeField]
    private TextMeshProUGUI coinText;
    private int coinNumber;

    //viên đạn
    [SerializeField]
    private GameObject bullet;

    //time text
    [SerializeField]
    private TextMeshProUGUI timeText;
    private int timeNumber;

    //Panel 
    [SerializeField]
    private GameObject panel;

    // hồi sinh
    [SerializeField]
    private Vector3 HoiSinh;

    // Vị trí ban đầu của tiền và nấm rùa
    private Vector3 originalCoinPosition;
    private Vector3 originalMushroomPosition;

    // số mạng
    [SerializeField]
    private TextMeshProUGUI livesText;
    private int livesNumber;
    private MangScripts mangScript;
    public GameObject gameOverScreen;

    // animation
    private float VanToc;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private bool isZombie = false;
    public Sprite zombieSprite; // Sprite zombie

    // FPS
    // Start is called before the first frame update
    void Start()
    {
        movingSpeed = 5.0f;
        isRight = true;
        animator = GetComponent<Animator>();
        speed = 0.0f;
        isOnFloor = true;

        source = GetComponent<AudioSource>();
        coinNumber = 0;
        coinText.text = coinNumber + "";

        // chạy đồng hồ 
        timeNumber = 0;
        StartCoroutine(UpdateTime());

        // hồi sinh
        HoiSinh = transform.position;

        // số mạng
        livesNumber = 3; // Số mạng ban đầu là 3
        UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện
        mangScript = FindObjectOfType<MangScripts>();

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    
     
    //50FPS
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", speed);
        animator.SetBool("IsOnFloor", isOnFloor);
        //Debug.Log(">>>>>>>" + Time.deltaTime);
        // thực hiện chuyển động
        MovingControl();
        //bắn đạn
        MarioFire();

    }

    private void UpdateLivesText()
    {
        // Cập nhật số mạng hiển thị trên giao diện
        livesText.text = livesNumber.ToString();
    }


    //Mario bắn đạn
    void MarioFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // tạo viện đạn
            GameObject _bullet = Instantiate(bullet);
            //xác định vị trí viên đạn
            _bullet.transform.position = new Vector3(
                transform.position.x + (isRight ? 0.5f : -0.5f),
                transform.position.y,
                transform.position.z
                );
            _bullet.GetComponent<BulletScripst>().SetSpeed(
                isRight ? 5f : -5f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //SceneManager.LoadScene(1);
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //điều khiển chuyển động
    void MovingControl()
    {
        //nếu --> (1, 0 ,0)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * movingSpeed * Time.deltaTime);
            if(isRight == false)
            {
                isRight = true;
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            speed = 1.0f;
        }

        //nếu <-- (1, 0, 0)
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * movingSpeed * Time.deltaTime);
            if(isRight == true)
            {
                isRight = false;
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            speed = 1.0f;
        }

        //nếu ^
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * movingSpeed;
            isOnFloor = false;
            // phát nhạc
            source.PlayOneShot(Clip);
        }
        else
        {
            speed = 0;
        }

    }

    // bắt sự kiện và chạm của 2 box collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(">>>>>>>>>OnCollisionEnter2D" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Nen_gach"))
        {
            isOnFloor = true;
        }   
        //mario chạm và nấm 2 mặt trái phải
        else if (collision.gameObject.CompareTag("Goombas"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x; // directionX > 0: phải, else trái
            float directionY = direction.y; // directionY > 1: trên, else dưới
                                            //Debug.Log(">>>>>>>>>X: " + direction + ">>>>>>>X: " + direction);
            if (directionX != 0 && directionY == 0)
            {
                livesNumber--;
                UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

                if (livesNumber < 0)
                {
                    EndGame();
                }
                else
                {
                    // Hồi sinh
                    transform.position = HoiSinh; // Di chuyển mario đến vị trí hồi sinh ban đầu
                    VanToc = 0.0f; // Đặt vận tốc của mario về 0
                    isOnFloor = true; // Đặt isOnFloor về true
                                      // Biến Mario thành zombie
                    spriteRenderer.sprite = zombieSprite;
                    if (!isZombie)
                    {
                        // Biến Mario thành zombie
                        isZombie = true;
                        animator.SetBool("IsZombie", true);
                        // Thực hiện các hành động liên quan đến việc biến thành zombie
                    }
                }

            }
            else if (directionY > 0 && directionX == 0)
            {
                // trên nấm ----> nấm lên trời
                Destroy(collision.gameObject);
            }
        }

        else if (collision.gameObject.CompareTag("Eremy"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x; // directionX > 0: phải, else trái
            float directionY = direction.y; // directionY > 1: trên, else dưới
            //Debug.Log(">>>>>>>>>X: " + direction + ">>>>>>>X: " + direction);
            if (directionX != 0 && directionY == 0)
            {
                livesNumber--;
                UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

                if (livesNumber < 0)
                {
                    EndGame();
                }
                else
                {
                    // Hồi sinh
                    transform.position = HoiSinh; // Di chuyển mario đến vị trí hồi sinh ban đầu
                    VanToc = 0.0f; // Đặt vận tốc của mario về 0
                    isOnFloor = true; // Đặt isOnFloor về true
                }
            }
            else if (directionY > 0 && directionX == 0)
            {
                // trên nấm --> rùa die
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Vector3 direction = collision.GetContact(0).normal;
            float directionX = direction.x; // directionX > 0: phải, else trái
            float directionY = direction.y; // directionY > 1: trên, else dưới
            Debug.Log(">>>>>>>>>X: " + direction + ">>>>>>>X: " + direction);
            if (directionX != 0 && directionY == 0)
            {
                // phải trái rùa --> mario die
                Destroy(gameObject); // xóa mario
                Time.timeScale = 0; // dừng game
            }

        }
        else if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            coinNumber += 1;
            coinText.text = coinNumber + "";
        }
        else if (collision.gameObject.CompareTag("Flower"))
        {
            livesNumber--;
            UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

            if (livesNumber < 0)
            {
                EndGame();
            }
            else
            {
                // Hồi sinh
                transform.position = HoiSinh; // Di chuyển mario đến vị trí hồi sinh ban đầu
                VanToc = 0.0f; // Đặt vận tốc của mario về 0
                isOnFloor = true; // Đặt isOnFloor về true
            }

        }
        else if (collision.gameObject.CompareTag("Bullet_Boss"))
        {
                livesNumber--;
                UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

                if (livesNumber < 0)
                {
                    EndGame();
                }
                else
                {
                    // Hồi sinh
                    transform.position = HoiSinh; // Di chuyển mario đến vị trí hồi sinh ban đầu
                    VanToc = 0.0f; // Đặt vận tốc của mario về 0
                    isOnFloor = true; // Đặt isOnFloor về true
            }
        }
        else if (collision.gameObject.CompareTag("danphao"))
    {
        Vector3 direction = collision.GetContact(0).normal;
        float directionX = direction.x; // directionX > 0: phải, else trái
        float directionY = direction.y; // directionY > 1: trên, else dưới
                                        
        if (directionX != 0 && directionY == 0)
        {
            livesNumber--;
            UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

            if (livesNumber < 0)
            {
                EndGame();
            }
            else
            {
                // Hồi sinh
                transform.position = HoiSinh; // Di chuyển Mario đến vị trí hồi sinh ban đầu
                VanToc = 0.0f; // Đặt vận tốc của Mario về 0
                isOnFloor = true; // Đặt isOnFloor về true
            }
        }
    }

        else if (collision.gameObject.CompareTag("ChuyenMan"))
        {
            // Chuyển đến Scene2
            SceneManager.LoadScene("Scene2");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            // phát nhạc
            source.volume = 1.0f;
            source.PlayOneShot(coinClip);
            // tăng điểm
            coinNumber++;
            coinText.text = coinNumber + "";

        }    
    }
    
    // [0s, 1s, 2s, 3s, ...]
    IEnumerator UpdateTime()
    {
        while (true)
        {
            timeNumber++;
            timeText.text = timeNumber + "s";
            yield return new WaitForSeconds(1);
        }
    }

    public void QuitGame()
    {
        // chỉ chạy khi build app
        Application.Quit();
    }

    public void ResumGame()
    {
        Debug.Log("RESUNE  ............");
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    private void EndGame()
    {
        // Dừng mọi hoạt động trên màn hình chính
        Time.timeScale = 0f;

        // Hiển thị màn hình kết thúc trò chơi
        // Ví dụ: hiển thị thông báo, điểm số cuối cùng, nút chơi lại, v.v.
        gameOverScreen.SetActive(true);
    }
}


    

