using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{

    private Rigidbody2D rb;
    // private Animator anim;
    private Collider2D Coll;
    //check the gound
    public LayerMask Ground;

    public Transform leftpoint, rightpoint;
    private float leftx, rightx;

    //set a boolean to check the frog is facing left side or right 
    private bool Faceleft = true;
    //basic speed.
    public float Speed;
    public float jumpForce;

    // Start is called before the first frame update
    protected override void Start()
    //override the start .
    {
        //base = super
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        //stop the child pos (left/right point)       method1.
        transform.DetachChildren();
        //leftx , right x get pos.x 
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        //which means the game background looks better and clean and run the program more faster!!!
        //you wont see a lot left/right on the hierarchy if there's a lot forgs in the map.
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);

    }

    // Update is called once per frame
    public void Update()
    {
        // Animation event (Add the movement)
        // Movement();
        SwitchAnim();
        base.update();

    }

    //enemy get hurt from player.

    void Movement()
    {
        if (Faceleft)
        {
            //ensure the jumping time
            anim.SetBool("jumping", true);
            //check if the frog's pos.x less than set left point's pos.x 
            if (transform.position.x < leftx)
            {
                //when the frog reach to the left point , turn around
                rb.velocity = new Vector2(Speed, jumpForce);
                transform.localScale = new Vector2(-1, 1);
                Faceleft = false;
            }
            else
            {
                rb.velocity = new Vector2(-Speed, jumpForce);
            }
        }
        //when the frog goes to the rightside. same with left side setting
        else
        {
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(-Speed, jumpForce);
                transform.localScale = new Vector2(1, 1);
                Faceleft = true;
            }
            else
            {
                rb.velocity = new Vector2(Speed, jumpForce);
            }
            anim.SetBool("jumping", true);
        }

    }

    //switch the jumping to falling
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y <= 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);

            }
        }
        if (Coll.IsTouchingLayers(Ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);

        }
    }

}
