using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer Sprite;

    private float dirX;

    
    [SerializeField] private int jumpForce = 5;
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private int maxJumps = 1; 

    private enum MovementState { idle, running, jumping, falling, double_jumping, wall_jumping}
    private MovementState state;

    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D boxCol;

    [Header("Audio")]
    [SerializeField] private AudioSource jumpSoundEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
    }

 
    void Update()
    {
        

        dirX = Input.GetAxisRaw("Horizontal");
        UpdateAnimationState();

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded()) {
           
            Jump();
        }

        else if(Input.GetButtonDown("Jump") && extraJumps > 0)
        {
          
            extraJumps--;
            
            DoubleJump();

        }

        if (IsGrounded())
        {
            extraJumps = maxJumps;
        }
    }

    private void Jump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void DoubleJump()
    {
        jumpSoundEffect.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    void UpdateAnimationState()
    {

    if (dirX > 0f)
        {
            state = MovementState.running;
            Sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            Sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f && extraJumps == 0)
        {
            state = MovementState.double_jumping; 
        }
        else if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) 
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }



    private bool IsGrounded(){
        return Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, .1f, jumpableGround);    
    }
}
