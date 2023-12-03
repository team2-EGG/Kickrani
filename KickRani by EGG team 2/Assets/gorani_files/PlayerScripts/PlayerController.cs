using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 ShieldSize = new Vector3(2, 2, 2);
    public bool isShield = false;
    public bool isHelmet = false;
    public bool isStar = false;
    Transform Shield;
    Transform Helmet;
    Material meshMaterial;
    Coroutine colorChangeCoroutine;

    void Start()
    {
        Shield = transform.Find("Shield");
        Helmet = transform.Find("Helmet");

        if (Shield == null)
            Debug.LogError("Shield ������Ʈ�� ã�� �� �����ϴ�.");

        if (Helmet == null)
            Debug.LogError("Helmet ������Ʈ�� ã�� �� �����ϴ�.");

        Transform goraTransform = transform.Find("gora");
        if (goraTransform == null)
        {
            Debug.LogError("gora ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        Transform meshTransform = goraTransform.Find("Mesh");
        if (meshTransform == null)
        {
            Debug.LogError("Mesh ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        Renderer meshRenderer = meshTransform.GetComponent<Renderer>();
        if (meshRenderer != null)
            meshMaterial = meshRenderer.material;
        else
            Debug.LogError("Mesh ������Ʈ�� Renderer ������Ʈ�� �����ϴ�.");
    }

    void Update()
    {
        if (Shield != null)
        {
            Shield.localScale = isShield ? ShieldSize : Vector3.zero;
            Debug.Log(isShield ? "�ǵ� Ȱ��ȭ��" : "�ǵ� ��Ȱ��ȭ��");
        }

        if (Helmet != null)
        {
            Helmet.localScale = isHelmet ? Vector3.one : Vector3.zero;
            Debug.Log(isHelmet ? "��� Ȱ��ȭ��" : "��� ��Ȱ��ȭ��");
        }

        if (isStar && meshMaterial != null && colorChangeCoroutine == null)
        {
            colorChangeCoroutine = StartCoroutine(ChangeColor());
        }
        else if (!isStar && colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
            colorChangeCoroutine = null;
            meshMaterial.color = Color.white; // ������ ������� ����
        }
    }

    IEnumerator ChangeColor()
    {
        Color[] baseColors = new Color[] { Color.red, Color.white, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta};
        Color[] colors = new Color[baseColors.Length];
        for (int i = 0; i < baseColors.Length; i++)
        {
            colors[i] = ReduceBrightness(baseColors[i], 0.4f); // ���� ?% ���� �������� ����
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

    // ���� ���̴� �Լ�
    Color ReduceBrightness(Color color, float brightnessFactor)
    {
        color.r *= brightnessFactor;
        color.g *= brightnessFactor;
        color.b *= brightnessFactor;
        return color;
    }
}