using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
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
      

      IEnumerator SetControllerVibration()
      {
         Debug.Log("brrrr");
         if (ButtonFunctions.instance.vibrationOn)
         {
            GamePad.SetVibration(playerIndex, .8f, .8f);
         }
         yield return new WaitForSeconds(timeVibration);
         GamePad.SetVibration(playerIndex, 0, 0);
         currentCoroutine = null;

      }

      private void Start()
      {
         StartCoroutine(PlaySound());
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
            TimerManager.Instance.source.PlayOneShot(winSound);
            UiManager.Instance.winText.text += $"{TimerManager.Instance.minutes:00}:{TimerManager.Instance.seconds:00}";
            UiManager.Instance.winPanel.gameObject.SetActive(true);
           
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
