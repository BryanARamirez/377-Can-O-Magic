using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    //How far the touch for dragging goes to the right
    private float middleToWallDistance = 4.6f;
    private float topBoundary = 9.5f;
    private float boundary;
    public GameObject currentObj;
    [SerializeField] private int nextObjIndex;
    [SerializeField] private int currentObjIndex;
    [SerializeField] private int zDist = 32;
    [SerializeField] private List<GameObject> magicObj = new List<GameObject>();
    [SerializeField] private SteamScript steamScript;
    private PlayerData _playerData;
    public int randomNextIndex;
    private bool isWaiting;
    public bool isPowerItemMenuOpen;
    private bool isReplacing = false;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        isWaiting = false;
        steamScript = GameObject.FindGameObjectWithTag("Steam").GetComponent<SteamScript>();
        int randomIndex = Random.Range(0, magicObj.Count);
        currentObjIndex = randomIndex;
        if(GameData.Instance.spawningStart == false)
        {
            currentObj = Instantiate(magicObj[randomIndex]);
            currentObj.transform.parent = this.transform;
            currentObj.transform.position = this.transform.position;
            currentObj.GetComponent<Rigidbody>().useGravity = false;
            boundary = middleToWallDistance - currentObj.GetComponentInChildren<Collider>().bounds.size.x / 2;
            transform.position = new Vector3(0f, transform.position.y, 0f);
            randomNextIndex = Random.Range(0, magicObj.Count);
        }
        _playerData.DisplayNextItem(magicObj[randomNextIndex].GetComponent<NextMagicItemSprite>().itemSprite);
        nextObjIndex = randomNextIndex;
        isPowerItemMenuOpen = false;
    }

    void Update()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                topBoundary = 9.5f;
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                topBoundary = 100f;
                break;

            default:
                break;
        }
        if (GameData.Instance.hasDoneTutorial == true && GameData.Instance.gameIsOver == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, zDist));
                Vector2 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(0,touch.position.y));
                Debug.Log("TouchPosY: " + tempPos.y);
                if(isPowerItemMenuOpen == false)
                {
                    if (touchedPos.x <= middleToWallDistance && touchedPos.x >= -middleToWallDistance && tempPos.y <= topBoundary)
                    {
                        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                        {
                            Vector3 lockedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, zDist));
                            lockedPos.y = this.transform.position.y;
                            if (lockedPos.x > boundary)
                            {
                                lockedPos.x = boundary;
                            }
                            else if (lockedPos.x < -boundary)
                            {
                                lockedPos.x = -boundary;
                            }
                            transform.position = lockedPos;
                        }
                    }
                    if (touch.phase == TouchPhase.Ended && touchedPos.x <= middleToWallDistance && touchedPos.x >= -middleToWallDistance && isWaiting == false && tempPos.y <= topBoundary)
                    {
                        isWaiting = true;
                        if (currentObj.GetComponent<MagicalItemScript>() != null)
                        {
                            currentObj.GetComponent<MagicalItemScript>().SetDrop();
                        }
                        currentObj.GetComponent<Rigidbody>().useGravity = true;
                        currentObj.transform.parent = null;
                        isReplacing = false;
                        steamScript.OnDrop();
                        StartCoroutine(spawnNext(1));
                    }
                }
            }
        } 
    }


    /// <summary>
    /// replaces current item with newItem
    /// done by destroying currentObj and Instantiating newItem
    /// </summary>
    /// <param name="newItem">Game Object to replace currentObj</param>
    public void ReplaceCurrentItem(GameObject newItem)
    {
        isReplacing = true;
        isWaiting = false;
        if (currentObj != null)
        {
            Destroy(currentObj);
        }

        currentObj = Instantiate(newItem);

        currentObj.transform.parent = transform;
        currentObj.transform.position = transform.position;
        currentObj.GetComponent<Rigidbody>().useGravity = false;
        boundary = middleToWallDistance - currentObj.GetComponentInChildren<Collider>().bounds.size.x / 2;
        transform.position = new Vector3(0f, transform.position.y, 0f);
    }

    /// <summary>
    /// replaces current item with newItem
    /// done by destroying currentObj and Instantiating newItem 
    /// </summary>
    /// <param name="newItem">Game Object to replace currentObj</param>
    /// <param name="mimic">if the new item is a mimic</param>
    public void ReplaceCurrentItem(GameObject newItem, bool mimic)
    {
        isReplacing = true;
        isWaiting = false;
        if (currentObj != null)
        {
            Destroy(currentObj);
        }

        currentObj = Instantiate(newItem);

        if (mimic)
        {
            currentObj.GetComponent<MagicalItemScript>().SetMimic();
        }

        currentObj.transform.parent = transform;
        currentObj.transform.position = transform.position;
        currentObj.GetComponent<Rigidbody>().useGravity = false;
        boundary = middleToWallDistance - currentObj.GetComponentInChildren<Collider>().bounds.size.x / 2;
    }

    IEnumerator spawnNext(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (!isReplacing)
        {
            isWaiting = false;
            currentObj = Instantiate(magicObj[randomNextIndex]);
            currentObj.transform.parent = this.transform;
            currentObj.transform.position = this.transform.position;
            currentObjIndex = randomNextIndex;
            randomNextIndex = Random.Range(0, magicObj.Count);
            _playerData.DisplayNextItem(magicObj[randomNextIndex].GetComponent<NextMagicItemSprite>().itemSprite);
            currentObj.GetComponent<Rigidbody>().useGravity = false;
            boundary = middleToWallDistance - currentObj.GetComponentInChildren<Collider>().bounds.size.x / 2;
            transform.position = new Vector3(0f, transform.position.y, 0f);
        }
    }
}
