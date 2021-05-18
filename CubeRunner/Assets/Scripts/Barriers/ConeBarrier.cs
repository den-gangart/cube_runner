using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBarrier : MonoBehaviour, BarrierInterface
{
    [SerializeField] JumpConeBarrier jumpCone;

    public float _minX;
    public float _maxX;

    public void LocateBarrier(GameObject platform)
    {
        transform.position = new Vector3(platform.transform.position.x, 0.5f, platform.transform.position.z);

        float randomX = Random.Range(_minX, _maxX);
        jumpCone.transform.position = new Vector3(randomX, jumpCone.transform.position.y, jumpCone.transform.position.z);
        //transform.SetParent(platform.transform, true);
    }
}
