using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SteamScript : MonoBehaviour
{
    [SerializeField] private GameObject _steamModel;
    [SerializeField] private TMP_Text _steamCounterV;
    [SerializeField] private TMP_Text _steamCounterH;
    [SerializeField] private int _maxDropCount = 3;
    public int _currentDropCount = 0;

    [SerializeField] private bool drop = false;

    //use to test steam
    private void Update()
    {
        if (drop)
        {
            drop = false;
            OnDrop();
        }
    }

    public void ActivateSteam()
    {
        _steamModel.SetActive(true);
        _currentDropCount = _maxDropCount;
        _steamCounterH.text = _currentDropCount.ToString();
        _steamCounterV.text = _currentDropCount.ToString();
        //add audio
        //add effects
    }

    //look into events
    public void OnDrop()
    {
        if (_currentDropCount > 0)
        {
            _currentDropCount--;
            _steamCounterH.text = _currentDropCount.ToString();
            _steamCounterV.text = _currentDropCount.ToString();

            if (_currentDropCount <= 0)
            {
                _steamModel.SetActive(false);
                _steamCounterH.text = "";
                _steamCounterV.text = "";
            }
        }
    }   
}
