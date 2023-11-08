using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] objectsToDestroy; // Reference to the objects marked as DontDestroyOnLoad

    public void EndGame()
    {
        // Destroy the objects marked as DontDestroyOnLoad
        foreach (var obj in objectsToDestroy)
        {
            Destroy(obj);
        }

    }
}
