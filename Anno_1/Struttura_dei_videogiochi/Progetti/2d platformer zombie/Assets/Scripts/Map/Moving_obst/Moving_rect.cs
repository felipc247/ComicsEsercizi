using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_rect : MonoBehaviour
{
    public float range;
    public float speed;
    public float start_delay;
    public float time_still;

    // determines the direction of the rect
    [SerializeField] private Direction direction;
    // tells if the rect can change from the original direction
    [SerializeField] private bool direction_changable;

    public enum Direction
    {
        left,
        right,
        up,
        down
    };

    private Vector3 starting_position;
    private Vector3 target_pos;

    private BoxCollider2D box_col;
    private float default_width = .99f;
    private float default_height = .01f;
    private float default_offset_x = 0f;
    // no default offset y, as it needs to calculated

    private int move_state; // 0 still, 1 moving in direction, 2 moving to starting_position
    private int previous_move_state;

    private bool first_time;

    // GET // SET
    public Direction Direction_rect => direction;
    

    // Start is called before the first frame update
    void Start()
    {
        // saving starting position to be able to go back at the exact point
        starting_position = transform.position;
        // setting up target_pos based on direction
        box_col = GetComponent<BoxCollider2D>();

        Vector3 starting_scale = transform.localScale;

        switch (direction)
        {
            case Direction.left:
                if (direction_changable)
                {
                    // if it was a vertical rect swap axises
                    if (transform.localScale.y > transform.localScale.x)
                    {
                        starting_scale.x = transform.localScale.y;
                        starting_scale.y = transform.localScale.x;
                    }
                    // setting size and offset of the collider
                    box_col.size.Set(default_width, default_height);
                    box_col.offset.Set(default_offset_x, starting_scale.y / 2 - default_height);
                }
                transform.localScale = starting_scale;
                target_pos = new Vector3(starting_position.x - range, starting_position.y);
                break;
            case Direction.right:
                if (direction_changable)
                {
                    // if it was a vertical rect swap axises
                    if (transform.localScale.y > transform.localScale.x)
                    {
                        starting_scale.x = transform.localScale.y;
                        starting_scale.y = transform.localScale.x;
                    }
                    // setting size and offset of the collider
                    box_col.size.Set(default_width, default_height);
                    box_col.offset.Set(default_offset_x, starting_scale.y / 2 - default_height);
                }
                transform.localScale = starting_scale;
                target_pos = new Vector3(starting_position.x + range, starting_position.y);
                break;
            case Direction.up:
                if (direction_changable)
                {
                    // if it was a horizontal rect swap axises
                    if (transform.localScale.x > transform.localScale.y)
                    {
                        starting_scale.x = transform.localScale.y;
                        starting_scale.y = transform.localScale.x;
                    }
                    // setting size and offset of the collider
                    box_col.size.Set(default_width, default_height);
                    box_col.offset.Set(default_offset_x, starting_scale.y / 2 - default_height);
                }
                transform.localScale = starting_scale;
                target_pos = new Vector3(starting_position.x, starting_position.y + range);
                break;
            case Direction.down:
                if (direction_changable)
                {
                    // if it was a horizontal rect swap axises
                    if (transform.localScale.x > transform.localScale.y)
                    {
                        starting_scale.x = transform.localScale.y;
                        starting_scale.y = transform.localScale.x;
                    }
                    // setting size and offset of the collider
                    box_col.size.Set(default_width, default_height);
                    box_col.offset.Set(default_offset_x, starting_scale.y / 2 - default_height);
                }
                transform.localScale = starting_scale;
                target_pos = new Vector3(starting_position.x, starting_position.y - range);
                break;
        }
        move_state = 1;
        previous_move_state = 1;
        first_time = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (first_time && start_delay > 0)
        {
            StartCoroutine(Start_delay());
        }
        else
        {
            switch (move_state)
            {
                case 1:
                    Move_up();
                    break;
                case 2:
                    Move_down();
                    break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            if (collision.gameObject.activeInHierarchy) collision.gameObject.transform.parent = null;
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

    private void Move_up()
    {
        Vector3 cur_pos = transform.position;

        Vector3 new_pos = Vector3.MoveTowards(cur_pos, target_pos, speed * Time.deltaTime);

        transform.position = new_pos;
        if (transform.position == target_pos)
        {
            previous_move_state = move_state;
            move_state = 0;
            StartCoroutine(Stay_still(time_still));
        }
    }

    private void Move_down()
    {
        Vector3 cur_pos = transform.position;

        Vector3 new_pos = Vector3.MoveTowards(cur_pos, starting_position, speed * Time.deltaTime);

        transform.position = new_pos;
        if (transform.position == starting_position)
        {
            previous_move_state = move_state;
            move_state = 0;
            StartCoroutine(Stay_still(time_still));
        }
    }


}
