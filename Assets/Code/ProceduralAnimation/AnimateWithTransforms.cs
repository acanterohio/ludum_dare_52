using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AnimateMode
{
    Loop,
    BackAndForth,
}

public class AnimateWithTransforms : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;
    [SerializeField] private float timeBetweenTargets = .5f;
    [SerializeField] private AnimateMode mode;
    [SerializeField] private bool startOnAwake = true;
    [SerializeField] private bool goForever = true;
    [SerializeField] private int cycles = 1;
    [SerializeField] private UnityEvent onFinish;
    private int currentFrame = 0, direction = 1;

    void Awake()
    {
        if (startOnAwake) StartAnimation();
    }

    public void StartAnimation()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        int i = 0;
        while (goForever || i < cycles)
        {
            yield return MoveThroughTargets();
            if (mode == AnimateMode.BackAndForth) yield return MoveBackwardsThroughTargets();
            i++;
            yield return null;
        }
        onFinish.Invoke();
    }

    private IEnumerator MoveThroughTargets()
    {
        direction = 1;
        for (int i = 0; i < targets.Count - 1; i++)
        {
            currentFrame = i;
            yield return MoveToNextTarget();
        }
    }

    private IEnumerator MoveBackwardsThroughTargets()
    {
        direction = -1;
        for (int i = targets.Count - 1; i > 0; i--)
        {
            currentFrame = i;
            yield return MoveToNextTarget();
        }
    }

    private IEnumerator MoveToNextTarget()
    {
        float t = 0;
        while (t < timeBetweenTargets)
        {
            transform.position = Vector3.Lerp(targets[currentFrame].position, targets[currentFrame + direction].position, t / timeBetweenTargets);
            transform.rotation = Quaternion.Lerp(targets[currentFrame].rotation, targets[currentFrame + direction].rotation, t / timeBetweenTargets);
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = targets[currentFrame + direction].position;
        transform.rotation = targets[currentFrame + direction].rotation;
    }

}
