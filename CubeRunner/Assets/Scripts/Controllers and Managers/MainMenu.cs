using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {
        MusicPlayer.currentGameStatus = GameStatus.Menu;
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("RoadScene");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
