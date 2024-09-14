using System;

struct Vector
{
    double x;
    double y;
    double z;

    public static Vector operator +(Vector vec6, Vector vec8)
    {
        Vector v = new Vector();
        v.x = vec6.x + vec8.x;
        v.y = vec6.y + vec8.y;
        v.z = vec6.z + vec8.z;
        return v;
    }

    public static double operator *(Vector vec6, Vector vec8)
    {
        

        return vec6.x*vec8.x+vec6.y*vec8.y+vec6.z*vec8.z;

    }

    public static Vector operator *(double vec6, Vector vec8)
    {
        vec8.x *= vec6;
        vec8.y *= vec6;
        vec8.z *= vec6;
        return vec8;
    }

    public static bool operator >(Vector vec6, Vector vec8)
    {
        return Math.Sqrt(vec6.x * vec6.x + vec6.y * vec6.y + vec6.z * vec6.z) > Math.Sqrt(vec8.x * vec8.x + vec8.y * vec8.y + vec8.z * vec8.z);
    }

    public static bool operator <(Vector vec6, Vector vec8)
    {
        return Math.Sqrt(vec6.x * vec6.x + vec6.y * vec6.y + vec6.z * vec6.z) < Math.Sqrt(vec8.x * vec8.x + vec8.y * vec8.y + vec8.z * vec8.z);
    }

    public static bool operator ==(Vector vec6, Vector vec8)
    {
        return Math.Sqrt(vec6.x * vec6.x + vec6.y * vec6.y + vec6.z * vec6.z) == Math.Sqrt(vec8.x * vec8.x + vec8.y * vec8.y + vec8.z * vec8.z);
    }

    public static bool operator !=(Vector vec6, Vector vec8)
    {
        return Math.Sqrt(vec6.x * vec6.x + vec6.y * vec6.y + vec6.z * vec6.z) != Math.Sqrt(vec8.x * vec8.x + vec8.y * vec8.y + vec8.z * vec8.z);
    }
}

class Car
{
    public string Name { get; set; }
    public string Engnie {  get; set; }

    public double Speed {  get; set; }

    public override string ToString()
    {
        return Name;
    }

    public Car(string name, string engnie, double speed)
    {
        Name = name;
        Engnie = engnie;
        Speed = speed;
    }

    public bool Equals(Car other)
    {
        return this.Name == other.Name && this.Engnie == other.Engnie && this.Speed == other.Speed;
    }

    public interface IEquatable<T>
    {
        bool Equals(T other);
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode()+this.Engnie.GetHashCode()+this.Speed.GetHashCode();
    }
}


class CarsCatalog
{
    private Car[] cartarse;

    public CarsCatalog(params Car[] cartarse)
    {
        this.cartarse = cartarse;
    }

    public string this[int index]
    {
        get
        {
            return this.cartarse[index].Name+"Engine"+ this.cartarse[index].Engnie;
        }
    }
}

class Currency
{
    public double Value { get; set; }

    public Currency(double value)
    {
        Value = value;
    }
}

class CurrencyRUB : Currency
{
    public CurrencyRUB(double valiue) : base(valiue) { }

    public static implicit operator CurrencyUSD(CurrencyRUB currub)
    {
        return new CurrencyUSD(currub.Value * 202);
    }

    public static implicit operator CurrencyEUR(CurrencyRUB currub)
    {
        return new CurrencyEUR(currub.Value * 382);
    }
}

class CurrencyEUR : Currency
{
    public CurrencyEUR(double valiue) : base(valiue) { }

    public static implicit operator CurrencyRUB(CurrencyEUR currub)
    {
        return new CurrencyRUB(currub.Value / 382);
    }

    public static implicit operator CurrencyUSD(CurrencyEUR currub)
    {
        return new CurrencyUSD(currub.Value * 2);
    }
}

class CurrencyUSD : Currency
{
    public CurrencyUSD(double valiue) : base(valiue) { }

    public static implicit operator CurrencyRUB(CurrencyUSD currub)
    {
        return new CurrencyRUB(currub.Value / 202);
    }

    public static implicit operator CurrencyEUR(CurrencyUSD currub)
    {
        return new CurrencyEUR(currub.Value /2);
    }
}








































class Program
{
    public static void Main()
    {
        Car carcar20 = new Car("aa", "bb", 202);
        Car carcar28 = new Car("aa", "bb", 202);
        Console.WriteLine(carcar28.Equals(carcar20));

        CurrencyRUB crurub = new CurrencyRUB(202);
        CurrencyUSD cruusd = crurub;
        Console.WriteLine(((CurrencyUSD)crurub).Value);
        Console.WriteLine(cruusd.Value);


    }
}