namespace Behaviours.Interfaces
{
    public interface IDestructible : IInteractable
    {
        public int Health { get; set; }
        public int OrbAmount { get; set; }
        
        public void Hit();
        public void Destruct();
    }
}