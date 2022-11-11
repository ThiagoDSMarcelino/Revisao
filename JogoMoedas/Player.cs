public abstract class Player
{
    public int Moeda { get; protected set; } = 1;
    public abstract bool Decidir();
    public virtual void Recebe(int valor) 
    {
        this.Moeda += valor;
    }
}