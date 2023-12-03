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

    //genTime���� �� Ÿ�� �ϳ��� ����. �� ������ �߰��� �ۺ� ���ӿ�����Ʈ�� ����ġ�� �߰��ϸ� �� 
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
                this.transform.Rotate(0, -90, 0); //��ȸ��
                break;
            case 3:
                road = Instantiate(Right, this.transform.position, Quaternion.Euler(this.transform.eulerAngles));
                this.transform.Rotate(0, 90, 0); //��ȸ��
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
