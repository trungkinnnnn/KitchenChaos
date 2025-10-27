using UnityEngine;

[CreateAssetMenu(menuName = "Data/KitchenObjSO")]
public class KitchenObjSO : ScriptableObject
{
    public KitchenType kitchenType;
    public Sprite spriteObj;
    public string nameObj;
}

public enum KitchenType
{
    Cheese,
    CheeseSlices,
    Tomato,
    TomatoSlices,
    MeatUnCooked,
    MeetCooked,
    MeetBurned,
}



