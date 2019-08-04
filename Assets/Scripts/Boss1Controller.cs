using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : MonoBehaviour
{
    public GameObject[] bullets;

    public int currentAttack = 0;
    float cooldown = 2;
    int attackPhase = 0;
    EnemyShooting shooter;
    GameObject player;

    Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<EnemyShooting>();
        player = GameObject.Find("Player");
        speed = new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        //attacks
        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            speed += new Vector3(Random.Range(-.4f,.4f), Random.Range(-.4f,.4f),0);
            if(Random.Range(1,8) < 2)
            {
                currentAttack = 6;
                cooldown = 3f;
                attackPhase = 0;
            }
            else if(Random.Range(1,8) < 2)
            {
                currentAttack = 5;
                cooldown = 2f;
                attackPhase = 0;
            }
            else 
            if(Random.Range(1,5) < 2)
            {
                currentAttack = 4;
                cooldown = 5f;
                attackPhase = 0;
            }
            else 
            if(Random.Range(1,4) < 2)
            {
                currentAttack = 3;
                cooldown = 5f;
                attackPhase = 0;
            }
            else if(Random.Range(1,3) < 2)
            {
                currentAttack = 1;
                cooldown = 5f;
                attackPhase = 0;
            }
            else
            {
                currentAttack = 2;
                cooldown = 2f;
                attackPhase = 0;
            }
        }

        //movement
        if(speed.magnitude > 1.2)
        {
            speed*=.9f;
        }
        if(transform.position.x<-8)
        {
            speed+=new Vector3(.1f,0,0);
        }
        if(transform.position.x>8)
        {
            speed+=new Vector3(-.1f,0,0);
        }
        if(transform.position.y<-4.5)
        {
            speed+=new Vector3(0,.1f,0);
        }
        if(transform.position.y>4.5)
        {
            speed+=new Vector3(0,-.1f,0);
        }
        transform.position += speed*Time.deltaTime;

        if(currentAttack == 1)
        {
            if(cooldown < 5 - attackPhase*0.05f)
            {
                attackPhase++;
                //shoot a spiral
                shooter.Shoot(180+attackPhase*9f, 4+attackPhase*0.01f, bullets[0]);
            }
        }

        if(currentAttack == 2)
        {
            if(cooldown < 2 - attackPhase*0.4f)
            {
                attackPhase++;
                //shoot at player
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*180 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
            }
        }

        if(currentAttack == 3)
        {
            if(cooldown < 5 - attackPhase*0.4f)
            {
                attackPhase++;
                shooter.Sprinkle(10,180+attackPhase*9f, 4, bullets[0]);
            }
        }

        if(currentAttack == 4)
        {
            if(cooldown < 5 - attackPhase*0.5f)
            {
                attackPhase++;
                if(attackPhase%2==1)
                {
                    shooter.Sprinkle(8,180, 3, 20f, bullets[1]);
                }
                else
                {
                    shooter.Sprinkle(8,180, 3, -20f, bullets[1]);
                }
            }
        }

        if(currentAttack == 5)
        {
            if(cooldown < 2 - attackPhase*0.1f)
            {
                attackPhase++;
                //shoot a spiral
                shooter.Sprinkle(5, 180+attackPhase*29f, 2+attackPhase*0.01f, bullets[0]);
            }
        }

        if(currentAttack == 6)
        {
            if(attackPhase==0)
            {
                attackPhase++;
                shooter.Sprinkle(12, Random.Range(0,180), 3+attackPhase*0.01f, 3, bullets[2]);
            }
        }
    }
}
