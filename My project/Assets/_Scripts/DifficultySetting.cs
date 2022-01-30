using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySetting_", menuName = "Difficulty Setting/Dificulty")]
public class DifficultySetting : ScriptableObject {

	public bool lightOn;
	public float timeSoundClue;
	public int Timer;
}