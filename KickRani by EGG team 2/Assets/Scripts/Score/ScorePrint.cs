using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrint : MonoBehaviour
{
    Text text;
    public GameObject SM;
    ScoreManager sm;
    void Start()
    {
        text = this.GetComponent<Text>();
        sm = SM.GetComponent<ScoreManager>();

        text.text = sm.Top10();
        //if (ScoreManager != null)
        //{
        //    string arrayText = string.Join(", ", ScoreManager.Top10);
        //    textComponent.text = "Array Values: " + arrayText;
        //}
    }

    void Update()
    {
        
    }
}
