
using Riutilizzabile;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

namespace GGJ
{

    public class UiManager : Singleton<UiManager>
    {
        [FormerlySerializedAs("DeathPanel")] public GameObject deathPanel;
        public GameObject optionPanel;
        [FormerlySerializedAs("Timer")] public TextMeshProUGUI timerElement;
        public TextMeshProUGUI winText;
        [FormerlySerializedAs("WinPanel")] public GameObject winPanel;


        public GameObject optionsFirstElement;
        public GameObject winPanelFirstElement;

        // Start is called before the first frame update
        void Start()
        {
            TimerManager.Instance.GameOver += () => deathPanel.SetActive(true);
        }

    }

}
