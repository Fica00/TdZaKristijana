using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bullet
{
    [field: SerializeField] public List<float> Speed { get; private set; }
    [field: SerializeField] public List<float> Damage { get; private set; }
}