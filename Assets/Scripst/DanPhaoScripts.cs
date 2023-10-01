using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanPhaoScripts : MonoBehaviour
{
    private bool isRising; // Biến đánh dấu xem hoa đang trong quá trình nhô lên hay không
    private bool isFalling; // Biến đánh dấu xem hoa đang trong quá trình hạ xuống hay không

    private float riseSpeed = 5.0f;

    private float originalX; // Vị trí Y ban đầu của hoa

    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.position.x; 
        StartCoroutine(AnimateFlower()); // Bắt đầu coroutine để hoa nhô lên và hạ xuống

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AnimateFlower()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);

            isRising = true;
            while (transform.position.x < originalX + 0.8f)
            {
                transform.Translate(Vector3.left * riseSpeed * Time.deltaTime);
                yield return null;
            }
            isRising = false;
        }
    }
}
