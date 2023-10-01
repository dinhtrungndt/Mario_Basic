using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Scripst : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // bien mat ngay lap tuc
            Destroy(gameObject);
        }
    }
}
