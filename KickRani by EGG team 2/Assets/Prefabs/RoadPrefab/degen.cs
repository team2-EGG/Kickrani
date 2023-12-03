using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class degen : MonoBehaviour
{
    public float degentime = 5;
    float time = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > degentime)
        {
            Destroy(this.gameObject);
        }
    }
}
