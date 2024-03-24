using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin; Ramirez, Bryan]
 * Last Updated: [3/20/2024]
 * [Merges magic items when touches the same type of magic items]
 */

public class MagicItemMergeScript : MonoBehaviour
{
    private MagicalItemScript _magicalItemScript;

    //vars for what the game object can merge into and if it can merge
    [SerializeField] private GameObject _mergeToPrefab;
    [SerializeField] private bool _isMerging = false;
    private bool _isInAura = false;

    /// <summary>
    /// get the needed stuff from game object
    /// </summary>
    private void Awake()
    {
        _magicalItemScript = GetComponent<MagicalItemScript>();
    }

    /// <summary>
    /// on collision enter:
    /// gets the item script and the merge script from game object
    /// checks if it can merge
    /// makes sure that only one is running script to merge
    /// instantiates new game object of _mergeToPrefab
    /// destroys this and other gameobjects
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MagicItem" && collision.gameObject.gameObject.GetComponent<MagicItemMergeScript>() != null)
        {
            MagicalItemScript otherMagicalItemScript = collision.gameObject.GetComponent<MagicalItemScript>();
            MagicItemMergeScript otherMergeScript = collision.gameObject.GetComponent<MagicItemMergeScript>();

            if (!otherMergeScript.isMerging && !isMerging && _magicalItemScript.magicItemName == otherMagicalItemScript.magicItemName)
            {
                int thisID = gameObject.GetInstanceID();
                int otherID = collision.gameObject.GetInstanceID();
                if(thisID > otherID)
                {
                    isMerging = true;
                    otherMergeScript.isMerging = true;

                    Vector3 spawnNewItem = (gameObject.transform.position + collision.transform.position) / 2f;
                    GameObject newItem = Instantiate(_mergeToPrefab, spawnNewItem, Quaternion.identity);

                    MergeVFX mergeVFX;
                    if (TryGetComponent<MergeVFX>(out mergeVFX))
                    {
                        mergeVFX.PlayMergeVFX(spawnNewItem);
                    }
                    newItem.GetComponent<MagicalItemScript>().SetDrop();

                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    return;
                }
            }

            //Gets the ID of the Enum to compare for HolyAura - Bryan 
            int mergeID = (int)_magicalItemScript.magicItemName;
            int otherMergeID = (int)otherMagicalItemScript.magicItemName;

            //Allows Holy Aura Merge to happen by checking if the magic item is inside of the holy aura - Bryan
            if (!otherMergeScript.isMerging && !isMerging && _isInAura == true && otherMergeScript._isInAura == true && mergeID == otherMergeID + 1)
            {
                isMerging = true;
                otherMergeScript.isMerging = true;

                Vector3 spawnNewItem = (gameObject.transform.position + collision.transform.position) / 2f;
                GameObject newItem = Instantiate(_mergeToPrefab, spawnNewItem, Quaternion.identity);
                newItem.GetComponent<MagicalItemScript>().SetDrop();

                MergeVFX mergeVFX;
                if (TryGetComponent<MergeVFX>(out mergeVFX))
                {
                    mergeVFX.PlayMergeVFX(spawnNewItem);
                }

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    //Trigger to check if the magic item has entered the holy aura and to also check if it has exited it. - Bryan
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HolyAura")
        {
            _isInAura = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HolyAura")
        {
            _isInAura = false;
        }
    }

    public bool isMerging
    {
        get { return _isMerging; }
        set { _isMerging = value; }
    }
}
