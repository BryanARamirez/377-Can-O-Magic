using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2024]
 * [Script for the slime ball]
 */

public class SlimeBallScript : MonoBehaviour
{
    [SerializeField]private List<GameObject> _stuckObjects;

    /// <summary>
    /// when collides with Magic item:
    /// checks if the object is already sticking
    /// adds a fixed joint to connect slime ball and magic item
    /// makes so items stuck together ignore collision
    /// 
    /// todo:maybe walls and other power items
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

            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.anchor = collision.contacts[0].point;
            joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            joint.enableCollision = false;

            if (_stuckObjects.Count > 0)
            {
                for (int index = _stuckObjects.Count - 1; index >= 0; index--)
                {
                    Physics.IgnoreCollision(collision.gameObject.GetComponentInChildren<Collider>(), _stuckObjects[index].GetComponentInChildren<Collider>(), true);
                }
            }
            _stuckObjects.Add(collision.gameObject);
        }
    }

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
