using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MovingObstacle : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    public Transform parentObject; // �θ� ������Ʈ�� �Ҵ��� ����
    public float teleportRange = 26f; // �ڷ���Ʈ�� ����
    NavMeshAgent agent;
    public bool A = false;
    public GameObject T;
    public GameObject B;

    Baker baker;

    private Vector3 initialRelativePosition;


    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        baker = B.GetComponent<Baker>();
        agent = GetComponent<NavMeshAgent>();
        parentObject = T.transform.parent;
        navMeshSurface = this.gameObject.GetComponent<NavMeshSurface>();
        initialRelativePosition = transform.localPosition;
    }

    void RestoreToInitialState()
    {

        transform.localPosition = initialRelativePosition;
    }

    void OnEnable()
    {

        baker.BakeRoad();
        TeleportWithinRange();
        agent.SetDestination(T.transform.position);

        RestoreToInitialState();
    }

    void TeleportWithinRange()
    {
        if (parentObject != null)
        {
            // �θ��� ��ġ�� �������� ������ ��ġ�� �ڷ���Ʈ
            Vector3 randomPosition = parentObject.position + new Vector3(26, 0, -4) + Random.insideUnitSphere * teleportRange;

            // Y ��ǥ�� �θ�� �����ϰ� �����Ͽ� ���� �̵��� ����
            randomPosition.y = parentObject.position.y;

            // ���� ��ġ�� ������ ��ġ�� ����
            T.transform.position = randomPosition;
        }
        else
        {
            Debug.LogError("�θ� ������Ʈ�� �������� �ʾҽ��ϴ�.");
        }
    }
}
