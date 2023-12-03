using UnityEngine;

public class PlayerJumpAnimated : MonoBehaviour
{
    public float jumpForce = 10f;
    public Transform childObject; 
    private bool isGrounded;
    private Rigidbody rb;
    private Vector3 originalChildPosition; 
    private Quaternion originalChildRotation; 

    // ���߿� �� ���� �� ������ ��ġ�� ȸ����
    private Vector3 tempPosition = new Vector3(-0.3f, 0.2f, 0f);
    private Quaternion tempRotation = Quaternion.Euler(25f, 90f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalChildPosition = childObject.localPosition;
        originalChildRotation = childObject.localRotation; 
    }

    void Update()
    {
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



