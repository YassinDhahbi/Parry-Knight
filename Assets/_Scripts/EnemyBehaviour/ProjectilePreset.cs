using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Presets / Projectile Preset")]
public class ProjectilePreset : ScriptableObject
{
    public string projectileName;
    public Sprite projectileSprite;
    public float damage;
    public float shootingForce;
}