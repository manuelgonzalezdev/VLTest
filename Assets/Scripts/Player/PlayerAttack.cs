using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLTest.Enemies;

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
                for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
                {
                    Fire();
                }
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
            Vector3 shotDirection = CreateDispersionShot(cameraTransform, currentWeapon.dispersion);
            if (Physics.Raycast(cameraTransform.position, shotDirection, out hit, MAX_RAYCAST_DISTANCE, enemyLayerMask))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.SetDamage(currentWeapon.damage);
            }
            remainingCooldown = currentWeapon.firingRate;
        }

        private Vector3 CreateDispersionShot(Transform cameraTransform, float dispersionRate)
        {
            Vector2 randomDispersion = Random.insideUnitCircle * dispersionRate;
            Vector3 dispersion = cameraTransform.forward + (cameraTransform.right * randomDispersion.x) + (cameraTransform.up * randomDispersion.y);
            Vector3 dispersionPoint = cameraTransform.position + dispersion;
            Vector3 dispersionDirection = (dispersionPoint - cameraTransform.position).normalized;
            Debug.DrawRay(cameraTransform.position, dispersionDirection * MAX_RAYCAST_DISTANCE, Color.red, 0.5f);
            return dispersionDirection;
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