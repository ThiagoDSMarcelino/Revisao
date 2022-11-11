public class Copiador : Player
{
    private bool vouAjudar { get; set; } = true;
    public override bool Decidir()
    {
        return this.vouAjudar;
    }
    public override void Recebe(int valor)
    {
        this.vouAjudar = valor > 0;
        base.Recebe(valor);
    }
}