using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortDragger : MonoBehaviour
{
    public string objectCategory;
    public bool allowDrag = true;
    public bool isInStorage;
    bool overSprite;

    Vector3 pos;
    
    void Update()
    {
        if(overSprite && allowDrag)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        if(overSprite)
            overSprite = false;
    }

    void OnMouseDrag()
    {
        overSprite = true;
    }

    public void AllowDrag(bool allow)
    {
        allowDrag = allow;
    }
}
