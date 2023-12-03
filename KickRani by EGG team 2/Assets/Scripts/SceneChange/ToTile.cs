using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToTitleBtn()
    {
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
