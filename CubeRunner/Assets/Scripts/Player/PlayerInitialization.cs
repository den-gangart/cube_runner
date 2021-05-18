using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitialization : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPlayerPosition;
    [SerializeField] private List<GameObject> playerPrefabs;

    private PlayerTypes playerType;
    private GameObject currentPlayerObject;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;

        int indexPlayerType = PlayerPrefs.GetInt("EquipedPlayerType", 0);

        playerType = (PlayerTypes)indexPlayerType;
        currentPlayerObject = Instantiate(playerPrefabs[indexPlayerType]) as GameObject;
        currentPlayerObject.transform.position = spawnPlayerPosition;

        PlayerBonuses.ResetBonuses();

        if (playerType == PlayerTypes.Golden)
            PlayerBonuses._moneyBonus = 2;
        if (playerType == PlayerTypes.DoubleScore)
            PlayerBonuses._scoreBonus = 2;
        if (playerType == PlayerTypes.Kiddy)
        {
            PlayerBonuses._speedImprove = 1.2f;
            PlayerBonuses._jumpImprove = 1.05f;
        }
    }

    public GameObject GetCurrentPlayerObject()
    {
        return currentPlayerObject;
    }
}
