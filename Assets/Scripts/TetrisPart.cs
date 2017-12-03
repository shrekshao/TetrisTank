using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPart : MonoBehaviour {

    //Falling m_fallingLogic;

    TetrisBlock[] childBlocks;
    Transform[] childBlocksTransform;

    float fallingSpeed;
    float movingSpeed = 1.0f;


    const float slowFallingSpeed = 1.0f;
    const float fastFallingSpeed = 10.0f;

    // Use this for initialization
    void Start () {
        fallingSpeed = slowFallingSpeed;

        //m_fallingLogic = GetComponent<Falling>();
        assembled = false;

        int numChildren = transform.childCount;
        childBlocks = new TetrisBlock[numChildren];
        childBlocksTransform = new Transform[numChildren];
        for (int i = 0; i < numChildren; i++)
        {
            childBlocksTransform[i] = transform.GetChild(i);
            childBlocks[i] = childBlocksTransform[i].GetComponent<TetrisBlock>();
        }
    }


    bool checkDirection(Vector2 direction, float distance, ref Collider2D collider)
    {
        for (int i = 0; i < childBlocks.Length; i++)
        {
            if (!childBlocks[i].checkIfCanMoveOnDirection(direction, distance, out collider))
            {
                return false;
            }
        }
        return true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            fallingSpeed = fastFallingSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            fallingSpeed = slowFallingSpeed;
        }


        Collider2D collider = null;
        float fallingDistance = fallingSpeed * Time.deltaTime;
        if (checkDirection(Vector2.down, fallingDistance, ref collider))
        {
            transform.position += Vector3.down * fallingDistance;
        }
        else
        {
            // hit tank, assemble
            Assemble(collider.transform.parent.GetComponent<TetrisTank>());
            return;
        }

        collider = null;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            float tmp = movingSpeed * Time.deltaTime;

            if (checkDirection(Vector2.left, tmp, ref collider))
            {
                transform.position += Vector3.left * tmp;
            }
            
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            float tmp = movingSpeed * Time.deltaTime;

            if (checkDirection(Vector2.right, tmp, ref collider))
            {
                transform.position += Vector3.right * tmp;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0.0f, 0.0f, 90.0f);
        }


        
    }


    bool assembled;
    // Called by child collider when hit player tank
    public void Assemble(TetrisTank tank)
    {
        if (!assembled)
        {
            assembled = true;
            //m_fallingLogic.enabled = false;


            // fit to tank grid system
            tank.SnapToGrid(transform);

            
            /////////////////
            var tankRigidBody = tank.gameObject.GetComponent<Rigidbody2D>();

            for (int i = 0; i < childBlocks.Length; i++)
            {
                // activate weapon coroutine
                childBlocks[i].TransformToTankBlock(tankRigidBody);
            }

            tank.UpdateCenterOfMass(childBlocksTransform);

            Destroy(gameObject);
        }

        
    }
}
