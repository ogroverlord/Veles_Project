
namespace ValhalaProject
{
    public interface IThrowable
    {
        public float ThrowSpeed { get; }
        public bool Thrown { get; } //Enabled when first cast is thrown by the player
        public bool FirstCastThrown { get; } //Enabled for other cast to exclude them from light spell count
        public void Throw();
    }
}