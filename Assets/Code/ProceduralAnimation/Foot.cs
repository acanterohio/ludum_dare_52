using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
    [SerializeField] private Transform groundPoint;
    [SerializeField] private float stepDistance;
    [SerializeField] private float stepHeight = .5f;
    [SerializeField] private List<Foot> groundedFeet;
    [SerializeField] private float groundedDistance = .2f;
    [SerializeField] private float stepTime = .5f;
    [SerializeField] private AnimationCurve stepCurve;
    private bool stepping;

    void Start()
    {
        StartCoroutine(WaitForStep());
    }

    private IEnumerator WaitForStep()
    {
        while (true)
        {
            if (OtherFeetGrounded() && ReadyToStep())
            {
                yield return Step();
            }
            yield return null;
        }
    }

    private IEnumerator Step()
    {
        stepping = true;
        float t = 0;
        Vector3 startPosition = transform.position;
        Vector3 newPosition = groundPoint.position + groundPoint.forward * stepDistance * .5f;
        while (t < stepTime)
        {
            newPosition = groundPoint.position + groundPoint.forward * stepDistance * .5f;
            Vector3 nextPos = Vector3.Lerp(startPosition, newPosition, t / stepTime);
            nextPos.y = stepCurve.Evaluate(t / stepTime) * stepHeight;
            transform.position = nextPos;
            t += Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
        stepping = false;
    }

    private bool ReadyToStep()
    {
        return Vector3.Distance(groundPoint.position, transform.position) > stepDistance;
    }

    private bool OtherFeetGrounded()
    {
        bool ready = true;
        foreach (Foot foot in groundedFeet)
        {
            if (!foot.Grounded()) ready = false;
        }
        return ready;
    }

    public bool Grounded()
    {
        // bool grounded = Physics.SphereCast(transform.position, 1f, Vector3.down, out RaycastHit hit, groundedDistance);
        return !stepping;
    }
}
