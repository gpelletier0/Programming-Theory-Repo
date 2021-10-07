using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class InputMenu : MonoBehaviour
    {
        public TMP_InputField playerName;
        public Button startButton;
        public Button quitButton;

        private void Start()
        {
            if (playerName == null)
                Debug.LogError($"{nameof(playerName)} {Constants.IsNotSet}");
            
            if(startButton != null)
                startButton.onClick.AddListener(OnStartGame);
            else
                 Debug.LogError($"{nameof(startButton)} {Constants.IsNotSet}");
            
            if(quitButton != null)
                quitButton.onClick.AddListener(OnQuit);
            else
                Debug.LogError($"{nameof(quitButton)} {Constants.IsNotSet}");
        }

        private void OnStartGame()
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
            SceneManager.LoadScene(1);
        }

        private void OnQuit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        }
    }
}