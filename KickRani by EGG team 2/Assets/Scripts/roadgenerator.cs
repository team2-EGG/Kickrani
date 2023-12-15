using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadgenerator : MonoBehaviour
{
    public GameObject baker;
    Baker bk;
    public GameObject[] roadTiles;
    public int[] roadCodes;
    public GameObject Fd = null;
    public GameObject Left = null;
    public GameObject Right = null;
    public int genroadcode = 0;
    public float chunkSize = 52;
    public float genTime = 1f;
    public int pullingRoadCode = 0;

    public GameObject itemprefab;

    void Start()
    {
        this.transform.position = Vector3.zero;
        bk = baker.GetComponent<Baker>(); ;
        // 오브젝트 풀링
        roadTiles = new GameObject[50];
        roadCodes = new int[50];
        for (int i = 0; i < 50; i++)
        {
            int randomNumber;
            System.Random random = new System.Random();
            
            while (true)
            {
                randomNumber = random.Next(0, 3);
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
                    roadTile = Instantiate(Fd);
                    break;
                case 1:
                    roadTile = Instantiate(Left);
                    break;
                case 2:
                    roadTile = Instantiate(Right);
                    break;
                default:
                    break;
            }

            roadTiles[i] = roadTile;
            roadTile.SetActive(false);
        }

        StartCoroutine(roadPulling());
    }

    //genTime마다 길 타일 하나씩 생성. 길 프리팹 추가시 퍼블릭 게임오브젝트와 스위치문 추가하면 됨 

    IEnumerator roadPulling()
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
        if (roadCodes[pullingRoadCode] == 2)
        {
            this.transform.Rotate(0, 90, 0);
        }

        bk.BakeRoad();

        // 10% 확률로 아이템 생성
        if (Random.value < 0.1f) // 0과 1 사이의 무작위 수 생성, 10% 확률 체크
        {
            GameObject item = Instantiate(itemprefab, RT.transform.position + Vector3.up, Quaternion.identity);
            // 아이템을 도로 타일 위에 위치시킵니다. 필요한 경우 위치를 조정합니다.
        }

        // 이동
        this.transform.position += this.transform.right * chunkSize;

        yield return new WaitForSeconds(genTime);

        // 비활성화 상태로 만들어 재사용을 위해 준비
        

        pullingRoadCode++;
        pullingRoadCode %= 50;
        StartCoroutine(roadPulling());

        yield return new WaitForSeconds(5);
        RT.SetActive(false);
    }

}
