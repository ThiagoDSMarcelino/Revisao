using static System.Console;

var pt = new Point(5, 5);
var circ = ConstrutorGeometrico.GetCirculo(pt, 5);
var squa = ConstrutorGeometrico.GetRetangulo(pt, 5, 5);
var tria = ConstrutorGeometrico.GetTriangulo(pt, 5, 5);

WriteLine(circ);
WriteLine(squa);
WriteLine(tria);

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public abstract class FiguraGeometrica
{
    public abstract float Area { get; }
    public abstract float Perimetro { get; }
}

public class Retangulo : FiguraGeometrica
{
    public Retangulo(float largura, float altura)
    {
        this.Base = largura;
        this.Altura = altura;
    }

    public float Base { get; set; }
    public float Altura { get; set; }
    public override float Area => this.Base * this.Altura;

    public override float Perimetro => this.Base * 2 + this.Altura * 2;
    public override string ToString() =>
        $"Retangulo:\nPerimetro: {this.Perimetro}\nArea: {this.Area}";
}

public class Triangluo : FiguraGeometrica
{
    public Triangluo(float largura, float altura)
    {
        this.Base = largura;
        this.Altura = altura;
    }

    public float Base { get; set; }
    public float Altura { get; set; }
    public override float Area => this.Base * this.Altura / 2;

    public override float Perimetro => this.Base + this.Altura + MathF.Sqrt(MathF.Pow(this.Base, 2) + MathF.Pow(this.Altura, 2));
    public override string ToString() =>
        $"Triangluo:\nPerimetro: {this.Perimetro}\nArea: {this.Area}";
}

public class Circulo : FiguraGeometrica
{
    public Circulo(float raio)
    {
        this.Raio = raio;
    }

    public float Raio { get; set; }
    public override float Area => MathF.PI * (this.Raio * this.Raio);

    public override float Perimetro => 2 * MathF.PI * this.Raio;
    public override string ToString() =>
        $"Circulo:\nPerimetro: {this.Perimetro}\nArea: {this.Area}";
}

public static class ConstrutorGeometrico
{
    public static FiguraGeometrica GetCirculo(
        Point centro, float raio)
    {
        return new Circulo(raio);
    }
    
    public static FiguraGeometrica GetRetangulo(
        Point cantoSuperiorEsquerdo, float altura, float largura)
    {
        return new Retangulo(altura, largura);
    }
    
    public static FiguraGeometrica GetTriangulo(
        Point cantoEsquerdo, float baseTriangulo, float altura)
    {
        return new Triangluo(baseTriangulo, altura);
    }
}