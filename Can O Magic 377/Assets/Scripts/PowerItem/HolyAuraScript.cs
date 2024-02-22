using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
