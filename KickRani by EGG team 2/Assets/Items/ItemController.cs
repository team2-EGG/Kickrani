using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public gameManager GM;
    public string myTag;
    public float life_time = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<gameManager>();
        myTag = gameObject.tag;
    }

    // Update is called once per frame  
    void Update()
    {
        life_time -= Time.deltaTime;

        /*
        if (life_time < 0)
        {
            Destroy(gameObject);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myTag == "bat" && other.gameObject.tag == "player")
        {
            GM.hit_bat = true;
            Destroy(gameObject);
        }
        if (myTag == "star" && other.gameObject.tag == "player")
        {
            GM.hit_star = true;
            Destroy(gameObject);
        }
        if (myTag == "helmet" && other.gameObject.tag == "player")
        {
            GM.hit_helmet = true;
            Destroy(gameObject);
        }
    }
}
