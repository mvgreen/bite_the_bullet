using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    Transform player;
    float scale;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        scale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(scale,scale,scale);
        }
        else
        {
            transform.localScale = new Vector3(-scale,scale,scale);
        }
    }
}
