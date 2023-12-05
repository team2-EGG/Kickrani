using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaveButton : MonoBehaviour
{
    public GameObject SM;
    public GameObject NameText;
    public GameObject ScoreText;
    public Text highScoreText;
    string playerName;
    int score = 0;
    ScoreManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        sm = SM.GetComponent<ScoreManager>();   
    }

    //이름 받는 부분과 애드플레이어, 출력부분 가져다 쓰면 될 듯
    public void OnClickSaveButton()
    {
        playerName = NameText.GetComponent<InputField>().text;
        if (playerName == "")
        {
            playerName = "Unknown";
        }

        score = int.Parse(ScoreText.GetComponent<InputField>().text);

        sm.AddPlayerScore(playerName, score);
        highScoreText.text = sm.Top10();

        return;
    }
}
