using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitDamage : MonoBehaviour {

    public int damage = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnColliderEnter2D(Collision2D c)
    //{
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {
        }
        else
        {
            // must be enemy
            c.gameObject.SendMessage("ReceiveDamage", damage);
            
        }
    }
}
