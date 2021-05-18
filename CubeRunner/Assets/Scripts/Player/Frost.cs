using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frost : MonoBehaviour
{
    [SerializeField] private float radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            DoorBarrier doorBarrier = hitCollider.GetComponent<DoorBarrier>();
            AxeBarrier axeBarrier = hitCollider.GetComponent<AxeBarrier>();

            if (doorBarrier != null || axeBarrier != null)
                hitCollider.SendMessage("DisableAnimation");
        }
    }
}
