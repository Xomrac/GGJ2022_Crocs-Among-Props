using System;
using System.Collections;
using Riutilizzabile;
using TMPro;
using UnityEngine;

namespace GGJ 
{

	public class TimerManager : Singleton<TimerManager> 
	{
		public float timeToSpawnEnemy;
		public Action GameOver;
		public Action EnemyCalled;
		public float timerForDeath;
		public bool timerIsRunning = false;
		public TextMeshProUGUI timeText;
		[HideInInspector]public float minutes;
		[HideInInspector]public float seconds;

		private void Start() {
			// StartCoroutine(StartLevelTimer());
			Time.timeScale = 1f;
			timerIsRunning = true;
			// GameOver += (() => StopTimer());
			GameOver += (() => Time.timeScale = 0f);

			timeText = UiManager.Instance.Timer;
			timeText.gameObject.SetActive(true);
		}

		// public IEnumerator DeathTimer() {
		// 	UiManager.Instance.Timer.SetActive(true);
		//
		// 	while (true) 
		// 	{
		// 		timerForDeath -= Time.deltaTime;
		// 		
		// 		yield return new WaitForSeconds(1f);
		// 	}
		// }

		private void Update()
		{
			if (timerIsRunning)
			{
				if (timerForDeath > 0)
				{
					timerForDeath -= Time.deltaTime;
					DisplayTime(timerForDeath);
				}
				else
				{
					timerForDeath = 0;
					timerIsRunning = false;
					GameOver();
				}
			}
		}

		void DisplayTime(float timeToDisplay)
		{
			timeToDisplay += 1f;

			 minutes = Mathf.FloorToInt(timeToDisplay / 60); 
			 seconds = Mathf.FloorToInt(timeToDisplay % 60);

			timeText.text= "Time Remaining: " + $"{minutes:00}:{seconds:00}";
		}

		// public IEnumerator StartLevelTimer() {
		// 	UiManager.Instance.Timer.SetActive(true);
		// 	StartCoroutine(DeathTimer());
		// 	yield return new WaitForSeconds(timerForDeath);
		// 	if (GameOver != null) {
		// 		GameOver();
		// 	}
		// }

		public IEnumerator CallEnemy() {
			yield return new WaitForSeconds(timeToSpawnEnemy);
			if (EnemyCalled != null) {
				EnemyCalled();
			}
		}

		// public void StopTimer() {
		// 	StopAllCoroutines();
		// 	UiManager.Instance.Timer.SetActive(false);
		// 	currentTime = 0;
		// }
	}

}