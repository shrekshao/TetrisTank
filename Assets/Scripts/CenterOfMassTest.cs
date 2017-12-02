using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float totalWeight = 0.0f;
        Vector3 centerOfMass = new Vector3(0.0f, 0.0f, 0.0f);
		foreach(Transform child in transform)
        {
            float w = 1.0f;
            totalWeight += w;

            centerOfMass += w * child.position;
        }

        centerOfMass /= totalWeight;
        GetComponent<Rigidbody2D>().centerOfMass = centerOfMass;
        Debug.Log(centerOfMass);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
