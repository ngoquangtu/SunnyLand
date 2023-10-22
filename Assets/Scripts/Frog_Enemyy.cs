using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_Enemyy : Enemy
{
    [SerializeField] private float jumpLength=10f;
    [SerializeField] private float jumpHeight=15f;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    private bool isFacingLeft=true;
    private float initialPositionX;
    protected override void Start()
    {
        base.Start();
        coll=GetComponent<Collider2D>();
        initialPositionX = transform.position.x;
    }
    void Update()
    {
        CheckingJumpingState();
        Move();
    }

    private void CheckingJumpingState()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void Move()
    {
        if(isFacingLeft)
        {
            if(transform.position.x>initialPositionX-5.0f)
            {
                if(transform.localScale.x!=1)
                {
                    transform.localScale=new Vector3(1,1);
                }
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity=new Vector2(-jumpLength,jumpHeight);
                    anim.SetBool("Jumping",true);
                }
            }
            else
            {
                isFacingLeft=false;
            }
        }
        else 
        {
            if(transform.position.x<initialPositionX+5.0f)
            {
                if(transform.localScale.x!=-1)
                {
                    transform.localScale=new Vector3(-1,1);
                }
            if(coll.IsTouchingLayers(ground))
            {
                rb.velocity=new Vector2(jumpLength,jumpHeight);
                anim.SetBool("Jumping",true);
            }
            }
        }
    }
}   

