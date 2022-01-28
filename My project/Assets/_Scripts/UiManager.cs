using System.Collections;
using System.Collections.Generic;
using GGJ;
using Riutilizzabile;
using UnityEngine;

public class UiManager : Singleton<UiManager> {
    public Canvas DeathCanvas;
    // Start is called before the first frame update
    void Start() {
        TimerManager.Instance.GameOver += () => DeathCanvas.gameObject.SetActive(true);
    }

}
