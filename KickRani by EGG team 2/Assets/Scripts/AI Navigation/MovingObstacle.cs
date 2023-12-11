using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class MovingObstacle : MonoBehaviour
{
    NavMeshSurface navMeshSurface;

    NavMeshAgent agent;
    public bool A = false;
    public GameObject T;

    private Vector3 initialRelativePosition;


    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        navMeshSurface = this.gameObject.GetComponent<NavMeshSurface>();
        initialRelativePosition = transform.localPosition;
    }

    void RestoreToInitialState()
    {
        transform.localPosition = initialRelativePosition;
    }

    void OnEnable()
    {
        agent.enabled = true;
        target = T.transform;
        navMeshSurface.BuildNavMesh();
        agent.SetDestination(target.position);

        RestoreToInitialState();
    }

    private void OnDisable()
    {
        agent.enabled = false;
    }

    // Update is called once per frame

}
