using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopStats
{
    public int HealthPoints { get; set; }
    public int AttackPower { get; set; }
    public string Range { get; set; }
    public float Speed { get; set; } // Change the type to float for speed
    public int Cost { get; set; }
    public string SpecialAbilities { get; set; }
}