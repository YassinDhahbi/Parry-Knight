using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dynamic Animating/Animation")]
public class DynamicAnimation : ScriptableObject
{
    public bool repeatable;

    public List<Sprite> listOfAnimationSprites;
}