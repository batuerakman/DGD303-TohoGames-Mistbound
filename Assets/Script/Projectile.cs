using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 3f;
        [SerializeField] private bool addUpwardMovement = true;  // Add this for radial projectiles

        private Vector2 direction;
        private float cameraSpeed;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Projectile spawned"); // Debug log to verify script is running
            Destroy(gameObject, lifetime);
            
            // Get initial direction from the object's rotation
            float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
            
            // Get camera speed from CameraController if it exists
            var camera = Camera.main;
            if (camera != null)
            {
                var cameraController = camera.GetComponent<CameraController>();
                if (cameraController != null)
                {
                    cameraSpeed = cameraController.Speed;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Move in the direction based on rotation
            Vector2 movement = direction * speed;
            
            // Add upward movement to match camera if needed
            if (addUpwardMovement)
            {
                movement += Vector2.up * cameraSpeed;
            }
            
            transform.position += (Vector3)(movement * Time.deltaTime);
        }

        // Keep the 2D collision method
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"2D Trigger hit: {other.gameObject.name}");
            HandleCollision(other.gameObject);
        }

        // Add 3D collision method
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"3D Trigger hit: {other.gameObject.name}");
            HandleCollision(other.gameObject);
        }

        private void HandleCollision(GameObject hitObject)
        {
            Health health = hitObject.GetComponent<Health>();
            if (health != null)
            {
                Debug.Log($"Dealing damage to: {hitObject.name}");
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"No Health component found on: {hitObject.name}");
            }
        }
    }
}
