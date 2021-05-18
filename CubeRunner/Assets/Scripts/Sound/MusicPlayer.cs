using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameStatus
{
    Menu,
    InGame,
    End
}

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip menuAudioClip;
    [SerializeField] private AudioClip gameAudioClip;
    [SerializeField] private AudioClip onEndClip;

    public static GameStatus currentGameStatus;

    private GameStatus prevGameStatus;
    private AudioSource audioSource;
    private static MusicPlayer musicInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (musicInstance == null)
        {
            musicInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentGameStatus = prevGameStatus = GameStatus.Menu;
        PlayCurrentClip();
    }

    // Update is called once per frame
    void Update()
    {   
        if(prevGameStatus != currentGameStatus)
        {
            prevGameStatus = currentGameStatus;
            PlayCurrentClip(); 
        }
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public void PlayCurrentClip()
    {
        audioSource.loop = true;
        audioSource.Stop();

        if (currentGameStatus == GameStatus.InGame)
            audioSource.clip = gameAudioClip;
        else if (currentGameStatus == GameStatus.Menu)
        {
            audioSource.clip = menuAudioClip;
        }
        else if (currentGameStatus == GameStatus.End)
        {
            audioSource.clip = onEndClip;
            audioSource.loop = false;
        }

        audioSource.Play();
    }
}
