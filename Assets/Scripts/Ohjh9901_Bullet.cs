using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjh9901_Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletPower;

    private  GameObject player;
    private Ohjh9901_Player tank;
    private Rigidbody2D rigidBullet;

    private bool isBarrier;

    void Start()
    {
        player = GameObject.Find("Tank");
        tank = player.GetComponent<Ohjh9901_Player>();
        rigidBullet = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        BossBullet1();
        BossBullet2();
        PlayerBullet();
        PlayerBarrierOn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer == 6) //ÇÃ·¹ÀÌ¾î°¡ ½ð bullet
        {
            if(collision.gameObject.tag == "Border" || collision.gameObject.tag == "Boss")
            {
                if(collision.gameObject.tag == "Boss")
                {
                    collision.gameObject.GetComponent<Ohjh9901_Boss>().hp -= bulletPower;
                    
                }
                gameObject.SetActive(false);
            }
        }
        else if(gameObject.layer == 7) //boss°¡ ½ð bullet
        {
            if (collision.gameObject.tag == "Border" || collision.gameObject.tag == "Player")
            {
                if(collision.gameObject.tag == "Player" && !isBarrier)
                {
                    collision.gameObject.GetComponent<Ohjh9901_Player>().hp -= bulletPower;
                }
                gameObject.SetActive(false);
            }
        }
    }

    void PlayerBarrierOn()
    {
        isBarrier = tank.isItem2;
    }

    void BossBullet1()
    {
       if(gameObject.tag == "BossBullet1")
        {
            rigidBullet.velocity = Vector2.left * bulletSpeed;
        }
    }
    void BossBullet2()
    {
        if (gameObject.tag == "BossBullet2")
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, 5f * Time.deltaTime);
        }
    }

    void PlayerBullet()
    {
        if (gameObject.tag == "PlayerBullet")
        {
            rigidBullet.velocity = Vector2.right * bulletSpeed;
        }
    }

}
