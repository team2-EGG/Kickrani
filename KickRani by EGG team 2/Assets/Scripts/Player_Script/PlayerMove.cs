using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 100.0f; // ������ �ӵ��� ������ ����

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // ���� ��ġ ������Ʈ
        transform.position += new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;

        // x�� ��ġ ����
        float clampedX = Mathf.Clamp(transform.position.x, -20f, 20f);

        // ��ġ ����
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
