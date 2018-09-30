using UnityEngine;
using VLTest.Game.Level;

namespace VLTest.UI
{
    public class UITitanStatus : MonoBehaviour
    {
        #region MEMBERS
        public GameObject titanPanel;
        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            titanPanel.SetActive(false);
        }

        private void OnEnable()
        {
            
            LevelController.OnTitanSpawned += OnTitanSpawned;
        }

        private void OnDisable()
        {
            LevelController.OnTitanSpawned -= OnTitanSpawned;
        }

        private void OnTitanSpawned()
        {
            titanPanel.SetActive(true);
        }
        #endregion
    }
}