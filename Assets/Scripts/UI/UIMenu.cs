using UnityEngine;
using UnityEngine.SceneManagement;
using VLTest.Game;

namespace VLTest.UI
{

    public class UIMenu : MonoBehaviour
    {

        public Scenes scenes;
        
        public void PlayGame()
        {
            SceneManager.LoadScene(scenes.gameScene);
        }

        public void Exit()
        {
            Application.Quit();
        }

    }
}