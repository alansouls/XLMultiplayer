
using UnityEngine;

namespace XLMultiplayer.Extra.GameOfSkate
{
    public class GameSkateUI : MonoBehaviour
    {
        public GameOfSkateManager GameOfSkateManagerInstance;

        void Start()
        {
            GameOfSkateManagerInstance = new GameOfSkateManager();
            TrickManager.Instance.onComboEnded += OnComboEnded;
        }

        void Update()
        {
            if (GameOfSkateManagerInstance == null)
                return;

            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameOfSkateManagerInstance.PrepareSetOrCopyTrick();
            }
            else if (Input.GetKeyDown(KeyCode.F5))
            {
                GameOfSkateManagerInstance.UnsetCurrentTrick();
            }
            else if (Input.GetKeyDown(KeyCode.F6))
            {
                GameOfSkateManagerInstance.UnsetCurrentTrick();
                GameOfSkateManagerInstance.NextPlayer();
            }
        }

        public void OnGUI()
        {
            var maxPhraseWidht = 500;
            var scoreBoardY = 25;
            var scoreBoardOffsetY = 25;
            int scoreBoardX = Screen.width / 2 - maxPhraseWidht;
            GUI.Label(new Rect(scoreBoardX, scoreBoardY, maxPhraseWidht, 30), "GAME OF SKATE SCOREBOARD");
            scoreBoardY += scoreBoardOffsetY;
            for (int i = 0; i < GameOfSkateManagerInstance.PlayerCount; ++i)
            {
                char currentPlayerChar = i == GameOfSkateManagerInstance.CurrentPlayerTurn ? '*' : '\0';
                GUI.Label(new Rect(scoreBoardX, scoreBoardY, maxPhraseWidht, 30), $"{currentPlayerChar} Player {i + 1}: {GameOfSkateManagerInstance.GameWord.Substring(0, GameOfSkateManagerInstance.PlayerLetters[i])}");
                scoreBoardY += scoreBoardOffsetY;
            }

            var currentTrickY = 25;
            var currentTrickOfssetY = 25;
            int currentTrickX = Screen.width / 2;
            GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Press F6 to pass turn to next player (also unsets current trick)");
            currentTrickY += currentTrickOfssetY;
            if (!GameOfSkateManagerInstance.IsSettingTrick && !GameOfSkateManagerInstance.IsCopyingTrick)
            {
                GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Press F1 to set or copy a trick");
                currentTrickY += currentTrickOfssetY;
            }
            if (GameOfSkateManagerInstance.IsTrickSet)
            {
                GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), "Press F5 to unset the current trick");
                currentTrickY += currentTrickOfssetY;
                GUI.Label(new Rect(currentTrickX, currentTrickY, maxPhraseWidht, 30), $"Current Trick: {GameOfSkateManagerInstance.CurrentTrick}");
                currentTrickY += currentTrickOfssetY;
            }
            else if (GameOfSkateManagerInstance.IsSettingTrick)
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

        private void OnComboEnded(TrickCombo trickCombo)
        {
            if (GameOfSkateManagerInstance.IsCopyingTrick)
                GameOfSkateManagerInstance.VerifiyTrickCopied(trickCombo);
            else if (GameOfSkateManagerInstance.IsSettingTrick)
                GameOfSkateManagerInstance.VerifyTrickSet(trickCombo);
        }
    }
}
