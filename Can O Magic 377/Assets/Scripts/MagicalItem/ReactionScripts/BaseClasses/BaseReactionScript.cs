using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/04/2024]
 * [Base class that all reaction needs to inherit from]
 */

public class BaseReactionScript : MonoBehaviour
{
    //id for which reaction it is for
    [SerializeField] protected MagicItemEnum _reactionFor;

    /// <summary>
    /// reaction that needs to be overrided when inheritted from
    /// </summary>
    public virtual void Reaction(GameObject otherItem)
    {
    
    }

    /// <summary>
    /// gets the name of the reaction
    /// </summary>
    public MagicItemEnum reactionFor
    {
        get { return _reactionFor; }
    }
}
