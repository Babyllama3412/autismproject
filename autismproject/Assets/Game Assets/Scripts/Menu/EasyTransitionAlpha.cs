using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTransitionAlpha : MonoBehaviour
{
    public CanvasGroup group;
    public float speed;
    public bool alphaOut;
    public bool allowChange;
    
    void Start()
    {
        group.alpha = alphaOut? 1 : 0;
    }

    void Update()
    {
        if(allowChange)
        {
            if(alphaOut)
            {
                group.alpha = Mathf.MoveTowards(group.alpha, 0, speed * Time.deltaTime);
            } 
            else
            {
                group.alpha = Mathf.MoveTowards(group.alpha, 1, speed * Time.deltaTime);
            }
        }
    }

    public void ToggleAlpha() 
    {
        alphaOut = !alphaOut;
    }
}
