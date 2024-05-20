using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Triangle_obstacle : MonoBehaviour
{
    public int dmg;
    public float dmg_delay;

    private bool can_damage;

    private void Start()
    {
        can_damage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Handle p = collision.gameObject.GetComponent<Player_Handle>();
            if (p.Can_be_hit) p.Take_damage(dmg);
            can_damage = false;
            StartCoroutine(Damage_delay());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (can_damage)
            {
                Player_Handle p = collision.gameObject.GetComponent<Player_Handle>();
                if (p.Can_be_hit) p.Take_damage(dmg);
                can_damage = false;
                StartCoroutine(Damage_delay());
            }


        }

    }

    // Methods

    private IEnumerator Damage_delay()
    {
        yield return new WaitForSeconds(dmg_delay);
        can_damage = true;
    }


}
