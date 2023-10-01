using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoMauScripts1 : MonoBehaviour
{
    [SerializeField]

    private TextMeshProUGUI livesText;
    private int livesNumber;

    // Start is called before the first frame update
    void Start()
    {
        livesNumber = 1000; 
        UpdateLivesText(); 
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
        int damage = 5; // Số máu bị trừ khi bắn trúng
        livesNumber -= damage; // Trừ số máu từ biến livesNumber
        UpdateLivesText(); // Cập nhật số mạng hiển thị trên giao diện

        if (livesNumber <= 0)
        {
            // Xử lý khi số mạng hết, ví dụ: kết thúc trò chơi
            // ...
        }
    }

}