using System;
using System.Collections;
using Aura2API;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using XInputDotNetPure;

public class ButtonFunctions : MonoBehaviour
{
	public static ButtonFunctions instance;

	private void Awake()
	{
		instance = this;
		GamePad.SetVibration(0,0,0);
	}

	public AudioMixer mixer;
	[FormerlySerializedAs("SettingPanel")] [FormerlySerializedAs("SettingPannel")] public GameObject settingPanel;
	[FormerlySerializedAs("camera")] public CinemachineFreeLook mainCamera;
	public AuraCamera auraCamera;
	public GameObject sun;
	public GameObject difficultyPanel;
	public GameObject mainMenuPanel;
	public GameObject postProcessVolume;

	public bool vibrationOn=true;
	
	public void ExitGame() {
		Application.Quit();
	}

	public void TurnToPlayLevel()
	{
		StartCoroutine(StartTimeTrial());
	}

	public void TurnToMainMenu()
	{
		StartCoroutine(ToMainMenu());
	}

	public IEnumerator StartTimeTrial() 
	{
		TransitionManager.instance.animator.SetTrigger("Fade");
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("Cose3D");
	}	
	public IEnumerator ToMainMenu()
	{
		Time.timeScale = 1f;
		TransitionManager.instance.animator.SetTrigger("Fade");
		yield return new WaitForSecondsRealtime(1);
		SceneManager.LoadScene("Main Menu");
	}

	public void SetMixerVolume(float slider) {
		mixer.SetFloat("Volume",(float)Math.Log10(slider)*20f);
	}

	public void OpenSetting() {
		settingPanel.SetActive(!settingPanel.activeSelf);
	}

	public void MouseSensitivity(float slider) {
		mainCamera.m_XAxis.m_MaxSpeed = slider;
	}

	public void MouseInvert(bool b) {
		mainCamera.m_XAxis.m_InvertInput = b;
	}
	
	public void TurnLowGraphics(bool checkValue)
	{
		sun.SetActive(!checkValue);
		auraCamera.enabled = !checkValue;
		postProcessVolume.SetActive(!checkValue);
	}

	public void TurnVibration(bool checkValue)
	{
		vibrationOn = checkValue;
	}

	public void DifficultySetting() {
		mainMenuPanel.SetActive(false);
		difficultyPanel.SetActive(true);
		EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(difficultyPanel.GetComponentInChildren<Button>().gameObject);
	}

	public void setEasy() {
		diffucltySettingKeeper.Instance.currentDifficulty = diffucltySettingKeeper.Instance.easy;
	}
	public void setMedium() {
		diffucltySettingKeeper.Instance.currentDifficulty = diffucltySettingKeeper.Instance.medium;
	}public void setHard() {
		diffucltySettingKeeper.Instance.currentDifficulty = diffucltySettingKeeper.Instance.hard;
	}
}
