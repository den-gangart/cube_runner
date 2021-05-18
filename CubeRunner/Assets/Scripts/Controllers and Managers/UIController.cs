using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject jumpButton;
    [SerializeField] private GameObject scoreLabel;
    [SerializeField] private GameObject moneyLabel;
    [SerializeField] private GameObject endWindow;
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI recordScore;
    [SerializeField] private TextMeshProUGUI earnedMoneyText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderEffect;

    private int _prevRecord;
    private int _money;
    private int _earnedMoney;

    private ScoreManager _scoreManager;
    private AdvManager advManager;
    private PlayerTypes playerType;

    // Start is called before the first frame update
    void Start()
    {
        _scoreManager = GetComponent<ScoreManager>();
        advManager = GetComponent<AdvManager>();
        menuPause.SetActive(false);
        endWindow.SetActive(false);
        settingsWindow.SetActive(false);
        buttonPause.SetActive(true);
        _prevRecord = PlayerPrefs.GetInt("Record", 0);
        _money = PlayerPrefs.GetInt("Coins", 0);

        playerType = (PlayerTypes)PlayerPrefs.GetInt("EquipedPlayerType", 0);

        if (playerType == PlayerTypes.Jumper)
            jumpButton.SetActive(true);
        else
            jumpButton.SetActive(false);

        MusicPlayer.currentGameStatus = GameStatus.InGame;

        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sliderEffect.value = PlayerPrefs.GetFloat("EffectVolume", 1);
    }

    public void OnPauseGame()
    {
        menuPause.SetActive(true);
        scoreLabel.SetActive(false);
        buttonPause.SetActive(false);
        jumpButton.SetActive(false);
        moneyLabel.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnResumeGame()
    {
        menuPause.SetActive(false);
        scoreLabel.SetActive(true);
        buttonPause.SetActive(true);
        moneyLabel.SetActive(true);

        if (playerType == PlayerTypes.Jumper)
            jumpButton.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void OnOpenSettings()
    {
        settingsWindow.SetActive(true);
        menuPause.SetActive(false);
    }

    public void OnCloseSettings()
    {
        settingsWindow.SetActive(false);
        menuPause.SetActive(true);
    }

    public void OnSetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        GameObject.FindGameObjectWithTag("Music").SendMessage("UpdateVolume");
    }

    public void OnSetEffectVolume(float value)
    {
        PlayerPrefs.SetFloat("EffectVolume", value);
        GameObject.FindGameObjectWithTag("UISound").SendMessage("UpdateVolume");
    }


    public void OnEndGame()
    {
        endWindow.SetActive(true);
        scoreLabel.SetActive(false);
        buttonPause.SetActive(false);
        jumpButton.SetActive(false);
        moneyLabel.SetActive(false);

        int score = _scoreManager.GetScore();
        _earnedMoney = (int)((score / 10)*PlayerBonuses._moneyBonus);
         
        totalMoneyText.text = _money.ToString();
        earnedMoneyText.text = _earnedMoney.ToString();

        PlayerPrefs.SetInt("Coins", _money +_earnedMoney);


        if (score > _prevRecord)
        {
            currentScore.text = "NEW RECORD!";
            recordScore.text = score.ToString();
            PlayerPrefs.SetInt("Record", score);
        }
        else
        {
            currentScore.text = "Total Score: "+score.ToString();
            recordScore.text = "Record: "+ _prevRecord.ToString();
        }

        StartCoroutine(StartRewriteMoney());
    }


    private IEnumerator StartRewriteMoney()
    {
        yield return new WaitForSeconds(1f);
        if(!advManager.AdsIsShowing())
            StartCoroutine(RewritingMoney());
        else
            StartCoroutine(StartRewriteMoney());
    }

    private IEnumerator RewritingMoney()
    {
        yield return new WaitForSeconds(0.005f);
        if (_earnedMoney > 0)
        {
            _earnedMoney--;
            _money++;

            totalMoneyText.text =  _money.ToString();
            earnedMoneyText.text = _earnedMoney.ToString();
            StartCoroutine(RewritingMoney());
        }
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene("RoadScene");
    }

    public void OnBackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void OnJump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.SendMessage("LetJump");
    }
}
