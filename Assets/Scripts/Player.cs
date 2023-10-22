using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private Transform trans;
    private SpriteRenderer sprite;
    // private GhostEffect ghostEffect;

    //jump var
    [SerializeField] protected float moveSpeed=3f;
    [SerializeField] protected float jumpForce;
    [SerializeField] private  float moving; 
    [SerializeField] private  float timePowerUps; 
    [SerializeField] private LayerMask ground;

    private bool isGiant=false;
    private float ratioScale=2.5f;


    private enum State {idle,running,jumping,falling,hurt}
    private State state=State.idle;
    [SerializeField] protected float hurtForce;
    [SerializeField] protected float delayTime=0.1f;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
        trans=GetComponent<Transform>();
        sprite=GetComponent<SpriteRenderer>();
        // ghostEffect=GetComponent<GhostEffect>();
    if (rb == null || anim == null || coll == null|| GetComponent<AudioSource>()==null)
        {
            Debug.LogError("One or more components are missing!");
        }
    }
    protected void Update()
    {
        if(state!=State.hurt)
        {
            Moving();
            Jumping();
        }
        VelocityState();
        anim.SetInteger("state",(int)state);
    }
    // player actions 
    protected void Moving()
    {
        moving = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moving*moveSpeed,rb.velocity.y);
        flip();
    }
    protected void Jumping()
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            jump();
        }
    }
    private void jump()
    {
        rb.velocity=new Vector2(rb.velocity.x,jumpForce);
        state=State.jumping;
    }
    void flip()
    {
        if(moving<0 && !isGiant)
        {
            // ghostEffect.MakeGhost=true;
            rb.velocity=new Vector2(-moveSpeed,rb.velocity.y);
            transform.localScale=new Vector2(-1,1);
        }
        else if(moving>0 && !isGiant)
        {
            // ghostEffect.MakeGhost=true;
            rb.velocity=new Vector2(moveSpeed,rb.velocity.y);
            transform.localScale=new Vector2(1,1);
        }
        else if(moving<0 && isGiant)
        {
            // ghostEffect.MakeGhost=true;
            rb.velocity=new Vector2(-moveSpeed,rb.velocity.y);
            transform.localScale=new Vector2(-1*ratioScale,1*ratioScale);
        }
        else if(moving>0 && isGiant)
        {
            // ghostEffect.MakeGhost=true;
            rb.velocity=new Vector2(moveSpeed,rb.velocity.y);
            transform.localScale=new Vector2(1*ratioScale,1*ratioScale);
        }
    }
    protected void VelocityState()
    {
    if (state == State.jumping && rb != null)
        {
            // ghostEffect.MakeGhost=true;
            if (rb.velocity.y <.1f)
            {
                state = State.falling;
            }
        }
    else if (state == State.falling && coll != null)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
                // ghostEffect.MakeGhost=false;
            }
        }
    else if (state == State.hurt && rb != null)
        { 

            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
                // ghostEffect.MakeGhost=false;
            }
        }
    else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
    else
        {
            state = State.idle;
            // ghostEffect.MakeGhost=false;
        }
    }
    protected void ApplyState()
    {
        anim.SetInteger("state",(int)state);
    }
    // TEST TRIGGER OBJECT 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Collectable")
        {
            CollectCherry(collision.gameObject);
        }
        if(collision.tag=="Power")
        {
            PowerUps(collision.gameObject);
        }
        if(collision.tag=="Giant")
        {
            Giant(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            if(state==State.falling)
            {
                enemy.JumpedOn();
                jump();
                StartCoroutine(enemy.Death(delayTime));
            }
            else
            {
                state=State.hurt; 
                getHealthy();        
                TakeDamage(other.gameObject.transform.position.x >transform.position.x);
            }
        }
    } 
    protected void TakeDamage(bool enemyOnRight)
    {
        rb.velocity = new Vector2(enemyOnRight ? -hurtForce : hurtForce, rb.velocity.y);
    }
    // COLECTABLE OBJECT 
    protected void CollectCherry(GameObject cherry)
    {
        Destroy(cherry);
        PermagnentUI.perm.cherries++;
        PermagnentUI.perm.CherriesPoint.text =  PermagnentUI.perm.cherries.ToString();
    }
    protected void PowerUps(GameObject gem)
    {
        Destroy(gem);
        jumpForce=10.0f;
        GetComponent<SpriteRenderer>().color=Color.red;
        StartCoroutine(Reset());
    }
    protected void Giant(GameObject huge)
    {
        Destroy(huge);
        if(!isGiant)
        {
        isGiant=true;
        }
        StartCoroutine(ResetGiant());
    }
    protected IEnumerator Reset()
    {
        yield return new WaitForSeconds(timePowerUps);
        jumpForce=10.0f;
        GetComponent<SpriteRenderer>().color=Color.white;
    }
    protected IEnumerator ResetGiant()
    {
        yield return new WaitForSeconds(timePowerUps);
        isGiant=false;

    }
    protected void getHealthy()
    {
         PermagnentUI.perm.healthAmount-=1;
         PermagnentUI.perm.Healthy.text =  PermagnentUI.perm.healthAmount.ToString();
        if( PermagnentUI.perm.healthAmount<=0)
        {
            PermagnentUI.perm.ResetPoint();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // regame
        }
    }

}

