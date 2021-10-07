using Saves;
//using Entities;
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

        private int _score;

        protected override void Initialize()
        {
            var playerName = PlayerPrefs.GetString("PlayerName");
            _playerName = string.IsNullOrEmpty(playerName) ? Constants.NewPlayerName : playerName;
            _score = 0;

            //var playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            //playerHealth.OnDeath += GameOver;
        }

        /// <summary>
        /// Player Died event listener
        /// </summary>
        private void GameOver()
        {
            Debug.Log("Game Over");
            _saveFile = new SaveFile();
            _saveFile.AddNewHiScore(_playerName, _score);

            SceneManager.LoadScene(2);
        }

        /// <summary>
        /// Add points to player score
        /// </summary>
        /// <param name="points">points to add</param>
        public void AddPoints(int points)
        {
            _score += points;
        }
    }
}