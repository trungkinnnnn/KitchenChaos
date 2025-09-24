using System.Numerics;

public interface ICounterInfo
{
    string GetName(PlayerInteraction player);
    Vector3 GetPositionSpawer();
    bool CheckNUll();
    void SetKitchenObj(KitchenObj kitchenObj);
}