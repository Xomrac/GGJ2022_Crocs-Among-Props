using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace GGJ
{
public class SpawnManager : MonoBehaviour
{
  public List<GameObject> spawnableObjects;
  public int objectsToSpawn;
  public GameObject keyObject;
  public List<Transform> keyObjectSpawnPoints;
  public Transform spawnPoint;



  private readonly List<GameObject> spawnedObjects=new List<GameObject>();
  private GameObject spawnedKeyItem;

  private void Start() {
    SpawnProps();
    SpawnKeyItem();
  }

  [Button]
  private void SpawnProps()
  {
    ClearSpawnedProps();
    for (int i = 0; i < objectsToSpawn; i++)
    {
      int randomNumber = Random.Range(0, spawnableObjects.Count);
      GameObject spawnedObject=Instantiate(spawnableObjects[randomNumber], spawnPoint.position, Quaternion.identity,spawnPoint);
      spawnedObjects.Add(spawnedObject);
    }
  }

  [Button]
  private void ShuffleSpawnedObjects()
  {
    
  }


  [Button]
  private void SpawnKeyItem()
  {
    if (keyObject != null)
    {
      DestroyImmediate(spawnedKeyItem);
      int randomNumber = Random.Range(0, keyObjectSpawnPoints.Count);
      Transform randomTransform = keyObjectSpawnPoints[randomNumber];
      spawnedKeyItem=Instantiate(keyObject, randomTransform.position, Quaternion.identity,randomTransform);
    }
  }

  [Button]
  private void ClearSpawnedProps()
  {
    if (spawnedObjects.Count != 0)
    {
      foreach (GameObject spawnedObject in spawnedObjects)
      {
        DestroyImmediate(spawnedObject);
      }
      spawnedObjects.Clear();
    }
  }
}
}