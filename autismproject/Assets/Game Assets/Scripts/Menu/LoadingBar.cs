using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadingBar : MonoBehaviour
{
    [Required] public GameObject loadingBar;
    [Required] public Animation animComp;
    [Required] public CanvasGroup canvasGroup;
    public float disappearSpeed = 2;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        loadingBar.transform.localScale = Vector3.one;
        canvasGroup.alpha = 1;
        loadingBar.SetActive(true);
    }

    void Update()
    {
        if(gameManager.readyToLoad)
        {
            animComp.Stop();
            GetComponent<Image>().fillAmount = 1;
            loadingBar.transform.localScale = 
            Vector3.MoveTowards(loadingBar.transform.localScale, Vector3.zero, disappearSpeed * Time.deltaTime);
        }
        if(transform.localScale == Vector3.zero)
        {
            gameManager.LoadReadyScene();
        }
        if(gameManager.sceneLoadAllow)
        {
            if(canvasGroup.alpha > 0)
                canvasGroup.alpha -= 1 * Time.deltaTime;
        }
    }
}
