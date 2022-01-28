using System;
using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;

namespace GGJ {

	public class TimerManager : Singleton<TimerManager> {
		public float timerForDeath;
		public float timeToSpwanEnemy;
		private float currentTime;
		public Action GameOver;
		public Action EnemyCalled;

		private void Start() {
			StartCoroutine(StartLevelTimer());
		}

		public IEnumerator StartLevelTimer() {
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

	}

}