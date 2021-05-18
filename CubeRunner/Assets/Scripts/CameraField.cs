using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraField : MonoBehaviour
{
    [SerializeField] LocationsController locationsController;
    [SerializeField] SpawnController spawnController;

    private Animator animator;
    private LocationType currentLocation;
    private float _fieldPos;
    // Start is called before the first frame update
    void Start()
    {
        _fieldPos = -1f;
        currentLocation = locationsController.GetLocation();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LocationType tempLocation = locationsController.GetLocation();
        if (currentLocation != tempLocation)
        {
            if (tempLocation == LocationType.Empty)
            {
                if (currentLocation == LocationType.Defualt || currentLocation == LocationType.Mixed)
                {
                    _fieldPos = spawnController.GetCurrentSpawnPos();
                   // animator.SetBool("Maximize", true);
                }
                else
                {
                    _fieldPos = spawnController.GetCurrentSpawnPos();
                   // animator.SetBool("Maximize", false);
                }
            }
            currentLocation = tempLocation;
        }

        if(_fieldPos > 0 && transform.position.z >= _fieldPos)
        {
            if(animator.GetBool("Maximize"))
                animator.SetBool("Maximize", false);
            else
                animator.SetBool("Maximize", true);
            _fieldPos = -1;
        }
    }
}
