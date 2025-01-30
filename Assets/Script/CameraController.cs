using UnityEngine;

namespace Shmup {
    public class CameraController : MonoBehaviour {
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 2f;
        public float Speed => speed;
        void Start() => transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        void LateUpdate() {
            // Move the camera along the battlefield at a constant speed
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}