using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button exitButton;
        private void Start() => 
            AddListeners();
        private void AddListeners()
        {
            playButton.onClick.AddListener(LoadGameplayScene);
            exitButton.onClick.AddListener(ExitGame);
        }
        private void LoadGameplayScene() => 
            SceneManager.LoadScene(Constants.Gameplay);
        private void ExitGame() => 
            Application.Quit();
    }
}
