using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jumper : MonoBehaviour
{
    [SerializeField] private Texture buttonTexture;
    [SerializeField] GUIStyle gUIStyle;

    private PlayerMovement playerMovement;
     // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnGUI()
    {
        if (playerMovement.GetAlive() && Time.timeScale != 0)
        {   
            if (GUI.Button(new Rect(650, 300, 80, 60), "Jump", gUIStyle))
            {
                playerMovement.LetJump();
            }
        }
    }
}
