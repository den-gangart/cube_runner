using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBarrier : MonoBehaviour, BarrierInterface
{
    [SerializeField] Animator animator;

    public void LocateBarrier(GameObject platform)
    {
        animator.Play("Doors", 0, Random.Range(0.1f, 0.9f));

        transform.position = new Vector3(platform.transform.position.x, 6f, platform.transform.position.z);
        //transform.SetParent(platform.transform, true);
    }

    public void DisableAnimation()
    {
        animator.enabled = false;
    }
}

