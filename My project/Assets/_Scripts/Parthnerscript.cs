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
   private float dxmotor;
   public float multiplier;
   private GameObject player;
   public float maxDistanceTreshold;
   private void Update() {
      dxmotor = findIfLFet(player.transform.forward, transform.position, player.transform.up);
      FindMultiplier();
   }

   private void FindMultiplier() {
      float temp;
      temp = Vector3.Distance(player.transform.position, transform.position);
      Debug.Log(temp);
      multiplier=temp/ maxDistanceTreshold;
   }
   private float findIfLFet(Vector3 fwd, Vector3 targetDir, Vector3 up) {
      Vector3 perp = Vector3.Cross(fwd, targetDir);
      float dir = Vector3.Dot(perp, up);
		
      if (dir > 0f) {
         return 1f;
      } else if (dir < 0f) {
         return -1f;
      } else {
         return 0f;
      }
   }
   
   IEnumerator vibra() {
      Debug.Log("brrrr");
      GamePad.SetVibration(playerIndex,-dxmotor*multiplier,dxmotor*multiplier);
      yield return new WaitForSeconds(timeVibration);
      GamePad.SetVibration(playerIndex,0,0);
      coru = null;

   }
   private void Start() {
      StartCoroutine(soundplay());
      player = FindObjectOfType<GGJ.PlayerMovement>().gameObject;
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
