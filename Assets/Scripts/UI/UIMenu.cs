using UnityEngine;
using UnityEngine.SceneManagement;
using VLTest.Game;

namespace VLTest.UI
{
    public class UIMenu : MonoBehaviour
    {
        #region MEMBERS
        public Scenes scenes;
        #endregion

        #region PUBLIC METHODS
        public void PlayGame()
        {
            SceneManager.LoadScene(scenes.gameScene);
        }

        public void Exit()
        {
            Application.Quit();
        }
        #endregion
    }
}