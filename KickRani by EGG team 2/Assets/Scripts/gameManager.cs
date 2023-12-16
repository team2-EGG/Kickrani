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

    // ������
    public int score = 0;
    public float score_time = 0.0f;
    public int increase_score = 50;
    public Text ScoreText;
    public GameObject Scoreboard;
    public static class DataHolder
    {
        public static int SomeValue = 0;
    }

    // ���̵�
    public int difficulty = 0;
    public float difficulty_time = 0.0f;
    public int pauseval = 1;

    // �����
    public AudioSource[] gameSound;

    public void gameOver()
    {
        gorani_moveScript.pauseval = 0.0f;
        // increase_score = 0;
        pauseval = 0;
        gorani_moveScript.death = true;
        DataHolder.SomeValue = score;
    }

    public void getItemHelmet() // ��� ����
    {
        gorani_ctrlScript.isHelmet = true;
    }

    public void getItemBattery() // ���� �ð� ���� ���� �� ��
    {
        gorani_ctrlScript.isShield = true;
        increase_score *= 2;
    }

    public void getItemStar() // ���� �ð� ���� �浹 ����, �ӵ� ������
    {
        gorani_ctrlScript.isStar = true;
        gorani_moveScript.speed *= 2.0f;
    }

    public void collideToSmallObstacle()
    {
        Debug.Log("������ �ǳ�?");
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
        Debug.Log("ū�� �ǳ�?");
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
        // ���� å��
        score_time += Time.deltaTime;
        if (score_time > 1.0)
        {
            score += (increase_score + (difficulty * 50)) * pauseval;
            score_time = 0;
            Debug.Log(score);
        }

        Scoreboard.GetComponent<Text>().text = score.ToString();

        // ���̵� å��
        difficulty_time += Time.deltaTime;
        if (difficulty_time > 5.0)
        {
            difficulty += 1 * pauseval;
            difficulty_time = 0.0f;
        }

        // ������ �浹 Ȯ�� �� ���� ������ ���뿩�� Ȯ��
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

        // ����ð� ������ �ϴ� ��
        /* ��� �ð� �ӽ�
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
