using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public enum State
    {
        SHOWER, MOVING, CHASING, ESCAPE
    }
    public State state;
    public float shootingCooldown;
    public float sqrEscapeDistance;
    public float chaseDistance;
    public float stageTime;
    public float chaseSpeed;
    public float verticalStep = 0.1f;

    public float bulletSpeed;
    public float bulletAngleSpeed;
    public float initialAngle;
    public float angularStep;
    public float linearStep;
    public float coneSize;



    public GameObject bulletPrefab;
    public GameObject player;

    public GameObject graphcs;
    private Animator animator;

    public float borderX;
    public float borderY;

    private System.Random rand;
    private float time;
    private float nextShot;
    private float nextStage;


    private EnemyShooting enemyShooting;
    private Rigidbody2D body;
    private float pivotAngle;
    private float virtualAngle;
    private bool movingUp;
    private Vector3 escapePoint;

    void Start()
    {
        animator = graphcs.GetComponent<Animator>();
        state = State.SHOWER;
        time = 0;
        nextStage = stageTime;
        nextShot = shootingCooldown * 5;
        movingUp = false;
        enemyShooting = gameObject.GetComponent<EnemyShooting>();
        rand = new System.Random();
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        switch (state)
        {
            case State.MOVING:
                updateMoving();
                break;
            case State.SHOWER:
                updateShower();
                break;
            case State.CHASING:
                updateChasing();
                break;
            case State.ESCAPE:
                updateEscaping();
                break;
        }
        //borders
        if (transform.position.x < -borderX)
        {
            transform.position = new Vector2(-borderX, transform.position.y);
        }
        if (transform.position.x > borderX)
        {
            transform.position = new Vector2(borderX, transform.position.y);
        }
        if (transform.position.y < -borderY)
        {
            transform.position = new Vector2(transform.position.x, -borderY);
        }
        if (transform.position.y > borderY)
        {
            transform.position = new Vector2(transform.position.x, borderY);
        }


        transform.rotation = Quaternion.identity;
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }



    private void updateMoving()
    {
        if (nextShot < time)
        {
            if (player.transform.position.x > transform.position.x)
                enemyShooting.Cone(1, initialAngle + 90, bulletSpeed, -bulletAngleSpeed, angularStep, linearStep, coneSize, bulletPrefab);
            else
                enemyShooting.Cone(1, initialAngle, bulletSpeed, bulletAngleSpeed, angularStep, linearStep, coneSize, bulletPrefab);
            nextShot += shootingCooldown / 2;
        }

        if ((player.transform.position - transform.position).sqrMagnitude < sqrEscapeDistance || nextStage < time)
        {
            nextStage += stageTime;
            state = State.ESCAPE;
            escapePoint = Vector3.zero;
            if (transform.position.x > 0)
                escapePoint.x = -8;
            else
                escapePoint.x = 8;

            animator.SetInteger("currentState", 3);
            return;
        }

        if (movingUp)
            transform.position = new Vector3(transform.position.x, transform.position.y + verticalStep, 0);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - verticalStep, 0);

        if (transform.position.y > borderY - 1)
            movingUp = false;
        if (transform.position.y < -borderY + 1)
            movingUp = true;

    }

    private void updateShower()
    {
        if (nextShot < time)
        {
            if (player.transform.position.x > transform.position.x)
                enemyShooting.Cone(5, initialAngle + 90, bulletSpeed, -bulletAngleSpeed, angularStep, linearStep, coneSize, bulletPrefab);
            else
                enemyShooting.Cone(5, initialAngle, bulletSpeed, bulletAngleSpeed, angularStep, linearStep, coneSize, bulletPrefab);
            nextShot += shootingCooldown;
        }

        if ((player.transform.position - transform.position).sqrMagnitude < sqrEscapeDistance)
        {
            Debug.Log((player.transform.position - transform.position).sqrMagnitude);
            state = State.ESCAPE;
            escapePoint = Vector3.zero;
            if (transform.position.x > 0)
                escapePoint.x = -8;
            else
                escapePoint.x = 8;
            animator.SetInteger("currentState", 3);
        }
        else if (nextStage < time)
        {
            nextStage += stageTime;
            state = State.CHASING;
            animator.SetInteger("currentState", 2);
        }
    }

    private void updateChasing()
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        Vector3 distance = new Vector3(x - transform.position.x, y - transform.position.y, 0);
        if (distance.sqrMagnitude <= chaseDistance * chaseDistance)
        {
            body.velocity = Vector2.zero;
            distance.Normalize();
            distance = -distance;
            transform.position = player.transform.position + (distance * chaseDistance);
            pivotAngle = Vector2.Angle(Vector2.right, distance);
            virtualAngle = 0;
            state = State.MOVING;
            animator.SetInteger("currentState", 1);
            return;
        }
        body.velocity = distance.normalized * chaseSpeed;
    }

    private void updateEscaping()
    { 
        Vector3 target = new Vector3(escapePoint.x - transform.position.x, escapePoint.y - transform.position.y, 0);
        if (target.sqrMagnitude <= 1)
        {
            body.velocity = Vector2.zero;
            state = State.SHOWER;
            animator.SetInteger("currentState", 0);
            return;
        }
        body.velocity = target.normalized * chaseSpeed;
    }
}
