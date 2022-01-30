using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;

public class diffucltySettingKeeper : SingletonDDOL<diffucltySettingKeeper> {
	public DifficultySetting easy;
	public DifficultySetting medium;
	public DifficultySetting hard;
	public DifficultySetting currentDifficulty;
}