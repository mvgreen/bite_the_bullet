using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterBossBehaviour : MonoBehaviour
{
    public enum BossMode
    {
        REST, TELEPORTING, REVOLVING, TELEPORTING_BEFORE_RUSH, AIM_RUSH, RUSH, SHOOT_STILL
    }
    public BossMode mode;
    public GameObject player;
    public GameObject orbitPrefab;
    public GameObject markerPrefab;
    public GameObject graphics;

    public float radialSpeedMultiplier = 1;
    public float sizeMultiplier = 3;
    public float unitRadius = 3;
    public float defaultSpeed = 1.3f;
    public float radiusReductionSpeed;
    public float rushSpeed;
    public float shootingCooldown;

    public float time;
    private float nextShotTime;
    private float scaledTime;
    private bool canShoot = true;

    private float currentSizeMultiplier;
    private float linearSpeedMultiplier;
    
    private GameObject activeMarker;
    private System.Random rand;
    private Rigidbody2D body;
    private BossShootingController shootingController;
    private Vector2 rushTarget;

    // Start is called before the first frame update
    void Start()
    {
        resetTime();
        linearSpeedMultiplier = radialSpeedMultiplier * sizeMultiplier;
        mode = BossMode.REST;
        rand = new System.Random();
        body = GetComponent<Rigidbody2D>();
        shootingController = GetComponent<BossShootingController>();
        currentSizeMultiplier = sizeMultiplier;
    }

    private void Update()
    { 
        graphics.transform.rotation = Quaternion.identity;
        if (mode == BossMode.RUSH)
            return;
        body.velocity = Vector3.zero;
        if (player.transform.position.x > transform.position.x)
        {
            graphics.transform.localScale = new Vector3(-3, 3, 2);
        }
        else
        {
            graphics.transform.localScale = new Vector3(3, 3, 2);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (mode)
        {
            case BossMode.REVOLVING:
                updateRevolve();
                break;
            case BossMode.SHOOT_STILL:
                updateShootStill();
                break;
            case BossMode.RUSH:
                break;
            case BossMode.REST:
                break;
            case BossMode.AIM_RUSH:
                fixAim();
                break;
            default:
                fixAim();
                updateTime();
                break;
        }
    }

    internal void appear()
    {
        mode = BossMode.TELEPORTING;
        Debug.Log("active marker is null: " + (activeMarker == null));
        if (activeMarker != null)
        {
            Debug.Log("(" + activeMarker.transform.position.x + "; " + activeMarker.transform.position.y + ")");
            transform.position = activeMarker.transform.position;
            Destroy(activeMarker);
            activeMarker = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            col.GetComponent<PlayerShootingController>().Die();
    }

    internal void stopShooting()
    {
        mode = BossMode.REST;
    }

    internal void markBlinkPosition(int places)
    {
        activeMarker = Instantiate(markerPrefab, player.transform.position, Quaternion.identity);
        int side = rand.Next() % places;
        switch (side)
        {
            case 0:
                activeMarker.transform.position = Vector3.left * 8;
                break;
            case 1:
                activeMarker.transform.position = Vector3.right * 8;
                break;
            case 2:
                activeMarker.transform.position = Vector3.up * 3.5f;
                break;
            case 3:
                activeMarker.transform.position = Vector3.down * 3.5f;
                break;
        }
        canShoot = false;
        Debug.Log("(" + activeMarker.transform.position.x + "; " + activeMarker.transform.position.y + ")");
    }

    public void enterArena()
    {
        mode = BossMode.TELEPORTING;
        transform.position = new Vector3(-8, 0, 0);
    }

    public void startRevolving()
    {
        mode = BossMode.REVOLVING;
        currentSizeMultiplier = sizeMultiplier;
        resetTime();
        scaledTime = rand.Next() % 7;
        canShoot = true;
    }

    public void blinkOnce()
    {
        mode = BossMode.TELEPORTING_BEFORE_RUSH;
        fixAim();
        canShoot = true;
    }

    public void hideOrbit()
    {
    }

    public void shootStill()
    {
        body.velocity = Vector2.zero;
        resetTime();
        mode = BossMode.SHOOT_STILL;
        canShoot = true;
    }

    public void prepareRush()
    {
        mode = BossMode.AIM_RUSH;
        canShoot = true;
    }

    public void aimRush()
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        rushTarget = new Vector2(x - transform.position.x, y - transform.position.y).normalized;
    }

    public void rush()
    {
        mode = BossMode.RUSH;
        body.velocity = rushTarget * rushSpeed;
    }

    private void resetTime()
    {
        time = 0;
        nextShotTime = shootingCooldown * 5;
        scaledTime = 0;
    }

    private void updateTime()
    {
        if (mode == BossMode.TELEPORTING || mode == BossMode.REST || !canShoot)
        {
            return;
        }
        time += Time.deltaTime;
        if (nextShotTime <= time)
        {
            if (mode != BossMode.SHOOT_STILL)
            {
                shootingController.shoot(transform.position, transform.up.normalized);
            }
            else
            {
                Vector3 centerDirection = transform.up.normalized;
                float deltaAngle = 6.5f;
                for (int i = -3; i <= 3; i++)
                {
                    Vector3 dir = Vector3.zero;
                    dir.x = Mathf.Cos(deltaAngle * i / 180 * Mathf.PI + Vector3.SignedAngle(centerDirection, Vector3.right, Vector3.back) / 180 * Mathf.PI);
                    dir.y = Mathf.Sin(deltaAngle * i / 180 * Mathf.PI + Vector3.SignedAngle(centerDirection, Vector3.right, Vector3.back) / 180 * Mathf.PI);
                    shootingController.shoot(transform.position, dir.normalized);
                }
            }
            nextShotTime += shootingCooldown;
        }
    }


    private void fixAim()
    {
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        transform.up = direction;
    }

    private void updateShootStill()
    {
        fixAim();
        updateTime();
    }

    private void updateRevolve()
    {
        updateTime();
        scaledTime += Time.deltaTime * defaultSpeed * radialSpeedMultiplier / currentSizeMultiplier;

        float x = Mathf.Cos(scaledTime) * unitRadius * currentSizeMultiplier + player.transform.position.x;
        float y = Mathf.Sin(scaledTime) * unitRadius * currentSizeMultiplier + player.transform.position.y;

        transform.position = new Vector3(x, y, 0);
        fixAim();

        currentSizeMultiplier -= radiusReductionSpeed;
    }

}
