using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get 
        { 
            if(_instance is null)
            {
                _instance = FindObjectOfType<GameManager>();
                if(_instance is null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).ToString());
                    _instance = singleton.AddComponent<GameManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    public GameObject PrefabHitText;
    public void Awake()
    {
        if(_instance is null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
