using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private float rotationAmount = 90f; // ȸ���� ����
    private float duration = 0.6f; // ȸ���ϴµ� �ɸ��� �ð� (��)
    private float rotationSpeed; // �ʴ� ȸ�� �ӵ�
    private float currentRotation = 0f; // ���� ȸ���� ����
    private bool isRotating = false; // ȸ�� ������ Ȯ��

    void Start()
    {
        rotationSpeed = rotationAmount / duration; // �ʴ� ȸ�� �ӵ� ���
    }

    void Update()
    {
        // 'R' Ű�� ���� ȸ�� ����
        if (Input.GetKeyDown(KeyCode.R) && !isRotating)
        {
            isRotating = true;
        }

        // ȸ�� ����
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
        
        if (Input.GetKeyDown(KeyCode.Q) && ! isRotating) // QŰ ������ �ٽ� �� ��ġ
        {
            this.transform.rotation = (Quaternion.Euler(0, -90, 0));
        }
    }
}
