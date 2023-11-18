using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class boardJump : MonoBehaviour
{
    float jumpForce = 5.0f;
    float fallForce = 5.0f;// 점프 힘의 크기
    float gravityadd = 1.0f;
    bool jumpActive = false;
    bool fallActive = false;
    float jumpHeight = 2.5f;
    float heightSum = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fallActive == false && jumpActive == false)
        {
            jumpActive = true;

        }
        if (jumpActive == true)
        {
            Vector3 jump1 = new Vector3(0f, jumpForce, 0f);

            gravityadd *= 0.994f;
            transform.Translate(jump1* Time.deltaTime*gravityadd);
            jumpHeight -= jumpForce * Time.deltaTime * gravityadd;
            heightSum += jumpForce * Time.deltaTime * gravityadd;

            if (jumpHeight <= 0)
            {
                jumpActive = false;
                fallActive = true;
                
            }
        }

        if (fallActive == true)
        {
            Vector3 jump1 = new Vector3(0f, fallForce, 0f);

            gravityadd *= 1.006f;
            transform.Translate(-1 * jump1 * Time.deltaTime * gravityadd);
            heightSum -= 1f * fallForce * Time.deltaTime * gravityadd;
            if (heightSum <= 0)
            {
                gravityadd = 1.0f;
                fallActive = false;
                heightSum = 0f;
                jumpHeight =2.5f;
            }

            Vector3 currentPosition = transform.position;
            currentPosition.y = Mathf.Max(currentPosition.y, 0.0f);
            transform.position = currentPosition;


        }
    }

}