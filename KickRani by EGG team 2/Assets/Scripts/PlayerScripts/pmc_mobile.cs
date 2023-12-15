using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pmc_mobile : MonoBehaviour
{
    // player move
    public float moveSpeed = 100.0f; // �̵� �ӵ�
    private Rigidbody rb;
    // player turn
    float turnAngle = 90f;
    public float turnSpeed = 500f;
    bool turnActive = false;
    int direction = 0;
    // player death
    private float rotationAmount = 90f; // ȸ���� ����
    private float duration = 0.6f; // ȸ���ϴµ� �ɸ��� �ð� (��)
    private float rotationSpeed; // �ʴ� ȸ�� �ӵ�
    private float currentRotation = 0f; // ���� ȸ���� ����
    public Transform goraTransform;
    public Transform riderTransform;
    public bool death = false;
    // player go forward
    public float speed = 10f; // ���� �ӵ�
    // player jump
    public float jumpForce = 10f;
    public Transform childObject;
    private bool isGrounded;
    private Vector3 originalChildPosition;
    private Quaternion originalChildRotation;
    // ���߿� �� ���� �� ������ ��ġ�� ȸ����
    private Vector3 tempPosition = new Vector3(-0.3f, 0.2f, 0f);
    private Quaternion tempRotation = Quaternion.Euler(25f, 90f, 0f);
    //������� ���� ������
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private Vector2 touchDif;
    public float sensitive = 0.7f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationAmount / duration; // �ʴ� ȸ�� �ӵ� ���
        originalChildPosition = childObject.localPosition;
        originalChildRotation = childObject.localRotation;
    }

    void Update()
    {
        float tiltX = Input.acceleration.x;

        // �¿� �Է¿� ���� ���� �Ʒ��� �̵�
        Vector3 movementDirection = (tiltX > 0) ? -transform.forward : transform.forward;
        Vector3 movement = movementDirection * Mathf.Abs(tiltX) * moveSpeed * Time.deltaTime*sensitive;

        // ������Ʈ�� ��ġ ������Ʈ
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


        // �÷��̾� �� ����
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

        // �÷��̾� ��� ����
  
        // ȸ�� ����
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


        // �÷��̾� ���� ����
        // ���� ������ ��� (������Ʈ�� ���� ����)
        Vector3 right = transform.right;

        // ������ٵ� ����Ͽ� ������Ʈ�� ������Ŵ
        rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);

        // �÷��̾� ���� ����
        if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;

            // ��ġ �� ȸ�� ����
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
