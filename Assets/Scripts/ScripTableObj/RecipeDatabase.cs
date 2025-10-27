using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    public List<ProcessGroup> processGroups;
}

[System.Serializable]
public class ProcessGroup
{
    public CounterType counterType;
    public List<ProcessRule> rules;
}

[System.Serializable]
public class ProcessRule
{
    public KitchenType inputType;
    public KitchenType outputType;
    public float processTime;
    public GameObject resultPrefab;
}

public enum CounterType
{
    Slice,
    Fry,
}