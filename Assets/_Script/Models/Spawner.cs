using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spawner
{
    [SerializeField] private Vector3 spawnCoordination;
    [SerializeField] private float radius;

    public Spawner()
    {
        spawnCoordination = Vector3.zero;
        radius = 0;
    }

    public Spawner(Vector3 spawnCoordination, float radius)
    {
        this.spawnCoordination = spawnCoordination;
        this.radius = radius;
    }

    public Vector3 SpawnCoordination { get => spawnCoordination; set => spawnCoordination = value; }
    public float Radius { get => radius; set => radius = value; }
}
