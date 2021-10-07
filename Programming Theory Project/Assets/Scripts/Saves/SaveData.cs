using System;
using System.Collections.Generic;

namespace Saves
{
    [Serializable]
    public class SaveData
    {
        public int nbHiScores = 5;
        public Dictionary<string, int> HiScoresDict = new Dictionary<string, int>();
    }
}
