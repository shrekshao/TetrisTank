using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTank : MonoBehaviour {

    Rigidbody2D m_rigidBody;

    float totalWeight = 1.0f;


    public SpriteRenderer refBlockSpriteRenderer;
    float gridSideLength;


    const int GRID_SIZE_X = 51;
    const int GRID_SIZE_Y = 30;

    const int GRID_ORIGIN_X = (GRID_SIZE_X + 1) / 2;

    TetrisBlock[,] grid2Block;

    Vector3 toppestPosition;    // used for camera zooming


    int[,] direction2Offset = new int[4, 2]
    {
        {0, -1},
        {0, 1},
        {-1, 0},
        {1, 0}
    };


	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();

        grid2Block = new TetrisBlock[GRID_SIZE_X, GRID_SIZE_Y];

        gridSideLength = refBlockSpriteRenderer.bounds.size.x;

        toppestPosition = new Vector3(0.0f, Mathf.NegativeInfinity, 0.0f);

        TetrisBlock[] childBlocks = new TetrisBlock[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childBlocks[i] = transform.GetChild(i).GetComponent<TetrisBlock>();
            assignToGridSystem(childBlocks[i]);
        }
        UpdateCenterOfMass(childBlocks);
    }
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(1);

            // for debugging

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(pos);

            Vector2 coord = getGridCoord(pos - transform.position);


            int x = Mathf.FloorToInt(coord.x + 0.1f) + GRID_ORIGIN_X;   // already on grid, + 0.1f for safety concern
            int y = Mathf.FloorToInt(coord.y + 0.1f);   // already on grid, + 0.1f for safety concern

            Debug.Log("grid x, y: " + x + ", " + y);

            if (grid2Block[x, y] != null)
            {
                grid2Block[x, y].BreakBlock();
            }
        }
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


    public void UpdateCenterOfMass(TetrisBlock[] blocks)
    {
        Vector3 centerOfMass = m_rigidBody.centerOfMass;
        for (int i = 0; i < blocks.Length; i++)
        {
            float w = blocks[i].weight;
            totalWeight += w;

            centerOfMass += w * blocks[i].transform.position;

            blocks[i].gameObject.layer = gameObject.layer;
            blocks[i].transform.parent = transform;
            blocks[i].tank = this;
        }

        centerOfMass /= totalWeight;
        m_rigidBody.centerOfMass = centerOfMass;
        m_rigidBody.mass = totalWeight;
    }

    //struct SortHelper{
    //    int id;
    //    int distToConnectedBlock;
    //};


    public void BuildConnection(TetrisBlock[] blocks, int connectBlockId)
    {
        TetrisBlock connectedBlock = blocks[connectBlockId];

        assignToGridSystem(connectedBlock);
        connectToTankBase(connectedBlock);

        int[] distToConnectedBlock = new int[blocks.Length];
        int[] ids = new int[blocks.Length];
        for (int i = 0; i < distToConnectedBlock.Length; i++)
        {
            ids[i] = i;
            if (i == connectBlockId)
            {
                distToConnectedBlock[i] = 0;
            }
            else
            {
                assignToGridSystem(blocks[i]);
                distToConnectedBlock[i] = 
                    System.Math.Abs(blocks[i].gridCoordX - connectedBlock.gridCoordX) 
                    + System.Math.Abs(blocks[i].gridCoordY - connectedBlock.gridCoordY);
            }
        }

        System.Array.Sort(distToConnectedBlock, ids);


        // 0 is connectedBlock;
        for (int i = 1; i < ids.Length; i++)
        {
            TetrisBlock b = blocks[ ids[i] ];
            connectToTankBase(b);
        }
    }


    Vector2 getGridCoord(Vector3 p)
    {
        Vector3 g = p / gridSideLength;

        return new Vector2(
            Mathf.Floor(g.x + 0.5f),
            Mathf.Floor(g.y + 0.5f)
            );
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


    void assignToGridSystem(TetrisBlock b)
    {
        toppestPosition.y = Mathf.Max(toppestPosition.y, b.transform.localPosition.y);

        Vector2 coord = getGridCoord(b.transform.localPosition) ;

        int x = Mathf.FloorToInt(coord.x + 0.1f) + GRID_ORIGIN_X;   // already on grid, + 0.1f for safety concern
        int y = Mathf.FloorToInt(coord.y + 0.1f);   // already on grid, + 0.1f for safety concern

        grid2Block[x, y] = b;

        b.gridCoordX = x;
        b.gridCoordY = y;
    }


    void connectToTankBase(TetrisBlock b)
    {

        //if (b.blockType != TetrisBlock.BlockType.Tank)
        //{
            
            b.blockType = TetrisBlock.BlockType.Tank;
            for (int dir = 0; dir < 4; dir++)
            {
                TetrisBlock neighbor = grid2Block[
                    direction2Offset[dir, 0] + b.gridCoordX,
                    direction2Offset[dir, 1] + b.gridCoordY
                    ];

                if (neighbor != null)
                {
                    if (neighbor.blockType == TetrisBlock.BlockType.Tank)
                    {
                        neighbor.childBlocks[getOppositeDirectionId(dir)] = b;
                        b.parentBlocks[dir] = neighbor;
                    }
                }
            }
        //}
        
    }


    public void DisConnectBlock(TetrisBlock b)
    {
        grid2Block[b.gridCoordX, b.gridCoordY] = null;
    }
}
