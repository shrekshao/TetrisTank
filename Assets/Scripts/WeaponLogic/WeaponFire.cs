﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour {

    public GameObject bullet;
    public Transform firePosition;

    Rigidbody2D m_rigidBodyTank;

    private IEnumerator coroutine;

    //private int playerAttackLayer;

    public float waitSeconds = 1.0f;
    public float force = 1.0f;

    public void Activate(Rigidbody2D rigidbodyTank)
    {
        //playerAttackLayer = LayerMask.NameToLayer("PlayerAttack");
        m_rigidBodyTank = rigidbodyTank;
        coroutine = Fire();
        StartCoroutine(coroutine);
    }

    public void Deactivate()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);

            m_rigidBodyTank.AddForceAtPosition(-transform.right * force, transform.position, ForceMode2D.Impulse);

            var g = Instantiate(bullet, firePosition.position, transform.rotation);
            //g.layer = playerAttackLayer;
        }

    }
}
