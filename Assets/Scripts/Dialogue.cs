using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using TMPro;

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    public string text;
    public string animationTrigger; //Animation to play (optional)
}

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance; //Static reference to the instance of the dialogue class

    public TextMeshPro textComponent;
    public float textSpeed;

    private int index;
    private DialogueLine[] dialogueLines;
    private TextMeshPro activeTextComponent;
    private Animator npcAnimator;

    //Reference to the TextMeshPro components on the Player and NPC
    public TextMeshPro playerText;
    public TextMeshPro npcText;

    private void Awake()
    {
        //Ensuring there is only one instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initially disable both text objects
        playerText.gameObject.SetActive(false);
        npcText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Get VR Controller Trigger Input
        UnityEngine.XR.InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool isTriggerPressed;
        bool isMousePressed = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame; //New input system for Mouse
        
        if ((rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out isTriggerPressed) && isTriggerPressed) || isMousePressed)
        {
            if (activeTextComponent.text == dialogueLines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                activeTextComponent.text = dialogueLines[index].text;
            }
        }
    }

    public void DisplayDialogue(DialogueLine[] lines)
    {
        StartDialogue(lines);
    }

    void StartDialogue(DialogueLine[] lines)
    {
        index = 0;
        dialogueLines = lines; //Sets the dialogueLines variable to the parameter dialogue lines given
        ShowLine();
    }

    void ShowLine()
    {
        //Determine who the speaker is and enable the corresponding TextMeshPro object
        if (dialogueLines[index].speakerName == "Player")
        {
            playerText.gameObject.SetActive(true);
            npcText.gameObject.SetActive(false);
            activeTextComponent = playerText;
        }
        else
        {
            playerText.gameObject.SetActive(false);
            npcText.gameObject.SetActive(true);
            activeTextComponent = npcText;

            //Check if there's an animation for the line and play it
            if (npcAnimator != null && !string.IsNullOrEmpty(dialogueLines[index].animationTrigger))
            {
                npcAnimator.SetTrigger(dialogueLines[index].animationTrigger);
            }
        }

        activeTextComponent.text = "";
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //Type each character 1 by 1 in the dialogue box
        foreach (char c in dialogueLines[index].text.ToCharArray())
        {
            activeTextComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            playerText.gameObject.SetActive(false);
            npcText.gameObject.SetActive(false);
        }
    }

    public void SetNPCAnimator(Animator animator)
    {
        npcAnimator = animator;
    }
}

