using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace GGJ
{
  [ExecuteInEditMode]
public class SpawnManager : MonoBehaviour
{
  public List<GameObject> spawnableObjects;
  public int objectsToSpawn;
  public float throwForce;
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
	  foreach(GameObject spawnedObject in spawnedObjects)
	  {
		  var rb=spawnedObject.GetComponent<Rigidbody>();
      Vector3 eulerAngles = transform.eulerAngles;
      eulerAngles = new Vector3(eulerAngles.x, Random.Range(0, 360), eulerAngles.z);
      Transform transform1 = transform;
      transform1.eulerAngles = eulerAngles;
      rb.isKinematic = false;
     Vector3 force = transform1.forward;
     force = new Vector3(force .x, 1, force .z);
     rb.AddForce(force * throwForce );
	  }
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