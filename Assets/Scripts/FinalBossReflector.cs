using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossReflector : MonoBehaviour
{
    public GameObject bullets;
    EnemyShooting shooter;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        shooter = GetComponent<EnemyShooting>();
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "PlayerBullet")
        {
            shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*180 / Mathf.PI)+ 90, 5, bullets);
            shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*190 / Mathf.PI)+ 90, 5, bullets);
            shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*200 / Mathf.PI)+ 90, 5, bullets);
            shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*170 / Mathf.PI)+ 90, 5, bullets);
            shooter.Shoot(-(Mathf.Atan2(player.transform.position.y-transform.position.y, player.transform.position.x-transform.position.x)*160 / Mathf.PI)+ 90, 5, bullets);
            Destroy(col.gameObject);
        }
    }
}
