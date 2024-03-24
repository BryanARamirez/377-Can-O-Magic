using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/14/2024]
 * [script use to indicate if buttons are interactable]
 */

public class PowerItemButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private PowerItemEnum _checkItem;

    private void Update()
    {
        if (PowerItemData.Instance.checkAvailable(_checkItem))
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }
}
