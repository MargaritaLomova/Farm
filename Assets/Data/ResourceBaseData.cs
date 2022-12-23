using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceBaseData", menuName = "Resorces/Base", order = 1)]
public class ResourceBaseData : ScriptableObject
{
    [SerializeField]
    public SpriteRenderer SpriteRenderer;
}