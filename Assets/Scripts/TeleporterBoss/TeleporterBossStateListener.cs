using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TeleporterBossBehaviour;

public class TeleporterBossStateListener : MonoBehaviour
{
    TeleporterBossBehaviour shipBehaviour;
    Animator animator;
    System.Random rand;

    void Start()
    {
        shipBehaviour = transform.parent.GetComponent<TeleporterBossBehaviour>();
        animator = GetComponent<Animator>();
        rand = new System.Random();
    }
   
    public void onEnteringArena()
    {
        shipBehaviour.enterArena();
    }

    public void onRevlovingStarted()
    {
        shipBehaviour.startRevolving();
        int nextMode = rand.Next() % 9 + 1;
        animator.SetInteger("mode", nextMode);
    }

    public void onBlinkPreparing()
    {
        if (animator.GetInteger("mode") == 8 || animator.GetInteger("mode") == 0)
            return;
        if (animator.GetInteger("mode") == 9)
            shipBehaviour.markBlinkPosition(2);
        else
            shipBehaviour.markBlinkPosition(4);
    }

    public void onBlinkSpamStarted()
    {
        shipBehaviour.blinkOnce();
        int nextMode = rand.Next() % 10;
        animator.SetInteger("mode", nextMode);
    }

    public void onRush()
    {
        shipBehaviour.rush();
        animator.SetInteger("mode", 9);
    }

    public void onShootStill()
    {
        shipBehaviour.shootStill();
        int nextMode = rand.Next() % 10;
        animator.SetInteger("mode", nextMode);
    }

    public void onPreparingRush()
    {
        shipBehaviour.prepareRush();
    }

    public void onAimingRush()
    {
        shipBehaviour.aimRush();
    }

    public void onShootingCooldown()
    {
        shipBehaviour.stopShooting();
    }

    public void onAppear()
    {
        shipBehaviour.appear();
    }
}
