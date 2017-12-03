using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {


    Rigidbody2D m_rigidBodyTank;

    private IEnumerator coroutine;


    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 20.0f * Time.deltaTime);
    }

    public void Activate(Rigidbody2D rigidbodyTank)
    {
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
        while(true)
        {
            yield return new WaitForSeconds(1.0f);

            m_rigidBodyTank.AddForceAtPosition(-transform.up, transform.position, ForceMode2D.Impulse);
        }
       
    }
}
