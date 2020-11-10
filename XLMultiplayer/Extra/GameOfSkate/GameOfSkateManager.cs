using System.Collections.Generic;
using System.Linq;
using XLMultiplayerServer.Extra.GameOfSkate;

namespace XLMultiplayer.Extra.GameOfSkate
{
    public class GameOfSkateManager
    {
        public Dictionary<string, int> Players;

        public bool IsTrickSet { get; set; }

        public string CurrentTrick { get; set; }

        public bool IsSettingTrick { get; set; }

        public bool IsCopyingTrick { get; set; }

        public string GameWord { get; set; }

        public int PlayerCount { get; set; }

        public string CurrentPlayerTurn;

        public bool IsTrickCopied { get; set; }

        public List<string> TricksDone { get; set; }

        public bool WasTrickRepeated { get; set; }

        public bool IsLastChance { get; set; }

        public GameOfSkateManager()
        {
            Reset();
        }

        public void Reset()
        {
            IsLastChance = false;
            WasTrickRepeated = false;
            IsTrickSet = false;
            CurrentTrick = "";
            IsSettingTrick = false;
            IsCopyingTrick = false;
            IsTrickCopied = false;
            GameWord = "SKATE";
            TricksDone = new List<string>();
        }

        public void PrepareSetOrCopyTrick()
        {
            if (IsSettingTrick || IsCopyingTrick)
                return;
            if (IsTrickSet)
            {
                WasTrickRepeated = false;
                IsCopyingTrick = true;
            }
            else
            {
                IsSettingTrick = true;
            }
        }
    }
}
