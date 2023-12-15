using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MovingObstacle : MonoBehaviour
{
    public Transform parentObject; // 부모 오브젝트를 할당할 변수
    public float teleportRange = 26f; // 텔레포트할 범위
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
            // 부모의 위치를 기준으로 랜덤한 위치로 텔레포트
            Vector3 randomPosition = parentObject.position + Random.insideUnitSphere * teleportRange;

            // Y 좌표를 부모와 동일하게 설정하여 수직 이동을 방지
            randomPosition.y = parentObject.position.y;

            // 현재 위치를 랜덤한 위치로 변경
            T.transform.position = randomPosition;

            return randomPosition;
    }
        else
        {
            Debug.LogError("부모 오브젝트가 지정되지 않았습니다.");
            return Vector3.zero;
        }
    }

    private void Update()
    {
        B.GetComponent<Baker>().BakeRoad();
        agent.SetDestination(T.transform.position);
    }
}