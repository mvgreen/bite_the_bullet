using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new movement pattern", menuName ="Movement Pattern")]
public class MovementPattern : ScriptableObject
{
    public enum Modes
    {
        Sine,
        ZigZagSine,
        Porabolic

    };

    public enum Orientations
    {
        horizontal,
        vertical
    };

    public enum Directions
    {
        Positive = 1,
        Negative = -1
    };

    public Modes mode;
    public Orientations orientation;
    public Directions direction;
    [Range(0, 10)]
    public float Period = 1;
    [Range(0, 10)]
    public float Amplitude = 1;
    public float a, b, c;

    //Speed per se
    public float delta = 0.01f;

    public void InvertDirection()
    {
        direction = (Directions)(-1 * (int)direction);
    }
}
