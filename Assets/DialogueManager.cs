using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // Singleton instance
    public TMP_Text dialogueTextUI; // Reference to the TextMeshPro UI component
    public GameObject dialogueBox; // UI panel to contain the dialogue text

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayDialogue(string dialogueText)
    {
        dialogueBox.SetActive(true);
        dialogueTextUI.text = dialogueText;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueTextUI.text = "";
    }

    private void Update()
    {
        // Hide the dialogue when the player presses a key (e.g., "E" for interact)
        if (Input.GetKeyDown(KeyCode.E) && dialogueBox.activeInHierarchy)
        {
            HideDialogue();
        }
    }
}
