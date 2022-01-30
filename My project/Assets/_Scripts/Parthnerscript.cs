using System;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using XInputDotNetPure;

public class Parthnerscript : MonoBehaviour {
   public AudioClip winSound;
   public AudioClip suonoAiuto;
   public float timeToReproduceSound;
   PlayerIndex playerIndex;
   GamePadState state;
   GamePadState prevState;
   public float timeVibration;
   private Coroutine coru;
   
   IEnumerator vibra() {
      Debug.Log("brrrr");
      GamePad.SetVibration(playerIndex,1,1);
      yield return new WaitForSeconds(timeVibration);
      GamePad.SetVibration(playerIndex,0,0);
      coru = null;

   }
   private void Start() {
      StartCoroutine(soundplay());
   }

   private IEnumerator soundplay() {
      yield return new WaitForSeconds( timeToReproduceSound);
      while (true) {
         GetComponent<AudioSource>().Play();
         if (coru==null) {
            Debug.Log("vibra");
            coru = StartCoroutine(vibra());
         }
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
