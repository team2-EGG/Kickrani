using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static gameManager;

public class ToScore : MonoBehaviour
{
    public GameObject scoreManager;
    ScoreManager sm;
    int score = DataHolder.SomeValue;
    public Text playerName;
    // Start is called before the first frame update
    public void ToScoreBtn()
    {
        
        sm = scoreManager.GetComponent<ScoreManager>();
        sm.AddPlayerScore(playerName.text, score);
        SceneManager.LoadScene("Score");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
