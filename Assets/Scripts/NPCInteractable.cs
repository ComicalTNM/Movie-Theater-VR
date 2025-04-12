using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private DialogueLine[] conversation;
    [SerializeField] private string interactText;
    private Animator animator;
    private Dialogue dialogue;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialogue = GetComponentInChildren<Dialogue>();
    }

    public void Interact()
    {

        if (dialogue != null)
        {
            dialogue.SetNPCAnimator(animator);
            dialogue.DisplayDialogue(conversation);
        }

    }
}
