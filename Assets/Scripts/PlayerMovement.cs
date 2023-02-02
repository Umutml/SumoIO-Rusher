using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5;
    
    private Animator playerAnimator;
    private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    private bool canMove;
    
    [SerializeField] private FloatingJoystick floatingJoystick;
   
    
    private void OnEnable()
    {
        EventManager.OnGameStartedEvent += PlayRunAnim;

    }

    private void OnDisable()
    {
        EventManager.OnGameStartedEvent -= PlayRunAnim;
    }


    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
       if (floatingJoystick.Horizontal != 0)
       {
           moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
           transform.forward = moveDirection;
       }

       if (canMove)
       {
           playerRigidbody.velocity = transform.forward * moveSpeed;
       }
    }

    private void PlayRunAnim()
    {
        playerAnimator.SetBool("isRunning",true);
        canMove = true;
    }
    

    
}
