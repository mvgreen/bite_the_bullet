using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthHandling : MonoBehaviour
{

    public Image score;
    public int health = 250;

    public int bossIndex;

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            if (score != null)
                score.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            PlayerPrefs.SetInt("Boss"+bossIndex.ToString()+"Killed", 1);
            if(PlayerPrefs.GetInt("Boss"+bossIndex.ToString()+"Hits",9999999)< int.Parse((score.rectTransform.GetChild(0).GetComponent<Text>()).text))
            {
                PlayerPrefs.SetInt("Boss"+bossIndex.ToString()+"Hits", int.Parse(score.rectTransform.GetChild(0).GetComponent<Text>().text));
            }
            score.GetComponent<ExitToMenu>().Exit();
            Destroy(gameObject);
        }

    }
}
