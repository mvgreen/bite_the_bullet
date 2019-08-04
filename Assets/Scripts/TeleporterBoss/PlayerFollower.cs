using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position + Vector3.forward;
    }

    public void changeSize(float newSize)
    {
        transform.localScale = Vector3.one * newSize;
    }
}
