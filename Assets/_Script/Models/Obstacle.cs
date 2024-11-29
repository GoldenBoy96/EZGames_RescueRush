using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle")]
public class Obstacle:ScriptableObject
{
    [SerializeField] float width = 1;
    [SerializeField] float length = 1;
    [SerializeField] float height = 1;
    [SerializeField] float rotation = 0;
    [SerializeField] bool isRandomRotate = false;   

    public float Width { get => width; set => width = value; }
    public float Length { get => length; set => length = value; }
    public float Height { get => height; set => height = value; }
    public float Rotation { get => rotation; set => rotation = value; }
    public bool IsRandomRotate { get => isRandomRotate; set => isRandomRotate = value; }
}
