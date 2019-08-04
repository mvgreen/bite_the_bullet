using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    public GameObject[] bullets;

    public int currentAttack = 0;
    float cooldown = 1;
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
            if(Random.Range(1,7) < 2)
            {
                currentAttack = 6;
                cooldown = 3f;
                attackPhase = 0;
            }
            else if(Random.Range(1,6) < 2)
            {
                currentAttack = 5;
                cooldown = 2f;
                attackPhase = 0;
            }
            else 
            if(Random.Range(1,5) < 2)
            {
                currentAttack = 4;
                cooldown = 3f;
                attackPhase = 0;
            }
            else 
            if(Random.Range(1,4) < 2)
            {
                currentAttack = 3;
                cooldown = 3f;
                attackPhase = 0;
            }
            else if(Random.Range(1,3) < 2)
            {
                currentAttack = 1;
                cooldown = 2f;
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
        {// teleport and spawn
            if(attackPhase == 0)
            {
                attackPhase++;
                //spawn enemy here
                Teleport();
            }
        }

        if(currentAttack == 2)
        {// teleport and leave bouncies
            if(attackPhase == 0)
            {
                attackPhase++;
                shooter.Sprinkle(18, Random.Range(0,180), 2, 2, bullets[2]);
                Teleport();
            }
        }

        if(currentAttack == 3)
        {// hyperspam
            if(cooldown%0.3<0.1)
            {
                attackPhase++;
                shooter.Sprinkle(6,180+attackPhase*9f, 2.4f, bullets[0]);
            }
        }

        if(currentAttack == 4)
        {// a lot of jumps and spread attacks
            if(cooldown < 3 - attackPhase*0.5f)
            {
                attackPhase++;
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*180 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*200 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*220 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*160 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*140 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
                Teleport();
            }
        }

        if(currentAttack == 5)
        {
            if(cooldown < 2 - attackPhase*0.14f)
            {//double sprinkle
                attackPhase++;
                shooter.Sprinkle(5, 180+attackPhase*29f, 2, bullets[0]);
                shooter.Sprinkle(5, 178+attackPhase*29f, 2, bullets[0]);
            }
        }

        if(currentAttack == 6)
        {
            if(cooldown%0.5<0.12)
            {
                if(attackPhase%2==1)
                {
                    shooter.Shoot(90+attackPhase*59, 3, 30f, bullets[1]);
                }
                else
                {
                    shooter.Shoot(90-attackPhase*59, 3, -30f, bullets[1]);
                }
                attackPhase++;
            }
        }
    }

    void Teleport()
    {
        Vector2 potentialPos = new Vector2(Random.Range(-7f,7f), Random.Range(-4f,4f));
        while(Vector2.Distance(potentialPos,player.transform.position) < 5)
        {
            potentialPos = new Vector2(Random.Range(-7f,7f), Random.Range(-4f,4f));
        }
        transform.position = potentialPos;
    }
}
