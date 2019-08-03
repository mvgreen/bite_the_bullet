using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShootingController : MonoBehaviour
{
    public SpriteRenderer graphics;

    public GameObject bulletPrefab;

    public void shoot(Vector2 position, Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<BossBulletMovement>().direction = direction.normalized;
    }

}
