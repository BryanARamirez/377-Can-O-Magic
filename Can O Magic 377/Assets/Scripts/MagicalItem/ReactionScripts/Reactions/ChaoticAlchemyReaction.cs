using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/11/2024]
 * [Reaction script that works independently from other scripts 
 * due to the nature of the reaction]
 */

public class ChaoticAlchemyReaction : MonoBehaviour
{
    private MagicalItemScript _magicalItemScript;

    [Range(0, 1)]
    [SerializeField] private float _chanceToChange = .1f;

    /// <summary>
    /// gets all needed scripts from game object
    /// </summary>
    private void Awake()
    {
        _magicalItemScript = GetComponent<MagicalItemScript>();
    }

    /// <summary>
    /// on collision enter, gives the item a random chance to change into the collided magic item
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        float chanceCheck = Random.Range(0f, 1f);
        if (collision.gameObject.tag == "MagicItem" && chanceCheck < _chanceToChange)
        {
            MagicalItemScript otherMagicalItemScript = collision.gameObject.GetComponent<MagicalItemScript>();

            if (otherMagicalItemScript.magicItemName != MagicItemEnum.UnstableRune)
            {
                _magicalItemScript.Reacted();
                otherMagicalItemScript.Reacted();

                Instantiate(collision.gameObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
