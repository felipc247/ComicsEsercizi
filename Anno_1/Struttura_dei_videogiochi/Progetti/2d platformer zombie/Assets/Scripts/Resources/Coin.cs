using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public RM rm;

    // Start is called before the first frame update
    void Start()
    {
        rm.GetComponent<RM>();
    }

    // if player touches coin, add 1 coin and destroy the coin
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rm.Add_coins(1);
            Destroy(gameObject);
        }
    }

}
