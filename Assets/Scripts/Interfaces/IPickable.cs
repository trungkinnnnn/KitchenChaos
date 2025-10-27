using UnityEngine;

public interface IPickable
{
    KitchenObj PickUp(PlayerInteraction player, Transform transform, BaseCounter counter = null);

    BaseCounter GetBaseCounter();
}

