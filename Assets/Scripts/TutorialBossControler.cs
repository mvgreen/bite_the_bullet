using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBossControler : MonoBehaviour
{
    public Text[] messages;
    public GameObject[] bullets;

    EnemyShooting shooter;

    int phase = 0;
    int bulletCounter = 0;
    
    bool movedV;
    bool movedH;

    Transform bullet;
    Transform player;

    float countdown = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        shooter = GetComponent<EnemyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(phase == 0)
        {
            messages[0].enabled = true;
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0.5f)
            {
                movedH = true;
            }
            if(Mathf.Abs(Input.GetAxisRaw("Vertical"))>0.5f)
            {
                movedV = true;
            }

            if(movedH && movedV)
            {
                messages[0].enabled = false;
                phase = 1;
            }
        }
        if (phase == 1)
        {
            messages[1].enabled = true;
            countdown-=Time.deltaTime;
            if(countdown<0)
            {
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*180 / Mathf.PI)+ 90, 4, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*200 / Mathf.PI)+ 90, 4, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*160 / Mathf.PI)+ 90, 4, bullets[0]);
                countdown=0.4f;
                bulletCounter++;
            }
            if(bulletCounter>10)
            {
                phase = 2;
                messages[1].enabled = false;
            }
        }
        if(phase == 2)
        {
            messages[2].enabled = true;
            Instantiate(bullets[0], player.position, Quaternion.identity);
            if(Input.GetButtonDown("Fire"))
            {
                messages[2].enabled = false;
                phase = 3;
                bulletCounter = 0;
            }
        }
        if(phase == 3)
        {
            messages[3].enabled = true;
            if(bulletCounter == 0)
            {
                bullet = Instantiate(bullets[1],player.position, Quaternion.identity);
                bulletCounter++;
            }
            if(bulletCounter>0)
            {
                if(!bullet)
                {
                    messages[3].enabled = false;
                    phase = 4;
                }
            }
        }
    }
}
