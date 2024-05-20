using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float player_speed;
    public float player_jumping_force;
    public Transform ground_check;

    [SerializeField] private LayerMask ground_layer;

    private Rigidbody2D rb;
    private GM gm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GM.Instance;
    }

    private float horizontal;

    public float Horizontal => horizontal;

    // if true, can jump
    private bool jump = true;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        // TODO, i bullet accedono direttamente a player_movement e non da GM
        if (horizontal != 0) gm.Player_horizontal = horizontal;

        //inputs
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private bool facing_right = true;

    void FixedUpdate()
    {
        // moves the player horizontally
        rb.velocity = new Vector2(horizontal * player_speed, rb.velocity.y);

        // flips the player when changing direction
        if (horizontal > 0 && !facing_right)
        {
            Flip();
        }
        else if (horizontal < 0 && facing_right)
        {
            Flip();
        }

    }

    private void OnDrawGizmos()
    {
        if (ground_check == null) return;

        Gizmos.color = Color.red;
        Vector2 boxSize = new Vector2(transform.localScale.x, 0.1f);
        Gizmos.DrawWireCube(ground_check.position, boxSize);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground"))
    //    {
    //        jump = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (LayerMask.LayerToName(collision.gameObject.layer).Equals("Ground"))
    //    {
    //        jump = false;
    //    }
    //}

    // Methods

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(rb.velocity.x, player_jumping_force), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // creates a small box, returns true if touching the ground else false
        return Physics2D.OverlapBox(ground_check.position, new Vector2(transform.localScale.x, .1f), 0f, ground_layer);
    }

    public void Flip()
    {
        Vector3 cur_scale = transform.localScale;
        cur_scale.x *= -1;
        transform.localScale = cur_scale;

        facing_right = !facing_right;
    }

    public Vector3 player_position()
    {
        return transform.position;
    }
}
