using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCSetTriggerOn : MonoBehaviour
{
    public UnityEvent onTrigger;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onTrigger?.Invoke();
        }
    }
}
