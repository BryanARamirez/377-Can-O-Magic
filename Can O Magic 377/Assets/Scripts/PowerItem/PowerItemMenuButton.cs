using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/17/2024]
 * [script use to indicate if buttons are interactable]
 */

public class PowerItemMenuButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private Image _buttonImage;

    private int lastAmount = 0;

    /// <summary>
    /// every frame, update the button text to show amount of power items 
    /// </summary>
    void Update()
    {
        if (PowerItemData.Instance.NumberOfPowerItems() != lastAmount)
        {
            if (PowerItemData.Instance.NumberOfPowerItems() > lastAmount)
            {
                StartCoroutine(FlashMenu());
            }
            lastAmount = PowerItemData.Instance.NumberOfPowerItems();
            _buttonText.text = "Power Item: " + PowerItemData.Instance.NumberOfPowerItems();
        }
    }

    /// <summary>
    /// changes menu color 
    /// TODO, update when new buttons come in
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlashMenu()
    {
        for (int i = 0; i < 3; i++)
        {
            _buttonImage.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(.25f);

            _buttonImage.GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(.25f);
        }
    }
}
