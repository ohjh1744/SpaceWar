using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjh9901_Boss : MonoBehaviour
{
    public float speed;
    public float hp;
   

    public GameObject skill1; // 레이저
    public GameObject warningSkill1; //레이저 쏘기전 반경표시
    public GameObject[] skill2;
    public GameObject[] warningSkill2;
    public Transform[] shotPos;
    public AudioClip[] audioClips;
    public AudioSource audioPattern2;

    private Rigidbody2D rigid;
    private GameObject bullet;
    private GameObject bullet2;
    private AudioSource audio;

    private bool isShot;
    private bool isShot2;
    private bool isPattern1; //pattern1레이저쏘기 시작
    private bool isPattern2; //pattern2 시작
    private bool shot4Lazer1;

    private int moveNum;
    private bool moveterm;
    int shotPosNum;


    void Start()
    {
        audio = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(BossEventPattern());
    }

   // Update is called once per frame
    void Update()
    {

        StartCoroutine(BossEventPattern2());

        if (!isPattern1)
        {
            StartCoroutine(BossMove());
            if (!isShot)
            {
                StartCoroutine(BossShot1(1));
                PlaySound(0);
            }
            if (!isShot2)
            {
                StartCoroutine(BossShot2(2));
                PlaySound(1);
            }
        }
    }


    IEnumerator BossMove()
    {
        if (!moveterm)
        {
            moveterm = true;
            moveNum = Random.Range(-1, 2); // -1~1까지
            rigid.velocity = new Vector2(0, moveNum) * speed;

            yield return new WaitForSeconds(0.5f);

            moveterm = false;

        }      
    }

    IEnumerator BossShot1(int bulletNum)
    {
        isShot = true;
        bullet = Ohjh9901_PullManager.pullManager.Get(bulletNum);
        bullet.transform.position = shotPos[shotPosNum].position;

        yield return new WaitForSeconds(0.25f);

        if(shotPosNum == 0)
        {
            shotPosNum = 1;
        }
        else
        {
            shotPosNum = 0;
        }
        isShot = false;
    }

    IEnumerator BossShot2(int bulletNum)
    {
        isShot2 = true;
        bullet2 = Ohjh9901_PullManager.pullManager.Get(bulletNum);
        bullet2.transform.position = shotPos[2].position;

        yield return new WaitForSeconds(3f);

        bullet2.SetActive(false);

        isShot2 = false;
      
    }

    IEnumerator BossSkill1()
    {
       isPattern1 = true;
        warningSkill1.SetActive(true);
       audio.clip = audioClips[2];
       audio.loop = true;
       audio.Play();

       yield return new WaitForSeconds(1f);

        warningSkill1.SetActive(false);
       skill1.SetActive(true);
       audio.clip = audioClips[3];
       audio.loop = true;
       audio.Play();

       yield return new WaitForSeconds(2f);

        isPattern1 = false;
       skill1.SetActive(false);
       audio.loop = false;

    }

    IEnumerator BossSkill2()
    {
        isPattern2 = true;
        if (shot4Lazer1)
        {
            warningSkill2[0].SetActive(true);
        }
        else
        {
            warningSkill2[1].SetActive(true);
        }
        
        yield return new WaitForSeconds(1f);

        audioPattern2.Play();
        if (shot4Lazer1)
        {
            warningSkill2[0].SetActive(false);
            skill2[0].SetActive(true);
        }
        else
        {
            warningSkill2[1].SetActive(false);
            skill2[1].SetActive(true);
        }

        yield return new WaitForSeconds(1f);


        if (shot4Lazer1)
        {
            skill2[0].SetActive(false);
            shot4Lazer1 = false;
        }
        else
        {
            skill2[1].SetActive(false);
            shot4Lazer1 = true;
        }

        yield return new WaitForSeconds(3f);

        isPattern2 = false;

    }

    IEnumerator BossEventPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(12f);
            StartCoroutine(BossSkill1());
        }

    }

    IEnumerator BossEventPattern2()
    {
       while(hp <= 500 && !isPattern2)
        {
            StartCoroutine(BossSkill2());
            yield return new WaitForSeconds(5f);
        }

    }


    void PlaySound(int soundNum)
    {
        audio.clip = audioClips[soundNum];
        audio.Play();
    }






}
