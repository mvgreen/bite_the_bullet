using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPatternMovement : MonoBehaviour
{
    public MovementPattern MP;
    int direction;
    public MovementPattern.Orientations orientation;
    public MovementPattern.Modes mode;
    public Vector3 MovementAxis;

    private void Start()
    {
        MovementAxis = transform.position;
        Debug.Log(gameObject.transform.position);
        direction = (int)MP.direction;
        orientation = MP.orientation;
        mode = MP.mode;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 v = GetComponent<Transform>().position;
        float coord1 = 0;
        float coord2 = 0;
        if (orientation == MovementPattern.Orientations.horizontal)
            coord1 = v.x;
        else
            coord1 = v.y;

        if (mode == MovementPattern.Modes.Sine)
        {
            coord2 = Mathf.Sin((coord1 + MP.delta *direction)* MP.Period)* MP.Amplitude;
        }
        if(mode == MovementPattern.Modes.ZigZagSine){
            coord2 = Mathf.Asin(Mathf.Sin((coord1 + MP.delta *direction) * MP.Period)) *MP.Amplitude;
        }

        if (orientation == MovementPattern.Orientations.horizontal) {
            v.x = coord1 + MP.delta* direction;
            v.y = coord2 + MovementAxis.y;
        }
        else
        {
            v.x = coord2 + MovementAxis.x;
            v.y = coord1 + MP.delta * direction;
        }

        GetComponent<Transform>().position = v;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Border Up" || collision.gameObject.tag == "Border Right" || collision.gameObject.tag == "Border Down" || collision.gameObject.tag == "Border Left")
        {
            
            direction *= -1;
        }
    }

}
