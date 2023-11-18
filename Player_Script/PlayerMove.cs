using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 100.0f; // 움직임 속도를 조절할 변수

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // 현재 위치 업데이트
        transform.position += new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;

        // x축 위치 제한
        float clampedX = Mathf.Clamp(transform.position.x, -20f, 20f);

        // 위치 조정
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
