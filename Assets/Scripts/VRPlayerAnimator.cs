using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController; //Reference to XR character controller
    private float speedThreshold = 0.1f; //Threshold to detect movement
    private float runThreshold = 2.5f; //Threshold to switch from walking to running



    // Update is called once per frame
    void Update()
    {
        if (characterController == null) return;

        // Get movement speed
        float speed = characterController.velocity.magnitude;

        // Update animator parameter
        animator.SetFloat("Speed", speed);
    }
}
