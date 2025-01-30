using UnityEngine;
using System.Collections;

namespace Shmup
{
    public class HitFlash : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Color originalColor;
        private bool isFlashing = false;
        
        [SerializeField] private float flashDuration = 0.1f;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            }
        }
        
        public void Flash()
        {
            if (!isFlashing && spriteRenderer != null)
            {
                StartCoroutine(FlashCoroutine());
            }
        }
        
        private IEnumerator FlashCoroutine()
        {
            isFlashing = true;
            
            // Set to white
            spriteRenderer.color = Color.white;
            
            float elapsed = 0f;
            while (elapsed < flashDuration)
            {
                elapsed += Time.deltaTime;
                float percentComplete = elapsed / flashDuration;
                
                spriteRenderer.color = Color.Lerp(Color.white, originalColor, percentComplete);
                
                yield return null;
            }
            
            spriteRenderer.color = originalColor;
            isFlashing = false;
        }
    }
} 