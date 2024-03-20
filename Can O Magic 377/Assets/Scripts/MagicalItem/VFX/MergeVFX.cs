using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [3/20/2024]
 * [calls to activate steam when called to react]
 */

public class MergeVFX : MonoBehaviour
{
    //gameobject for vfx
    [SerializeField] private GameObject _mergeVFX;

    /// <summary>
    /// instantiate gameobject with vfx at location
    /// </summary>
    /// <param name="location">where the vfx is played</param>
    public void PlayMergeVFX(Vector3 location)
    {
        Instantiate(_mergeVFX, location, Quaternion.identity);
    }
}
