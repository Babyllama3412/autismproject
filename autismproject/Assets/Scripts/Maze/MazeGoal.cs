using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class MazeGoal : MonoBehaviour
{
    public UnityEvent OnWin;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
            OnWin.Invoke();
    }
}
