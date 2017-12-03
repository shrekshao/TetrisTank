﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove1 : MonoBehaviour {

    public float speedScale = 5.0f;
    public float verticleSpeedScale = 20.0f;
    public float timeScale = 5.0f;
    Rigidbody2D m_rigidBody;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();
        
    }
	
	// Update is called once per frame
	void Update () {
        m_rigidBody.velocity = new Vector3(-speedScale, verticleSpeedScale * Mathf.Sin(timeScale * Time.time));
	}
}
