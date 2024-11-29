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
    [SerializeField] float rotation = 0;
    [SerializeField] bool isRandomRotate = false;

    public float Speed { get => speed; set => speed = value; }
    public float Distance { get => distance; set => distance = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public bool IsRandomRotate { get => isRandomRotate; set => isRandomRotate = value; }
}
