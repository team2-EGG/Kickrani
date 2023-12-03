using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadgenerator : MonoBehaviour
{
    public GameObject Fd;
    public GameObject Left;
    public GameObject Right;
    public int genroadcode = 0;
    Vector3 genrot = Vector3.zero;
    public bool timetogen;
    public float chunkSize = 52;
    public float genTime = 1f;

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
    void Start()
    {
        this.transform.position = Vector3.zero;
        timetogen = true;
    }

    void Update()
    {
        if (timetogen)
        {
            timetogen = false;
            StartCoroutine(generateRoad());
        }
    }
}
