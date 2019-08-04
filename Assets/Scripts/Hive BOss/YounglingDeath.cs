using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YounglingDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        int tmp = BigMomaController.YounglingCount;
        BigMomaController.YounglingCount = tmp-1;
        
    }
}
