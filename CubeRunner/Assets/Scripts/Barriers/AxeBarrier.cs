using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBarrier : MonoBehaviour, BarrierInterface
{
    [SerializeField] Animator animator;

    public void LocateBarrier(GameObject platform)
    {
        float platformZ = platform.transform.position.z;

        float platformWidth = platform.transform.localScale.x;
        float platformDeep = platform.transform.localScale.z;

        float barriermWidth = transform.localScale.x;
        float barriermDeep = transform.localScale.z;


        float posX = Random.Range(-(platformWidth - 2) / 2, (platformWidth - 2) / 2);

        transform.position = new Vector3(posX, -0.5f, platform.transform.position.z);

        animator.Play("Axe", 0, Random.Range(0.1f, 0.9f));

       // transform.position = new Vector3(platform.transform.position.x, 6.72f, platform.transform.position.z);
        //transform.SetParent(platform.transform, true);
    }

    public void DisableAnimation()
    {
        animator.enabled = false;
    }
}
