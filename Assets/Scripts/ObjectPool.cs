using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private List<GameObject> pooledObjecteds = new List<GameObject>();
    private int amountToPool = 10;
    [SerializeField] private GameObject arrowPrefabs;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(arrowPrefabs);
            pooledObjecteds.Add(obj);
            obj.SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjecteds.Count; i++)
        {
            if (!pooledObjecteds[i].activeInHierarchy)
                return pooledObjecteds[i];
            
        }

        return null;
    }

}