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
  public float throwForce;
  public GameObject keyObject;
  public List<Transform> keyObjectSpawnPoints;
  public Transform spawnPoint;


  [SerializeField][HideInInspector]List<GameObject> spawnedObjects=new List<GameObject>();
  private GameObject spawnedKeyItem;

  private void Start()
  {
    ShuffleSpawnedObjects();
    SpawnKeyItemOnStart();
  }


  [Button]
  private void SpawnProps()
  {
    ClearSpawnedProps();
    for (int i = 0; i < objectsToSpawn; i++)
    {
      int randomNumber = Random.Range(0, spawnableObjects.Count);
      GameObject spawnedObject = Instantiate(spawnableObjects[randomNumber], spawnPoint.position, Quaternion.identity, spawnPoint);
      spawnedObjects.Add(spawnedObject);
    }
  }

  private void ShuffleSpawnedObjects()
  {
	  foreach(GameObject spawnedObject in spawnedObjects)
	  {
		  var rb=spawnedObject.GetComponent<Rigidbody>();
      Vector3 spawnedObjectRotation = spawnedObject.transform.eulerAngles;
      spawnedObjectRotation = new Vector3(spawnedObjectRotation.x, Random.Range(0, 360), spawnedObjectRotation.z);
      Transform spawnedObjectTransform = spawnedObject.transform;
      spawnedObjectTransform.eulerAngles = spawnedObjectRotation;
      rb.isKinematic = false;
     Vector3 appliedForce = spawnedObjectTransform.forward;
     appliedForce = new Vector3(appliedForce .x, 1, appliedForce .z);
     rb.AddForce(appliedForce * throwForce,ForceMode.Impulse);

    }
  }

  private void SpawnKeyItemOnStart()
  {
    int randomNumber = Random.Range(0, keyObjectSpawnPoints.Count);
    Transform randomTransform = keyObjectSpawnPoints[randomNumber];
    spawnedKeyItem=Instantiate(keyObject, randomTransform.position, Quaternion.identity,randomTransform);
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