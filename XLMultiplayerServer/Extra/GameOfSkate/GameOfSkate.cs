using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLMultiplayerServer.Extra.GameOfSkate
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
            Players = new Dictionary<string, int>();
        }

        /// <summary>
        /// Returns if game is ended
        /// </summary>
        /// <param name="trickCombo"></param>
        /// <returns></returns>
        public void VerifiyTrickCopied(TrickComboMessage trickCombo)
        {
            IsCopyingTrick = false;
            IsTrickSet = false;
            var copiedTrick = trickCombo.Trick;
            if (copiedTrick == CurrentTrick && trickCombo.Landed)
            {
                IsTrickCopied = true;
            }
            else
            {
                Players[CurrentPlayerTurn] += 1;
                if (Players[CurrentPlayerTurn] >= GameWord.Length && IsLastChance)
                {
                    if (Players.Where(s => s.Value == GameWord.Length).Count() == PlayerCount - 1)
                    {
                        Reset();
                        return;
                    }
                }
                else if (Players[CurrentPlayerTurn] >= GameWord.Length)
                {
                    IsTrickSet = true;
                    Players[CurrentPlayerTurn] -= 1;
                    IsLastChance = true;
                    return;
                }
                IsTrickCopied = false;
            }
            CurrentTrick = "";
            NextPlayer();
        }

        public void VerifyTrickSet(TrickComboMessage trickCombo)
        {
            IsSettingTrick = false;
            var trickName = trickCombo.Trick;
            var wasTrickRepeated = TricksDone.Contains(trickName);
            if (trickCombo.Landed && !wasTrickRepeated)
            {
                IsTrickSet = true;
                CurrentTrick = trickName;
                TricksDone.Add(CurrentTrick);
            }
            WasTrickRepeated = wasTrickRepeated && trickCombo.Landed;
            NextPlayer();
        }

        public void NextPlayer()
        {
            var nextIndex = Players.Keys.ToList().IndexOf(CurrentPlayerTurn) + 1;
            nextIndex = nextIndex >= PlayerCount ? 0 : nextIndex;
            CurrentPlayerTurn = Players.Keys.ElementAt(nextIndex);
        }

        public void UnsetCurrentTrick()
        {
            if (!IsTrickSet)
                return;
            IsTrickSet = false;
            IsSettingTrick = false;
            IsCopyingTrick = false;
            TricksDone.RemoveAt(TricksDone.Count - 1);
        }

        public bool AddPlayer(string player)
        {
            if (player == null || Players.Values.Where(v => v > 0).Any() || Players.Keys.Contains(player))
                return false;
            Players.Add(player, 0);
            PlayerCount++;
            if (PlayerCount == 1)
                CurrentPlayerTurn = player;
            return true;
        }
    }
}
