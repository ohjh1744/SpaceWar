using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ohjh9901_GameManager : MonoBehaviour
{
    public Slider bossHpBar;
    public Slider playerHpBar;
    public GameObject subUi;
    public GameObject subUiButton;
    public GameObject winUi;
    public GameObject loseUi;
    public Ohjh9901_Player player;
    public Ohjh9901_Boss boss;

    private float playerHp;
    private float bossHp;
    private float playerMaxHp = 100;
    private float bossMaxHp = 1000;
    private bool gameFinish;

    private AudioSource canvasAudio;

    void Start()
    {
        canvasAudio = GameObject.Find("Canvas").gameObject.GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 1f;
            player = player.GetComponent<Ohjh9901_Player>();
            boss = boss.GetComponent<Ohjh9901_Boss>();

            bossHpBar.value = (float)bossHp / (float)bossMaxHp;
            playerHpBar.value = (float)playerHp / (float)playerMaxHp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            UpdateHp();

            if (Input.GetButtonDown("Cancel") && !gameFinish)
            {
                VisibleButton();
            }

            if (playerHp <= 0 && !gameFinish)
            {
                subUiButton.SetActive(false);
                gameFinish = true;
                Time.timeScale = 0f;
                loseUi.SetActive(true);
                subUi.gameObject.SetActive(true);
            }
            else if (bossHp <= 0 && !gameFinish)
            {
                subUiButton.SetActive(false);
                gameFinish = true;
                Time.timeScale = 0f;
                winUi.SetActive(true);
                subUi.gameObject.SetActive(true);
            }


        }

    }

    void UpdateHp()
    {
        bossHp = boss.hp;
        playerHp = player.hp;
        bossHpBar.value = (float)bossHp / (float)bossMaxHp;
        playerHpBar.value = (float)playerHp / (float)playerMaxHp;
    }

    public void VisibleButton()
    {
        canvasAudio.Play();
       if(subUi.activeSelf == true)
       {
            Time.timeScale = 1f;
            subUi.gameObject.SetActive(false);
       }
       else
       {
            Time.timeScale = 0f;
            subUi.gameObject.SetActive(true);
       }
    }


    private void End()
    {
        Application.Quit();
    }

    private void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    

    public void DoEnd()
    {
        canvasAudio.Play();
        Time.timeScale = 1f;
        Invoke("End", 0.2f);
    }

    public void DoGameStart()
    {
        canvasAudio.Play();
        Time.timeScale = 1f;
        Invoke("GameStart", 0.2f);
    }


}
