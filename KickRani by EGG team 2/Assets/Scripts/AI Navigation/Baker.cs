using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class Baker : MonoBehaviour
{
    NavMeshSurface navMeshSurface;
    public bool A = false;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = this.gameObject.GetComponent<NavMeshSurface>();
    }

    public void BakeRoad()
    {
        navMeshSurface.BuildNavMesh();
    }
    // Update is called once per frame
    
}
