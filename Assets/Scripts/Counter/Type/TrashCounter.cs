public class TrashCounter : BaseCounter
{
    public override void SetKitchenObj(KitchenObj objKitchen)
    {
        objKitchen.TrashKitchen();
    }
}

