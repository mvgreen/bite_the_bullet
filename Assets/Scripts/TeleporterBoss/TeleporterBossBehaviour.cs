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

    private float currentSizeMultiplier;
    private float linearSpeedMultiplier;

    private GameObject activeOrbit;
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
        activeOrbit = null;
        rand = new System.Random();
        body = GetComponent<Rigidbody2D>();
        shootingController = GetComponent<BossShootingController>();
        currentSizeMultiplier = sizeMultiplier;
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
    }


    internal void prepareRevolve()
    {
        activeOrbit = Instantiate(orbitPrefab, player.transform.position, Quaternion.identity);
        activeOrbit.GetComponent<PlayerFollower>().player = player;
        currentSizeMultiplier = sizeMultiplier;
        activeOrbit.transform.localScale = Vector3.one * currentSizeMultiplier;
    }

    public void enterArena()
    {
        mode = BossMode.TELEPORTING;
        transform.position = new Vector3(-8, 0, 0);
    }

    public void startRevolving()
    {
        mode = BossMode.REVOLVING;
        resetTime();
        scaledTime = rand.Next() % 7;
    }

    public void blinkOnce()
    {
        mode = BossMode.TELEPORTING_BEFORE_RUSH;
        transform.position = activeMarker.transform.position;
        Destroy(activeMarker);
        activeMarker = null;
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        transform.up = direction;
    }

    public void hideOrbit()
    {
        Destroy(activeOrbit);
        activeOrbit = null;
    }

    public void shootStill()
    {
        body.velocity = Vector2.zero;
        resetTime();
        mode = BossMode.SHOOT_STILL;
        transform.position = activeMarker.transform.position;
        Destroy(activeMarker);
        activeMarker = null;
    }

    public void prepareRush()
    {
        mode = BossMode.AIM_RUSH;
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
        Debug.Log("Reset");
        time = 0;
        nextShotTime = shootingCooldown * 5;
        scaledTime = 0;
    }

    private void updateTime()
    {
        if (mode == BossMode.TELEPORTING || mode == BossMode.REST)
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
        if (activeOrbit == null)
        {
            mode = BossMode.TELEPORTING_BEFORE_RUSH;
            return;
        }

        float x = Mathf.Cos(scaledTime) * unitRadius * currentSizeMultiplier + player.transform.position.x;
        float y = Mathf.Sin(scaledTime) * unitRadius * currentSizeMultiplier + player.transform.position.y;

        transform.position = new Vector3(x, y, 0);
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        transform.up = direction;

        activeOrbit.transform.localScale = Vector3.one * currentSizeMultiplier;
        currentSizeMultiplier -= radiusReductionSpeed;
    }

}
