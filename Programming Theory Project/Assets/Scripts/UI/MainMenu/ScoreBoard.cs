using Saves;
using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class ScoreBoard : MonoBehaviour
    {
        public TMP_Text hiScoreText;
        
        private SaveFile _saveFile;

        private void Start()
        {
            if (hiScoreText == null)
                Debug.LogError($"{nameof(hiScoreText)} {Constants.IsNotSet}");
            else
            {
                _saveFile = new SaveFile();
                hiScoreText.text = _saveFile.GetHiScoreString();
            }
        }
    }
}