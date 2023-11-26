using System.Collections;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI; // 이 부분 추가
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class Player
{
    public string playerName;
    public int playerScore;
}

[System.Serializable]
public class PlayerData
{
    public List<Player> playerList;
}

public class ScoreManager : MonoBehaviour
{
    public Text highScoreText; // UI Text 컴포넌트 참조를 위한 변수 추가
    public Text topScoreText;  // UI Text 컴포넌트 참조를 위한 변수 추가

    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        LoadPlayerData();
        DisplayTop();
        DisplayTop10();
    }

    public void DisplayTop()
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null && playerData.playerList.Count > 0)
        {
            Player topPlayer = playerData.playerList[0];
            topScoreText.text = "Top Score: " + topPlayer.playerName + " - " + topPlayer.playerScore + " points";
        }
        else
        {
            topScoreText.text = "No High Score Yet";
        }
      
    }
    public void DisplayTop10()
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null && playerData.playerList.Count > 0)
        {
            // 상위 10명만 표시
            int displayCount = Mathf.Min(playerData.playerList.Count, 10);

            string highScoreString = "";
            for (int i = 0; i < displayCount; i++)
            {
                Player currentPlayer = playerData.playerList[i];
                highScoreString += (i + 1) + ". " + currentPlayer.playerName + " - " + currentPlayer.playerScore + " points\n";
            }

            // UI에 텍스트 출력
            highScoreText.text = highScoreString;
        }
        else
        {
            highScoreText.text = "No High Score Yet";
        }
    }

    PlayerData LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // 파일이 없을 경우, 빈 데이터 생성
            return new PlayerData { playerList = new List<Player>() };
        }
    }

    public void AddPlayerScore(string playerName, int score)
    {
        PlayerData playerData = LoadPlayerData();

        // 새로운 플레이어 정보 추가
        Player newPlayer = new Player();
        newPlayer.playerName = playerName;
        newPlayer.playerScore = score;
        playerData.playerList.Add(newPlayer);

        // 리스트를 점수를 기준으로 내림차순으로 정렬
        playerData.playerList.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

        // 상위 10개만 유지
        if (playerData.playerList.Count > 10)
        {
            playerData.playerList.RemoveAt(10);
        }

        // 플레이어 정보 저장
        SavePlayerData(playerData);

        // 텍스트 업데이트 추가
    }

    void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
    }

    public void resetData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        // 파일이 존재하는지 확인 후 삭제
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted: " + filePath);
        }
        else
        {
            Debug.Log("File does not exist: " + filePath);
        }
    }
}