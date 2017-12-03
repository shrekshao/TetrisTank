using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    Rigidbody2D m_rigidBodyTank;
    //private IEnumerator coroutine;

    private bool applyForce;

    public float spinSpeed = 100.0f;
    public float forceScale = 10.0f;


    public Transform maceCenter;
    private float radius;

    // Use this for initialization
    void Start()
    {
        applyForce = false;

        radius = (maceCenter.position - transform.position).magnitude;
    }


    public void Activate(Rigidbody2D rigidbodyTank)
    {
        //playerAttackLayer = LayerMask.NameToLayer("PlayerAttack");
        m_rigidBodyTank = rigidbodyTank;

        applyForce = true;
        
    }

    public void Deactivate()
    {
        applyForce = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, spinSpeed * Time.deltaTime);
        if (applyForce)
        {
            float w = (Mathf.Deg2Rad * spinSpeed);

            m_rigidBodyTank.AddForceAtPosition(
                (maceCenter.position - transform.position) * w * w * forceScale,
                transform.position,
                ForceMode2D.Force);
            //m_rigidBodyTank.AddTorque();
        }
    }
}
