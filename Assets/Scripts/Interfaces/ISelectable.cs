using UnityEngine;
public interface ISelectable
{
    void OnSelected(PlayerInteraction player);
    void OnDeselected();
    Transform GetTransform();
}