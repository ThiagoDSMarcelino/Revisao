using System;
using System.Linq;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

public static class App
{
    public static void Run(Universe universe, int N = 1000)
    {
        ApplicationConfiguration.Initialize();

        var form = new Form();
        form.WindowState = FormWindowState.Maximized;
        form.FormBorderStyle = FormBorderStyle.None;

        PictureBox pb = new PictureBox();
        pb.Dock = DockStyle.Fill;
        form.Controls.Add(pb);

        Bitmap bmp = null;
        Graphics g = null;
        Point center = Point.Empty;

        Timer tm = new Timer();
        tm.Interval = 40;

        form.KeyDown += (o, e) =>
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        };

        form.Load += delegate
        {
            bmp = new Bitmap(pb.Width, pb.Height);
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            pb.Image = bmp;
            tm.Start();
            center = new Point(pb.Width / 2, pb.Height / 2);
        };

        tm.Tick += delegate
        {
            g.Clear(Color.Black);

            for (int i = 0; i < N; i++)
                universe.Update(60 * 0.025f);

            foreach (var x in universe.Bodies)
                x.Draw(g, center);

            pb.Refresh();
        };

        Application.Run(form);
    }
}

public abstract partial class Body
{
    public void Draw(Graphics g, Point center)
    {
        g.FillEllipse(new SolidBrush(this.Color),
            Position.X - Size / 2 + center.X,
            Position.Y - Size / 2 + center.Y,
            Size, Size);
    }
}