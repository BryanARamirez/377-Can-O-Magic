using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReactionToolTip : MonoBehaviour
{
    private bool reactionOccured;
    private bool toolTipOpen;
    [SerializeField] private TMP_Text toolTip;

    private void Update()
    {
        if(toolTipOpen == false)
        {
            reactionOccured = GameManager.Instance.reactionHappened;
            if (reactionOccured)
            {
                StartCoroutine(Tooltip());
            }
        }
    }

    IEnumerator Tooltip()
    {
        toolTipOpen = true;
        toolTip.text = GameManager.Instance.reactionOrb1.ToString() + " reacted with " + GameManager.Instance.reactionOrb2.ToString();
        yield return new WaitForSeconds(3);
        toolTipOpen = false;
        reactionOccured = false;
        toolTip.text = "";
        GameManager.Instance.reactionHappened = reactionOccured;
    }
}
