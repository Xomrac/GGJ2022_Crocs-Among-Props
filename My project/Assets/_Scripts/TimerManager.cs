using System;
using System.Collections;
using Riutilizzabile;
using UnityEngine;

namespace GGJ {

	public class TimerManager : Singleton<TimerManager> {
		public float timerForDeath;
		public float timeToSpwanEnemy;
		public float currentTime;
		public Action GameOver;
		public Action EnemyCalled;
		public int SecondRemaning;
		 public int MinutesRemaning;
		private void Start() {
			StartCoroutine(StartLevelTimer());
			GameOver += (() => StopTimer());
		}


		public IEnumerator DeathTimer() {
			UiManager.Instance.Timer.SetActive(true);
			
			while (true) {
				currentTime += 1;
				SecondRemaning = (int)timerForDeath - (int)currentTime;
				Debug.Log(SecondRemaning);
				MinutesRemaning =SecondRemaning/ 60;
				Debug.Log(MinutesRemaning);
				SecondRemaning -= MinutesRemaning * 60;
				UiManager.Instance.Timer.GetComponentInChildren<TMPro.TMP_Text>().text = $"Time Remaning: {MinutesRemaning}:{SecondRemaning}";
				yield return new WaitForSeconds(1f);
			}
		}
		public IEnumerator StartLevelTimer() {
			UiManager.Instance.Timer.SetActive(true);
			StartCoroutine(DeathTimer());
			yield return new WaitForSeconds(timerForDeath);
			if (GameOver!=null) {
				GameOver();
			}
		}

		public IEnumerator CallEnemy() {
			yield return new WaitForSeconds(timeToSpwanEnemy);
			if (EnemyCalled!=null) {
				EnemyCalled();

			}
		}

		public void StopTimer() {
			StopAllCoroutines();
			UiManager.Instance.Timer.SetActive(false);
			currentTime = 0;
		}
	}

}