using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    
    private void Update()
    {
        //Check if a primary button is pressed on the right-hand controller
        UnityEngine.XR.InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        bool isButtonPressed;

        //Get keyboard input for testing without a VR headset
        Keyboard currentKeyboard = Keyboard.current;
        bool isKeyboardPressed = currentKeyboard != null && currentKeyboard.eKey.wasPressedThisFrame;


        if((rightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out isButtonPressed) && isButtonPressed) || isKeyboardPressed)
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }
            }
        }  
    }
}
