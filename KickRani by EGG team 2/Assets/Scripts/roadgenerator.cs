using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadgenerator : MonoBehaviour
{
    public GameObject[] roadTiles;
    public int[] roadCodes;
    public GameObject Fd;
    public GameObject Left;
    public GameObject Right;
    public int genroadcode = 0;
    Vector3 genrot = Vector3.zero;
    public bool timetogen;
    public float chunkSize = 52;
    public float genTime = 1f;
    public int pullingRoadCode = 0;

    void Start()
    {
        Debug.Log("정상영업중");
        this.transform.position = Vector3.zero;
        timetogen = true;

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
    }

    //genTime마다 길 타일 하나씩 생성. 길 프리팹 추가시 퍼블릭 게임오브젝트와 스위치문 추가하면 됨 
    IEnumerator generateRoad()
    {
        int randomNumber;
        System.Random random = new System.Random();
        while (true)
        {
            randomNumber = random.Next(1, 4);
            if (randomNumber != genroadcode)
            {
                genroadcode = randomNumber;
                break;
            }
        }
        
        GameObject road = new GameObject();

        switch (randomNumber)
        {
            case 1:
                road = Instantiate(Fd, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
                break;
            case 2:
                road = Instantiate(Left, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
                this.transform.Rotate(0, -90, 0); //좌회전
                break;
            case 3:
                road = Instantiate(Right, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
                this.transform.Rotate(0, 90, 0); //우회전
                break;
            default:
                break;

        }
        this.transform.position += this.transform.right * chunkSize;



        yield return new WaitForSeconds(genTime);
        timetogen = true;
    }

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

        // 이동
        this.transform.position += this.transform.right * chunkSize;

        yield return new WaitForSeconds(genTime);

        // 비활성화 상태로 만들어 재사용을 위해 준비
        

        pullingRoadCode++;
        pullingRoadCode %= 50;
        timetogen = true;

        yield return new WaitForSeconds(5);
        RT.SetActive(false);
    }


    void Update()
    {
        if (timetogen)
        {
            timetogen = false;
            StartCoroutine(roadPulling());
        }
    }
}
