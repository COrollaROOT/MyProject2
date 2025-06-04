using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;


public enum ResourceType { Tree, Rock } // 자원 종류
public class ResourceObject : MonoBehaviour
{
    public ResourceType resourceType;

    public int maxHp = 5;

    public GameObject dropItemPrefab;

    public int dropAmount = 1;

    private int currenHp;

    public GameObject hpBarPrefab;
    private GameObject hpBarUI;
    private Image fillImage;

    private void Awake()
    {
        currenHp = maxHp;

        var canvas = GameObject.Find("HPBarCanvas");

        if (canvas != null && hpBarPrefab != null)
        {
            hpBarUI = Instantiate(hpBarPrefab, canvas.transform);
            fillImage = hpBarUI.transform.Find("Bar").GetComponent<Image>();
        }
    }

    private void LateUpdate()
    {
        if (hpBarUI != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
            hpBarUI.transform.position = screenPos;
        }
    }

    public void TakeDamage(int damage)
    {
        currenHp -= damage;

        UpdateHPBar();

        if (currenHp <= 0)
        {
            DestroyResource();
        }
    }

    private void UpdateHPBar()
    {
        if (fillImage != null)
        {
            float ratio = Mathf.Clamp01((float)currenHp / maxHp);
            fillImage.fillAmount = ratio;
        }
    }

    private void DestroyResource()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Instantiate(dropItemPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        if (hpBarUI != null)
        {
            Destroy(hpBarUI);
        }

        Destroy(gameObject);
    }
}
