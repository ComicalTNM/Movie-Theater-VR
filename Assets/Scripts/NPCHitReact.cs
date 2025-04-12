using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHitReact : MonoBehaviour
{
    public DialogueLine[] hitReactionLines;
    public Animator animator;
    public Dialogue dialogue;
    public float reactionCooldown = 2f;
    private float lastReactionTime = -Mathf.Infinity;
    private int reactionIndex = 0;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialogue = GetComponentInChildren<Dialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Softdrink") && Time.time - lastReactionTime >= reactionCooldown)
        {
            lastReactionTime = Time.time;

            if(dialogue != null && hitReactionLines.Length > 0)
            {
                //Clamp index so it doesn't go out of bounds
                int index = reactionIndex % hitReactionLines.Length;

                dialogue.SetNPCAnimator(animator);
                dialogue.DisplayDialogue(new DialogueLine[] { hitReactionLines[index] });

                reactionIndex++; //Escalate each hit
            }
        }
    }
}
