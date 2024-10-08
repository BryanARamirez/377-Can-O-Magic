using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [4/19/2024]
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
        //GetComponent<ParticleSystem>().Emit(1);
        Destroy(gameObject, 3);
    }
}
