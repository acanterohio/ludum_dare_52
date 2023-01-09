using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DonorHealth : MonoBehaviour, IDamageable
{
    public Transform shotSounds, hurtSounds;
    public float health = 25f;
    private Donor donor;
    private DonorController donorController;
    public UnityEvent garbrielDead;
    bool dead;
    private void Start()
    {
        donorController= GetComponent<DonorController>();
        donor= GetComponent<Donor>();
    }
    public bool damage(float damage, Transform playerTransform)
    {
        donorController.setTarget(playerTransform);
        foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
        {
            if (ps.gameObject.name == "BloodSpurt") ps.Play();
        }
        shotSounds.GetChild(Random.Range(0, shotSounds.childCount)).GetComponent<AudioSource>().Play();
        hurtSounds.GetChild(Random.Range(0, hurtSounds.childCount)).GetComponent<AudioSource>().Play();
        health -= damage;
        if (health <= 0 && !dead)
        {
            dead = true;
            donor.kill();
            StartCoroutine(FallOver());
            if (transform.parent.name == "Garbriel") garbrielDead.Invoke();
            return true;
        }
        return false;
    }

    private IEnumerator FallOver()
    {
        float t = 0;
        float duration = 1f;
        Vector3 targetLocalPos = new Vector3(transform.localPosition.x, .25f, transform.localPosition.z);
        Quaternion targetRot = Quaternion.Euler(-90, 0, 0);
        Vector3 originalLocalPos = transform.localPosition;
        Quaternion originalRot = transform.localRotation;
        while (t < duration)
        {
            transform.localPosition = Vector3.Lerp(originalLocalPos, targetLocalPos, (t / duration));
            transform.localRotation = Quaternion.Lerp(originalRot, targetRot, (t / duration));
            t += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Vector3.Lerp(originalLocalPos, targetLocalPos, t / duration);
        transform.localRotation = Quaternion.Lerp(originalRot, targetRot, t / duration);
    }
}
