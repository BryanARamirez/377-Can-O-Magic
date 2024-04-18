using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/17/2024]
 * [reaction for conduction]
 */

public class WaterPlasmaReaction : BaseReactionScript
{
    private GameObject _conductingObject;
    [SerializeField] private GameObject _mergeToPrefab;

    private bool _isConducting = false;
    private bool _isConnected = false;

    /// <summary>
    /// resets the default for _reactionFor
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.PlasmaOrb;
    }

    /// <summary>
    /// when reaction is called,
    /// set the _conductingObject to the reacting object
    /// and switch _isConducting to true
    /// </summary>
    /// <param name="otherItem"></param>
    public override void Reaction(GameObject otherItem)
    {
        _isConducting = true;
        _conductingObject = otherItem;
    }

    /// <summary>
    /// on collision enter:
    /// checks if it is doing this reaction
    /// connects the plasma and water orb if it hasn't done so already
    /// has a modified version of the merge code to merge the two game objects
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (_isConducting && _conductingObject != null)
        {
            if (!_isConnected && collision.gameObject == _conductingObject)
            {
                FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                joint.anchor = collision.contacts[0].point;
                joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
                joint.enableCollision = false;
                _isConnected = true;
            }

            if (collision.gameObject != _conductingObject && collision.gameObject.tag == "MagicItem" && collision.gameObject.GetComponent<MagicItemMergeScript>() != null)
            {
                Debug.Log("buh");
                MagicalItemScript otherMagicalItemScript = collision.gameObject.GetComponent<MagicalItemScript>();
                MagicItemMergeScript otherMergeScript = collision.gameObject.GetComponent<MagicItemMergeScript>();

                MagicalItemScript connectedMagicItemScript = _conductingObject.gameObject.GetComponent<MagicalItemScript>();
                MagicItemMergeScript connectedMagicMergeScript = _conductingObject.gameObject.GetComponent<MagicItemMergeScript>();

                if (!otherMergeScript.isMerging && !connectedMagicMergeScript.isMerging && connectedMagicItemScript.magicItemName == otherMagicalItemScript.magicItemName)
                {
                    int thisID = gameObject.GetInstanceID();
                    int otherID = collision.gameObject.GetInstanceID();
                    if (thisID > otherID)
                    {
                        connectedMagicMergeScript.isMerging = true;
                        otherMergeScript.isMerging = true;

                        Vector3 spawnNewItem = (gameObject.transform.position + collision.transform.position) / 2f;
                        GameObject newItem = Instantiate(_mergeToPrefab, spawnNewItem, Quaternion.identity);
                        newItem.GetComponent<MagicalItemScript>().SetDrop();

                        Destroy(collision.gameObject);
                        Destroy(_conductingObject);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }


}
