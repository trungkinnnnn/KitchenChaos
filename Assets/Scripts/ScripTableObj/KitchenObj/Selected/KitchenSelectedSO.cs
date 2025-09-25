using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/KitchenSelected")]
public class KitchenSelectedSO : ScriptableObject
{
    public Material normalMat;
    public Material selectedMat;
    public Texture2D seletedTexture2D;
}
