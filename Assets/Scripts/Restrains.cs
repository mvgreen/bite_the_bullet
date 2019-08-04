using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restrains : MonoBehaviour
{
    public GameObject BossSprite;
    public GameObject Boss;
    public GameObject[] Bosses;
    public GameObject[] BossesSprites;
    public static int BossesBeaten;
    private void Start()
    {
        if (PlayerPrefs.GetInt("Boss0Killed", 0) == 1)
        {
            foreach (GameObject go in Bosses)
            {
                go.SetActive(true);
            }

        }
        if (PlayerPrefs.GetInt("Boss0Killed", 0) == 1 && PlayerPrefs.GetInt("Boss1Killed", 0) == 1 && PlayerPrefs.GetInt("Boss2Killed", 0) == 1 && PlayerPrefs.GetInt("Boss3Killed", 0) == 1 && PlayerPrefs.GetInt("Boss4Killed", 0) == 1){ {
                Boss.SetActive(true);
                BossSprite.SetActive(true);
            }
        } }

}
