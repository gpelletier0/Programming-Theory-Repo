using Entities;
using Saves;
using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    [RequireComponent(typeof(EnemyManager))]
    public class GameManager : SceneSingleton<GameManager>
    {
        private string _playerName;
        private SaveFile _saveFile;
        
        public int Score { get; private set; }

        protected override void Initialize()
        {
            var playerName = PlayerPrefs.GetString("PlayerName");
            _playerName = string.IsNullOrEmpty(playerName) ? Constants.NewPlayerName : playerName;
            Score = 0;

            var playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            playerHealth.OnDeath += GameOver;
        }

        /// <summary>
        /// Player Died event listener
        /// </summary>
        private void GameOver()
        {
            Debug.Log("Game Over");
            _saveFile = new SaveFile();
            _saveFile.AddNewHiScore(_playerName, Score);

            Time.timeScale = 0;
            //SceneManager.LoadScene(2);
        }

        /// <summary>
        /// Add points to player score
        /// </summary>
        /// <param name="points">points to add</param>
        public void AddPoints(int points)
        {
            Score += points;
        }
    }
}