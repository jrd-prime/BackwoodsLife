namespace BackwoodsLife.Scripts.Gameplay.Environment.InteractableZone.Interact.InteractZoneState
{
    public interface IInteractZoneState
    {
        public string StateDesc { get; }
        public void Enter(InteractZone interactZone);
        public void Exit();
    }
}
