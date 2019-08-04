using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour
{

    public float XBorder;
    public float YBorder;

    public Vector2 newPos;
    public float RelocationTimer;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        SetNewTargetLocation();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= RelocationTimer)
        {
            SetNewTargetLocation();
            Timer = 0;
        }
        transform.position = Vector2.Lerp(transform.position, newPos, 0.08f);
    }

    public void SetNewTargetLocation()
    {
        Vector2 loc;
        do
        {
            loc = 0.7f*Random.insideUnitCircle.normalized;
            loc = loc + (Vector2)transform.position;
        } while (Mathf.Abs(loc.x) >= XBorder || Mathf.Abs(loc.y) >= YBorder);
        newPos = loc;
    }
}
