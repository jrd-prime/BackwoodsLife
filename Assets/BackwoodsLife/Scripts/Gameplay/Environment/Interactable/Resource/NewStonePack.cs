using BackwoodsLife.Scripts.Data.Inventory.JObjects.ResourceObjects;

namespace BackwoodsLife.Scripts.Gameplay.Environment.Interactable.Resource
{
    public class NewStonePack : NewResource
    {
        public override EResourceType ResourceType { get; protected set; } = EResourceType.Stone;
    }
}
