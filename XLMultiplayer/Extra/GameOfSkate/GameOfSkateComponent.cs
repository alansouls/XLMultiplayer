
using System;
using System.Linq;
using UnityEngine;

namespace XLMultiplayer.Extra.GameOfSkate
{
    public class GameOfSkateComponent : MonoBehaviour
    {
        public GameOfSkateManager GameOfSkateManagerInstance;
        public string UserName { get; set; }
        private bool blocked;

        public bool isInGame;

        void Start()
        {
            isInGame = false;
            blocked = false;
            GameOfSkateManagerInstance = null;
        }

        void Update()
        {
            if (GameOfSkateManagerInstance == null || blocked)
                return;

            if (Input.GetKeyDown(KeyCode.F1) && UserName == GameOfSkateManagerInstance.CurrentPlayerTurn)
            {
                GameOfSkateManagerInstance.PrepareSetOrCopyTrick();
            }
        }

        public void OnGUI()
        {
            var maxPhraseWidht = 500;
            var scoreBoardY = 25;
            var scoreBoardOffsetY = 25;
            int scoreBoardX = Screen.width / 2 - maxPhraseWidht;
            if (!isInGame)
            {
                GUI.Label(new Rect(scoreBoardX, scoreBoardY, maxPhraseWidht, 30), "Press F5 to join game of skate");
                return;
            }
            GUI.Label(new Rect(scoreBoardX, scoreBoardY, maxPhraseWidht, 30), "GAME OF SKATE SCOREBOARD");
            scoreBoardY += scoreBoardOffsetY;
            for (int i = 0; i < GameOfSkateManagerInstance.PlayerCount; ++i)
            {
                var player = GameOfSkateManagerInstance.Players.Keys.ElementAt(i);
                char currentPlayerChar = player == GameOfSkateManagerInstance.CurrentPlayerTurn ? '*' : '\0';
                GUI.Label(new Rect(scoreBoardX, scoreBoardY, maxPhraseWidht, 30), $"{currentPlayerChar} {player}: {GameOfSkateManagerInstance.GameWord.Substring(0, GameOfSkateManagerInstance.Players[player])}");
                scoreBoardY += scoreBoardOffsetY;
            }

            var currentTrickY = 25;
            var currentTrickOfssetY = 25;
            int currentTrickX = Screen.width / 2;
            var isCurrentPlayer = GameOfSkateManagerInstance.CurrentPlayerTurn == UserName;
            if (!blocked)
            {
                if (!GameOfSkateManagerInstance.IsSettingTrick && !GameOfSkateManagerInstance.IsCopyingTrick && isCurrentPlayer)
                {
                    GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Press F1 to set or copy a trick");
                    currentTrickY += currentTrickOfssetY;
                }
                if (GameOfSkateManagerInstance.IsTrickSet)
                {
                    GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), $"Current Trick: {GameOfSkateManagerInstance.CurrentTrick}");
                    currentTrickY += currentTrickOfssetY;
                }
                if (GameOfSkateManagerInstance.IsSettingTrick)
                {
                    GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Waiting for trick to be set");
                    currentTrickY += currentTrickOfssetY;
                }
                if (GameOfSkateManagerInstance.IsCopyingTrick)
                {
                    GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Waiting for trick to be copied");
                    currentTrickY += currentTrickOfssetY;
                    if (GameOfSkateManagerInstance.IsLastChance)
                    {
                        GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Last chance to land current trick!");
                        currentTrickY += currentTrickOfssetY;
                    }
                }
                if (GameOfSkateManagerInstance.WasTrickRepeated)
                {
                    GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Couldn't set repeated trick");
                    currentTrickY += currentTrickOfssetY;
                }
            }
            else
            {
                GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Waiting for server...");
                currentTrickY += currentTrickOfssetY;
            }
        }

        public void Unblock(GameOfSkateManager updatedManager)
        {
            GameOfSkateManagerInstance = updatedManager;
            blocked = false;
        }

        public void Block()
        {
            blocked = true;
        }
    }
}
