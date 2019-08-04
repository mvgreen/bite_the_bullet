using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsThePlayer : MonoBehaviour
{
    // iframe prevents the player from being instakilled by his own bullets
    float iframe = 0.5f;
    public GameObject bulletSmoke;

    // Update is called once per frame
    void Update()
    {
        iframe -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && iframe < 0)
        {
            col.GetComponent<PlayerShootingController>().Die();
            Instantiate(bulletSmoke, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
