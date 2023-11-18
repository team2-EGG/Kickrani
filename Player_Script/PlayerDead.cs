using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private float rotationAmount = 90f; // 회전할 각도
    private float duration = 0.6f; // 회전하는데 걸리는 시간 (초)
    private float rotationSpeed; // 초당 회전 속도
    private float currentRotation = 0f; // 현재 회전된 각도
    private bool isRotating = false; // 회전 중인지 확인

    void Start()
    {
        rotationSpeed = rotationAmount / duration; // 초당 회전 속도 계산
    }

    void Update()
    {
        // 'R' 키를 눌러 회전 시작
        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            isRotating = true;
        }

        // 회전 로직
        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(-step, 0, 0);
            currentRotation += step;

            if (currentRotation >= rotationAmount)
            {
                isRotating = false;
                currentRotation = 0f;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && ! isRotating) // Q키 누르면 다시 원 위치
        {
            this.transform.rotation = (Quaternion.Euler(0, -90, 0));
        }
    }
}
