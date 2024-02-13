using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamScript : MonoBehaviour
{
    [SerializeField] private GameObject _steamModel;
    [SerializeField] private int _maxDropCount = 3;
    private int _currentDropCount = 0;

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
        //add audio
        //add effects
    }

    //look into events
    public void OnDrop()
    {
        if (_currentDropCount > 0)
        {
            _currentDropCount--;

            if (_currentDropCount <= 0)
            {
                _steamModel.SetActive(false);
            }
        }
    }
}
