using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject pause;
    public string dialogueText; // Text to display when interacting with the item

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowDialogue();
            Time.timeScale = 0;
            pause.SetActive(false);
        }
    }

    private void ShowDialogue()
    {
        DialogueManager.Instance.DisplayDialogue(dialogueText);
    }
}