using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BigMomaController : MonoBehaviour
{
    public GameObject spawnPoint1, spawnPoint2;
    public Transform[] MomaSpawnPoints;
    public GameObject[] Younglings;


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
        Random.InitState(System.DateTime.Now.Millisecond);
        int rand = Random.Range(0, 2);
        Instantiate(Younglings[rand], spawnPoint1.transform.position, Quaternion.identity);
        rand = Random.Range(0, 2);
        Instantiate(Younglings[rand], spawnPoint2.transform.position, Quaternion.identity);
        YounglingCount += 2;
        UpdateYounglignsCount();
    }

    public void UpdateYounglignsCount()
    {
        GetComponent<Animator>().SetInteger("LilCount", YounglingCount);
    }


    public void TeleportMom()
    {

        Random.InitState(System.DateTime.Now.Millisecond);
        int index = Random.Range(0, 4);
        Transform NewLocation = MomaSpawnPoints[index].transform;
        if(index == CurrentSpawnPoint && Random.Range(0,1) <= 0.85f)
        {
            index = Random.Range(0, 4);
            NewLocation = MomaSpawnPoints[index].transform;
        }
        CurrentSpawnPoint = index;
        transform.rotation = Quaternion.Euler(Vector3.forward*90*index);
        transform.position = NewLocation.position;
        
    }    

    public void Roll()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float rand = 100 - YounglingCount * 15f;
        GetComponent<Animator>().SetFloat("Roll", rand);
    }

}
