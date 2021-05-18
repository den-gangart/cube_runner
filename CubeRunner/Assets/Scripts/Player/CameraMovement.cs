using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] PlayerInitialization playerInitialization;

    private Transform playerPos;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = playerInitialization.GetCurrentPlayerObject().transform;

        _offset = playerPos.position - transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        float smoothX = ((playerPos.position.x - transform.position.x)/0.3f)*Time.deltaTime;

        Vector3 move  = playerPos.position - _offset;
        move.x = smoothX+ transform.position.x;

        transform.position = move;
    }
}
