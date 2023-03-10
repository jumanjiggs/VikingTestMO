using CodeBase.Infrastructure.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Logic
{
    public class UIGameplay : MonoBehaviour
    {
        public GameObject loseUI;
        public TextMeshProUGUI lastScore;
        public TextMeshProUGUI textScore;
        public Button restartButton;
        public Button exitButton;
        private int _uiScoreKills;

        private EventsHolder EventsHolder => EventsHolder.Instance;

        private void Start()
        {
            AddListeners();
            UpdateScoreKills();
        }

        private void AddListeners()
        {
            restartButton.onClick.AddListener(RestartScene);
            exitButton.onClick.AddListener(ExitGame);
        }
        private void OnEnable()
        {
            EventsHolder.PlayerDie += ShowLoseUI;
            EventsHolder.EnemyDie += IncreaseScore;
        }

        private void OnDisable()
        {
            EventsHolder.PlayerDie -= ShowLoseUI;
            EventsHolder.EnemyDie -= IncreaseScore;
        }
        private void IncreaseScore()
        {
            _uiScoreKills++;
            UpdateScoreKills();
        }

        private void UpdateScoreKills() => 
            textScore.text = "Scores : " +  _uiScoreKills;

        private void ShowLoseUI()
        {
            ActivateCursor();
            loseUI.SetActive(true);
            lastScore.text = "Scores : " + _uiScoreKills;
        }

        private void RestartScene() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        private void ExitGame() => 
            Application.Quit();
        private void ActivateCursor() => 
            Cursor.lockState = CursorLockMode.None;
    }
}