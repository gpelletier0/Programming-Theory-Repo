using System;
using System.IO;
using System.Linq;
using System.Text;
//using Newtonsoft.Json;
using UnityEngine;

namespace Saves
{
    public class SaveFile
    {
        /// <summary>
        /// Add new hi score to file
        /// </summary>
        /// <param name="name">name of player</param>
        /// <param name="score">player score</param>
        public void AddNewHiScore(string name, int score)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("PlayerName is empty");
                return;
            }

            var data = ReadSaveData();
            if (data != null)
            {
                if (data.HiScoresDict.ContainsKey(name))
                {
                    if (data.HiScoresDict[name] < score)
                        data.HiScoresDict[name] = score;
                }
                else
                {
                    data.HiScoresDict.Add(name, score);
                }
            }
            else
            {
                data = new SaveData();
                data.HiScoresDict.Add(name, score);
            }
            
            WriteSaveData(data);
        }

        /// <summary>
        /// Reads hi scores from json file
        /// </summary>
        private SaveData ReadSaveData()
        {
            SaveData data = null;
            string path = Application.persistentDataPath + "/savefile.json";
            
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                //data = JsonConvert.DeserializeObject<SaveData>(json);
                TrimHiScores(ref data);
            }

            return data;
        }

        /// <summary>
        /// Writes hi scores to json file
        /// </summary>
        private void WriteSaveData(SaveData data)
        {
            TrimHiScores(ref data);
            //string json = JsonConvert.SerializeObject(data);
            //File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        /// <summary>
        /// Removes lowest hi scores from save data keeping a set number of hi scores
        /// </summary>
        private void TrimHiScores(ref SaveData data)
        {
            if (data?.HiScoresDict == null || data.HiScoresDict.Count == 0) return;

            data.HiScoresDict = data.HiScoresDict
                .OrderByDescending(x => x.Value)
                .Take(data.nbHiScores)
                .ToDictionary(k => k.Key, v => v.Value);
        }

        /// <summary>
        /// Returns a string of hi scores with newlines
        /// </summary>
        /// <returns>hi scores string</returns>
        public string GetHiScoreString()
        {
            var data = ReadSaveData();
            if (data != null)
            {
                StringBuilder hiScores = new StringBuilder();
                if (data.HiScoresDict.Count > 0)
                {
                    foreach (var kp in data.HiScoresDict)
                        hiScores.Append($"{kp.Key} : {kp.Value}{Environment.NewLine}");
                }
                return hiScores.ToString();
            }

            return Constants.NoHiScores;
        }
    }
}