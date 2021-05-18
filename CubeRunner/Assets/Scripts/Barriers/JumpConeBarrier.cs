using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpConeBarrier : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerMovement>();

        if(player != null)
            player.LetJump();
    }
}
