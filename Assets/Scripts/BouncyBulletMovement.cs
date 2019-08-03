using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulletMovement : MonoBehaviour
{

    public Vector2 direction;
    public float speed = 7;
    public int damage = 10;

    Rigidbody2D body;
    EnemyBullet info;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        info = GetComponent<EnemyBullet>();
        damage = info.life;
        body.velocity = info.direction * info.speed;
    }

    void Update()
    {
        if(damage < 1)
        {
            Destroy(gameObject);
        }
    }

    //wall bouncind
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Border Up" || col.tag == "Border Down")
        {
            body.velocity = new Vector2(body.velocity.x, -body.velocity.y);
            damage--;
        }
        if(col.tag == "Border Left" || col.tag == "Border Right")
        {
            body.velocity = new Vector2(-body.velocity.x, body.velocity.y);
            damage--;
        }
    }
}
