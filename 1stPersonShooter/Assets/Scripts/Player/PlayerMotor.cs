using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMotor : MonoBehaviour
{
  private CharacterController characterController;
  private Vector3 playerVelocity;
  private bool isGrounded;

  public float speed = 5f;
  public float gravity = -9.8f;
  public float jumpHeight = 3f;

  public bool lerpCrouch = false;
  public bool crouching = false;
  public bool sprinting = false;
  public float crouchTimer = -1;

  private AudioSource walking;
  public bool isWalking = false;
  // Start is called before the first frame update
  void Start()
    {
       characterController =  GetComponent<CharacterController>();
       walking = GetComponent<AudioSource>();
  }

    // Update is called once per frame
    void Update()
    {
     isGrounded = characterController.isGrounded;
    if (lerpCrouch)
    {
      crouchTimer += Time.deltaTime;
      float p = crouchTimer / 1;
      p *= p;
      if(crouching)
        characterController.height = Mathf.Lerp(characterController.height, 1, p);
      else
        characterController.height = Mathf.Lerp(characterController.height, 2, p);

      if (p > 1)
      {
        lerpCrouch = false;
        crouchTimer = 0f;
      }
    
    }

  }
  //receive the inputs for our InputManager.cs and apply them to our character controller
  public void ProcessMove(Vector2 input)
  {
    Vector3 moveDirection = Vector3.zero;
    moveDirection.x = input.x;
    moveDirection.z = input.y;
    characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
    playerVelocity.y += gravity * Time.deltaTime;


    if (isGrounded && playerVelocity.y < 0)
      playerVelocity.y = -2f;

    characterController.Move(playerVelocity * Time.deltaTime);
  }

  public void Jump()
  {
    if (isGrounded)
    {
      playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.5f * gravity);
    }
  }

  public void Crouch()
  {
    crouching = !crouching;
    crouchTimer = 0;
    lerpCrouch = true;
  }

  public void Sprint()
  {
    sprinting = !sprinting;
    if (sprinting)
      speed = 8;
    else
      speed = 5;
  }

  public void WalkingNoise()
  {
    walking.Play();
  }

  public void NotWalking()
  {
    walking.Stop();
  }

}
