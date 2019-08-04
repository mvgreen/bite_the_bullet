using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restrains : MonoBehaviour
{
    public GameObject BossSprite;
    public GameObject Boss;
    public GameObject[] Bosses;
    public static int BossesBeaten;
    private void Start()
    {
        BossesBeaten = PlayerPrefs.GetInt("Bosses Beaten");
        if(BossesBeaten < 1)
        {
            foreach(GameObject go in Bosses)
            {
                go.SetActive(false);
            }
            
        }
        if(BossesBeaten < 5)
        {
            Boss.SetActive(false);
            BossSprite.SetActive(false);
        }
    }

}
