using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YounglingController : MonoBehaviour
{
    public float shootDelay;
    float timer;
    public EnemyShooting shooter;
    public GameObject bullet;
    public float speed;
    public GameObject player;

    private void Start()
    {
        shooter = GetComponent<EnemyShooting>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(shootDelay <= timer)
        {
            int rng = Random.RandomRange(0, 2);
            if (rng < 1)
            {

                shooter.Sprinkle(7, Random.RandomRange(0, 359), speed, bullet);
            }
            else
            {
                shooter.Shoot(-(Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * 180 / Mathf.PI) + 90, 4, bullet);
            }
            timer = 0;
        }
    }
}
