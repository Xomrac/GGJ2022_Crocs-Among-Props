using System;
using Aura2API;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ButtonFunctions : MonoBehaviour
{
	public static ButtonFunctions instance;

	private void Awake()
	{
		instance = this;
	}

	public AudioMixer mixer;
	[FormerlySerializedAs("SettingPanel")] [FormerlySerializedAs("SettingPannel")] public GameObject settingPanel;
	[FormerlySerializedAs("camera")] public CinemachineFreeLook mainCamera;
	public AuraCamera auraCamera;
	public GameObject sun;
	public GameObject postProcessVolume;

	public bool vibrationOn=true;
	
	public void ExitGame() {
		Application.Quit();
	}

	public void StartTimeTrial() {
		SceneManager.LoadScene(1);
	}	
	public void ToMainMenu() {
		SceneManager.LoadScene(0);
	}

	public void SetMixerVolume(float slider) {
		mixer.SetFloat("Volume",slider);
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
	
}
