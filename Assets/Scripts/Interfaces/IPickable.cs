
using Unity.VisualScripting;
using UnityEngine;

public interface IPickable
{
    void Interact(PlayerInteraction player);
    KitchenObj PickUp(PlayerInteraction player, Transform transform);
    void Place(PlayerInteraction player, Transform transform = null);
}

