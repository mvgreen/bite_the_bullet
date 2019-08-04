using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCoursor : MonoBehaviour
{
    public GameObject cursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cursorTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
