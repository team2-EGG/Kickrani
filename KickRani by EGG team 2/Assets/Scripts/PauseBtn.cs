using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseBtn : MonoBehaviour
{
    bool running = true;
    public Image targetImage;

    void Start()
    {
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
                targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            }
            running = false;
        }
        else
        {
            //���ߴ°� Ǯ��
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            running = true;
        }
        
    }

    
}
