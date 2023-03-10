using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader : MonoBehaviour
    {
        private void Start()
        {
            StartGame();
        }

        private void StartGame() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}