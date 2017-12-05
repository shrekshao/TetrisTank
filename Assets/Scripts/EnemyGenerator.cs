using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public Enemy[] enemyLib;
    public Transform spawnHighest;
    public Transform spawnLowest;

    public Transform enemyRoot;

    public float waitSeconds = 10.0f;
    public float minWaitSeconds = 3.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            waitSeconds = Mathf.Max(minWaitSeconds, waitSeconds - 0.1f);
            var e = enemyLib[Random.Range(0, enemyLib.Length)];
            var g = Instantiate(e, 
                Vector3.Lerp(spawnLowest.position, spawnHighest.position, Random.value), 
                Quaternion.identity);

            g.transform.parent = enemyRoot;
        }

    }
}
