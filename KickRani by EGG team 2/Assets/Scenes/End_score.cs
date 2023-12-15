using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static gameManager;

public class End_score : MonoBehaviour
{
    int score = DataHolder.SomeValue;
    public GameObject Scoreboard;
    // Start is called before the first frame update
    void Start()
    {
        Scoreboard.GetComponent<Text>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
