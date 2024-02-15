using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2024]
 * [scripts to manage all reaction when magic item touches other magic items]
 */

public class MagicItemReactionManager : MonoBehaviour
{
    private MagicalItemScript _magicalItemScript;
    private Dictionary<MagicItemEnum, BaseReactionScript> _reactions;

    /// <summary>
    /// gets all needed scripts from game object
    /// 
    /// also gets all reactions and places it in a dictionary
    /// </summary>
    private void Awake()
    {
        _magicalItemScript = GetComponent<MagicalItemScript>();

        _reactions = new Dictionary<MagicItemEnum, BaseReactionScript>();

        BaseReactionScript[] reaction;
        reaction = GetComponents<BaseReactionScript>();

        foreach (BaseReactionScript reactionScripts in reaction)
        {
            _reactions.Add(reactionScripts.reactionFor, reactionScripts);
        }
    }

    /// <summary>
    /// on collision with game object tagged as MagicItem:
    /// checks if the game object can react with collision
    /// if it can, call for reaction
    /// set game object so it cant react again
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MagicItem")
        {
            MagicalItemScript otherMagicalItemScript = collision.gameObject.GetComponent<MagicalItemScript>();

            BaseReactionScript temp;

            if (_reactions.TryGetValue(otherMagicalItemScript.magicItemName, out temp) && !_magicalItemScript.hasReacted && !otherMagicalItemScript.hasReacted)
            {
                _magicalItemScript.hasReacted = true;
                otherMagicalItemScript.hasReacted = true;

                temp.Reaction(collision.gameObject);
            }
        }
    }
}
