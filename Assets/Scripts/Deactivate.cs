using System;
using System.Collections;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
