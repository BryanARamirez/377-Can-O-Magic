using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/12/2024]
 * [WIP, test script to test multiple reactions]
 */

public class WaterBlueReaction : BaseReactionScript
{
    //game object for steam
    private GameObject _steam;
    private Transform _topRightOfCan;
    private Transform _bottomLeftOfCan;

    private void Reset()
    {
        _reactionFor = MagicItemEnum.BlueRune;
    }

    /// <summary>
    /// get steam game object
    /// </summary>
    private void Awake()
    {
        _steam = GameObject.FindWithTag("Steam");
        _topRightOfCan = GameObject.FindGameObjectWithTag("TopRight").transform;
        _bottomLeftOfCan = GameObject.FindGameObjectWithTag("BottomLeft").transform;
    }

    public override void Reaction(GameObject otherItem)
    {
        GameObject[] AllPowerItems = GameObject.FindGameObjectsWithTag("PowerItem");

        foreach (GameObject powerItem in AllPowerItems)
        {
            Destroy(powerItem);
        }

        GameObject[] AllMagicItems = GameObject.FindGameObjectsWithTag("MagicItem");
        foreach (GameObject magicItem in AllMagicItems)
        {
            if (magicItem.GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.FireOrb && !magicItem.GetComponent<MagicalItemScript>().hasReacted)
            {
                _steam.GetComponent<SteamScript>().ActivateSteam();
                magicItem.GetComponent<MagicalItemScript>().Reacted();
            }

            if (magicItem.GetComponent<MagicalItemScript>().hasDropped)
            {
                magicItem.transform.position = new Vector3(Random.Range(_bottomLeftOfCan.position.x, _topRightOfCan.position.x), Random.Range(_bottomLeftOfCan.position.y, _topRightOfCan.position.y), 0f);

            }       
        }
    }
}
