using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartBtn()
    {
        SceneManager.LoadScene("Shin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
