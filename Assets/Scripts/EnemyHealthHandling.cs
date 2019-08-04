using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandling : MonoBehaviour
{

    public UnityEngine.UI.Image score;
    public int health = 250;

    public int bossIndex;

    // Update is called once per frame
    void Update()
    {
        if(health < 0)
        {
            if (score != null)
                score.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Destroy(gameObject);
        }

    }
}
