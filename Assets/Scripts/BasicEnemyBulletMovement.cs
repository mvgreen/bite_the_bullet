using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBulletMovement : MonoBehaviour
{
    int damage = 10;

    Rigidbody2D body;
    EnemyBullet info;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        info = GetComponent<EnemyBullet>();
        body.velocity = info.direction * info.speed;
    }

    void Update()
    {
        if(transform.position.magnitude > 20)
        {
            Destroy(gameObject);
        }
    }
}
