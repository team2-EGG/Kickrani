using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnBoard : MonoBehaviour
{
    float turnAngle = 90f;
    public float turnSpeed = 5f;
    bool turnActive = false;
    int direction = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Z) && turnActive == false)
        {
            turnActive = true;
            direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.X) && turnActive == false)
        {
            turnActive = true;
            direction = 1;
        }
        
            
        
        if (turnActive == true)
        {
            
                    
            Vector3 turn = new Vector3(0, turnSpeed, 0);
            transform.Rotate(turn * direction * Time.deltaTime);
            turnAngle -= turnSpeed * Time.deltaTime;

            if (turnAngle <= 0)
            {
                turnActive = false;
                turnAngle = 90.0f;
            }
        }


    }
}
