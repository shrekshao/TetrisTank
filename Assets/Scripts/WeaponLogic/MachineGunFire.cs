using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunFire : MonoBehaviour {

    public GameObject bullet;
    public Transform firePosition;

    Rigidbody2D m_rigidBodyTank;

    private IEnumerator coroutine;

    public float force = 1.0f;
    public float waitSeconds = 2.0f;
    public float subWaitSeconds = 0.2f;
    public int numPerRound = 10;

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

            StartCoroutine(SubFire());
            //g.layer = playerAttackLayer;
        }

    }

    IEnumerator SubFire()
    {
        for (int i = 0; i < numPerRound; i++)
        {
            yield return new WaitForSeconds(subWaitSeconds);

            m_rigidBodyTank.AddForceAtPosition(-transform.right * force, transform.position, ForceMode2D.Impulse);

            var g = Instantiate(bullet, firePosition.position, transform.rotation);
            //g.layer = playerAttackLayer;
        }

    }
}
