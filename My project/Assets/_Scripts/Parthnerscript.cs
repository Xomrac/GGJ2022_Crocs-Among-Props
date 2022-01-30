using System;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Parthnerscript : MonoBehaviour {
   public AudioClip winSound;
   public AudioClip suonoAiuto;
   public float timeToReproduceSound;

   private void Start() {
      StartCoroutine(soundplay());
   }

   private IEnumerator soundplay() {
      while (true) {
         GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds( timeToReproduceSound);
      }
   }
   private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
         TimerManager.Instance.StopAllCoroutines();
         TimerManager.Instance.OSTSource.Stop();
         TimerManager.Instance.source.PlayOneShot(winSound);
         UiManager.Instance.winText.text += $"{TimerManager.Instance.minutes:00}:{TimerManager.Instance.seconds:00}";
         UiManager.Instance.WinPanel.gameObject.SetActive(true);
         Cursor.visible = true;
         Time.timeScale = 0f;
      }
   }
}
