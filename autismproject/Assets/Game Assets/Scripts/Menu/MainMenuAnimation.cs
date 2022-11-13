using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public EasyTransitionAlpha[] playGameText;
    public EasyTransitionAlpha[] menuButtonText;
    public Transform topA;
    public Transform bottomA;
    public Transform topAEnd;
    public Transform bottomAEnd;
    public float transitionSpeed;
    public float moveSpeed;

    bool moveMenuText;
    public void StartAnimation()
    {
        StartCoroutine(Anim());
    }

    void Update()
    {
        if(moveMenuText)
        {
            topA.transform.position = Vector3.MoveTowards(
            topA.transform.position, topAEnd.position, moveSpeed * Time.deltaTime
            );
            bottomA.transform.position = Vector3.MoveTowards(
                bottomA.transform.position, bottomAEnd.position, moveSpeed/2 * Time.deltaTime
            );
        }
    }

    IEnumerator Anim()
    {
        foreach(EasyTransitionAlpha et in playGameText)
        {
            et.allowChange = true;
            et.ToggleAlpha();
        }

        yield return new WaitForSeconds(transitionSpeed);
        
        moveMenuText = true;

        yield return new WaitForSeconds(transitionSpeed);

        foreach(EasyTransitionAlpha et in menuButtonText)
        {
            et.allowChange = true;
        }
    }
}
