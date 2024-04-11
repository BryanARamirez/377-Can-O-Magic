using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTips : MonoBehaviour
{
    [SerializeField] private GameObject text;
    public void ToolTip()
    {
        text.SetActive(true);
    }
    public void ToolTipDisable()
    {
        text.SetActive(false);
    }
}
