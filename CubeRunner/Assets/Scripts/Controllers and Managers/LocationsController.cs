using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LocationType
{
    Defualt,
    Mixed,
    Axe,
    Cone,
    Door,
    Hole,
    Empty
}

public class LocationsController : MonoBehaviour
{
    public int minLocationLength;
    public int maxLocationLength;
    public int emptyLocationLength;
    public int themeLocationLength;
    public int defaultLocationLength;
    public int mixedStep;
    private LocationType location;
    private LocationType prevLocation;

    private int[] barrierSequence;
    
    private int currentBarrier;
    private int locationLength;

    // Start is called before the first frame update
    void Awake()
    {
       // openedLocations = new List<LocationType>();
        location = LocationType.Empty;
        prevLocation = location;

        SetLocationLength();
        currentBarrier = 0;
        barrierSequence = new int[locationLength];
        CreateBarrierSequence();
    }

    private void SetLocationLength()
    {
        if (location == LocationType.Empty)
            locationLength = emptyLocationLength;
        else if (location != LocationType.Mixed && location != LocationType.Defualt)
            locationLength = themeLocationLength;
        else if (location == LocationType.Defualt)
            locationLength = defaultLocationLength;
        else
            locationLength = Random.Range(minLocationLength, maxLocationLength);
    }

    private void CreateBarrierSequence()
    {
        int mixetStepTemp = mixedStep;

        if (location == LocationType.Empty)
        {
            for (int i = 0; i < locationLength; i++)
            {
                barrierSequence[i] = 0;
            }
        }
        else if(location == LocationType.Defualt)
        {
            for (int i = 0; i < locationLength; i++)
            {
                barrierSequence[i] = 1;
            }
        }
        else if(location == LocationType.Hole)
        {
            for (int i = 0; i < locationLength; i++)
            {
                if( i % 3 == 0)
                    barrierSequence[i] = 2;
                else
                    barrierSequence[i] = 0;
            }
        }
        else if(location == LocationType.Axe)
        {
            for (int i = 0; i < locationLength; i++)
            {
                if (i % 2 == 0)
                    barrierSequence[i] = 0;
                else
                    barrierSequence[i] = 3;
            }
        }
        else if (location == LocationType.Door)
        {
            for (int i = 0; i < locationLength; i++)
            {
                if (i % 3 == 0)
                    barrierSequence[i] = 4;
                else
                    barrierSequence[i] = 0;
            }
        }
        else if (location == LocationType.Cone)
        {
            for (int i = 0; i < locationLength; i++)
            {
                if (i % 3 == 0)
                    barrierSequence[i] = 5;
                else
                    barrierSequence[i] = 0;
            }
        }
        else if (location == LocationType.Mixed)
        {
            for (int i = 0; i < locationLength; i++)
            {
                if (i == mixetStepTemp)
                {
                    barrierSequence[i] = Random.Range(2, 6);
                    mixetStepTemp += mixetStepTemp;
                }
                else if(i == mixetStepTemp+1 || i == mixetStepTemp - 1)
                    barrierSequence[i] = 0;
                else
                    barrierSequence[i] = 1;
            }

        }

    }

    public int GetCurrentBarrierType()
    {
        currentBarrier++;
        if (currentBarrier >= locationLength)
            UpdateLocation();

        return barrierSequence[currentBarrier-1];
    }

 
    private void UpdateLocation()
    {
        if (location == LocationType.Empty && prevLocation == LocationType.Empty)
        {
            location = LocationType.Defualt;
        }
        else
        {
            if (location == LocationType.Empty)
            {
                if (prevLocation == LocationType.Axe || prevLocation == LocationType.Cone 
                    || prevLocation == LocationType.Door || prevLocation == LocationType.Hole)
                {
                    prevLocation = location;
                    location = LocationType.Mixed;
                }
                else
                {
                    prevLocation = location;

                    int randomLocation = Random.Range(1, 5);

                    if(randomLocation == 1)
                        location = LocationType.Hole;
                    else if (randomLocation == 2)
                        location = LocationType.Axe;
                    else if (randomLocation == 3)
                        location = LocationType.Door;
                    else if (randomLocation == 4)
                        location = LocationType.Cone;
                }
            }
            else
            {
                prevLocation = location;
                location = LocationType.Empty;
            }
        }

        SetLocationLength();
        currentBarrier = 1;
        barrierSequence = new int[locationLength];
        CreateBarrierSequence();
    }

    public LocationType GetLocation()
    {
        return location;
    }
}
