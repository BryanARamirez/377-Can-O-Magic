using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [3/20/2024]
 * [Script for all particle effect vfx
 * (MAKE SURE PLAY ON AWAKE IS TRUE)]
 */

public class ParticleGameObject : MonoBehaviour
{
    /// <summary>
    /// on enable, destroy after 3 sec
    /// </summary>
    private void OnEnable()
    {
        Destroy(gameObject, 3);
    }
}
