using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private bool timed = false;
    [SerializeField] private float timer = 10f;

    void Start()
    {
        if (timed) StartCoroutine(WaitForTimer());
    }

    private IEnumerator WaitForTimer()
    {
        yield return new WaitForSeconds(timer);
        DoDestroy();
    }
    
    public void DoDestroy()
    {
        Debug.Log("Self-Destroying");
        Destroy(gameObject);
    }
}
