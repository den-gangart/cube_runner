using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedPlatform
{
    private GameObject _platform;
    private List<GameObject> _barriers;

    public CreatedPlatform(GameObject platform)
    {
        _platform = platform;
        _barriers = new List<GameObject>();
    }

    public void AddBarrier(GameObject barrier)
    {
        _barriers.Add(barrier);
    }

    public GameObject GetPlatform()
    {
        return _platform;
    }

    public GameObject GetBarrier(int index)
    {
        return _barriers[index];
    }

    public void RemoveBarrier(int index)
    {
        _barriers.RemoveAt(index);
    }

    public int GetCountBarriers()
    {
        return _barriers.Count;
    }
}

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject[] barrierPrefabs;
    [SerializeField] private GameObject coinPrefab;

    public Vector3 _spawnPos;
    public float step;
    public int maxCountPlatforms;

    private int _barrierType;
    private List<CreatedPlatform> _platforms;
    private LocationsController locationsController;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerInitialization>().GetCurrentPlayerObject().transform;

        locationsController = GetComponent<LocationsController>();
        _barrierType = 1;
        _platforms = new List<CreatedPlatform>();
        for (int i = 0; i < 10; i++)
            SpawnPlatform();

        StartCoroutine(PauseSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (_platforms.Count > maxCountPlatforms)
        {
            GameObject temp = _platforms[0].GetPlatform();
            if (Vector3.Distance(temp.transform.position, player.position) > 10 && temp.transform.position.z < player.position.z)
            {
                RemoveCreatedPlatform(_platforms[0]);
                _platforms.RemoveAt(0);
            }
        }
    }

    private void RemoveCreatedPlatform(CreatedPlatform createdPlatform)
    {
        Destroy(createdPlatform.GetPlatform().gameObject);

        while (createdPlatform.GetCountBarriers() != 0)
        {
            Destroy(createdPlatform.GetBarrier(0).gameObject);
            createdPlatform.RemoveBarrier(0);
        }
    }

    private IEnumerator PauseSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnPlatform();
        StartCoroutine(PauseSpawn());
    }

    private void SpawnPlatform()
    {
        if(_platforms.Count <= maxCountPlatforms)
        {
            GameObject platform = Instantiate(platformPrefab) as GameObject;

            platform.transform.position = _spawnPos;
            platform.name = "platform" + _spawnPos.z.ToString();
            _spawnPos.z += step; 
            _platforms.Add(new CreatedPlatform(platform));
       

            SpawnBarrier();
        }
    }

    public float GetCurrentSpawnPos()
    {
        return _spawnPos.z-step;
    }

    private void SpawnBarrier()
    {
        int lastPlatform = _platforms.Count - 1;
        _barrierType = locationsController.GetCurrentBarrierType();
        if (_barrierType == 1)
        {
            int countBarriers = Random.Range(2, 4);
            List<int> unusedPos = new List<int>() {1,2,3};

            for (int i = 0; i < countBarriers; i++)
            {
                GameObject barrier = Instantiate(barrierPrefabs[0]) as GameObject;
                barrier.GetComponent<Barrier>().LocateBarrier(_platforms[lastPlatform].GetPlatform());

                int rand = Random.Range(0, unusedPos.Count);
                barrier.GetComponent<Barrier>().SetOtherPos(unusedPos[rand]);
                unusedPos.RemoveAt(rand);
                _platforms[lastPlatform].AddBarrier(barrier);
            }
        }
        else if (_barrierType == 2)
        {
            GameObject barrier = Instantiate(barrierPrefabs[1]) as GameObject;
            barrier.GetComponent<WallBarrier>().LocateBarrier(_platforms[lastPlatform].GetPlatform());
            _platforms[lastPlatform].AddBarrier(barrier);
        }
        else if (_barrierType == 3)
        {
            GameObject barrier = Instantiate(barrierPrefabs[2]) as GameObject;
            barrier.GetComponent<AxeBarrier>().LocateBarrier(_platforms[lastPlatform].GetPlatform());
            _platforms[lastPlatform].AddBarrier(barrier);
        }
        else if (_barrierType == 4)
        {
            GameObject barrier = Instantiate(barrierPrefabs[3]) as GameObject;
            barrier.GetComponent<DoorBarrier>().LocateBarrier(_platforms[lastPlatform].GetPlatform());
            _platforms[lastPlatform].AddBarrier(barrier);
        }
        else if (_barrierType == 5)
        {
            GameObject barrier = Instantiate(barrierPrefabs[4]) as GameObject;
            barrier.GetComponent<ConeBarrier>().LocateBarrier(_platforms[lastPlatform].GetPlatform());
            _platforms[lastPlatform].AddBarrier(barrier);
        }
        else if(_barrierType == 0)
        {
            SpawnCoin();
        }
    }

    public void SpawnCoin()
    {
        GameObject coin = Instantiate(coinPrefab) as GameObject;

        GameObject platform = _platforms[_platforms.Count - 1].GetPlatform();

        float platformZ = platform.transform.position.z;

        float posX = Random.Range(-6, 6);
        float posZ = Random.Range(platformZ - 2, platformZ + 2);

        coin.transform.position = new Vector3(posX, coin.transform.position.y, posZ);
        coin.GetComponent<CoinPickUp>().target = player.gameObject.transform;
    }
}
