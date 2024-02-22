using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Bryan; Lam, Justin]
 * Last Updated: [2/21/2024]
 * [allows magic items to merge if it's one level less]
 */

public class HolyAuraScript : MonoBehaviour
{
    [SerializeField] private GameObject holyAuraAOE;

    private void Awake()
    {
        holyAuraAOE.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
            holyAuraAOE.SetActive(true);
    }
}
