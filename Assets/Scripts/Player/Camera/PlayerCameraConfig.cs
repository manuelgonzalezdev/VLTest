using UnityEngine;

namespace VLTest.Player.Cameras
{
    [CreateAssetMenu(menuName = "Player/Camera Config")]
    public class PlayerCameraConfig : ScriptableObject
    {
        #region MEMBERS
        public float horizontalSensibility = 1;
        public float verticalSensibility = 1;
        public float distanceToPivot = 0;
        public float maxY = 3;
        public float minY = -1;
        #endregion
    }
}