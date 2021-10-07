using Managers;
using TMPro;
using UnityEngine;

namespace UI.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public TMP_Text playerScore;

        private void Update()
        {
            playerScore.text = GameManager.Instance.Score.ToString();
        }
    }
}
