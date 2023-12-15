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
    public GameObject B;

    Baker baker;

    private Vector3 initialRelativePosition;


    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = T.transform;
        baker = B.GetComponent<Baker>();
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
        //agent.enabled = true;
        baker.BakeRoad();
        agent.SetDestination(target.position);

        RestoreToInitialState();
    }

    private void OnDisable()
    {
        //agent.enabled = false;
    }

    // Update is called once per frame

}
