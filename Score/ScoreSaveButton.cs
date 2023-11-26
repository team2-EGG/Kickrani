using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaveButton : MonoBehaviour
{
    public GameObject SM;
    public GameObject NameText;
    public GameObject ScoreText;
    string name = null;
    int score = 0;
    ScoreManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        sm = SM.GetComponent<ScoreManager>();   
    }

    // Update is called once per frame
    public void OnClickSaveButton()
    {
        name = NameText.GetComponent<InputField>().text;
        sm.AddPlayerScore(name, score);
        return;
    }
}
