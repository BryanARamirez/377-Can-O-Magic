using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2024]
 * [Script meant to prevent game overs from tsunami]
 */

public class TsunamiLimiterScript : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private float _limitTime = 1f;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        Transform top = GameObject.FindGameObjectWithTag("TopRight").transform;

        transform.position = new Vector3(0f, top.position.y - .5f, 0f);
    }

    public void LimitTsunami()
    {
        StartCoroutine(TurnOnCollider());
    }

    private IEnumerator TurnOnCollider()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(_limitTime);
        _collider.enabled = false;
    }
}
