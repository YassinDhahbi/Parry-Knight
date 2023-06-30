using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemyDataPreset", menuName = "Presets/EnemyDataPreset/RangedEnemyPreset")]
public class RangedEnemyDataPreset : EnemyDataPreset
{
    public float shootingRate;
    public ProjectilePreset preset;
}