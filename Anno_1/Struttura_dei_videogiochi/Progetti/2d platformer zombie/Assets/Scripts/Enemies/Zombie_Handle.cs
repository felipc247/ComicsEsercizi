using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Zombie_Handle : MonoBehaviour
{
    public int health;
    public float speed_movement;
    public int damage;
    public float damage_delay;
    public float knockback_horizontal;
    public float knockback_vertical;
    public GameObject player;
    public Animator animator;

    public GameObject pointA;
    public GameObject pointB;
    public float distance_from_border;
    public float search_range;


    private bool can_damage;
    private int current_health;
    private bool facing_right;
    private Transform curr_point;
    private bool is_inside_path;

    private bool chasing_player;
    private bool seeking_player;
    private bool reaching_lpp;
    private bool idle;
    private Vector3 last_player_position;

    Rigidbody2D rb;
    Player_Movement pm;
    Player_Handle ph;

    // Start is called before the first frame update
    void Start()
    {
        current_health = health;
        can_damage = true;
        rb = GetComponent<Rigidbody2D>();
        pm = player.GetComponent<Player_Movement>();
        ph = player.GetComponent<Player_Handle>();
        previous_position = transform.position;

        curr_point = pointA.transform;

        chasing_player = false;
        reaching_lpp = false;
        seeking_player = false;
        idle = false;
        is_inside_path = true;
    }

    public float horizontal;

    Vector3 previous_position;

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        // if player visible
        if (chasing_player)
        {
            Move_towards_player();
        }

        // if there's a lpp
        if (reaching_lpp)
        {
            Reach_lpp();
        }

        // searching around for player
        if (seeking_player)
        {
            Seek_player();
        }

        // checking if we're inside the path
        Inside_path();

        // 
        //Update_direction();

        if (!chasing_player && !seeking_player && !reaching_lpp && is_inside_path) Loop_points();

        //if (chasing_player)
        //{
        //    // flips the zombie when changing direction
        //    if (horizontal > 0 && !facing_right)
        //    {
        //        Flip();
        //        Debug.Log("Flip right");
        //    }
        //    else if (horizontal < 0 && facing_right)
        //    {
        //        Flip();
        //        Debug.Log("Flip left");
        //    }
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (can_damage) StartCoroutine(Damage_player());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (can_damage) StartCoroutine(Damage_player());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            idle = false;
            seeking_player = false;
            reaching_lpp = false;
            chasing_player = true;
            //Debug.Log("cur_point" + curr_point.name);
            animator.SetBool("Chasing", chasing_player);
            Animation_flags_setter();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            chasing_player = false;
            // now moving to last player's known position
            reaching_lpp = true;
            last_player_position = pm.player_position();
            Animation_flags_setter();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, distance_from_border);
        Gizmos.DrawWireSphere(pointB.transform.position, distance_from_border);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    // methods

    private void Loop_points()
    {
        if (curr_point == pointA.transform)
        {
            Vector3 cur_position = transform.position;

            Vector3 target_position = curr_point.transform.position;

            Vector3 new_position = Vector3.MoveTowards(cur_position, target_position, speed_movement * Time.deltaTime);

            transform.position = new_position;
            //rb.velocity = new Vector2(-speed_movement, rb.velocity.y);
            if (Vector2.Distance(transform.position, curr_point.position) <= distance_from_border)
            {
                curr_point = pointB.transform;
                Flip();
            }
        }

        if (curr_point == pointB.transform)
        {
            Vector3 cur_position = transform.position;

            Vector3 target_position = curr_point.transform.position;

            Vector3 new_position = Vector3.MoveTowards(cur_position, target_position, speed_movement * Time.deltaTime);

            transform.position = new_position;
            //rb.velocity = new Vector2(speed_movement, rb.velocity.y);
            if (Vector2.Distance(transform.position, curr_point.position) <= distance_from_border)
            {
                curr_point = pointA.transform;
                Flip();
            }
        }
    }

    private void Update_direction()
    {
        // updating direction of zombie
        if (previous_position.x > transform.position.x)
        {
            horizontal = -1;
        }
        else if (previous_position.x < transform.position.x)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }
        previous_position = transform.position;
    }

    private IEnumerator Damage_player()
    {
        can_damage = false;

        if (ph.Can_be_hit) {
            ph.Take_damage(damage);
            if (ph.enabled) Knockback_player(); // if player is not dead, knockback
        }
        
        yield return new WaitForSeconds(damage_delay);

        can_damage = true;
    }

    private void Knockback_player()
    {
        Vector3 direction = transform.position;

        if (direction.x > 0)
        {
            Debug.Log("Knockback right");
            // moving and facing right
            pm.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockback_horizontal, knockback_vertical), ForceMode2D.Impulse);
        }
        else if (direction.x < 0)
        {
            Debug.Log("Knockback left");
            // moving and facing left
            pm.GetComponent<Rigidbody2D>().AddForce(new Vector2(-knockback_horizontal, knockback_vertical), ForceMode2D.Force);
        }
    }

    private void Move_towards_player()
    {
        Vector3 cur_position = transform.position;

        Vector3 target_position = pm.player_position();

        Vector3 new_position = Vector3.MoveTowards(cur_position, target_position, speed_movement * Time.deltaTime);

        transform.position = new_position;
    }

    // reaching last player known position
    private void Reach_lpp()
    {
        Debug.Log("Reaching last player position");
        //int left_right = (facing_right) ? 1 : -1;
        //rb.velocity = new Vector2(left_right * speed_movement, rb.velocity.y);

        Vector3 cur_position = transform.position;

        Vector3 l_p_p = last_player_position;
        l_p_p.y = cur_position.y;
        Vector3 new_position = Vector3.MoveTowards(cur_position, l_p_p, speed_movement * Time.deltaTime);

        transform.position = new_position;

        // seeks around, left and right for player, if not found, go idle, or go back following path if still inside it
        if (transform.position.x == last_player_position.x)
        {            
            reaching_lpp = false;
            seeking_player = true;
            animator.SetBool("Seeking", seeking_player);
            Animation_flags_setter();

            starting_dir = facing_right;
            seeked_left = false;
            seeked_right = false;
            Flip();
        }

        //Debug.Log(transform.position.x);
    }

    // vars for seeking player
    private bool starting_dir;
    private bool seeked_left;
    private bool seeked_right;

    private void Seek_player()
    {
        // looking for player after reaching last position
        // if facing right, we first look back, then forth
        // if not, the opposite

        Vector3 cur_position = transform.position;
        Vector3 new_position;
        if (starting_dir)
        {
            Vector3 search_position = last_player_position;
            search_position.y = cur_position.y;

            if (!seeked_left && cur_position.x > last_player_position.x - search_range)
            {
                // searching left
                search_position.x -= search_range;
                new_position = Vector3.MoveTowards(cur_position, search_position, speed_movement * Time.deltaTime);
                transform.position = new_position;
                // if reached left search position we flip the zombie
                if (transform.position.x <= search_position.x)
                {
                    seeked_left = true;
                    Flip();
                }
            }
            else if (!seeked_right && cur_position.x < last_player_position.x + search_range)
            {
                // searching right  
                search_position.x += search_range;
                new_position = Vector3.MoveTowards(cur_position, search_position, speed_movement * Time.deltaTime);
                transform.position = new_position;
                // if reached left search position we flip the zombie
                if (transform.position.x >= search_position.x)
                {
                    seeked_right = true;
                    Flip();
                }
            }
            else
            {
                Vector3 target_position = last_player_position;
                target_position.y = transform.position.y;
                // going back to last player position
                new_position = Vector3.MoveTowards(transform.position, target_position, speed_movement * Time.deltaTime);

                transform.position = new_position;
                // Going Idle if nothing found, unless inside path == true
                if (!is_inside_path && Vector2.Distance(transform.position, target_position) < 0.01f)
                {
                    Flip();
                    seeking_player = false;
                    // ANIMATION
                    idle = true;
                    animator.SetBool("Idle", idle);
                    Animation_flags_setter();
                }
            }
        }
        else
        {
            Vector3 search_position = last_player_position;
            search_position.y = cur_position.y;

            if (!seeked_right && cur_position.x < last_player_position.x + search_range)
            {
                // searching right  
                search_position.x += search_range;
                new_position = Vector3.MoveTowards(cur_position, search_position, speed_movement * Time.deltaTime);
                transform.position = new_position;
                // if reached left search position we flip the zombie
                if (transform.position.x >= search_position.x)
                {
                    seeked_right = true;
                    Flip();
                }
            }
            else if (!seeked_left && cur_position.x > last_player_position.x - search_range)
            {
                // searching left
                search_position.x -= search_range;
                new_position = Vector3.MoveTowards(cur_position, search_position, speed_movement * Time.deltaTime);
                transform.position = new_position;
                // if reached left search position we flip the zombie
                if (transform.position.x <= search_position.x)
                {
                    seeked_left = true;
                    Flip();
                }
            }
            else
            {
                Vector3 target_position = last_player_position;
                target_position.y = transform.position.y;
                // going back to last player position
                new_position = Vector3.MoveTowards(transform.position, target_position, speed_movement * Time.deltaTime);

                transform.position = new_position;
                // Going Idle if nothing found, unless inside path == true
                if (!is_inside_path && Vector2.Distance(transform.position, target_position) < 0.0001f)
                {
                    Flip();
                    seeking_player = false;
                    // ANIMATION
                    idle = true;
                    animator.SetBool("Idle", idle);
                    Animation_flags_setter();
                }
            }
        }
    }

    // sets the flag to the right state

    private void Animation_flags_setter()
    {
        if (is_inside_path && !chasing_player && !seeking_player && !reaching_lpp)
        {
            animator.SetBool("Inside_path", true);
            animator.SetBool("Chasing", false);
            animator.SetBool("Seeking", false);
            animator.SetBool("Idle", false);

            idle = false;
        }

        if (idle)
        {
            animator.SetBool("Chasing", false);
            animator.SetBool("Seeking", false);
            animator.SetBool("Inside_path", false);
        }

        if (chasing_player)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Seeking", false);
            animator.SetBool("Inside_path", false);
        }

        if (seeking_player)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Chasing", false);
            animator.SetBool("Inside_path", false);
        }

        if (reaching_lpp) {
            animator.SetBool("Idle", false);
            animator.SetBool("Chasing", false);
            animator.SetBool("Inside_path", false);
            animator.SetBool("Seeking", false);
        }

    }

    private void Inside_path()
    {
        // checks if we inside the path
        if (transform.position.x > (pointA.transform.position.x) && transform.position.x < (pointB.transform.position.x))
        {
            if (Math.Abs(transform.position.y - pointA.transform.position.y) < 0.3f)
            {
                is_inside_path = true;
                
                Animation_flags_setter();
            }
            else
            {
                is_inside_path = false;
            }
        }
        else
        {
            is_inside_path = false;
        }
    }

    public void Take_damage(int dmg)
    {
        current_health -= dmg;
        Debug.Log("Cur health: " + current_health);
        if (current_health < 1)
        {
            Die();
        }
        else
        {
            // Take damage
            // ANIMATION
        }
    }

    public void Die()
    {
        // ANIMATION

        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        Destroy(gameObject);
    }

    public void Flip()
    {
        Vector3 cur_scale = transform.localScale;
        cur_scale.x *= -1;
        transform.localScale = cur_scale;

        facing_right = !facing_right;
    }


}
