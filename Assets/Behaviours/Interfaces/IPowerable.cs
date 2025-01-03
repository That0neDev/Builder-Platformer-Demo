namespace Behaviours.Interfaces
{
    public interface IPowerable
    {
        public bool Powered { get; set; }
        public void PowerUp(float Value);
        public float GetPower();
    }
}