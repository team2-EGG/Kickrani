using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MovingObstacle : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    public Transform parentObject; // 부모 오브젝트를 할당할 변수
    public float teleportRange = 26f; // 텔레포트할 범위
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
            // 부모의 위치를 기준으로 랜덤한 위치로 텔레포트
            Vector3 randomPosition = parentObject.position + new Vector3(26, 0, -4) + Random.insideUnitSphere * teleportRange;

            // Y 좌표를 부모와 동일하게 설정하여 수직 이동을 방지
            randomPosition.y = parentObject.position.y;

            // 현재 위치를 랜덤한 위치로 변경
            T.transform.position = randomPosition;
        }
        else
        {
            Debug.LogError("부모 오브젝트가 지정되지 않았습니다.");
        }
    }
}
