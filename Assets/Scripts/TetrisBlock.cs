using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridDirection { Down = 0, Up, Left, Right };

public class TetrisBlock : MonoBehaviour {

    public enum BlockType { Falling = 0, Tank, Broken };

    public BlockType blockType = BlockType.Falling;

    public float weight = 0.1f;

    public int hp = 10;

    TetrisPart parentPart;

    GameObject childWeapon = null;

    int moveLayerMask;

    float halfBlockSideLength;

    public bool breakable = true;


    // store neighbor linked and connected to the tank base
    
    public TetrisBlock[] parentBlocks;
    public TetrisBlock[] childBlocks;
    public int gridCoordX;
    public int gridCoordY;

    public TetrisTank tank = null;

    // Use this for initialization
    void Start () {

        parentPart = transform.parent.GetComponent<TetrisPart>();

        halfBlockSideLength = GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;

        if (transform.childCount > 0)
        {
            childWeapon = transform.GetChild(0).gameObject;
        }


        moveLayerMask = LayerMask.GetMask("Player");
        //moveLayerMask = LayerMask.NameToLayer("Player");

        parentBlocks = new TetrisBlock[4];
        childBlocks = new TetrisBlock[4];
    }


    // for debug
    const float debugLineLength = 1.28f / 2.0f;
    Vector3[] direction2Offset = new Vector3[4]
    {
        new Vector3(0.0f, -debugLineLength, 0.0f),
        new Vector3(0.0f, debugLineLength, 0.0f),
        new Vector3(-debugLineLength, 0.0f, 0.0f),
        new Vector3(debugLineLength, 0.0f, 0.0f)
    };


    // Update is called once per frame
    void Update () {


        //// debug
        //for (int i = 0; i < parentBlocks.Length; i++)
        //{
        //    if (parentBlocks[i] != null)
        //    {
        //        Debug.DrawRay(transform.position - new Vector3(0.64f, 0.0f, 0.0f), direction2Offset[i], Color.red);
        //    }

        //    if (childBlocks[i] != null)
        //    {
        //        Debug.DrawRay(transform.position + new Vector3(0.64f, 0.0f, 0.0f), direction2Offset[i], Color.blue);
        //    }
        //}

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Ground"))
        {

            if (blockType == BlockType.Broken)
            {
                Destroy(gameObject);
            }
            else if (blockType == BlockType.Falling)
            {
                Destroy(transform.parent.gameObject);
            }
            else if (blockType == BlockType.Tank)
            {
                BreakBlock();
            }
            
        }
        
        
    }

    //void OnTriggerEnter2D(Collider2D c)
    //{
    //    if (blockType == BlockType.Falling)
    //    {
    //        //Debug.Log("Falls on Tank");

    //        ////TetrisBlock hitTankBlock = c.gameObject.GetComponent<TetrisBlock>();

    //        //if (parentPart != null)
    //        //{
    //        //    parentPart.Assemble(
    //        //        c.gameObject.transform.parent.GetComponent<TetrisTank>()
    //        //    );
    //        //}

    //        ////transform.parent.SendMessage("Assemble");

    //        if (c.gameObject.CompareTag("Ground"))
    //        {
    //            Destroy(transform.parent.gameObject);
    //        }
    //    }

    //}

    public void TransformToTankBlock(Rigidbody2D tankRigidBody)
    {
        gameObject.layer = LayerMask.NameToLayer("Player");

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
        collider = Physics2D.Raycast(transform.position, direction, distance + halfBlockSideLength, moveLayerMask).collider;
        return (collider == null); 
    }



    public void CheckIfConnected()
    {
        for (int i = 0; i < parentBlocks.Length; i++)
        {
            if (parentBlocks[i] != null)
            {
                return;
            }
        }

        // no parents exists
        BreakBlock();
    }


    int getOppositeDirectionId(int dir)
    {
        if (dir == 0 || dir == 2)
        {
            return dir + 1;
        }
        else
        {
            return dir - 1;
        }
    }

    public void BreakBlock()
    {
        if (!breakable)
        {
            return;
        }

        for (int i = 0; i < parentBlocks.Length; i++)
        {
            if (parentBlocks[i] != null)
            {
                parentBlocks[i].childBlocks[getOppositeDirectionId(i)] = null;
            }
        }


        blockType = BlockType.Broken;


        if (childWeapon != null)
        {
            childWeapon.SendMessage("Deactivate");
        }


        var collider = GetComponent<Collider2D>();
        collider.enabled = true;
        collider.isTrigger = false;

        Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D>();
        gameObject.layer = LayerMask.NameToLayer("Broken");

        

        tank.DisConnectBlock(this);

        transform.parent = null;

        for (int i = 0; i < childBlocks.Length; i++)
        {
            if (childBlocks[i] != null)
            {
                childBlocks[i].parentBlocks[getOppositeDirectionId(i)] = null;
                childBlocks[i].CheckIfConnected();
            }
        }


        rigidBody.velocity = new Vector3(
            //2.0f * (Random.value - 0.5f), 
            Mathf.Sign(transform.localPosition.x) * Mathf.Clamp(Mathf.Abs(transform.localPosition.x), 3.0f, 10.0f) * Random.value,
            5.0f * Random.value , 
            0.0f); 
        
        
    }


    public void ReceiveDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            BreakBlock();
        }
    }


}
