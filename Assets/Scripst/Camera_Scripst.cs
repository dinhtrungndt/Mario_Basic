using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Scripst : MonoBehaviour
{
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;
    [SerializeField]
    private GameObject mario; // camera chạy theo mario
    [SerializeField]
    private float marioYBround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float marioX = mario.transform.position.x;
        float marioY = mario.transform.position.y;

        float cameraX = transform.position.x;
        float cameraY = transform.position.y;

        // dịch chuyển camera theo chiều ngang
        if(marioX <= leftBound)
        {
            cameraX = leftBound;
        }else if(marioX >= rightBound)
        {
            cameraY = rightBound;
        }
        else
        {
            cameraX = marioX;
        }
        //dịch chuyển theo chiều y
        cameraY = marioY > marioYBround ? marioY : 0;

        // set camera vị trí của camera
        transform.position = new Vector3(cameraX, cameraY, -10);
    }
}
