using UnityEngine;
using UnityEngine.UI;
using VLTest.Game.Level;

namespace VLTest.UI
{
    public class UITitanStatus : MonoBehaviour
    {
        public GameObject titanPanel;

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
        
    }
}