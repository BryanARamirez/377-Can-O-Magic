using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/14/2024]
 * [script for the bomb power item]
 */

public class BombScript : MonoBehaviour
{
    [SerializeField] private GameObject explosionHitBox;
    [SerializeField] private float explosionDelayTime = 3f;
    [SerializeField] private float explosionTime = .1f;

    /// <summary>
    /// starts bomb explosion countdown
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Explode());
    }

    /// <summary>
    /// destroy anything in expoision hitbox
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    /// <summary>
    /// cause the explosion hit box to appear after a delay and remove bomb
    /// </summary>
    /// <returns></returns>
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelayTime);
        explosionHitBox.SetActive(true);

        yield return new WaitForSeconds(explosionTime);
        Destroy(gameObject);
    }
}
