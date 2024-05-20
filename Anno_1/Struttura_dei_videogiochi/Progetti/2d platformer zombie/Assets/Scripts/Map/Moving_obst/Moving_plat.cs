using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_plat : MonoBehaviour
{
    public float range;
    public float speed;
    public bool horizontal;
    public int pivot; // 0 left/down, 1 middle, 2 right/up
    public float start_delay;
    public float time_still;

    private Vector3 starting_position;
    private Vector3 target_posX;
    private Vector3 target_posY;
    private float last_x_position;

    private int move_state; // 0 still, 1 moving left/up, 2 moving right/down
    private int previous_move_state;

    private bool first_time;

    // Start is called before the first frame update
    void Start()
    {
        // saving starting position to be able to go back at the exact point
        starting_position = transform.position;
        last_x_position = starting_position.x;

        if (pivot == 1) range /= 2; // halve range in case of middle pivot

        if (horizontal)
        {
            if (pivot == 1 || pivot == 2)
                target_posX = new Vector3(starting_position.x - range, starting_position.y);
            else
                target_posX = new Vector3(starting_position.x + range, starting_position.y);
        }
        else
        {
            if (pivot == 1 || pivot == 2)
                target_posY = new Vector3(starting_position.x, starting_position.y - range);
            else
                target_posY = new Vector3(starting_position.x, starting_position.y + range);
        }

        move_state = (pivot == 0) ? 2 : 1;
        previous_move_state = move_state;
        first_time = true;
    }

    // Update is called once per frame
    void Update()
    {
        last_x_position = transform.position.x;

        if (first_time && start_delay > 0)
        {
            StartCoroutine(Start_delay());
        }
        else
        {
            if (move_state != 0) Move();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.activeInHierarchy) collision.gameObject.transform.parent = null;
        }
    }


    private void FixedUpdate()
    {
        float cur_x_position = transform.position.x;
        // Adjust player's position according to the platform's velocity
        foreach (Transform child in transform)
        {
            // TODO, any object with a rb should have the same position adjustment
            // Create an interface I_Movable that has a method returning float horizontal
            if (child.CompareTag("Player"))
            {
                Player_Movement pm = child.GetComponent<Player_Movement>();

                if (!horizontal) return;
                if (move_state == 0) return;
                float horiz = pm.Horizontal;
                if (horiz == 0) return;

                // containes the distance between cur pos and last pos
                float movement = (last_x_position > cur_x_position) ? last_x_position - cur_x_position : cur_x_position - last_x_position;

                if (move_state == 1)
                {
                    if (horiz < 0)
                    {
                        // platform <-- player <--
                        movement = movement;
                        //if (last_x_position > 0 && cur_x_position < 0)
                        //{
                        //    movement = -cur_x_position + last_x_position;
                        //}
                        //else
                        //{
                        //    movement = last_x_position + cur_x_position;
                        //}

                    }
                    else
                    {
                        // platform <-- player -->
                        movement = 3 * movement;
                    }
                }
                else if (move_state == 2)
                {
                    if (horiz < 0)
                    {
                        // platform --> player <--
                        movement = -3 * movement;
                    }

                    else
                    {
                        // platform --> player -->
                        movement = -movement;
                    }
                }
                Debug.Log(movement);

                child.transform.position += new Vector3(movement, 0);

            }
            else
            {

            }
        }


    }

    // Methods

    private IEnumerator Stay_still(float time)
    {
        yield return new WaitForSeconds(time);
        move_state = (previous_move_state == 1) ? 2 : 1;
    }

    private IEnumerator Start_delay()
    {
        yield return new WaitForSeconds(start_delay);
        first_time = false;
    }

    // Handles the movement of the platform
    private void Move()
    {
        Vector3 cur_pos = transform.position;

        if (horizontal)
        {
            Vector3 new_pos = Vector3.MoveTowards(cur_pos, target_posX, speed * Time.deltaTime);

            transform.position = new_pos;
            if (transform.position == target_posX)
            {
                Calculate_target();
                previous_move_state = move_state;
                move_state = 0;
                StartCoroutine(Stay_still(time_still));
            }
        }
        else
        {
            Vector3 new_pos = Vector3.MoveTowards(cur_pos, target_posY, speed * Time.deltaTime);

            transform.position = new_pos;
            if (transform.position == target_posY)
            {
                Calculate_target();
                previous_move_state = move_state;
                move_state = 0;
                StartCoroutine(Stay_still(time_still));
            }
        }

    }

    // based on the actual target_pos value, we swap it, so if target was going to right, we tell it to go left and so on
    private void Calculate_target()
    {
        if (horizontal)
        {
            if (pivot == 1)
                target_posX.x = (target_posX.x == starting_position.x - range) ? starting_position.x + range : starting_position.x - range;
            else if (target_posX.x != starting_position.x)
                target_posX.x = starting_position.x;
            else if (pivot == 0)
                target_posX.x = starting_position.x + range;
            else if (pivot == 2)
                target_posX.x = starting_position.x - range;
        }
        else
        {
            if (pivot == 1)
                target_posY.y = (target_posY.y == starting_position.y - range) ? starting_position.y + range : starting_position.y - range;
            else if (target_posY.y != starting_position.y)
                target_posY.y = starting_position.y;
            else if (pivot == 0)
                target_posY.y = starting_position.y + range;
            else if (pivot == 2)
                target_posY.y = starting_position.y - range;
        }
    }

}
