using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject panel;
    [SerializeField] GameObject doneButton;
    [SerializeField] GameObject previousButton;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject playerCam;

    public bool dialogeActive;

    public List<string> stringDialogue = new List<string>();
    public int currentIndex;

    public static DialoguePanel Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentIndex = 0;
        panel.SetActive(false);
    }

    void Update()
    {
        if(currentIndex <= stringDialogue.Count - 1 && currentIndex >= 0 && stringDialogue.Count != 0)
        {
            dialogueText.text = stringDialogue[currentIndex];
        } else dialogueText.text = "";

        doneButton.SetActive(stringDialogue.Count != 0 && currentIndex == stringDialogue.Count - 1);
        previousButton.SetActive(stringDialogue.Count != 0 && currentIndex > 0);
        nextButton.SetActive(stringDialogue.Count != 0 && currentIndex <= stringDialogue.Count - 2);
    }

    public void StartDialogue() 
    { 
        dialogeActive = true;
        panel.SetActive(true);
        playerCam.SetActive(false);
        FirstPersonController.Instance.allowLook = false;
        FirstPersonController.Instance.allowMove = false;
    }

    public void PerformScroll(int amount)
    {
        currentIndex = Mathf.Clamp(currentIndex+amount, 0, stringDialogue.Count - 1);
    }

    public void FinishTalk()
    {
        dialogeActive = false;
        playerCam.SetActive(true);
        FirstPersonController.Instance.allowLook = true;
        FirstPersonController.Instance.allowMove = true;

        panel.SetActive(false);
    }

    public void ResetCurrentIndex()
    {
        currentIndex = 0;
    }
}