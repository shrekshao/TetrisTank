using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

    public float waitSeconds = 1.0f;
    public GameObject bullet;

    // Use this for initialization
    void Start () {
        StartCoroutine(Fire());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);

            var g = Instantiate(bullet, transform.position, Quaternion.Euler(0.0f, 0.0f, 180.0f));
        }

    }
}
