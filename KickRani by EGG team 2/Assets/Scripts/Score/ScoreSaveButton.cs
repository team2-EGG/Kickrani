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
    string name;
    int score = 0;
    ScoreManager sm;
    // Start is called before the first frame update
    void Awake()
    {
        sm = SM.GetComponent<ScoreManager>();   
    }

    //�̸� �޴� �κа� �ֵ��÷��̾�, ��ºκ� ������ ���� �� ��
    public void OnClickSaveButton()
    {
        name = NameText.GetComponent<InputField>().text;
        if (name == "")
        {
            name = "Unknown";
        }

        score = int.Parse(ScoreText.GetComponent<InputField>().text);

        sm.AddPlayerScore(name, score);
        highScoreText.text = sm.Top10();

        return;
    }
}
