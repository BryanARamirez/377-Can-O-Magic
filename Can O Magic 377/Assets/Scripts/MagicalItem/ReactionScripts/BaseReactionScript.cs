using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [2/12/2024]
 * [Base class that all reaction needs to inherit from]
 */

public class BaseReactionScript : MonoBehaviour
{
    //id for which reaction it is for
    [SerializeField] protected MagicItemEnum _reactionFor;

    /// <summary>
    /// reaction that needs to be overrided when inheritted from
    /// </summary>
    public virtual void Reaction()
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