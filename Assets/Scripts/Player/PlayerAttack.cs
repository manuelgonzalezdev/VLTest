using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Player.Cameras;

namespace VLTest.Player
{

    public class PlayerAttack : PlayerComponent
    {
        private const float MAX_RAYCAST_DISTANCE = 100f;

        public string enemyLayer = "Enemy";

        private Transform currentCameraTransform
        {
            get { return player.cameraController.currentCamera.transform; }
        }
        private Weapon currentWeapon;
        private float remainingCooldown;
        private bool weaponReady
        {
            get { return remainingCooldown <= 0; }
        }
        private int enemyLayerMask;

        private void Awake()
        {
            enemyLayerMask = LayerMask.GetMask("Enemy");
        }

        private void Update()
        {
            if (player.input.fire && weaponReady)
            {
                Fire();
            }
            if (!weaponReady)
            {
                remainingCooldown -= Time.deltaTime;
            }
        }

        void Fire()
        {
            Transform cameraTransform = currentCameraTransform;
            RaycastHit hit;
            float distance;
            Vector3 hitPosition;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, MAX_RAYCAST_DISTANCE, enemyLayerMask))
            {
                hitPosition = hit.point;
                distance = hit.distance;
            }
            remainingCooldown = currentWeapon.firingRate;
        }

        private void OnEnable()
        {
            player.stats.OnWeaponChanged += OnWeapongChanged;
            currentWeapon = player.stats.currentWeapon;
        }

        private void OnDisable()
        {
            player.stats.OnWeaponChanged -= OnWeapongChanged;
        }

        private void OnWeapongChanged(Weapon weapon)
        {
            currentWeapon = weapon;
            remainingCooldown = currentWeapon.firingRate;
        }

    }
}