using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // 좌우 이동 입력

        // 좌우 입력에 따라 위나 아래로 이동
        Vector3 movementDirection = (horizontalInput > 0) ? -transform.forward : transform.forward;
        Vector3 movement = movementDirection * Mathf.Abs(horizontalInput) * moveSpeed * Time.deltaTime;

        // 오브젝트의 위치 업데이트
        rb.MovePosition(rb.position + movement);
    }
}



