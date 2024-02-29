using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //How far the touch for dragging goes to the right
    private float rightDis = 4.5f;
    //How far the touch for dragging goes to the left
    private float leftDis = -4.5f;
    [SerializeField] private GameObject currentObj;
    [SerializeField] private int nextObjIndex;
    [SerializeField] private int currentObjIndex;
    [SerializeField] private int zDist = 32;
    [SerializeField] private List<GameObject> magicObj = new List<GameObject>();
    [SerializeField] private SteamScript steamScript;
    private PlayerData _playerData;
    public int randomNextIndex;
    private bool isWaiting;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        Application.targetFrameRate = 60;
        isWaiting = false;
        steamScript = GameObject.FindGameObjectWithTag("Steam").GetComponent<SteamScript>();
        int randomIndex = Random.Range(0, magicObj.Count);
        currentObjIndex = randomIndex;
        currentObj = Instantiate(magicObj[randomIndex]);
        currentObj.transform.parent = this.transform;
        currentObj.transform.position = this.transform.position;
        currentObj.GetComponent<Rigidbody>().useGravity = false;
        randomNextIndex = Random.Range(0, magicObj.Count);
        _playerData.DisplayNextItem(magicObj[randomNextIndex].GetComponent<NextMagicItemSprite>().itemSprite);
        nextObjIndex = randomNextIndex;
    }

    void Update()
    {
        if(_playerData.inTutorial == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, zDist));
                if (touchedPos.x <= rightDis && touchedPos.x >= leftDis)
                {
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        Vector3 lockedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, zDist));
                        lockedPos.y = this.transform.position.y;
                        transform.position = lockedPos;
                    }
                    if (touch.phase == TouchPhase.Ended && touchedPos.x <= rightDis && touchedPos.x >= leftDis && isWaiting == false)
                    {
                        isWaiting = true;
                        currentObj.GetComponent<MagicalItemScript>().SetDrop();
                        currentObj.GetComponent<Rigidbody>().useGravity = true;
                        currentObj.transform.parent = null;
                        StartCoroutine(spawnNext(1));
                        steamScript.OnDrop();
                    }
                }
            }
        } 
    }

    IEnumerator spawnNext(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isWaiting = false;
        currentObj = Instantiate(magicObj[randomNextIndex]);
        currentObj.transform.parent = this.transform;
        currentObj.transform.position = this.transform.position;
        currentObjIndex = randomNextIndex;
        randomNextIndex = Random.Range(0, magicObj.Count);
        _playerData.DisplayNextItem(magicObj[randomNextIndex].GetComponent<NextMagicItemSprite>().itemSprite);
        currentObj.GetComponent<Rigidbody>().useGravity = false;
    }
}
