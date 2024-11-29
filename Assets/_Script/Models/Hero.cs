using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hero
{
    [SerializeField] private float speed;
    [SerializeField] private float heroDefaultSpeed = 20;

    public Hero()
    {
        this.speed = heroDefaultSpeed;
    }
    public Hero(float speed)
    {
        this.speed = speed;
    }

    public float Speed { get => speed; set => speed = value; }
    public float HeroDefaultSpeed { get => heroDefaultSpeed; set => heroDefaultSpeed = value; }
}
