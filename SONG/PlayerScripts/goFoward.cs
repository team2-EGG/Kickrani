using UnityEngine;

public class goFoward : MonoBehaviour
{
    public float speed = 5f; // 전진 속도
    private Rigidbody rb; // 리지드바디 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 리지드바디 컴포넌트를 가져옴
    }

    void FixedUpdate()
    {
        // 전진 방향을 계산 (오브젝트의 앞쪽 방향)
        Vector3 right = transform.right;

        // 리지드바디를 사용하여 오브젝트를 전진시킴
        rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);
    }
}

