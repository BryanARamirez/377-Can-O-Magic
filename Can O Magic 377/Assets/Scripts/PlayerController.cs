using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //How far the touch for dragging goes to the right
    private int rightDis = 5;
    //How far the touch for dragging goes to the left
    private int leftDis = -5;
    [SerializeField] private GameObject currentObj;
    [SerializeField] private int nextObjIndex;
    [SerializeField] private int currentObjIndex;
    [SerializeField] private List<GameObject> magicObj = new List<GameObject>();
    [SerializeField] private SteamScript steamScript;
    public int randomNextIndex;
    private bool isWaiting;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        isWaiting = false;
        int randomIndex = Random.Range(0, magicObj.Count);
        currentObjIndex = randomIndex;
        currentObj = Instantiate(magicObj[randomIndex]);
        currentObj.transform.parent = this.transform;
        currentObj.transform.position = this.transform.position;
        currentObj.GetComponent<Rigidbody>().useGravity = false;
        randomNextIndex = Random.Range(0, magicObj.Count);
        nextObjIndex = randomNextIndex;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 10));
            if (touchedPos.x <= rightDis && touchedPos.x >= leftDis)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 lockedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 10));
                    lockedPos.y = this.transform.position.y;
                    transform.position = lockedPos;
                }
                if (touch.phase == TouchPhase.Ended && touchedPos.x <= rightDis && touchedPos.x >= leftDis && isWaiting == false)
                {
                    isWaiting = true;
                    currentObj.GetComponent<Rigidbody>().useGravity = true;
                    currentObj.transform.parent = null;
                    StartCoroutine(spawnNext(1));
                    steamScript.OnDrop();
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
        currentObj.GetComponent<Rigidbody>().useGravity = false;
    }
}
