using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/04/2024]
 * [script use to indicate if buttons are interactable]
 */

public class PowerItemMenuButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;

    /// <summary>
    /// every frame, update the button text to show amount of power items 
    /// </summary>
    void Update()
    {
        _buttonText.text = "Power Item: " + PowerItemData.Instance.NumberOfPowerItems();
    }
}
