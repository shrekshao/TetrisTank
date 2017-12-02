using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPartGenerator : MonoBehaviour {

    public TetrisPart[] tetrisPartLib;
    public Transform tetrisTank;

    Vector3 spawnLocationOffset = new Vector3(0.0f, 10.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Slash))
        {
            SpawnPart();
        }
	}

    public void SpawnPart()
    {
        var part = tetrisPartLib[Random.Range(0, tetrisPartLib.Length)];

        GameObject.Instantiate(part, tetrisTank.position + spawnLocationOffset, Quaternion.identity);
    }
}
