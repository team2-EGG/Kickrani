using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRoadTile : MonoBehaviour
{
    public GameObject RM;
    roadgenerator rm;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("RT ���� ��");
        RM = GameObject.FindWithTag("RoadGenerator");
        rm = RM.GetComponent<roadgenerator>();
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("������ϴ�");
        if (collision.gameObject.tag == "player")
        {
            //StartCoroutine(rm.roadPulling());
            rm.roadPulling();
            StartCoroutine(DisableAfter2sec());
        }
    }
    IEnumerator DisableAfter2sec()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
