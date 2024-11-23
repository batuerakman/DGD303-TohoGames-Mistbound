using UnityEngine;

namespace Shmup
{
    public class ParallaxController : MonoBehaviour
    {
     [SerializeField] private Transform[] backgrounds;
     [SerializeField] float smoothing = 10f;
     [SerializeField] float multiplier = 15f;

     Transform cam;
     Vector3 previousCamPosition;
    void Awake() => cam = Camera.main.transform;

    void Start() => previousCamPosition = cam.position;

    void Update()
    {
        // Apply parallax to all backgrounds except the last one
        for (int i = 0; i < backgrounds.Length - 1; i++)
        {
            var parallax = (previousCamPosition.y - cam.position.y) * (multiplier / (backgrounds.Length - i));
            var targetPosition = backgrounds[i].position + new Vector3(0, parallax, 0);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        // Make the last background follow the camera exactly
        int lastIndex = backgrounds.Length - 1;
        backgrounds[lastIndex].position = new Vector3(backgrounds[lastIndex].position.x, cam.position.y, backgrounds[lastIndex].position.z);
        
        previousCamPosition = cam.position;
    }
}
}