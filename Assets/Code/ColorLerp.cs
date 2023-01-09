using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color startColor, endColor;
    [SerializeField] private float duration;
    [SerializeField] private UnityEvent finish;
    [SerializeField] private AnimationCurve lerpCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public void StartFade()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float t = 0;
        while (t < duration)
        {
            image.color = Color.Lerp(startColor, endColor, lerpCurve.Evaluate(t / duration));
            t += Time.deltaTime;
            yield return null;
        }
        image.color = endColor;
        finish.Invoke();
    }
}
