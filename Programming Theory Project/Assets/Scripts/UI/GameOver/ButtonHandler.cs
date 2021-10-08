using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.GameOver
{
    public class ButtonHandler : MonoBehaviour
    {
        public Button retryButton;
        public Button mainMenuButton;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            retryButton.onClick.AddListener(OnRetryButtonClick);
            mainMenuButton.onClick.AddListener(OnMainMenuClick);
        }
        
        private void OnMainMenuClick() => SceneManager.LoadScene(0);

        private void OnRetryButtonClick() => SceneManager.LoadScene(1);
    }
}
