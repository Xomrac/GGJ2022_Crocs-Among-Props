
using Riutilizzabile;
using UnityEngine;
using TMPro;

namespace GGJ
{

    public class UiManager : Singleton<UiManager>
    {
        public GameObject DeathPanel;
        public GameObject optionPanel;
        public TextMeshProUGUI Timer;
        public TextMeshProUGUI WinPanel;

        // Start is called before the first frame update
        void Start()
        {
            TimerManager.Instance.GameOver += () => DeathPanel.SetActive(true);
        }

    }

}
