using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadConduction : MonoBehaviour
{
    public bool canConduct;
    private bool conducted = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (conducted == false)
        {
            if (canConduct)
            {
                if (collision.gameObject.GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.PlasmaOrb)
                {
                    if(this.GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.WaterOrb)
                    {
                        this.GetComponent<WaterPlasmaReaction>()._conductingObject = collision.gameObject;
                        this.GetComponent<WaterPlasmaReaction>()._isConducting = true;
                        this.GetComponent<WaterPlasmaReaction>()._isConnected = true;
                        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
                        joint.anchor = collision.contacts[0].point;
                        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
                        joint.enableCollision = false;
                    }
                }
            }
        }
    }
}
