using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public void Shoot(float angle, float speed, GameObject bulletPrefab)
    {
        Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().direction = direction;
        bullet.GetComponent<EnemyBullet>().speed = speed;
    }

    public void Sprinkle(int spread, float angle, float speed, GameObject bulletPrefab)
    {
        for(int i = 0; i < spread; i++)
        {
            Vector2 direction = new Vector2(Mathf.Sin((angle + (360/spread)*i) * Mathf.Deg2Rad), Mathf.Cos((angle + (360/spread)*i) * Mathf.Deg2Rad));
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = direction;
            bullet.GetComponent<EnemyBullet>().speed = speed;
        }
    }
}
