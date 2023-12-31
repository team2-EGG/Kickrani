using System.Collections;
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
    private string filePath;

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
        LoadPlayerData();
    }

    //베스트 스코어
    public string BestScore()
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null && playerData.playerList.Count > 0)
        {
            Player topPlayer = playerData.playerList[0];
            return "Top Score: " + topPlayer.playerName + " - " + topPlayer.playerScore + " points";
        }
        else
        {
            return "No High Score Yet";
        }
      
    }

    //탑 10
    public string Top10()
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

            return highScoreString;
            // UI에 텍스트 출력
            //highScoreText.text = highScoreString;
        }
        else
        {
            return "No High Score Yet";
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

    //플레이어 이름, 점수만 넣으면 됨
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

    //지금까지 모든 기록 삭제
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