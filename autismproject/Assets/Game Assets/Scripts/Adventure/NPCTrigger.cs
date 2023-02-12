using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] GameObject npcCam;
    [SerializeField] GameObject boundary;
    [SerializeField] List<string> stringDialogue = new List<string>();
    [SerializeField] GameObject winPanel;
    public bool end;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            npcCam.SetActive(true);
            DialoguePanel.Instance.stringDialogue = stringDialogue;
            DialoguePanel.Instance.StartDialogue();

            if(end)
                winPanel.SetActive(true);
        }
    }

    
}
