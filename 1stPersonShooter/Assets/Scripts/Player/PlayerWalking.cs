using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
  private InputManager inputManager;

  private AudioSource walking;

  // Start is called before the first frame update
  void Start()
    {
    inputManager = GetComponent<InputManager>();
    walking = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputManager.onFoot.Movement.triggered)
         walking.Stop();

        else
         walking.Play();
       
    }
}
