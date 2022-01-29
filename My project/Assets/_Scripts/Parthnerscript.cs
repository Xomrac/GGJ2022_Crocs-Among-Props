using System;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Parthnerscript : MonoBehaviour {
   public AudioClip sound;
   public float timeToReproduceSound;

   private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
         TimerManager.Instance.StopAllCoroutines();
         UiManager.Instance.WinPanel.GetComponentInChildren<TMP_Text>().text +=$"\nwith {TimerManager.Instance.MinutesRemaning} minutes and {TimerManager.Instance.SecondRemaning} seconds" ;
         UiManager.Instance.WinPanel.SetActive(true);
         Time.timeScale = 0f;
      }
   }
}
