using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public TetrisTank tank;


    Vector3 offset = new Vector3(0.0f, 20.0f, 0.0f);

    Vector3 targetPosition;
    float targetSize;


    public Transform referenceBottomPosition;

	// Use this for initialization
	void Start () {
        targetPosition = Camera.main.transform.position;
        targetSize = Camera.main.orthographicSize;
	}


    float scaleSize = 1.0f;
	// Update is called once per frame
	void Update () {
        //float w = Input.GetAxis("Mouse ScrollWheel");
        //if (w < 0)
        //{
        //    Camera.main.orthographicSize += scaleSize;
        //}
        //else if (w > 0)
        //{
        //    Camera.main.orthographicSize -= scaleSize;
        //}


        targetSize = (tank.toppestPosition.y) / 2.0f + offset.y;
        targetPosition = (targetSize - 7.0f) * Vector3.up + tank.transform.position ;

        if (Camera.main.orthographicSize < targetSize)
        {
            Camera.main.orthographicSize += Mathf.Max(0.01f, (targetSize - Camera.main.orthographicSize) * 0.3f);
        }

        Vector3 delta = targetPosition - Camera.main.transform.position;
        delta.z = 0.0f;

        if (delta.sqrMagnitude > 0.1f)
        {
            Camera.main.transform.position += 0.3f * delta;
        }
    }
}
