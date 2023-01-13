using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    //玩家的碰撞方框（可以是胶囊也可以是正方形）
    public Collider2D coll;
    public Collider2D DisColl;
    public float speed, jumpforce;
    //LayerMask 指的是图层，告诉系统那个图层是真正的地面.
    public LayerMask ground;

    public Transform CellingCheck;
    
    //记录樱桃
    public Text CherryNum;


    //记录吃了多少樱桃
    public int Cherry = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()   //自适应变化帧数 ， 根据不同电脑 ，有的电脑卡 自动会掉帧 所以要fix叼
    {
        Movement();
        SwitchAnim();

    }

    void Movement()
    {
        float horizontalmove = Input.GetAxisRaw("Horizontal");  //get -1 , 0 , 1 if 1 right , -1 left
        //左右移动
        rb.velocity = new Vector2(horizontalmove * speed, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(horizontalmove));
        //Time.deltaTime 乘以物理时钟的百分比 ， 所以在运行中 会变得平滑不跳帧数
        //rb.velocity = new Vector2(horizontalmove*speed,rb.velocity.y);

        //控制左右朝向
        if (horizontalmove != 0)
        {
            transform.localScale = new Vector3(horizontalmove, 1, 1);  //x , y , z （Y,Z 保持不变）
        }


        //跳跃   
        //1-11 remove super jumps !!!  同时执行的时候才算
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }

        Crouch();
    }
    //切换动画
    void SwitchAnim()
    {
        //无论如何都会执行 因为这是一开始的基础移动 ， 也只有在将落地的时候 重新切换为true
        anim.SetBool("ldle", false);
        //直接返回一个boolean 因为我设置的boolean variable!!!
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                //当y轴小于0的时候 关掉jumping 因为已经开始下降了，同时fall为true
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        //如果玩家下降碰到地面，那么下落为false ， 回归正常ldle ， 但是要确保如果下落一次 就会一直保持一样 ， 所以在一开始LINE56 要给个trigger值 去control it back
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("ldle", true);
        }
    }

    //吃cherry！ 收集method
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            Cherry+=1;
            //把int 变string 用ToString();
            CherryNum.text = Cherry.ToString() ;
        }
    }
    // 下蹲 crouching 
    void Crouch()
    {
        //顶头 ， 如果钻进去   ， 不能让他站立！
        if (!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground))
        {
            if (Input.GetButtonDown("Crouch"))
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }

    }
}
