using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPart : MonoBehaviour {

    Falling m_fallingLogic;
    


	// Use this for initialization
	void Start () {
        m_fallingLogic = GetComponent<Falling>();
        assembled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    bool assembled;
    // Called by child collider when hit player tank
    public void Assemble(TetrisTank tank)
    {
        if (!assembled)
        {
            assembled = true;
            m_fallingLogic.enabled = false;

            // activate weapon coroutine

            var tankRigidBody = tank.gameObject.GetComponent<Rigidbody2D>();

            int numChildren = transform.childCount;
            Transform[] childBlocks = new Transform[numChildren];
            for (int i = 0; i < numChildren; i++)
            {
                
                childBlocks[i] = transform.GetChild(i);
                //childBlocks[i].SendMessage("TransformToTankBlock");
                childBlocks[i].GetComponent<TetrisBlock>().TransformToTankBlock(tankRigidBody);
            }

            tank.UpdateCenterOfMass(childBlocks);

            Destroy(gameObject);
        }

        
    }
}
