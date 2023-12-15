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
        // ������Ʈ Ǯ��
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

    //genTime���� �� Ÿ�� �ϳ��� ����. �� ������ �߰��� �ۺ� ���ӿ�����Ʈ�� ����ġ�� �߰��ϸ� �� 

    IEnumerator roadPulling()
    {
        GameObject RT = roadTiles[pullingRoadCode];
        

        // ��Ȱ��ȭ�� ���¿��� Ȱ��ȭ
        RT.SetActive(true);

        // ���� ��ġ�� ȸ���� �����ϸ鼭 ������Ʈ�� �̵�
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

        // 10% Ȯ���� ������ ����
        if (Random.value < 0.1f) // 0�� 1 ������ ������ �� ����, 10% Ȯ�� üũ
        {
            GameObject item = Instantiate(itemprefab, RT.transform.position + Vector3.up, Quaternion.identity);
            // �������� ���� Ÿ�� ���� ��ġ��ŵ�ϴ�. �ʿ��� ��� ��ġ�� �����մϴ�.
        }

        // �̵�
        this.transform.position += this.transform.right * chunkSize;

        yield return new WaitForSeconds(genTime);

        // ��Ȱ��ȭ ���·� ����� ������ ���� �غ�
        

        pullingRoadCode++;
        pullingRoadCode %= 50;
        StartCoroutine(roadPulling());

        yield return new WaitForSeconds(5);
        RT.SetActive(false);
    }

}
