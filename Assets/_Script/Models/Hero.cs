using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Hero", menuName = "ScriptableObjects/Hero")]
public class Hero : ScriptableObject
{
    [SerializeField] private float speed;

    public float Speed { get => speed; set => speed = value; }
}
