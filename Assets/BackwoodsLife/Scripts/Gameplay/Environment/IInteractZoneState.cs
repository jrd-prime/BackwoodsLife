namespace BackwoodsLife.Scripts.Gameplay.Environment
{
    public interface IInteractZoneState
    {
        public string StateDesc { get; }
        public void Enter(InteractZone interactZone);
        public void Exit();
    }
}
