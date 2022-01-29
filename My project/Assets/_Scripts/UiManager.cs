using System;
using System.Collections;
using System.Collections.Generic;
using GGJ;
using Riutilizzabile;
using UnityEngine;

public class UiManager : Singleton<UiManager> {
    public GameObject DeathPanel;
    public GameObject Timer;
    public GameObject WinPanel;
    // Start is called before the first frame update
    void Start() {
        TimerManager.Instance.GameOver += () => DeathPanel.SetActive(true);
    }


}
