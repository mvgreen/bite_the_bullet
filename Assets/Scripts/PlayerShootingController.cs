using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootingController : MonoBehaviour
{

    bool isCharged = true;
    Animator anim;

    public SpriteRenderer graphics;

    public GameObject bulletPrefab;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Fire"))
        {
            if(isCharged)
            {
                anim.SetTrigger("Fire");
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
            anim.SetTrigger("Catch");
            isCharged = true;
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        int currentNumber = int.Parse(text.text);
        currentNumber++;
        text.text = "" + currentNumber;
    }
}
