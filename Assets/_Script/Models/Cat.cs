using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Cat", menuName = "ScriptableObjects/Cat")]
public class Cat :ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    public float Speed { get => speed; set => speed = value; }
    public float Distance { get => distance; set => distance = value; }
}
