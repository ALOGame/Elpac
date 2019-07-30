using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning { get; private set; }

    void Start()
    {
        gameRunning = true;
    }

    void Update()
    {
        
    }
}
