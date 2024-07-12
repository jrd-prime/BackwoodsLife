using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Resource
{
    public class NewStonePack : NewCollectable
    {
        public override EResourceType ResourceType { get; protected set; } = EResourceType.Stone;
    }
}
