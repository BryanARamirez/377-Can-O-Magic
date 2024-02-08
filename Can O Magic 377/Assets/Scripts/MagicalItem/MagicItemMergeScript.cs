using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicItemMergeScript : MonoBehaviour
{
    //on collision enter
    //Checks if it is colliding with the same magic item
    //adds new magic item
    //removes this and colliding object
    private MagicalItemScript _magicalItemScript;

    [SerializeField] private GameObject _mergeToPrefab;
    [SerializeField] private bool _isMerging = false;

    private void Awake()
    {
        _magicalItemScript = GetComponent<MagicalItemScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MagicItem")
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
