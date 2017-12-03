using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float originSpeed = 30.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = transform.right * originSpeed;
	}
	
}
