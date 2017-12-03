using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpin : MonoBehaviour {

    public float spinSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, spinSpeed * Time.deltaTime);
    }
}
