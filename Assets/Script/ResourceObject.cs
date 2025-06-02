using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public int maxHp = 5;

    public GameObject dropItemPrefab;

    public int dropAmount = 1;

    private int currenHp;

    private void Awake()
    {
        currenHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        currenHp -= damage;

        if (currenHp <= 0)
        {
            DestroyResource();
        }
    }

    private void DestroyResource()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Instantiate(dropItemPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
