
using UnityEngine;

public interface IPickable
{
    void PickUp(PlayerInteraction player, Transform transform, Vector3 position);
}

