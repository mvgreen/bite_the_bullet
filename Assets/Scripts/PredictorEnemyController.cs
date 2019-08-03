using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictorEnemyController : MonoBehaviour
{
    public GameObject Target;
    private Vector3 playerVelocity;
    private Vector3 playerPosition;

    public GameObject bulletPrefab;
    public float BulletSpeed = 7;
    //time between shots
    public float ShotDelay;
    //timer
    public float shootTimer;


    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        shootTimer = 0;
    }

    private void FixedUpdate()
    {
        if(shootTimer >= ShotDelay) {
            TryShoot();
            shootTimer = 0;
        }
        shootTimer += Time.fixedDeltaTime;   
        
    }

    Vector3 PredictPostition()
    {
        playerVelocity = Target.GetComponent<Rigidbody2D>().velocity;
        playerPosition = Target.GetComponent<Transform>().transform.position;
        Vector3 offset = playerPosition - transform.position;
        float playerMoveAngle = Vector3.Angle(-offset, playerVelocity) * Mathf.Deg2Rad;
        if (playerVelocity.magnitude == 0 )
        {
            return playerPosition;
        }
        float enemyShootAngle = Mathf.Asin(Mathf.Sin(playerMoveAngle) * playerVelocity.magnitude / BulletSpeed);
        Vector3 PredictedPosition =playerPosition + playerVelocity * offset.magnitude / Mathf.Sin(Mathf.PI - playerMoveAngle - enemyShootAngle) * Mathf.Sin(enemyShootAngle) / playerVelocity.magnitude;
        return PredictedPosition;
    }

    void TryShoot()
    {
        Vector3 rot = PredictPostition();
        transform.up = rot - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<PlayerBulletMovement>().direction = transform.up.normalized;
        
    }
}
