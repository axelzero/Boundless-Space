﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public bool dontDestroy = false;

    static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();

                if (m_instance == null)
                {
                    GameObject singletone = new GameObject(typeof(T).Name);
                    m_instance = singletone.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            if (dontDestroy)
            {
                transform.parent = null;
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}