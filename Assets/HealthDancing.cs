using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shmup
{
    public class HealthDancing : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private float rotationAmplitude = 10f; // Degrees to rotate
        [SerializeField] private float rotationSpeed = 2f;      // How fast it rotates

        [Header("Movement Settings")]
        [SerializeField] private float movementAmplitude = 0.5f; // Horizontal movement range
        [SerializeField] private float movementSpeed = 1f;       // How fast it moves side-to-side

        private Vector3 initialPosition;

        void Start()
        {
            // Store the starting position for movement reference
            initialPosition = transform.localPosition;
        }

        void Update()
        {
            // Rotation dance using sine wave
            float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * rotationAmplitude;
            transform.localEulerAngles = new Vector3(0, 0, rotationAngle);

            // Horizontal movement using cosine wave (offset for different timing)
            float horizontalOffset = Mathf.Cos(Time.time * movementSpeed) * movementAmplitude;
            transform.localPosition = initialPosition + new Vector3(horizontalOffset, 0, 0);
        }
    }
}