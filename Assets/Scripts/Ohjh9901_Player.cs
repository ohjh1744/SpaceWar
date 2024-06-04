using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjh9901_Player : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float hp;
    private float previousHp;

    public Transform shotPos;
    public AudioSource audioJump; //점프용 소리
    public AudioClip[] audioClips;


    private float h;
    private bool isJump;
    private  bool isShot;
    private bool isPattern1; //패턴1공격에 맞았을때
    private bool isPattern2; //패턴2공격에 맞았을때
    private bool isItem1;
    private float itemTime1; 
    private float itemTime2;

    [HideInInspector]
    public bool isItem2;

    private Rigidbody2D rigid;
    private GameObject bullet;
    private GameObject barrier;
    private AudioSource audio;
    

    //피격시 색상
    private SpriteRenderer spriteRenderer;
    private GameObject playerBody1;
    private SpriteRenderer body1renderer;
    private GameObject playerBody2;
    private SpriteRenderer body2renderer;

    void Start()
    {
        previousHp = hp;
        rigid = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        playerBody1 = GameObject.Find("Tank").gameObject.transform.Find("TankBody").gameObject;
        playerBody2 = GameObject.Find("Tank").gameObject.transform.Find("TankShoot").gameObject;
        barrier = gameObject.transform.Find("Barrier").gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        body1renderer = playerBody1.GetComponent<SpriteRenderer>();
        body2renderer = playerBody2.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        JumpCheck();
        Jump();
        Shot();
        StartCoroutine(PlayerHit());
        ItemTime();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EatItem(collision);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPattern1)
        {
            StartCoroutine(bePatternAttacked1(collision));
        }

        if (!isPattern2)
        {
            StartCoroutine(bePatternAttacked2(collision));
        }
    }



    void Move() //좌우이동
    {
        rigid.velocity = new Vector2(h, 0) * speed;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            audioJump.Play();
            rigid.AddForce( Vector2.up * jumpPower);
        }
    }    

    void JumpCheck()
    {
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.7f, LayerMask.GetMask("Floor"));
            if (rayHit.collider != null)
            {
                isJump = false;
            }
            else
            {
                isJump = true;
            }
        }    

    }

    void Shot()
    {
        if (Input.GetButton("Fire1") && !isShot)
        {
            if (isItem1)
            {
                StartCoroutine(ReadyShot(3));
                PlaySound(0);
            }
            else
            {
                StartCoroutine(ReadyShot(0));
                PlaySound(1);
            }

        } 
    }

    IEnumerator ReadyShot(int bulletNum)
    {
        isShot = true;
        bullet = Ohjh9901_PullManager.pullManager.Get(bulletNum);
        bullet.transform.position = shotPos.position;

        yield return new WaitForSeconds(0.1f);

        isShot = false;
    }

    void ItemTime()
    {
        itemTime1 = itemTime1 - Time.deltaTime;
        itemTime2 = itemTime2 - Time.deltaTime;

        if(itemTime1 <= 0)
        {
            isItem1 = false;
        }

        if(itemTime2 <= 0)
        {
            isItem2 = false;
            barrier.SetActive(false);
        }
    }
    void EatItem(Collider2D collision)
    {
        audio.clip = audioClips[2];
        
        if(collision.tag == "Item1")
        {
            isItem1 = true;
            itemTime1 = 5f;
            collision.gameObject.SetActive(false);
            audio.Play();
        }
        if(collision.tag == "Item2")
        {
            isItem2 = true;
            itemTime2 = 3f;
            collision.gameObject.SetActive(false);
            barrier.SetActive(true);
            audio.Play();
        }

    }


    IEnumerator PlayerHit()
    {
        if(hp < previousHp)
        {
            spriteRenderer.color = Color.red;
            body1renderer.color = Color.red;
            body2renderer.color = Color.red;
            previousHp = hp;

            yield return new WaitForSeconds(0.1f);

            spriteRenderer.color = Color.blue;
            body1renderer.color = Color.blue;
            body2renderer.color = Color.blue;
        }
    }

    IEnumerator bePatternAttacked1(Collider2D collision)
    {
        if(collision.tag == "Pattern1" && !isItem2)
        {
            isPattern1 = true;
            hp -= 15;

            yield return new WaitForSeconds(0.5f);

            isPattern1 = false;
        }
    }

    IEnumerator bePatternAttacked2(Collider2D collision)
    {

        if (collision.tag == "Pattern2" && !isItem2)
        {
            isPattern2 = true;
            hp -= 20;

            yield return new WaitForSeconds(0.5f);

            isPattern2 = false;
        }
    }


    void PlaySound(int soundNum)
    {
        audio.clip = audioClips[soundNum];
        audio.Play();
    }



}
