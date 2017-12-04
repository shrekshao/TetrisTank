using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadScene();
        }
	}

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }


    //public void GotoMainScene()
    //{
    //    Scene
    //}

    //public void GotoMenuScene()
    //{

    //}
}
