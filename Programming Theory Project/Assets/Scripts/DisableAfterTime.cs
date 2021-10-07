using System.Collections;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float timeToDisable = 0.8f;

    private Coroutine _deactivateCoroutine;
    
    private void OnEnable()
    {
        if(_deactivateCoroutine != null)
            StopCoroutine(_deactivateCoroutine);
        _deactivateCoroutine = StartCoroutine(nameof(Deactivate));
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timeToDisable);
        gameObject.SetActive(false);
    }
}