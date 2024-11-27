using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spawner
{
    [SerializeField] private Vector3 spawnCoordination;
    [SerializeField] private Vector3 radius;

    public Spawner()
    {
        spawnCoordination = Vector3.zero;
        radius = Vector3.zero;
    }

    public Spawner(Vector3 spawnCoordination, Vector3 radius)
    {
        this.spawnCoordination = spawnCoordination;
        this.radius = radius;
    }

    public Vector3 SpawnCoordination { get => spawnCoordination; set => spawnCoordination = value; }
    public Vector3 Radius { get => radius; set => radius = value; }
}
