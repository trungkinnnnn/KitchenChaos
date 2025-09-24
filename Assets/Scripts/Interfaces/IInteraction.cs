public interface IInteraction : ISelectable
{
    void Interact(PlayerInteraction player);    
}

public interface ISelectable
{
    void OnSelected();
    void OnDeselected();
}