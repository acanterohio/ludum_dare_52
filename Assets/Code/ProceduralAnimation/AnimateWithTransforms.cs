using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

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
    [SerializeField] private bool onlyIfMoving = false;
    [SerializeField] private UnityEvent onFinish;
    private int currentFrame = 0, direction = 1;
    private bool canceled;

    void Awake()
    {
        if (startOnAwake) StartAnimation();
    }

    public void SetTargets(List<Transform> newTargets)
    {
        targets = newTargets;
    }

    public void StartAnimation()
    {
        canceled = false;
        StartCoroutine(Animate());
    }

    public void Cancel()
    {
        canceled = true;
    }

    private IEnumerator Animate()
    {
        int i = 0;
        while (!canceled && (goForever || i < cycles))
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
        for (int i = 0; i < targets.Count - 1 && !canceled; i++)
        {
            currentFrame = i;
            yield return MoveToNextTarget();
        }
    }

    private IEnumerator MoveBackwardsThroughTargets()
    {
        direction = -1;
        for (int i = targets.Count - 1; i > 0 && !canceled; i--)
        {
            currentFrame = i;
            yield return MoveToNextTarget();
        }
    }

    private IEnumerator MoveToNextTarget()
    {
        transform.position = targets[currentFrame].position;
        transform.rotation = targets[currentFrame].rotation;
        float t = 0;
        while (t < timeBetweenTargets && !canceled)
        {
            if (!onlyIfMoving || Moving())
            {
                transform.position = Vector3.Lerp(targets[currentFrame].position, targets[currentFrame + direction].position, t / timeBetweenTargets);
                transform.rotation = Quaternion.Lerp(targets[currentFrame].rotation, targets[currentFrame + direction].rotation, t / timeBetweenTargets);
                t += Time.deltaTime;
            }
            yield return null;
        }
        transform.position = targets[currentFrame + direction].position;
        transform.rotation = targets[currentFrame + direction].rotation;
    }

    private bool Moving()
    {
        bool moving = true;
        Rigidbody rb = GetComponentInParent<Rigidbody>();
        NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();
        if (agent != null && agent.velocity.magnitude == 0)
        {
            moving = false;
        }
        else if (rb != null && rb.velocity.magnitude == 0)
        {
            moving = false;
        }
        return moving;
    }

}
