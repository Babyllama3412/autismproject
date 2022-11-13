using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Level Loader")][SerializeField][Required] GameObject loadingCanvas;
    [BoxGroup("Level Loader")][SerializeField] int delayStart;
    [BoxGroup("Level Loader")][SerializeField] bool loadAtStart;
    [BoxGroup("Level Loader")][SerializeField][Scene][ShowIf("LoadAtStart")] int sceneToLoad;
    
    [BoxGroup("Debug")][ReadOnly] public bool readyToLoad;
    [BoxGroup("Debug")][ReadOnly] public bool sceneLoadAllow;
    public static GameManager Instance;

    public AsyncOperation scene;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance != null) Destroy(Instance.gameObject);
        else Instance = this;
    }

    void Start()
    {
        loadingCanvas.SetActive(false);
        sceneLoadAllow = false;

        if(loadAtStart)
            LoadScene(sceneToLoad);
    }

    // LEVEL LOADER
    public async void LoadScene(int index)
    {
        loadingCanvas.SetActive(true);
        scene = SceneManager.LoadSceneAsync(index);

        readyToLoad = false;
        scene.allowSceneActivation = false;

        do 
        {
            await Task.Delay(2000);
        } while (scene.progress < 0.9f);
        
        readyToLoad = true;
    }
    public async void LoadReadyScene()
    {
        if(readyToLoad)
        {
            await Task.Delay(delayStart * 1000);
            scene.allowSceneActivation = true;
            sceneLoadAllow = true;
        }
    }
    bool LoadAtStart() => loadAtStart;
}
