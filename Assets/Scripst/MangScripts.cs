using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MangScripts : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesText;
    private int livesNumber;

    // Start is called before the first frame update
    void Start()
    {
        livesNumber = 3; // Số mạng ban đầu là 3
        UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void UpdateLivesText()
    {
        livesText.text = livesNumber.ToString(); // Cập nhật số mạng hiển thị trên giao diện
    }
    public void DecreaseLives()
    {
        livesNumber--; // Giảm số mạng
        UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

        if (livesNumber <= 0)
        {
            // Xử lý khi số mạng hết, ví dụ: kết thúc trò chơi
            // ...
        }
    }

}
