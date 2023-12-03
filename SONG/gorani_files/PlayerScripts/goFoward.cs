using UnityEngine;

public class goFoward : MonoBehaviour
{
    public float speed = 5f; // ���� �ӵ�
    private Rigidbody rb; // ������ٵ� ������Ʈ

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // ������ٵ� ������Ʈ�� ������
    }

    void FixedUpdate()
    {
        // ���� ������ ��� (������Ʈ�� ���� ����)
        Vector3 right = transform.right;

        // ������ٵ� ����Ͽ� ������Ʈ�� ������Ŵ
        rb.MovePosition(rb.position + right * speed * Time.fixedDeltaTime);
    }
}

