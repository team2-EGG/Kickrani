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
                targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            }
            running = false;
        }
        else
        {
            //멈추는거 풀기
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
            running = true;
        }
        
    }

    
}
