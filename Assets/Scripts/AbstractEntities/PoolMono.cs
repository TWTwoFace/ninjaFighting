using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab;
    public bool autoExpand { get; set; }
    public Transform container { get; set; }

    private List<T> pool;
    
    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        CreatePool(count);
    }
    
    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (mono.gameObject.activeInHierarchy == false)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
        {
            return element;
        }
        else
        {
            return CreateObject(true);
        }
    }
}
