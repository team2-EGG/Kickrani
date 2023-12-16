using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseBtn : MonoBehaviour
{
    bool running = true;
    public Image targetImage;

    public gameManager GM;
    public GameObject gorani;
    public PlayerController gorani_ctrlScript;
    public pmc_mobile gorani_moveScript;

    public float tmp_speed;

    public AudioSource sound;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<gameManager>();
        gorani = GameObject.FindWithTag("player");
        gorani_ctrlScript = gorani.GetComponent<PlayerController>();
        gorani_moveScript = gorani.GetComponent<pmc_mobile>();
        sound = GetComponent<AudioSource>();
        // 초기에 이미지를 숨김
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);
        }
    }

    public void ShowImage()
    {
        // 이미지의 활성/비활성 상태를 토글
        if (running)
        {
            //멈추기
            if (targetImage != null)
            {
                gorani_moveScript.pauseval = 0.0f;
                GM.pauseval = 0;
                targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
                Debug.Log("멈춰랏!");
            }
            running = false;
            sound.Play();
        }
        else
        {
            //멈추는거 풀기
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            running = true;
            sound.Play();

            GM.pauseval = 1;
            gorani_moveScript.pauseval = 1.0f;
        }
        
    }

    
}
