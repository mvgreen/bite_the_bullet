using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BigMomaController : MonoBehaviour
{
    public GameObject spawnPoint1, spawnPoint2;
    public Transform[] MomaSpawnPoints;
    public GameObject Youngling;

    public MovementPattern MP;

    private int CurrentSpawnPoint;

    public static int YounglingCount = 0;

    private void Start()
    {
        CurrentSpawnPoint = 0; 
    }

    private void Update()
    {
        
    }

    public void SpawnYounglings1()
    {
        switch (CurrentSpawnPoint)
        {
            case 0:
                MP.orientation = MovementPattern.Orientations.horizontal;
                MP.a = -Mathf.Abs(MP.a);
                break;
            case 1:
                MP.orientation = MovementPattern.Orientations.vertical;
                MP.a = -Mathf.Abs(MP.a);
                break;
            case 2:
                MP.orientation = MovementPattern.Orientations.horizontal;
                MP.a = Mathf.Abs(MP.a);
                break;
            case 3:
                MP.orientation = MovementPattern.Orientations.vertical;
                MP.a = Mathf.Abs(MP.a);
                break;
        }
        Youngling.GetComponent<EnemyPatternMovement>().MP = MP;
        Instantiate(Youngling, spawnPoint1.transform.position, Quaternion.identity);
        MP.InvertDirection();
        Instantiate(Youngling, spawnPoint2.transform.position, Quaternion.identity);
        YounglingCount += 2;
        UpdateYounglignsCount();
    }

    public void UpdateYounglignsCount()
    {
        GetComponent<Animator>().SetInteger("LilCount", YounglingCount);
    }


    public void TeleportMom()
    {

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int index = UnityEngine.Random.Range(0, 4);
        Transform NewLocation = MomaSpawnPoints[index].transform;
        if(index == CurrentSpawnPoint && UnityEngine.Random.Range(0,1) <= 0.85f)
        {
            index = UnityEngine.Random.Range(0, 4);
            NewLocation = MomaSpawnPoints[index].transform;
        }
        CurrentSpawnPoint = index;
        transform.rotation = Quaternion.Euler(Vector3.forward*90*index);
        transform.position = NewLocation.position;
        
    }    

    public void Roll()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int rand = UnityEngine.Random.Range(0, 100);
        GetComponent<Animator>().SetFloat("Roll", rand);
    }

}
