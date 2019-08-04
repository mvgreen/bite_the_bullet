using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += new Vector3(0,0, speed*Time.deltaTime);
        transform.GetChild(0).localEulerAngles = -transform.localEulerAngles;
    }
}
