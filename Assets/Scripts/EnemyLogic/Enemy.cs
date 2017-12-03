using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int hp = 10;

    private Rigidbody2D m_rigidBody;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            gameObject.layer = LayerMask.NameToLayer("Broken");

            m_rigidBody.gravityScale = 10.0f;

            m_rigidBody.angularVelocity = Random.value * 40.0f;

            m_rigidBody.velocity = new Vector3(
            10.0f * 2.0f * (Random.value - 0.5f), 
            //40.0f * Random.value,
            30.0f,
            0.0f);

            Destroy(GetComponent<EnemyMove1>());
            Destroy(GetComponent<EnemyFire>());
        }
    }


    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }


    }
}
