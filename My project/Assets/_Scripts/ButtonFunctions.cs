using Aura2API;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour {
	public AudioMixer mixer;
	public GameObject SettingPannel;
	public CinemachineFreeLook camera;
	public AuraCamera auraCamera;
	public GameObject sun;
	public GameObject postProcessVolume;
	
	public void exitGame() {
		Application.Quit();
	}

	public void StartTimeTrial() {
		SceneManager.LoadScene(1);
	}	
	public void ToMainMenu() {
		SceneManager.LoadScene(0);
	}

	public void volume(float slider) {
		mixer.SetFloat("Volume",slider);
	}

	public void OpenSetting() {
		SettingPannel.SetActive(!SettingPannel.activeSelf);
	}

	public void MouseSensitivity(float slider) {
		camera.m_XAxis.m_MaxSpeed = slider;
	}

	public void MouseInvert(bool b) {
		camera.m_XAxis.m_InvertInput = b;
	}
	
	public void TurnLowGraphics(bool check)
	{
		sun.SetActive(!check);
		auraCamera.enabled = !check;
		postProcessVolume.SetActive(!check);
	}

	public void ps4Controller(bool check) {
		if (check) {
			
		}
	}
	
}
