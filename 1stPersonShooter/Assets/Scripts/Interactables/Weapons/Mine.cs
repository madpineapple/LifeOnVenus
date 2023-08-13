using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
  public int damage;

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag != "floor")
    {
      Debug.Log(collision.gameObject.name);

      Destroy(this.gameObject);
    }
    if(collision.gameObject.tag != "floor" && collision.gameObject.GetComponent<Health>()!=null)
    {
      Debug.Log("BOOM");
      collision.gameObject.GetComponent<Health>().TakeDamage(damage);
      Destroy(this.gameObject);
    }

  }
}
