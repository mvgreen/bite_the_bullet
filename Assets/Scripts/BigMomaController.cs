using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BigMomaController : MonoBehaviour
{
    public GameObject spawnPoint1, spawnPoint2;
    public Transform[] MomaSpawnPoints;
    public GameObject Youngling;

    public MovementPattern[] MovementPatterns;

    private Transform LastSpawnPoint;

    private void Start()
    {
        LastSpawnPoint = MomaSpawnPoints[0];
    }

    public void SpawnYounglings1()
    {
        Youngling.GetComponent<EnemyPatternMovement>().MP = MovementPatterns[0];
        Instantiate(Youngling, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(Youngling, spawnPoint2.transform.position, Quaternion.identity);
    }

    public void SpawnYounglings2()
    {
        Youngling.GetComponent<EnemyPatternMovement>().MP = MovementPatterns[1];
        Instantiate(Youngling, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(Youngling, spawnPoint2.transform.position, Quaternion.identity);
    }

    public void SpawnYounglings3()
    {
        Youngling.GetComponent<EnemyPatternMovement>().MP = MovementPatterns[2];
        Instantiate(Youngling, spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(Youngling, spawnPoint2.transform.position, Quaternion.identity);
    }

    public void TeleportMom()
    {

        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int index = UnityEngine.Random.Range(0, 4);
        Debug.Log(index);
        Transform NewLocation = MomaSpawnPoints[index].transform;
        if(NewLocation == LastSpawnPoint && UnityEngine.Random.Range(0,1) <= 0.75f)
        {
            index = UnityEngine.Random.Range(0, 4);
            Debug.Log(index);
            NewLocation = MomaSpawnPoints[index].transform;
        }
        LastSpawnPoint = NewLocation;
        transform.rotation = Quaternion.Euler(Vector3.forward*90*index);
        transform.position = NewLocation.position;
        
    }    


}
