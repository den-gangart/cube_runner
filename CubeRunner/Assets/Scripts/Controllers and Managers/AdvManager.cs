using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvManager : MonoBehaviour
{
    public float timePause;

    private bool _canShow;
    // Start is called before the first frame update
    void Start()
    {
        if(Advertisement.isSupported)
        {
            Advertisement.Initialize("4103737", false);     
        }
        _canShow = false;
        StartCoroutine(SetCanShow());
    }

    public void ShowAds()
    {
        if(Advertisement.IsReady() && _canShow)
        {
            Advertisement.Show();
        }
    }

    public bool AdsIsShowing()
    {
        return Advertisement.isShowing;
    }

    private IEnumerator SetCanShow()
    {
        yield return new WaitForSeconds(timePause);
        _canShow = true;
    }
}
