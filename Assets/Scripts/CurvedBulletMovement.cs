using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedBulletMovement : MonoBehaviour
{

    Transform child;
    EnemyBullet info;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
        info = GetComponent<EnemyBullet>();
        child.GetComponent<EnemyBullet>().direction = new Vector2(1,0);
        child.GetComponent<EnemyBullet>().speed = info.speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z+Time.deltaTime * info.angularSpeed);
        if(!child)
        {
            Destroy(gameObject);
        }
    }
}
