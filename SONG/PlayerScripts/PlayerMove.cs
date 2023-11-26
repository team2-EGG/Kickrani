using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �̵� �ӵ�
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // �¿� �̵� �Է�

        // �¿� �Է¿� ���� ���� �Ʒ��� �̵�
        Vector3 movementDirection = (horizontalInput > 0) ? -transform.forward : transform.forward;
        Vector3 movement = movementDirection * Mathf.Abs(horizontalInput) * moveSpeed * Time.deltaTime;

        // ������Ʈ�� ��ġ ������Ʈ
        rb.MovePosition(rb.position + movement);
    }
}



