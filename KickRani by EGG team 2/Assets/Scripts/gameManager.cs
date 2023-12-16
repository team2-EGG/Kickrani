using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int stamina = 100;
    public GameObject gorani;
    public PlayerController gorani_ctrlScript;
    public pmc_mobile gorani_moveScript;
    // public Player_movecontroller gorani_moveScript;
    public bool hit_helmet = false;
    public bool hit_star = false;
    public bool hit_bat = false;
    public float helmet_time = 0.0f;
    public float star_time = 0.0f;
    public float bat_time = 0.0f;
    public CapsuleCollider gorani_col;

    // 점수쓰
    public int score = 0;
    public float score_time = 0.0f;
    public int increase_score = 50;
    public Text ScoreText;
    public GameObject Scoreboard;
    public static class DataHolder
    {
        public static int SomeValue = 0;
    }

    // 난이도
    public int difficulty = 0;
    public float difficulty_time = 0.0f;
    public int pauseval = 1;

    // 오디오
    public AudioSource[] gameSound;

    public void gameOver()
    {
        gorani_moveScript.pauseval = 0.0f;
        // increase_score = 0;
        pauseval = 0;
        gorani_moveScript.death = true;
        DataHolder.SomeValue = score;
    }

    public void getItemHelmet() // 헬멧 착용
    {
        gorani_ctrlScript.isHelmet = true;
    }

    public void getItemBattery() // 일정 시간 동안 점수 두 배
    {
        gorani_ctrlScript.isShield = true;
        increase_score *= 2;
    }

    public void getItemStar() // 일정 시간 동안 충돌 무시, 속도 빠르게
    {
        gorani_ctrlScript.isStar = true;
        gorani_moveScript.speed *= 2.0f;
    }

    public void collideToSmallObstacle()
    {
        Debug.Log("작은거 되냐?");
        if (gorani_ctrlScript.isStar)
        {
            
        }
        else if (gorani_ctrlScript.isHelmet)
        {
            gameSound[3].Play();
            gorani_ctrlScript.isHelmet = false;
        }
        else
        {
            gameOver();
        }
    }

    public void collideToBigObstacle()
    {
        Debug.Log("큰거 되냐?");
        if (gorani_ctrlScript.isStar)
        {

        }
        else
        {
            gameOver();
        }
    }

    void Start()
    {
        gorani = GameObject.FindWithTag("player");
        gorani_ctrlScript = gorani.GetComponent<PlayerController>();
        gorani_moveScript = gorani.GetComponent<pmc_mobile>();
        // gorani_moveScript = gorani.GetComponent<Player_movecontroller>();
        gorani_col = gorani.GetComponent<CapsuleCollider>();
        gameSound = GetComponents<AudioSource>();
    }

    void Update()
    {
        // 점수 책정
        score_time += Time.deltaTime;
        if (score_time > 1.0)
        {
            score += (increase_score + (difficulty * 50)) * pauseval;
            score_time = 0;
            Debug.Log(score);
        }

        Scoreboard.GetComponent<Text>().text = score.ToString();

        // 난이도 책정
        difficulty_time += Time.deltaTime;
        if (difficulty_time > 5.0)
        {
            difficulty += 1 * pauseval;
            difficulty_time = 0.0f;
        }

        // 아이템 충돌 확인 및 현재 아이템 적용여부 확인
        if (hit_helmet && gorani_ctrlScript.isHelmet == false)
        {
            gameSound[2].Play();
            getItemHelmet();
            hit_helmet = false;
        }
        if (hit_star && gorani_ctrlScript.isStar == false)
        {
            gameSound[1].Play();
            getItemStar();
            hit_star = false;
        }
        if (hit_bat && gorani_ctrlScript.isShield == false)
        {
            gameSound[2].Play();
            getItemBattery();
            hit_bat = false;
        }

        // 적용시간 끝나면 하는 것
        /* 헬멧 시간 임시
        if (gorani_ctrlScript.isHelmet)
        {
            helmet_time += Time.deltaTime;
            if (helmet_time > 5.0f)
            {
                gorani_ctrlScript.isHelmet = false;
                helmet_time = 0;
            }
        }
        */
        if (gorani_ctrlScript.isStar)
        {
            star_time += Time.deltaTime;
            if (star_time > 5.0f)
            {
                gorani_ctrlScript.isStar = false;
                gorani_moveScript.speed /= 2.0f;
                star_time = 0;
            }
        }
        if (gorani_ctrlScript.isShield)
        {
            bat_time += Time.deltaTime;
            if (bat_time > 5.0f)
            {
                gorani_ctrlScript.isShield = false;
                increase_score /= 2;
                bat_time = 0;
            }
        }
    }










}
