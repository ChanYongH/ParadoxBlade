using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager<T> : MonoBehaviour where T: GameManager<T>
{
    static public T instance;

    private void Awake()
    {
        if (instance == null)
            instance = (T)this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
}
