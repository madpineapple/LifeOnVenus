using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
  private InputManager inputManager;
  public static bool gameIsPaused;

  private void Start()
  {
    inputManager = GetComponent<InputManager>();
  }
  void Update()
    {
    if (inputManager.onFoot.Pause.triggered)
    {
      gameIsPaused = !gameIsPaused;
      PauseGame();

    }
  }
  void PauseGame()
  {
    if (gameIsPaused)
    {
      Time.timeScale = 0f;
    }
    else
    {
      Time.timeScale = 1;
    }
  }
}
