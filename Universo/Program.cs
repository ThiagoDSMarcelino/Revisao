using System;
using System.Drawing;
using System.Collections.Generic;

Universe universe = new Universe();
universe.Add(new Earth());
universe.Add(new Mon(270));
universe.Add(new Mon(90, -1));
universe.Add(new Foguete(20));
universe.Add(new Foguete(5, -1));
universe.Add(new Satelite(270));

App.Run(universe, 1000);

public abstract partial class Body
{
    public PointF Position { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public Color Color { get; set; }
    public float Size { get; set; }
    public float Mass { get; set; }

    public void Update(float dt)
    {
        Position = new PointF(
            Position.X + VelocityX * dt,
            Position.Y + VelocityY * dt
        );
    }

    public void ApplyForce(Body other, float dt)
    {
        const float G = 6.6743E-11f;
        float r = 1000 * 1000 * Distance(other);
        float force = G * this.Mass * other.Mass / (r * r);
        float acceleration = force / Mass;
        float dx = 1000 * 1000 * (other.Position.X - this.Position.X);
        float dy = 1000 * 1000 * (other.Position.Y - this.Position.Y);
        this.VelocityX += (dt * acceleration * dx / r) / 1000 / 1000;
        this.VelocityY += (dt * acceleration * dy / r) / 1000 / 1000;
    }

    public float Distance(Body other)
    {
        float dx = other.Position.X - this.Position.X;
        float dy = other.Position.Y - this.Position.Y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }
}

public class Earth : Body
{
    public Earth()
    {
        Position = PointF.Empty;
        VelocityX = 0f;
        VelocityY = 0f;
        Color = Color.Blue;
        Mass = 5.9742E24f;
        Size = 12.742f;
    }
}

public class Mon : Body
{
    public Mon(int angulo, int direcao = 1)
    {
        Position = new PointF((384.4f + 12.742f / 2) * MathF.Cos(angulo * (MathF.PI/180)), (384.4f + 12.742f / 2) * MathF.Sin(angulo * (MathF.PI/180))); // distância Terra-Lua
        VelocityX = 0.00103f * direcao;
        VelocityY = 0;
        Color = Color.White;
        Mass = 7.36E22f;
        Size = 3.4748f; // 3474,8 km
    }
}

public class Foguete : Body
{
    public Foguete(int velocidade, int direcao = 1)
    {
        Position = new PointF(0, 12.742f / 2 * direcao);
        VelocityX = 0;
        VelocityY = 0.001f * direcao * velocidade;
        Color = Color.White;
        Mass = 5E5f;
        Size = 3;
    }
}

public class Satelite : Body
{
    public Satelite(int angulo, int direcao = 1)
    {
        Position = new PointF((10f + 12.742f / 2) * MathF.Cos(angulo * (MathF.PI/180)), (10f + 12.742f / 2) * MathF.Sin(angulo * (MathF.PI/180)));
        VelocityX = MathF.Sqrt((6.6743E-11f * 5.9742E24f) / (12.742f / 2 + 10f)) / 1E9f;
        VelocityY = 0;
        Color = Color.White;
        Mass = 5E5f;
        Size = 3;
    }
}

public class Universe
{
    public List<Body> Bodies { get; private set; }
        = new List<Body>();
    
    public void Add(Body body)
        => Bodies.Add(body);

    public void Update(float dt)
    {
        foreach (var x in Bodies)
        {
            foreach (var y in Bodies)
            {
                if (x == y)
                    continue;
                
                x.ApplyForce(y, dt);
            }
        }

        foreach (var x in Bodies)
            x.Update(dt);
    }
}