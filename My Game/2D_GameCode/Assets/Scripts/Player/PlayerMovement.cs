using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask enemyLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool runningCheck = false;
    private float jumpCooldown;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        //Grabs references for Rigidbody and Animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Flips player deirection when moving Left/Right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animation parameters
        anim.SetBool("Run", horizontalInput != 0);
        runningCheck = true;
        anim.SetBool("Grounded", isGrounded());

        if (jumpCooldown > 0.5f)
        {
            if (Input.GetKey(KeyCode.W) && (isGrounded()))// || onEnemy()))
            Jump();
        }
        else
            jumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        SoundManager.instance.PlaySound(jumpSound); //adds the sound cue to the jumping
        body.velocity = new Vector2(body.velocity.x, jumpPower); //moves the player up by adding velocity to it's y axis
        anim.SetTrigger("Jump"); //triggers the jump animation
        jumpCooldown = 0; //cooldown to limit jumps per second
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

     private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //private bool onEnemy()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, enemyLayer);
    //    return raycastHit.collider != null;
    //}

    public bool canAttack()
    {
        if (horizontalInput == 0)
            return true;
        else if (runningCheck == true)
            return true;

        return false; // return horizontalInput == 0 && isGrounded(); (if you wanna be able to attack only when on the ground and idle)
    }
}

