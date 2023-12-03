using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScore : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToScoreBtn()
    {
        SceneManager.LoadScene("Score");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
