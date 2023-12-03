using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelSpin : MonoBehaviour
{
    public float spinSpeed = -75f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spin = new Vector3(0f, 0f, spinSpeed); 

        transform.Rotate(spin * Time.deltaTime);
        
    }
}
