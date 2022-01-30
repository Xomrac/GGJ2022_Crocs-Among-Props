using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using XInputDotNetPure;

namespace GGJ
{
   public class PartnerController : MonoBehaviour
   {
      public AudioClip winSound;
      public float timeToReproduceSound;
      private PlayerIndex playerIndex;
      private GamePadState currentState;
      private GamePadState previousState;
      public float timeVibration;
      private Coroutine currentCoroutine;
      public bool isPausing;
      public GameObject player;
      public float maxDistanceTreshold;
      public float lessThanMaxDistanceTreshold;
      public float mediumDistanceTreshold;
      public float lessThanMediumDistanceTreshold;
      public float minDistanceTreshold;
      private float dxmotor;
      public float multiplier;
      public GameObject lightCue;
      public GameObject particleCue;

      private void FindMultiplier() {
         float temp;
         temp = Vector3.Distance(player.transform.position, transform.position);
         Debug.Log(temp);
         if (temp<maxDistanceTreshold) {
            multiplier = 0f;
         }
         if (temp<lessThanMaxDistanceTreshold) {
            multiplier = 0.25f;
         }
         if (temp<mediumDistanceTreshold) {
            multiplier = 0.5f;
         }
         if (temp<lessThanMediumDistanceTreshold) {
            multiplier = 0.75f;
         }
         if (temp<minDistanceTreshold) {
            multiplier = 1f;
         }
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

      IEnumerator SetControllerVibration()
      {
         Debug.Log("brrrr");
         if (ButtonFunctions.instance.vibrationOn)
         {
            GamePad.SetVibration(playerIndex,-dxmotor*multiplier,dxmotor*multiplier);
         }
         yield return new WaitForSeconds(timeVibration);
         GamePad.SetVibration(playerIndex, 0, 0);
         currentCoroutine = null;

      }

      private void Start()
      {
         StartCoroutine(PlaySound());
         player = FindObjectOfType<GGJ.PlayerMovement>().gameObject;
         if (diffucltySettingKeeper.Instance.currentDifficulty.lightOn) {
            lightCue.SetActive(true);
            particleCue.SetActive(true);
         }
         else {
            lightCue.SetActive(false);
            particleCue.SetActive(false);
         }
         timeToReproduceSound = diffucltySettingKeeper.Instance.currentDifficulty.timeSoundClue;
         TimerManager.Instance.timerForDeath = diffucltySettingKeeper.Instance.currentDifficulty.Timer;

      }

      private IEnumerator PlaySound()
      {
         yield return new WaitForSeconds(timeToReproduceSound);
         while (true)
         {
            if (!isPausing) {
               GetComponent<AudioSource>().Play();
               if (currentCoroutine == null) {
                  Debug.Log("vibra");
                  currentCoroutine = StartCoroutine(SetControllerVibration());
               }
            }
            yield return new WaitForSeconds(timeToReproduceSound);
         }
         // ReSharper disable once IteratorNeverReturns
      }

      private void Update() {
         isPausing = UiManager.Instance.optionPanel.activeSelf;
         dxmotor = findIfLFet(player.transform.forward, transform.position, player.transform.up);
         FindMultiplier();
         if (isPausing) {
            GamePad.SetVibration(playerIndex, 0, 0);
         }
      }

      private void OnTriggerEnter(Collider other)
      {
         if (other.CompareTag("Player"))
         {
            TimerManager.Instance.StopAllCoroutines();
            TimerManager.Instance.OSTSource.Stop();
            TimerManager.Instance.source.PlayOneShot(winSound,0.3f);
            UiManager.Instance.winText.text += $" Time Remaning: {TimerManager.Instance.minutes:00}:{TimerManager.Instance.seconds:00}";
            UiManager.Instance.winPanel.gameObject.SetActive(true);
            GamePad.SetVibration(playerIndex, 0, 0);
           
               EventSystem.current.SetSelectedGameObject(UiManager.Instance.winPanelFirstElement);
            
            Cursor.visible = true;
            Time.timeScale = 0f;
         }
      }

      private void OnApplicationQuit() {
         GamePad.SetVibration(playerIndex, 0, 0);
      }
   }

}
