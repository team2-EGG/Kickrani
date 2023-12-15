using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MovingObstacle : MonoBehaviour
{
    public Transform parentObject; // �θ� ������Ʈ�� �Ҵ��� ����
    public float teleportRange = 26f; // �ڷ���Ʈ�� ����
    NavMeshAgent agent;
    public bool A = false;
    public GameObject T;
    public GameObject B;

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        B = GameObject.FindGameObjectWithTag("baker");
        agent = GetComponent<NavMeshAgent>();



        

    }



    void OnEnable()
    {
        TeleportWithinRange();

    }

    Vector3 TeleportWithinRange()
    {
        if (parentObject != null)
        {
            // �θ��� ��ġ�� �������� ������ ��ġ�� �ڷ���Ʈ
            Vector3 randomPosition = parentObject.position + Random.insideUnitSphere * teleportRange;

            // Y ��ǥ�� �θ�� �����ϰ� �����Ͽ� ���� �̵��� ����
            randomPosition.y = parentObject.position.y;

            // ���� ��ġ�� ������ ��ġ�� ����
            T.transform.position = randomPosition;

            return randomPosition;
    }
        else
        {
            Debug.LogError("�θ� ������Ʈ�� �������� �ʾҽ��ϴ�.");
            return Vector3.zero;
        }
    }

    private void Update()
    {
        B.GetComponent<Baker>().BakeRoad();
        agent.SetDestination(T.transform.position);
    }
}