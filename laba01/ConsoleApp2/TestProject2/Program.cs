using System;

public class Rectangle
{
    private double sideA;
    private double sideB;

    public Rectangle(double sideA, double sideB)
    {
        this.sideA = sideA;
        this.sideB = sideB;
    }

    private double CalculateArea()
    {
        return sideA * sideB;
    }

    private double CalculatePerimeter()
    {
        return 2 * (sideA + sideB);
    }

    public double Area
    {
        get { return CalculateArea(); }
    }

    public double Perimeter
    {
        get { return CalculatePerimeter(); }
    }
}

class Point
{
    private int x;
    private int y;
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }
}

class Figure
{
    public string Name { get; set; }

    private Point[] pp;


    public Figure(Point op2, Point op4, Point op6)
    {
        pp = new Point[3];
        pp[0] = op2;
        pp[1] = op4;
        pp[2] = op6;
        Name = "treugolnik";
    }

    public Figure(Point op2, Point op4, Point op6, Point op8) : this(op2, op4, op6)
    {
        Array.Resize(ref pp, 4);
        pp[3] = op8;
        Name = "cheturehugolnik";
    }

    public Figure(Point op2, Point op4, Point op6, Point op8, Point op12) : this(op2, op4, op6, op8)
    {
        Array.Resize(ref pp, 5);
        pp[4] = op12;
        Name = "pyatiugolnik";
    }

    public double LengthSide(Point A, Point B)
    {
        return Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
    }

    public double PerimeterCalculator()
    {
        double p = 0;
        for (int i = 0; i < pp.Length; i++)
        {
            p += LengthSide(pp[i], pp[(i + 1) % pp.Length]);
        }
        return p;
    }
}


