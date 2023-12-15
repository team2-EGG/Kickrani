using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 ShieldSize = new Vector3(2.5f, 2.5f, 2.5f);
    public bool isShield = false;
    public bool isHelmet = false;
    public bool isStar = false;

    Transform Shield;
    Transform Helmet;
    Material meshMaterial;
    Coroutine colorChangeCoroutine;

    public gameManager GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<gameManager>();
        Transform goraTransform = transform.Find("gora");
        Shield = goraTransform.Find("Shield");
        Helmet = goraTransform.Find("MESH_Hat_Skater");
        Transform meshTransform = goraTransform.Find("Mesh");


        if (meshTransform == null)
        {
            Debug.LogError("Mesh 오브젝트를 찾을 수 없습니다.");
            return;
        }

        Renderer meshRenderer = meshTransform.GetComponent<Renderer>();
        if (meshRenderer != null)
            meshMaterial = meshRenderer.material;
        else
            Debug.LogError("Mesh 오브젝트에 Renderer 컴포넌트가 없습니다.");
    }

    void Update()
    {
        if (Shield != null)
        {
            Shield.localScale = isShield ? ShieldSize : Vector3.zero;
            Debug.Log("없음1");
        }

        if (Helmet != null)
        {
            Helmet.localScale = isHelmet ? Vector3.one : Vector3.zero;
            Debug.Log("없음2");
        }

        if (isStar && meshMaterial != null && colorChangeCoroutine == null)
        {
            colorChangeCoroutine = StartCoroutine(ChangeColor());
        }
        else if (!isStar && colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
            colorChangeCoroutine = null;
            meshMaterial.color = Color.white; // 색상을 원래대로 설정
        }
    }

    IEnumerator ChangeColor()
    {
        Color[] baseColors = new Color[] { Color.red, Color.white, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta};
        Color[] colors = new Color[baseColors.Length];
        for (int i = 0; i < baseColors.Length; i++)
        {
            colors[i] = ReduceBrightness(baseColors[i], 0.6f); // 명도를 ?% 줄인 색상으로 설정
        }

        int index = 0;

        while (true)
        {
            meshMaterial.color = colors[index];
            index = (index + 1) % colors.Length;
            meshMaterial.SetFloat("_Emission", 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 명도를 줄이는 함수
    Color ReduceBrightness(Color color, float brightnessFactor)
    {
        color.r *= brightnessFactor;
        color.g *= brightnessFactor;
        color.b *= brightnessFactor;
        return color;
    }

    // 장애물 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "smallObstacle")
        {
            GM.collideToSmallObstacle();
            Debug.Log("플충됨?");
        }
        else if (other.gameObject.tag == "bigObstacle")
        {
            GM.collideToBigObstacle();
            Debug.Log("플충됨?");
        }
    }
}