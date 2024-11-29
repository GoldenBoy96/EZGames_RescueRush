using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] Obstacle obstacle;

    private void Start()
    {
        if (obstacle.IsRandomRotate)
        {
            float randomEuler = Random.Range(0, 360);
            transform.Rotate(new(0, randomEuler, 0));
        }
        else
        {
            transform.Rotate(new(0, obstacle.Rotation, 0));
        }
    }
}
