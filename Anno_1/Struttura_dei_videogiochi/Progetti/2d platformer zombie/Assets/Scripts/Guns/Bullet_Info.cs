using UnityEngine;

public class Bullet_Info : MonoBehaviour
{
    public float bullet_distance;

    private Rigidbody2D rb;
    private GM gm;
    public float bullet_speed;
    public int dmg;

    private Vector3 starting_fire_position;

    // Start is called before the first frame update
    void Start()
    {
        starting_fire_position = transform.position;
        gm = GM.Instance;
        rb = GetComponent<Rigidbody2D>();
        // flips bullet in the player's direction
        Vector3 cur_scale = gameObject.transform.localScale;
        cur_scale.x *= gm.Player_horizontal;
        gameObject.transform.localScale = cur_scale;
        // bullet movement
        rb.velocity = new Vector2(gm.Player_horizontal * bullet_speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // make the bullet disappear after distance
        float distance = Vector3.Distance(transform.position, starting_fire_position);
        if (distance > bullet_distance)
        {
            Destroy(gameObject);
        };
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            collision.gameObject.GetComponent<Zombie_Handle>().Take_damage(dmg);
            // ANIMATION

        }
        Destroy(gameObject);
    }

    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<Zombie_Handle>().Take_damage(dmg);
    //        // ANIMATION

    //    }
    //    Destroy(gameObject);
    //}

}
