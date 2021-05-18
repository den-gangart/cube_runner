using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private int coinValue;
    private int _coins;
    public Transform target;
    public UISounds uISounds;

    // Start is called before the first frame update
    void Start()
    {
        _coins = PlayerPrefs.GetInt("Coins", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(target.position.z > transform.position.z+5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<ScoreManager>().OnChangeMoney(coinValue);

        _coins += coinValue;
        PlayerPrefs.SetInt("Coins", _coins);

        Destroy(this.gameObject);
    }
}
