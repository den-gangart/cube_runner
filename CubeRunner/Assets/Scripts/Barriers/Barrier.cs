using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour, BarrierInterface
{
    public int indexBarrier;
    private GameObject _platform;

    public void LocateBarrier(GameObject platform)
    {
        _platform = platform;
        float scaleY = Random.Range(2, 6);
        float scaleX = Random.Range(1, 4);

        transform.localScale = new Vector3(scaleX, scaleY, 1);

        float platformZ = platform.transform.position.z;

        float platformWidth = platform.transform.localScale.x;
        float platformDeep = platform.transform.localScale.z;

        float barriermWidth = transform.localScale.x;
        float barriermDeep = transform.localScale.z;
        
        float posX = Random.Range(-(platformWidth - barriermWidth) / 2, (platformWidth - barriermWidth) / 2);
        float posZ = Random.Range(-(platformDeep - barriermDeep) / 2, (platformDeep - barriermDeep) / 2) + platformZ;

        transform.position = new Vector3(posX, scaleY / 2, posZ);

    }

    public void SetOtherPos(int indexPos)
    {

        float platformZ = _platform.transform.position.z;

        float platformWidth = _platform.transform.localScale.x;
        float platformDeep = _platform.transform.localScale.z;

        float barriermWidth = transform.localScale.x;
        float barriermDeep = transform.localScale.z;

        float posX = 0f;
        if (indexPos == 1)
        {
           posX = Random.Range(-(platformWidth - barriermWidth) / 2, -(platformWidth/2 - barriermWidth) / 1.5f);
        }
        else if(indexPos == 2)
        {
            posX = Random.Range(-(platformWidth/2 - barriermWidth) / 1.5f, (platformWidth /2 - barriermWidth) / 1.5f);
        }
        else if (indexPos == 3)
        {
            posX = Random.Range((platformWidth / 2 - barriermWidth) / 1.5f, (platformWidth - barriermWidth) / 2);
        }

        float posZ = Random.Range(-(platformDeep - barriermDeep/2) / 4, (platformDeep - barriermDeep/2) / 4) + platformZ;

        transform.position = new Vector3(posX, transform.position.y / 2, posZ);
    }
}
