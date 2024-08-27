using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [05/01/2024]
 * [Script for the slime ball]
 */


/*
 * NOTE: this script has been modified to use spring joints...
 * there is a good chance that a lot of this is useless now
 * but since the last time i tried to delete some of this 
 * it causes major bugs with some of the stuff i thought useless...
 * so we are just not going to touch it...
*/
public class SlimeBallScript : MonoBehaviour
{
    [SerializeField]private List<GameObject> _stuckObjects;

    private MergeVFX _vFX;

    private void Awake()
    {
        _vFX = GetComponent<MergeVFX>();
    }

    /// <summary>
    /// when collides with Magic item:
    /// checks if the object is already sticking
    /// adds a fixed joint to connect slime ball and magic item
    /// makes so items stuck together ignore collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MagicItem")
        {
            CheckForNull();
            if (_stuckObjects.Count > 0)
            {
                for (int index = _stuckObjects.Count - 1; index >= 0; index--)
                {
                    if (_stuckObjects[index] == collision.gameObject)
                    {
                        return;
                    }
                }
            }

            SpringJoint joint = gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = Vector3.zero;
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            joint.enableCollision = true;
            joint.spring = 1000;
            _vFX.PlayMergeVFX(joint.transform.position);

            if (_stuckObjects.Count > 0)
            {
                for (int index = _stuckObjects.Count - 1; index >= 0; index--)
                {
                    //Physics.IgnoreCollision(collision.gameObject.GetComponentInChildren<Collider>(), _stuckObjects[index].GetComponentInChildren<Collider>(), true);
                }
            }
            _stuckObjects.Add(collision.gameObject);
        }
    }

    /// <summary>
    /// on destroy, make sure all stuck objects can interact with eachother again
    /// </summary>
    private void OnDestroy()
    {
        CheckForNull();
        if (_stuckObjects.Count > 0)
        {
            for (int index = _stuckObjects.Count - 1; index > 0; index--)
            {
                for (int index2 = index - 1; index2 >= 0; index2--)
                {
                    Physics.IgnoreCollision(_stuckObjects[index].gameObject.GetComponentInChildren<Collider>(), _stuckObjects[index2].GetComponentInChildren<Collider>(), false);
                }
            }
        }
    }

    /// <summary>
    /// checks _stuckObjects for null objects and removes them
    /// </summary>
    private void CheckForNull()
    {
        if (_stuckObjects.Count > 0)
        {
            for (int index = _stuckObjects.Count - 1; index >= 0; index--)
            {
                if (_stuckObjects[index] == null)
                {
                    _stuckObjects.RemoveAt(index);
                }
            }
        }
    }
}
