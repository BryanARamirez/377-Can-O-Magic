using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemMenuScript : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] private GameObject holyAuraPrefab;
    [SerializeField] private GameObject mimicToungePrefab;
    [SerializeField] private GameObject slimeBallPrefab;
    [SerializeField] private GameManager gameManager;
    public void HolyAuraSpawn()
    {
        Destroy(playerController.currentObj);
        playerController.currentObj = Instantiate(holyAuraPrefab);
        playerController.currentObj.transform.parent = playerController.transform;
        playerController.currentObj.transform.position = playerController.transform.position;
        gameManager.powerItemClose();
    }
    public void MimicToungeSpawn()
    {
        Destroy(playerController.currentObj);
        playerController.currentObj = Instantiate(mimicToungePrefab);
        playerController.currentObj.transform.parent = playerController.transform;
        playerController.currentObj.transform.position = playerController.transform.position;
        gameManager.powerItemClose();
    }
    public void SlimeBallSpawn()
    {
        Destroy(playerController.currentObj);
        playerController.currentObj = Instantiate(slimeBallPrefab);
        playerController.currentObj.transform.parent = playerController.transform;
        playerController.currentObj.transform.position = playerController.transform.position;
        gameManager.powerItemClose();
    }
}
