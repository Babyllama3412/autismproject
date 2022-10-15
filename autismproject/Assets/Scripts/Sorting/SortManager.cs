using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SortManager : MonoBehaviour
{
    public UnityEvent OnWin;
    public List<SortStorage> storages = new List<SortStorage>();
    public List<SortDragger> allObjects = new List<SortDragger>();

    bool hasWon;

    void Update()
    {
        if(!hasWon)
        {
            if(storages.All(x=>x.sortedProperly) && allObjects.All(x=>x.isInStorage))
            {
                hasWon = true;
                OnWin.Invoke();
            }
        }   
    }
}
