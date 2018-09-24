using UnityEngine;

namespace VLTest.Enemy
{
    public class Movement : MonoBehaviour
    {
        private Rotator rotator;

        private void Awake()
        {
            rotator = GetComponent<Rotator>();
        }

        private void Start()
        {
            Move();
        }

        void Move()
        {
            Vector3 pivot = transform.position + (transform.forward * 0.5f);
            pivot.y -= 0.5f;
            rotator.Rotate(pivot, transform.right, 90, 100, Move);
        }

    }

}