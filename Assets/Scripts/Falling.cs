using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {

    float fallingSpeed;
    float movingSpeed = 1.0f;
    

    const float slowFallingSpeed = 1.0f;
    const float fastFallingSpeed = 5.0f;

	// Use this for initialization
	void Start () {
        fallingSpeed = slowFallingSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveX(-movingSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveX(movingSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0.0f, 0.0f, 90.0f);
        }




        if (Input.GetKeyDown(KeyCode.S))
        {
            fallingSpeed = fastFallingSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            fallingSpeed = slowFallingSpeed;
        }
    }
    

    public void MoveX(float s)
    {
        transform.position += Vector3.right * s;
    }

    public void MoveZ(float s)
    {
        transform.position += Vector3.forward * s;
    }
    
}
