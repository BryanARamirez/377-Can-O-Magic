using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [2/17/2024]
 * [Merges magic items when touches the same type of magic items]
 */

public class MagicItemMergeScript : MonoBehaviour
{
    private MagicalItemScript _magicalItemScript;

    //vars for what the game object can merge into and if it can merge
    [SerializeField] private GameObject _mergeToPrefab;
    [SerializeField] private bool _isMerging = false;

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

                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                    return;
                }
            }
        }
    }

    public bool isMerging
    {
        get { return _isMerging; }
        set { _isMerging = value; }
    }
}
