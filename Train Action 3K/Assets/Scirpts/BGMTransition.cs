using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTransition : MonoBehaviour
{
    private static BGMTransition instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
