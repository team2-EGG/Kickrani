using System.Collections;
using UnityEngine;
using UnityEngine.UI; // �� �κ� �߰�
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

    //����Ʈ ���ھ�
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

    //ž 10
    public string Top10()
    {
        PlayerData playerData = LoadPlayerData();
        if (playerData != null && playerData.playerList.Count > 0)
        {
            // ���� 10�� ǥ��
            int displayCount = Mathf.Min(playerData.playerList.Count, 10);

            string highScoreString = "";
            for (int i = 0; i < displayCount; i++)
            {
                Player currentPlayer = playerData.playerList[i];
                highScoreString += (i + 1) + ". " + currentPlayer.playerName + " - " + currentPlayer.playerScore + " points\n";
            }

            return highScoreString;
            // UI�� �ؽ�Ʈ ���
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
            // ������ ���� ���, �� ������ ����
            return new PlayerData { playerList = new List<Player>() };
        }
    }

    //�÷��̾� �̸�, ������ ������ ��
    public void AddPlayerScore(string playerName, int score)
    {
        PlayerData playerData = LoadPlayerData();

        // ���ο� �÷��̾� ���� �߰�
        Player newPlayer = new Player();
        newPlayer.playerName = playerName;
        newPlayer.playerScore = score;
        playerData.playerList.Add(newPlayer);

        // ����Ʈ�� ������ �������� ������������ ����
        playerData.playerList.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

        // ���� 10���� ����
        if (playerData.playerList.Count > 10)
        {
            playerData.playerList.RemoveAt(10);
        }

        // �÷��̾� ���� ����
        SavePlayerData(playerData);

        // �ؽ�Ʈ ������Ʈ �߰�
    }

    void SavePlayerData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
    }

    //���ݱ��� ��� ��� ����
    public void resetData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        // ������ �����ϴ��� Ȯ�� �� ����
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