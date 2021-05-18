using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI coinLabel;
    [SerializeField] private GameObject addMoneyLabelPrefab;
    [SerializeField] private GameObject canvas;
    [SerializeField] private UISounds uISounds;

    private int _score;
    private int _prevScore;

    private int _coin;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerInitialization>().GetCurrentPlayerObject().transform;
        _score = 0;
        _prevScore = Convert.ToInt32(player.transform.position.z);
        scoreLabel.text = "Score: " + _score.ToString();
        _coin = PlayerPrefs.GetInt("Coins", 0);
        coinLabel.text = _coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        int currZ = Convert.ToInt32(player.transform.position.z);
        if (currZ-_prevScore >= 1)
        {
            _score += (int)((currZ-_prevScore)*PlayerBonuses._scoreBonus);
            _prevScore = currZ;
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    public int GetScore()
    {
        return _score;
    }

    public void OnChangeMoney(int value)
    {
        uISounds.OnCoinPickUp();
        _coin += value;
        coinLabel.text = _coin.ToString();
        GameObject addMoney = Instantiate(addMoneyLabelPrefab) as GameObject;
        addMoney.GetComponent<RectTransform>().parent = canvas.GetComponent<RectTransform>();
        addMoney.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }
}
