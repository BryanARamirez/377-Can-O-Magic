using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipTextHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] text = new GameObject[20];
    public void DisableAll()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i].SetActive(false);
        }
    }
}
