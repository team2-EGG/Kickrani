using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movecontroller : MonoBehaviour
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


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotationSpeed = rotationAmount / duration; // �ʴ� ȸ�� �ӵ� ���
        originalChildPosition = childObject.localPosition;
        originalChildRotation = childObject.localRotation;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // �¿� �̵� �Է�

        // �¿� �Է¿� ���� ���� �Ʒ��� �̵�
        Vector3 movementDirection = (horizontalInput > 0) ? -transform.forward : transform.forward;
        Vector3 movement = movementDirection * Mathf.Abs(horizontalInput) * moveSpeed * Time.deltaTime;

        // ������Ʈ�� ��ġ ������Ʈ
        rb.MovePosition(rb.position + movement);

        // �÷��̾� �� ����
        if (Input.GetKeyDown(KeyCode.Z) && turnActive == false)
        {
            turnActive = true;
            direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.X) && turnActive == false)
        {
            turnActive = true;
            direction = 1;
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

        if (Input.GetKeyDown(KeyCode.Q)) // QŰ ������ �ٽ� �� ��ġ
        {
            this.transform.rotation = (Quaternion.Euler(0, -90, 0));
        }

        // �÷��̾� ���� ����
        // ���� ������ ��� (������Ʈ�� ���� ����)
        Vector3 right = transform.right;

        // ������ٵ� ����Ͽ� ������Ʈ�� ������Ŵ
        rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);

        // �÷��̾� ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;

            // ��ġ �� ȸ�� ����
            childObject.localPosition = tempPosition;
            childObject.localRotation = tempRotation;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            childObject.localPosition = originalChildPosition;
            childObject.localRotation = originalChildRotation;
        }
    }

}
