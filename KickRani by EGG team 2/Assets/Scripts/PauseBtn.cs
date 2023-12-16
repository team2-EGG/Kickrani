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
        // �ʱ⿡ �̹����� ����
        if (targetImage != null)
        {
            targetImage.gameObject.SetActive(false);
        }
    }

    public void ShowImage()
    {
        // �̹����� Ȱ��/��Ȱ�� ���¸� ���
        if (running)
        {
            //���߱�
            if (targetImage != null)
            {
                gorani_moveScript.pauseval = 0.0f;
                GM.pauseval = 0;
                targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
                Debug.Log("�����!");
            }
            running = false;
            sound.Play();
        }
        else
        {
            //���ߴ°� Ǯ��
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            running = true;
            sound.Play();

            GM.pauseval = 1;
            gorani_moveScript.pauseval = 1.0f;
        }
        
    }

    
}
