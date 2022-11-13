using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicTest : MonoBehaviour
{
    public bool canTryAgain;
    
    void Start()
    {
        canTryAgain = false;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canTryAgain)
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetCanTryAgain()
    {
        canTryAgain = true;
    }
}
