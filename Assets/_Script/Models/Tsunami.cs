using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Tsunami", menuName = "ScriptableObjects/Tsunami")]
public class Tsunami:ScriptableObject
{
    [SerializeField] private float speedPhase_0;
    [SerializeField] private float speedPhase_1;
    [SerializeField] private float speedPhase_2;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private float startDistance;

    public float SpeedPhase_0 { get => speedPhase_0; set => speedPhase_0 = value; }
    public float SpeedPhase_1 { get => speedPhase_1; set => speedPhase_1 = value; }
    public float SpeedPhase_2 { get => speedPhase_2; set => speedPhase_2 = value; }
    public float Width { get => width; set => width = value; }
    public float Height { get => height; set => height = value; }
    public float StartDistance { get => startDistance; set => startDistance = value; }
}
