using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  private float health;
  private float lerpTimer;
  public float maxHealth = 100f;
  public float chipSpeed = 2f;
  public Image frontHealthBar;
  public Image backHealthBar;
    // Start is called before the first frame update
    void Start()
    {
    health = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
     health = Mathf.Clamp(health, 0, maxHealth);
    UpdateHealthUI();
    }

  public void UpdateHealthUI()
  {
    float fillFrontHealthBar = frontHealthBar.fillAmount;
    float fillBackHealthBar = backHealthBar.fillAmount;
    float healthFraction = health / maxHealth;
    if(fillBackHealthBar > healthFraction)
    {
      frontHealthBar.fillAmount = healthFraction;
      backHealthBar.color = Color.red;
      lerpTimer += Time.deltaTime;
      float percentComplete = lerpTimer / chipSpeed;
      backHealthBar.fillAmount = Mathf.Lerp(fillBackHealthBar,healthFraction, percentComplete);
    }
    if (fillFrontHealthBar < healthFraction)
    {
      backHealthBar.color = Color.green;
      backHealthBar.fillAmount = healthFraction;
      lerpTimer += Time.deltaTime;
      float percentComplete = lerpTimer / chipSpeed;
      frontHealthBar.fillAmount = Mathf.Lerp(fillFrontHealthBar, backHealthBar.fillAmount, percentComplete);
      frontHealthBar.fillAmount = healthFraction;
      
    }
  }

  public void TakeDamage(float damage)
  {
    health -= damage;
    lerpTimer = 0f;
  }
  public void RestoreHealth(float healAmount)
  {
    health += healAmount;
    lerpTimer = 0f; 
  }
}
