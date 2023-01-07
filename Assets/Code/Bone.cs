using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private float despawnTime = 5f;
    private float damage = 10f;
    void Start()
    {
        StartCoroutine(DespawnTimer());
    }
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null )
        {
            damageable.damage(damage);
            Destroy(gameObject);
        }
    }
}
