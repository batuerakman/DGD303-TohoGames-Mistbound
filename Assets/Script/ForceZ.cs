using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float swayAngle = 30f;
    [SerializeField] private float swaySpeed = 2f;

    void Update()
    {
        // Calculate the sway angle using a sine wave
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAngle;
        
        // Apply the rotation, keeping X and Z at 0, but allowing Y to sway
        transform.rotation = Quaternion.Euler(0, 0, sway);
    }
}
