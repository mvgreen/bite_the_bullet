using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{

    bool isCharged = true;
    public Color colorCharged;
    public Color colorUncharged;

    public SpriteRenderer graphics;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCharged)
        {
            graphics.color = colorCharged;
        }
        else
        {
            graphics.color = colorUncharged;
        }

        if(Input.GetButton("Fire"))
        {
            if(isCharged)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<PlayerBulletMovement>().direction = transform.up.normalized;
                isCharged = false;
            }
        }
    }

    public void ReceiveBullet()
    {
        if(!isCharged)
        {
            isCharged = true;
        }
        else
        {
            //the player dies
        }
    }
}
