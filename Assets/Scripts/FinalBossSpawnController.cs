using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossSpawnController : MonoBehaviour
{
    public GameObject[] bullets;

    public int currentAttack = 0;
    float cooldown = 2;
    int attackPhase = 0;
    EnemyShooting shooter;
    GameObject player;

    Vector2 newPos;
    float XBorder = 7;
    float YBorder = 4;
    
    public void SetNewTargetLocation()
    {
        Vector2 loc;
        do
        {
            loc = 0.7f*Random.insideUnitCircle.normalized;
            loc = loc + (Vector2)transform.position;
        } while (Mathf.Abs(loc.x) >= XBorder || Mathf.Abs(loc.y) >= YBorder);
        newPos = loc;
    }

    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<EnemyShooting>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, newPos, 0.08f);
        //attacks
        cooldown -= Time.deltaTime;
        if(cooldown < 0)
        {
            SetNewTargetLocation();
            
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

        if(currentAttack == 1)
        {
            if(attackPhase==0)
            {
                attackPhase++;
                //shoot a bouncy
                shooter.Shoot(180+attackPhase*9f, 4+attackPhase*0.01f,2, bullets[1]);
            }
        }

        if(currentAttack == 2)
        {
            if(attackPhase==0)
            {
                attackPhase++;
                //shoot at player
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*180 / Mathf.PI)+ 90, 4+attackPhase*0.01f, bullets[0]);
            }
        }

        if(currentAttack == 3)
        {
            if(attackPhase==0)
            {
                attackPhase++;
                shooter.Sprinkle(6,Random.Range(0,360), 4, bullets[0]);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
