using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemRandomSpawnManager : MonoBehaviour
{
    [Header("Setting")]
    public List<RandomSpawnItemDetails> randomSpawnItemDetailsList = new List<RandomSpawnItemDetails>();

    private void Start()
    {
        RandomSpawnItemAction();
    }

    /// <summary>
    /// Read "randomSpawnItemDetailsList" data and for each to spawn item
    /// </summary>
    private void RandomSpawnItemAction()
    {
        foreach (RandomSpawnItemDetails data in randomSpawnItemDetailsList)
        {
            // Spawn item
            for (int i = 0; i < data.spawnCount; i++)
            {
                // Random spawn point
                int randomInt = Random.Range(0, data.SpawnPointsList.Count);

                Instantiate(data.spawnItem, data.SpawnPointsList[randomInt].transform.position, Quaternion.identity);
                Debug.Log($"Spawn Item: {data.spawnItem.name}"); // TODO: Log
                
                // Remove spawn point
                data.SpawnPointsList.RemoveAt(randomInt);
            }
        }
        
        // IsDone
        GameManager.Instance.isMapPrepare = true;
    }
}
