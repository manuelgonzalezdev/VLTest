using UnityEngine;
using UnityEngine.UI;
using VLTest.Game;

namespace VLTest.UI
{

    public class UIFinish : MonoBehaviour
    {

        public string winTitle;
        public string winSubtitle;

        public string loseTitle;
        public string loseSubtitle;

        public Text title;
        public Text subtitle;

        private void OnEnable()
        {
            GameStateManager.GameState current = GameStateManager.currentState;
            title.text = current == GameStateManager.GameState.WIN ? winTitle : loseTitle;
            subtitle.text = current == GameStateManager.GameState.WIN ? winSubtitle : loseSubtitle;
        }

    }
}