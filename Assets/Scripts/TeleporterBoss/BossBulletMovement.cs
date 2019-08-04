using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{

    public Vector2 direction;
    public float speed = 7;
    int damage = 10;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = direction * speed;
    }

    //wall bouncind
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Border Up" || col.tag == "Border Down" || col.tag == "Border Left" || col.tag == "Border Right")
        {
            Destroy(gameObject);
        }
    }
}
