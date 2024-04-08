using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/24/2024]
 * [reaction that removes all power item, 
 * cause steam if fire orb is present, 
 * and randomizes all magic item location]
 */

public class WaterBlueReaction : BaseReactionScript
{
    //game object for steam
    private GameObject _steam;
    private TsunamiLimiterScript _tsunamiLimiterScript;
    private Transform _topRightOfCan;
    private Transform _bottomLeftOfCan;
    private GameObject[] AllMagicItems;
    private GameObject _tsunamiVFX;

    private void Reset()
    {
        _reactionFor = MagicItemEnum.BlueRune;
    }

    /// <summary>
    /// get steam game object
    /// </summary>
    private void Awake()
    {
        _steam = GameObject.FindGameObjectWithTag("Steam");
        _tsunamiLimiterScript = GameObject.FindGameObjectWithTag("TsunamiLimiter").GetComponent<TsunamiLimiterScript>();
        _topRightOfCan = GameObject.FindGameObjectWithTag("TopRight").transform;
        _bottomLeftOfCan = GameObject.FindGameObjectWithTag("BottomLeft").transform;
        _tsunamiVFX = GameObject.FindGameObjectWithTag("TsunamiVFX");
    }

    /// <summary>
    /// removes all power item,
    /// cause steam if fire orb is present,
    /// and randomizes all magic item location
    /// </summary>
    /// <param name="otherItem"></param>
    public override void Reaction(GameObject otherItem)
    {
        _tsunamiVFX.GetComponent<ParticleSystem>().Play();
        _tsunamiVFX.GetComponent<AudioSource>().Play();
        GameObject[] AllPowerItems = GameObject.FindGameObjectsWithTag("PowerItem");

        foreach (GameObject powerItem in AllPowerItems)
        {
            Destroy(powerItem);
        }

        _tsunamiLimiterScript.LimitTsunami();

        AllMagicItems = GameObject.FindGameObjectsWithTag("MagicItem");
        foreach (GameObject magicItem in AllMagicItems)
        {
            if (magicItem.GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.FireOrb && !magicItem.GetComponent<MagicalItemScript>().hasReacted)
            {
                _steam.GetComponent<SteamScript>().ActivateSteam();
                magicItem.GetComponent<MagicalItemScript>().Reacted();
            }

            if (magicItem.GetComponent<MagicalItemScript>().hasDropped)
            {
                magicItem.transform.position = new Vector3(Random.Range(_bottomLeftOfCan.position.x+1, _topRightOfCan.position.x-1), Random.Range(_bottomLeftOfCan.position.y, _topRightOfCan.position.y-1f), 0f);

            }       
        }
    }
}
