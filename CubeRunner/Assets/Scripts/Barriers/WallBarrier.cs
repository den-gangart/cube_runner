using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBarrier : MonoBehaviour, BarrierInterface
{
    [SerializeField] GameObject leftPart;
    [SerializeField] GameObject rightPart;
    [SerializeField] GameObject hole;

    public float holeWidth = 0.2f;
    
    public void LocateBarrier(GameObject platform)
    {
        transform.position = new Vector3(platform.transform.position.x, 4, platform.transform.position.z);

        float xScaleLeft = Random.Range(0.1f, 0.85f);
        leftPart.transform.localScale = new Vector3(xScaleLeft, leftPart.transform.localScale.y, leftPart.transform.localScale.z);

        float posXLeft = -(15 - xScaleLeft*15) / 2;
        leftPart.transform.position = new Vector3(posXLeft, leftPart.transform.position.y, leftPart.transform.position.z);

        float xScaleRight = 1 - holeWidth - xScaleLeft;

        if (xScaleRight <= 0)
        {
            Destroy(rightPart);
        }
        else
        {
            rightPart.transform.localScale = new Vector3(xScaleRight, rightPart.transform.localScale.y, rightPart.transform.localScale.z);

            float posXRight = (15 - xScaleRight * 15) / 2;
            rightPart.transform.position = new Vector3(posXRight, rightPart.transform.position.y, rightPart.transform.position.z);
        }

        float posXHole = posXLeft + ((xScaleLeft - hole.transform.localScale.x) * 15) / 2;
        posXHole += hole.transform.localScale.x * 15 + 0.01f;

        hole.transform.position = new Vector3(posXHole, hole.transform.position.y, hole.transform.position.z);
    }
}
