using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTank : MonoBehaviour {

    Rigidbody2D m_rigidBody;

    float totalWeight = 1.0f;


    public SpriteRenderer refBlockSpriteRenderer;
    float gridSideLength;

	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();

        gridSideLength = refBlockSpriteRenderer.bounds.size.x;

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
            float w = 0.1f;
            totalWeight += w;

            centerOfMass += w * blocks[i].position;

            blocks[i].gameObject.layer = gameObject.layer;
            blocks[i].parent = transform;
        }

        centerOfMass /= totalWeight;
        m_rigidBody.centerOfMass = centerOfMass;
        m_rigidBody.mass = totalWeight;
    }

    public void SnapToGrid(Transform tetrisPart)
    {
        tetrisPart.Rotate(0.0f, 0.0f, transform.rotation.eulerAngles.z);
        tetrisPart.parent = transform;
        Vector3 g = tetrisPart.localPosition / gridSideLength;

        tetrisPart.localPosition = new Vector3(
            Mathf.Floor(g.x + 0.5f) * gridSideLength,
            Mathf.Floor(g.y + 0.5f) * gridSideLength,
            0.0f
        );
    }
}
