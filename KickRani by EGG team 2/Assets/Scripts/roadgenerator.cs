using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadgenerator : MonoBehaviour
{
    public GameObject[] roadTiles;
    public int[] roadCodes;
    public GameObject Fd1 = null;
    public GameObject Fd2 = null;
    public GameObject Fd3 = null;
    public GameObject Fd4 = null;
    public GameObject Fd5 = null;
    public GameObject Left = null;
    public GameObject Right1 = null;
    public GameObject Right2 = null;
    public int genroadcode = 0;
    public float chunkSize = 52;
    public float genTime = 1f;
    public int pullingRoadCode = 0;

    public GameObject itemprefab_star;
    public GameObject itemprefab_helmet;
    public GameObject itemprefab_bat;

    void Start()
    {
        this.transform.position = Vector3.zero;
        // 오브젝트 풀링
        roadTiles = new GameObject[50];
        roadCodes = new int[50];
        for (int i = 0; i < 50; i++)
        {
            int randomNumber;
            System.Random random = new System.Random();
            
            while (true)
            {
                randomNumber = random.Next(0, 7);
                roadCodes[i] = randomNumber;
                if (randomNumber != genroadcode)
                {
                    genroadcode = randomNumber;
                    break;
                }
            }

            GameObject roadTile = null;
            switch (randomNumber)
            {
                case 0:
                    roadTile = Instantiate(Fd1);
                    break;
                case 1:
                    roadTile = Instantiate(Left);
                    break;
                case 2:
                    roadTile = Instantiate(Right1);
                    break;
                case 3:
                    roadTile = Instantiate(Fd2);
                    break;

                case 4:
                    roadTile = Instantiate(Fd3);
                    break;
                case 5:
                    roadTile = Instantiate(Fd4);
                    break;
                case 6:
                    roadTile = Instantiate(Fd5);
                    break;
                case 7:
                    roadTile = Instantiate(Right2);
                    break;
                default:
                    break;
            }

            roadTiles[i] = roadTile;
            roadTile.SetActive(false);
        }

        //StartCoroutine(roadPulling());
        for (int i= 0; i < 7; i++)
        {
            roadPulling();
        }
        
    }

    //genTime마다 길 타일 하나씩 생성. 길 프리팹 추가시 퍼블릭 게임오브젝트와 스위치문 추가하면 됨 

    public void roadPulling()
    {
        GameObject RT = roadTiles[pullingRoadCode];
        

        // 비활성화된 상태에서 활성화
        RT.SetActive(true);

        // 현재 위치와 회전을 유지하면서 오브젝트를 이동
        RT.transform.position = this.transform.position;
        RT.transform.rotation = this.transform.rotation;
        if (roadCodes[pullingRoadCode] == 1)
        {
            this.transform.Rotate(0, -90, 0);
        }
        if (roadCodes[pullingRoadCode] == 2 || roadCodes[pullingRoadCode] == 7)
        {
            this.transform.Rotate(0, 90, 0);
        }

        float ranval = Random.value;

        // 10% 확률로 아이템 생성
        if (ranval < 0.1f) // 0과 1 사이의 무작위 수 생성, 10% 확률 체크
        {
            Vector3 ranvec = new Vector3(Random.value * 5, 0, Random.value * 5);
            Vector3 newvec = RT.transform.position + Vector3.up + ranvec;
            GameObject staritem = Instantiate(itemprefab_star, newvec, Quaternion.identity);
        }
        else if (ranval < 0.6f && ranval > 0.4f) // 0과 1 사이의 무작위 수 생성, 10% 확률 체크
        {
            Vector3 ranvec = new Vector3(Random.value * 5, 0, Random.value * 5);
            Vector3 newvec = RT.transform.position + Vector3.up + ranvec;
            GameObject helmetitem = Instantiate(itemprefab_helmet, newvec, Quaternion.identity);
        }
        else if (ranval > 0.9f) // 0과 1 사이의 무작위 수 생성, 10% 확률 체크
        {
            Vector3 ranvec = new Vector3(Random.value * 5, 0, Random.value * 5);
            Vector3 newvec = RT.transform.position + Vector3.up + ranvec;
            GameObject batitem = Instantiate(itemprefab_bat, newvec, Quaternion.identity);
        }


        // 이동
        this.transform.position += this.transform.right * chunkSize;

        //yield return new WaitForSeconds(genTime);

        // 비활성화 상태로 만들어 재사용을 위해 준비
        

        pullingRoadCode++;
        pullingRoadCode %= 50;
        //StartCoroutine(roadPulling());

        
    }

}
