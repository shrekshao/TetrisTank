using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

    public int damage = 10;


    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else
        {
            // must be enemy
            c.gameObject.SendMessage("ReceiveDamage", damage);

            Destroy(gameObject);
        }
    }
}
