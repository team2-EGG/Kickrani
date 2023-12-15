using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public bool wearingHelmet = false;
    public int stamina = 100;
    public GameObject gorani;
    public PlayerController gorani_ctrlScript;
    public Player_movecontroller gorani_moveScript;
    public bool hit_helmet = false;
    public bool hit_star = false;
    public bool hit_bat = false;
    public float helmet_time = 0.0f;
    public float star_time = 0.0f;
    public float bat_time = 0.0f;
    public CapsuleCollider gorani_col;


    public void gameOver()
    {
        
    }

    public void getItemHelmet() // ��� ����
    {
        gorani_ctrlScript.isHelmet = true;
    }

    public void getItemBattery() // ���� �ð� ���� ���� �� ��
    {
        // ���� ������ ����
    }

    public void getItemStar() // ���� �ð� ���� �浹 ����, �ӵ� ������
    {
        gorani_ctrlScript.isStar = true;
        gorani_moveScript.speed = 20.0f;
    }

    public void collideToSmallObstacle()
    {
        if (gorani_ctrlScript.isHelmet)
        {

        }
        else
        {
            gameOver();
        }
    }

    public void collideToBigObstacle()
    {
        gameOver();
    }

    void Start()
    {
        gorani = GameObject.FindWithTag("player");
        gorani_ctrlScript = gorani.GetComponent<PlayerController>();
        gorani_moveScript = gorani.GetComponent<Player_movecontroller>();
        gorani_col = gorani.GetComponent<CapsuleCollider>();
    }

    void Update()
    {   
        if (hit_helmet && gorani_ctrlScript.isHelmet == false)
        {
            getItemHelmet();
            hit_helmet = false;
        }
        if (hit_star && gorani_ctrlScript.isStar == false)
        {
            getItemStar();
            hit_star = false;
        }
        if (hit_bat)
        {
            Debug.Log("col_bat");
            hit_bat = false;
        }

        if (gorani_ctrlScript.isHelmet)
        {
            helmet_time += Time.deltaTime;
            Debug.Log(helmet_time);
            if (helmet_time > 5.0f)
            {
                gorani_ctrlScript.isHelmet = false;
                helmet_time = 0;
            }
        }

        if (gorani_ctrlScript.isStar)
        {
            star_time += Time.deltaTime;
            Debug.Log(star_time);
            if (star_time > 5.0f)
            {
                gorani_ctrlScript.isStar = false;
                gorani_moveScript.speed = 10.0f;
                star_time = 0;
            }
        }

        // ���͸� �ð� ����
    }

}
