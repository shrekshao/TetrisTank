using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisPartGenerator : MonoBehaviour {

    public TetrisPart[] tetrisPartLib;
    public Transform tetrisTank;

    Vector3 spawnLocationOffset = new Vector3(0.0f, 25.0f, 0.0f);

    public float respawnTime = 5.0f;

    bool ready;

	// Use this for initialization
	void Start () {
        ready = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Slash))
        {
            if (ready)
            {
                SpawnPart();

                ready = false;

                StartCoroutine(RespawnTime());
            }
            
        }
	}

    public void SpawnPart()
    {
        var part = tetrisPartLib[Random.Range(0, tetrisPartLib.Length)];

        GameObject.Instantiate(part, tetrisTank.position + spawnLocationOffset, Quaternion.identity);

        
    }

    IEnumerator RespawnTime()
    {
        yield return new WaitForSeconds(respawnTime);
        ready = true;
    }
}
