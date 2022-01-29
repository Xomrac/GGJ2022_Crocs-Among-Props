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
         UiManager.Instance.WinPanel.text += $"{TimerManager.Instance.minutes:00}:{TimerManager.Instance.seconds:00}";
         UiManager.Instance.WinPanel.gameObject.SetActive(true);
         Time.timeScale = 0f;
      }
   }
}
