using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumableGround;
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, jumping, falling, death }

    [SerializeField] private AudioSource jumpSoundEffect;

    MovementState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.gravityScale = 3;
        }

        if (Input.GetKeyDown("s") && !IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.gravityScale = 10;
        }

        UpdateAnimationState();

    }
    private void UpdateAnimationState()
    {

      
        state = MovementState.idle;
        

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
    }

    private void callGameOver()
    {
        FindObjectOfType<GameManager>().GameOver();
    }
  



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumableGround);
    }

   
}

