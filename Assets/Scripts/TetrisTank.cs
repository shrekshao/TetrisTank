using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTank : MonoBehaviour {

    Rigidbody2D m_rigidBody;

    float totalWeight = 0.0f;
    

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();


        Transform[] childBlocks = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childBlocks[i] = transform.GetChild(i);
        }
        UpdateCenterOfMass(childBlocks);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void UpdateCenterOfMass(Transform[] blocks)
    {
        Vector3 centerOfMass = m_rigidBody.centerOfMass;
        for (int i = 0; i < blocks.Length; i++)
        {
            float w = 1.0f;
            totalWeight += w;

            centerOfMass += w * blocks[i].position;

            blocks[i].gameObject.layer = gameObject.layer;
            blocks[i].parent = transform;
        }

        centerOfMass /= totalWeight;
        m_rigidBody.centerOfMass = centerOfMass;
    }
}
