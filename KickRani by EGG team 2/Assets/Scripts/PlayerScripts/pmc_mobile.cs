using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pmc_mobile : MonoBehaviour
{
    // player move
    public float moveSpeed = 100.0f; // 이동 속도
    private Rigidbody rb;
    // player turn
    float turnAngle = 90f;
    public float turnSpeed = 500f;
    bool turnActive = false;
    int direction = 0;
    // player death
    private float rotationAmount = 90f; // 회전할 각도
    private float duration = 0.6f; // 회전하는데 걸리는 시간 (초)
    private float rotationSpeed; // 초당 회전 속도
    private float currentRotation = 0f; // 현재 회전된 각도
    public Transform goraTransform;
    public Transform riderTransform;
    public bool death = false;
    // player go forward
    public float speed = 10f; // 전진 속도
    // player jump
    public float jumpForce = 10f;
    public Transform childObject;
    private bool isGrounded;
    private Vector3 originalChildPosition;
    private Quaternion originalChildRotation;
    // 공중에 떠 있을 때 적용할 위치와 회전값
    private Vector3 tempPosition = new Vector3(-0.3f, 0.2f, 0f);
    private Quaternion tempRotation = Quaternion.Euler(25f, 90f, 0f);
    //모바일을 위한 변수들
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private Vector2 touchDif;
    public float sensitive = 0.7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationAmount / duration; // 초당 회전 속도 계산
        originalChildPosition = childObject.localPosition;
        originalChildRotation = childObject.localRotation;
    }

    void Update()
    {
        float tiltX = Input.acceleration.x;

        // 좌우 입력에 따라 위나 아래로 이동
        Vector3 movementDirection = (tiltX > 0) ? -transform.forward : transform.forward;
        Vector3 movement = movementDirection * Mathf.Abs(tiltX) * moveSpeed * Time.deltaTime*sensitive;

        // 오브젝트의 위치 업데이트
        rb.MovePosition(rb.position + movement);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPos = touch.position;
                touchDif = (endTouchPos-startTouchPos);
            }
        }    


        // 플레이어 턴 관련
        if (touchDif.x > 0 && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y) && turnActive == false)
        {
            turnActive = true;
            direction = 1;
            touchDif = Vector3.zero;
            
        }
        else if (touchDif.x < 0 && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y) && turnActive == false)
        {
            turnActive = true;
            direction = -1;
            touchDif = Vector3.zero;

        }

        if (turnActive == true)
        {


            Vector3 turn = new Vector3(0, turnSpeed, 0);
            transform.Rotate(turn * direction * Time.deltaTime);
            turnAngle -= turnSpeed * Time.deltaTime;

            if (turnAngle <= 0)
            {
                turnActive = false;
                turnAngle = 90.0f;
                Vector3 currentRotation = transform.eulerAngles;
                currentRotation.y = Mathf.Round(currentRotation.y / 90f) * 90f;
                transform.eulerAngles = currentRotation;

            }
        }

        // 플레이어 사망 관련
  
        // 회전 로직
        if (death)
        {
            float step = rotationSpeed * Time.deltaTime;
            goraTransform.Rotate(0, 0, step);
            riderTransform.Rotate(step, 0, 0);
            currentRotation += step;

            if (currentRotation >= rotationAmount)
            {
                death = false;
                currentRotation = 0f;
            }
        }


        // 플레이어 전진 관련
        // 전진 방향을 계산 (오브젝트의 앞쪽 방향)
        Vector3 right = transform.right;

        // 리지드바디를 사용하여 오브젝트를 전진시킴
        rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);

        // 플레이어 점프 관련
        if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;

            // 위치 및 회전 변경
            childObject.localPosition = tempPosition;
            childObject.localRotation = tempRotation;
            touchDif = Vector3.zero;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;

            childObject.localPosition = originalChildPosition;
            childObject.localRotation = originalChildRotation;
        }
    }

}
