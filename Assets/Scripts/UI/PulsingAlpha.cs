using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PulsingAlpha : MonoBehaviour
    {
        public float duration = 1.0f;
        public float initialDelay = 1.0f; // час затримки перед початком анімації
        private TextMeshProUGUI textMeshPro;
        private Color originalColor;

        void Start()
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
                return;
            }
            originalColor = textMeshPro.color;
            SetAlpha(0.0f); // встановлюємо початкову альфу в 0
            StartCoroutine(StartWithDelay());
        }

        void SetAlpha(float alpha)
        {
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }

        IEnumerator StartWithDelay()
        {
            yield return new WaitForSeconds(initialDelay);
            StartCoroutine(Pulse());
        }

        IEnumerator Pulse()
        {
            while (true)
            {
                yield return StartCoroutine(FadeTo(0.0f, duration / 2));
                yield return StartCoroutine(FadeTo(1.0f, duration / 2));
            }
        }

        IEnumerator FadeTo(float targetAlpha, float duration)
        {
            float startAlpha = textMeshPro.color.a;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
        }
    }
}