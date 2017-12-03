using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour {

    public enum BlockType { Falling = 0, Tank };

    public BlockType blockType = BlockType.Falling;

    TetrisPart parentPart;

    GameObject childWeapon = null;

    int moveLayerMask;

	// Use this for initialization
	void Start () {
        parentPart = transform.parent.GetComponent<TetrisPart>();

        if (transform.childCount > 0)
        {
            childWeapon = transform.GetChild(0).gameObject;
        }


        moveLayerMask = LayerMask.GetMask("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnCollisionEnter(Collision c)
    //{

    //}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (blockType == BlockType.Falling)
        {
            Debug.Log("Falls on Tank");

            //TetrisBlock hitTankBlock = c.gameObject.GetComponent<TetrisBlock>();

            if (parentPart != null)
            {
                parentPart.Assemble(
                    c.gameObject.transform.parent.GetComponent<TetrisTank>()
                );
            }
            
            //transform.parent.SendMessage("Assemble");
        }
        
    }

    public void TransformToTankBlock(Rigidbody2D tankRigidBody)
    {
        blockType = BlockType.Tank;

        var collider = GetComponent<Collider2D>();
        collider.enabled = true;
        collider.isTrigger = false;
        
        if (childWeapon != null)
        {
            childWeapon.SendMessage("Activate", tankRigidBody);
        }
    }

    public bool checkIfCanMoveOnDirection(Vector2 direction, float distance, out Collider2D collider)
    {
        // assume size x = y
        collider = Physics2D.Raycast(transform.position, direction, distance + GetComponent<SpriteRenderer>().size.x / 2.0f, moveLayerMask).collider;
        return (collider == null); 
    }
}
