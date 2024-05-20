using System.Collections;
using UnityEngine;

public class Player_Handle : MonoBehaviour
{
    public Player_stats p_stats;
    public GameObject hotbar;
    public Animator animator;

    [SerializeField] private float dmg_take_delay;

    private bool can_be_hit;

    private Rigidbody2D rb;

    // GET // SET
    public bool Can_be_hit => can_be_hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        p_stats.cur_health = 100;
        can_be_hit = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    // methods

    public void Take_damage(int dmg)
    {
        p_stats.cur_health -= dmg;
        Debug.Log("Cur health:" + p_stats.cur_health);
        if (p_stats.cur_health < 1)
        {
            Die();
        }
        else
        {
            //Take damage
            // ANIMATION
            can_be_hit = false;
            animator.SetBool("Took_damage", !can_be_hit);
            StartCoroutine(Take_damage_delay());
        }
    }

    private IEnumerator Take_damage_delay() {
        yield return new WaitForSeconds(dmg_take_delay);
        can_be_hit = true;
        animator.SetBool("Took_damage", !can_be_hit);

    }

    private void Die()
    {
        //ANIMATION

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Player_Movement>().enabled = false;
        enabled = false;
    }

}
