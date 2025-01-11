using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance;
    public static SceneController Instance
    {
        get
        {
            if(_instance is null)
            {
                _instance = FindObjectOfType<SceneController>();
                if(_instance is null)
                {
                    GameObject singleton = new GameObject(typeof(SceneController).ToString());
                    _instance = singleton.AddComponent<SceneController>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
    private static SceneManager _sceneManager = new SceneManager();

    private List<Scene> AllScene;

    public void Awake()
    {
        if (_instance is null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(int numberScene)
    {
        SceneManager.LoadSceneAsync(numberScene, LoadSceneMode.Additive);

        foreach (Scene scene in AllScene)
            if(!scene.Equals(AllScene[numberScene])) SceneManager.UnloadSceneAsync(scene);
    }

}
