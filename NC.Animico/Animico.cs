using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace NC.Animico
{
    public static class Animico
    {
        #region Ease Types
        // Linear easing type
        public static float Linear(float t)
        {
            return t;
        }

        // Ease In
        public static float EaseIn(float t)
        {
            return t * t;
        }

        // Ease Out
        public static float EaseOut(float t)
        {
            return t * (2 - t);
        }
        
        // Ease In Out
        public static float EaseInOut(float t)
        {
            return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
        }
        #endregion

        #region Kinetic Methods

        /// <summary>
        /// Animates an object along the X axis.
        /// </summary>
        /// <param name="target">The target object to animate (Transform (3D) or RectTransform (2D/UI).</param>
        /// <param name="end">The target position on the X axis.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the animation. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        public static void AniX(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniXCoroutine(target, end, duration, easingFunction, onComplete));
        }

        private static IEnumerator AniXCoroutine(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            if (!(target is Transform) && !(target is RectTransform))
            {
                throw new ArgumentException("Target must be of type Transform or RectTransform");
            }
            
            float start;
            float time = 0;

            if (target is RectTransform)
            {
                RectTransform rectTransform = target as RectTransform;
                start = rectTransform.anchoredPosition.x;

                while (time < duration)
                {
                    time += Time.deltaTime;
                    float t = time / duration;

                    if (easingFunction != null)
                        t = easingFunction(t);

                    float value = Mathf.Lerp(start, end, t);
                    rectTransform.anchoredPosition = new Vector2(value, rectTransform.anchoredPosition.y);

                    yield return null;
                }

                rectTransform.anchoredPosition = new Vector2(end, rectTransform.anchoredPosition.y);
            }
            else
            {
                Transform transform = target as Transform;
                start = transform.position.x;

                while (time < duration)
                {
                    time += Time.deltaTime;
                    float t = time / duration;

                    if (easingFunction != null)
                        t = easingFunction(t);

                    float value = Mathf.Lerp(start, end, t);
                    transform.position = new Vector3(value, transform.position.y, transform.position.z);

                    yield return null;
                }

                transform.position = new Vector3(end, transform.position.y, transform.position.z);
            }

            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        
        /// <summary>
        /// Animates an object along the Y axis.
        /// </summary>
        /// <param name="target">The target object to animate (Transform (3D) or RectTransform (2D/UI).</param>
        /// <param name="end">The target position on the Y axis.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the animation. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the animation.</returns>
        public static void AniY(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniYCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniYCoroutine(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            var rectTransform = target as RectTransform;
            var transform = target as Transform;

            if (rectTransform == null && transform == null)
            {
                throw new ArgumentException("Target must be of type Transform or RectTransform");
            }

            float start = rectTransform != null ? rectTransform.anchoredPosition.y : transform.position.y;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(start, end, t);
        
                if(rectTransform != null)
                {
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, value);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, value, transform.position.z);
                }

                yield return null;
            }

            if(rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, end);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, end, transform.position.z);
            }
    
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }


        /// <summary>
        /// Animates an object along the Z axis.
        /// </summary>
        /// <param name="target">The target object to animate (Transform (3D) or RectTransform (2D/UI).</param>
        /// <param name="end">The target position on the Z axis.</param>
        /// <param name="duration">The duration of the animation in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the animation. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the animation.</returns>
        public static void AniZ(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniZCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniZCoroutine(Component target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            var rectTransform = target as RectTransform;
            var transform = target as Transform;

            if (rectTransform == null && transform == null)
                throw new ArgumentException("The target component must be of type RectTransform or Transform.");

            float start = rectTransform != null ? rectTransform.anchoredPosition3D.z : transform.position.z;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(start, end, t);

                if (rectTransform != null)
                    rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, value);
                else
                    transform.position = new Vector3(transform.position.x, transform.position.y, value);

                yield return null;
            }

            if (rectTransform != null)
                rectTransform.anchoredPosition3D = new Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, end);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, end);
    
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }


        /// <summary>
        /// Scales an object along the X axis.
        /// </summary>
        /// <param name="target">The target object to scale (Transform (3D) or RectTransform (2D/UI).</param>
        /// <param name="end">The target scale on the X axis.</param>
        /// <param name="duration">The duration of the scaling in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the scaling. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the scaling.</returns>
        public static void AniScaleX(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniScaleXCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniScaleXCoroutine(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            float start = target.transform.localScale.x;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(start, end, t);
                target.transform.localScale = new Vector3(value, target.transform.localScale.y, target.transform.localScale.z);

                yield return null;
            }

            target.transform.localScale = new Vector3(end, target.transform.localScale.y, target.transform.localScale.z);
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        /// <summary>
        /// Scales an object along the Y axis.
        /// </summary>
        /// <param name="target">The target object to scale.</param>
        /// <param name="end">The target scale on the Y axis.</param>
        /// <param name="duration">The duration of the scaling in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the scaling. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the scaling.</returns>
        public static void AniScaleY(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniScaleYCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniScaleYCoroutine(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            float start = target.transform.localScale.y;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(start, end, t);
                target.transform.localScale = new Vector3(target.transform.localScale.x, value, target.transform.localScale.z);

                yield return null;
            }

            target.transform.localScale = new Vector3(target.transform.localScale.x, end, target.transform.localScale.z);
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        /// <summary>
        /// Scales an object along the Z axis.
        /// </summary>
        /// <param name="target">The target object to scale.</param>
        /// <param name="end">The target scale on the Z axis.</param>
        /// <param name="duration">The duration of the scaling in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the scaling. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the scaling.</returns>
        public static void AniScaleZ(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniScaleZCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniScaleZCoroutine(GameObject target, float end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            float start = target.transform.localScale.z;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(start, end, t);
                target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, value);

                yield return null;
            }

            target.transform.localScale = new Vector3(target.transform.localScale.x, target.transform.localScale.y, end);
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        /// <summary>
        /// Scales an object along all axes equally.
        /// </summary>
        /// <param name="target">The target object to scale.</param>
        /// <param name="end">The target scale for all axes.</param>
        /// <param name="duration">The duration of the scaling in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the scaling. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on complete callback.</param>
        /// <returns>A coroutine that performs the scaling.</returns>
        public static void AniScale(GameObject target, Vector3 end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniScaleCoroutine(target, end, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniScaleCoroutine(GameObject target, Vector3 end, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            Vector3 start = target.transform.localScale;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                Vector3 value = Vector3.Lerp(start, end, t);
                target.transform.localScale = value;

                yield return null;
            }

            target.transform.localScale = end;
    
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        #endregion
        
        #region Color Methods

        /// <summary>
        /// Changes the color of an image.
        /// </summary>
        /// <param name="target">The target image to change color.</param>
        /// <param name="endColor">The target color to change to.</param>
        /// <param name="duration">The duration of the color change in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the color change. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the color change.</returns>
        public static void AniColorImage(Image target, Color endColor, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniColorImageCoroutine(target, endColor, duration, easingFunction, onComplete));
        }

        private static IEnumerator AniColorImageCoroutine(Image target, Color endColor, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            Color startColor = target.color;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                Color value = Color.Lerp(startColor, endColor, t);
                target.color = value;

                yield return null;
            }

            target.color = endColor;
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }
        
        /// <summary>
        /// Changes the color of a material.
        /// </summary>
        /// <param name="target">The Renderer with the target Material to change color.</param>
        /// <param name="endColor">The target color to change to.</param>
        /// <param name="duration">The duration of the color change in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the color change. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        public static void AniColorMaterial(Renderer target, Color endColor, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniColorMaterialCoroutine(target, endColor, duration, easingFunction, onComplete));
        }

        private static IEnumerator AniColorMaterialCoroutine(Renderer target, Color endColor, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            if(target.material == null)
            {
                throw new System.InvalidOperationException("The target Renderer does not have a Material.");
            }

            Color startColor = target.material.color;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                Color value = Color.Lerp(startColor, endColor, t);
                target.material.color = value;

                yield return null;
            }

            target.material.color = endColor;
    
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }


        /// <summary>
        /// Changes the alpha of an image.
        /// </summary>
        /// <param name="target">The target image to change alpha.</param>
        /// <param name="endAlpha">The target alpha to change to. This should be a float between 0 (completely transparent) and 1 (completely opaque).</param>
        /// <param name="duration">The duration of the alpha change in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the alpha change. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        /// <returns>A coroutine that performs the alpha change.</returns>
        public static void AniAlpha(Image target, float endAlpha, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniAlphaCoroutine(target, endAlpha, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniAlphaCoroutine(Image target, float endAlpha, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            float startAlpha = target.color.a;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(startAlpha, endAlpha, t);
                target.color = new Color(target.color.r, target.color.g, target.color.b, value);

                yield return null;
            }

            target.color = new Color(target.color.r, target.color.g, target.color.b, endAlpha);
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }
        
        /// <summary>
        /// Changes the alpha value of a material over time.
        /// </summary>
        /// <param name="target">The Renderer with the target Material to change the alpha.</param>
        /// <param name="endAlpha">The target alpha to change to. This should be a float between 0 (completely transparent) and 1 (completely opaque).</param>
        /// <param name="duration">The duration of the alpha change in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the alpha change. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        public static void AniAlphaMaterial(Renderer target, float endAlpha, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniAlphaMaterialCoroutine(target, endAlpha, duration, easingFunction, onComplete));
        }
        
        private static IEnumerator AniAlphaMaterialCoroutine(Renderer target, float endAlpha, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            if(target.material == null)
            {
                throw new System.InvalidOperationException("The target Renderer does not have a Material.");
            }

            float startAlpha = target.material.color.a;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float value = Mathf.Lerp(startAlpha, endAlpha, t);
                target.material.color = new Color(target.material.color.r, target.material.color.g, target.material.color.b, value);

                yield return null;
            }

            target.material.color = new Color(target.material.color.r, target.material.color.g, target.material.color.b, endAlpha);
            
            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }
        
        /// <summary>
        /// Changes the orthographic size of a camera over time.
        /// </summary>
        /// <param name="target">The target camera to change the orthographic size.</param>
        /// <param name="endSize">The target size to change to.</param>
        /// <param name="duration">The duration of the size change in seconds.</param>
        /// <param name="easingFunction">An optional easing function for the size change. Defaults to null (linear interpolation).</param>
        /// <param name="onComplete">An optional on completion callback.</param>
        public static void AniOrthoSize(Camera target, float endSize, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            AnimicoHelper.Instance.StartCoroutine(AniOrthoSizeCoroutine(target, endSize, duration, easingFunction, onComplete));
        }

        private static IEnumerator AniOrthoSizeCoroutine(Camera target, float endSize, float duration, System.Func<float, float> easingFunction = null, Action onComplete = null)
        {
            float startSize = target.orthographicSize;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;

                if (easingFunction != null)
                    t = easingFunction(t);

                float newSize = Mathf.Lerp(startSize, endSize, t);
                target.orthographicSize = newSize;

                yield return null;
            }

            target.orthographicSize = endSize;

            // Call the completion callback, if one was provided
            onComplete?.Invoke();
        }

        
        #endregion

        public class AnimicoHelper : MonoBehaviour
        {
            private static AnimicoHelper _instance;

            public static AnimicoHelper Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("AnimicoHelper");
                        go.hideFlags = HideFlags.HideAndDontSave; 
                        _instance = go.AddComponent<AnimicoHelper>();
                    }

                    return _instance;
                }
            }
        }
        

    }
}
