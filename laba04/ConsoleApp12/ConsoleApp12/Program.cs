using System;

class MyMatrix
{
    int[,] matrixaa;
    private Random randemor = new Random();
    int n;
    int m;

    public MyMatrix(int n, int m, int minzap, int maxzap)
    {
        matrixaa = new int[n, m];
        for(int i = 0; i < n; i++)
        {
            for(int j = 0; j < m; j++)
            {
                matrixaa[i, j]= randemor.Next(minzap, maxzap);
            }
        }
        this.n = n;
        this.m = m;
    }

    public int this[int n, int m]
    {
        get { return matrixaa[n, m]; }
        set { matrixaa[n, m] = value; }

    }

    public static MyMatrix operator +(MyMatrix m1, MyMatrix m2)
    {
        if (m1.n == m2.n && m1.m == m2.m) {
        MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
        for (int i = 0; i < m1.n; i++)
        {
            for (int j = 0; j < m2.m; j++)
            {
                result[i, j] = m1[i, j] + m2[i, j];
            }
        }
        return result;
        }
        else
        {
            MyMatrix result = new MyMatrix(0, 0, 0, 0);
            return result;
        }
    }

    public static MyMatrix operator -(MyMatrix m1, MyMatrix m2)
    {
        if (m1.n == m2.n && m1.m == m2.m)
        {
            MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
            for (int i = 0; i < m1.n; i++)
            {
                for (int j = 0; j < m2.m; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return result;
        }
        else
        {
            MyMatrix result = new MyMatrix(0, 0, 0, 0);
            return result;
        }
    }

    public static MyMatrix operator *(MyMatrix m1, int m2)
    {
            MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
            for (int i = 0; i < m1.n; i++)
            {
                for (int j = 0; j < m1.m; j++)
                {
                    result[i, j] = m1[i, j]*m2;
                }
            }
            return result;
    }

    public static MyMatrix operator /(MyMatrix m1, int m2)
    {
        MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
        for (int i = 0; i < m1.n; i++)
        {
            for (int j = 0; j < m1.m; j++)
            {
                result[i, j] = m1[i, j] / m2;
            }
        }
        return result;
    }

}

class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }

    public int MaxSpeed { get; set; }

    public Car(string n, int m, int zaer)
    {
        Name = n;
        ProductionYear = m;
        MaxSpeed = zaer;
    }

}

class CarComparer : IComparer<Car>
{
    private int howcompe;

    public CarComparer(int howcompell)
    {
        this.howcompe = howcompell;
    }
    public int Compare(Car x, Car y)
    {
        if(howcompe == 6)
        {
            return x.Name.CompareTo(y.Name);
        }
        else if(howcompe == 8)
        {
            return x.ProductionYear.CompareTo(y.ProductionYear);
        }
        else if( howcompe == 98) {
            return x.MaxSpeed.CompareTo(y.MaxSpeed);
        }
        else
        {
            return 0;
        }
    }
}



class CarCatalor
{
    private Car[] carserer;

    public CarCatalor(params Car[] carserer)
    {
        this.carserer = carserer;
    }

    public IEnumerator<Car> GetEnumerator()
    {
        foreach(Car carserqw in carserer)
        {
            yield return carserqw;
        }
    }

    public IEnumerator<Car> GetEnumeratorReverse()
    {
        for (int i = carserer.Length-1; i > 0; i--)
        {
            yield return carserer[i];
        }
    }

    public IEnumerator<Car> GetEnumeratorYe(int yeeerosk)
    {
        foreach (Car carserqw in carserer)
        {
            if (carserqw.ProductionYear == yeeerosk)
            {
                yield return carserqw;
            }
        }
    }

    public IEnumerator<Car> GetEnumeratorSp(int eeeespod)
    {
        foreach (Car carserqw in carserer)
        {
            if(carserqw.MaxSpeed == eeeespod) 
            {
            yield return carserqw;
            }
        }
    }











}



class Program
{
    public static void Main()
    {
        Car[] allcarsa = new Car[2];
        allcarsa[0] = new Car("aawweerr", 2088, 202);
        allcarsa[1] = new Car("aa", 1998, 292);
        for(int i=0; i<allcarsa.Length; i++)
        {
            Console.WriteLine(allcarsa[i].Name);
            Console.WriteLine(allcarsa[i].ProductionYear);
            Console.WriteLine(allcarsa[i].MaxSpeed);
        }
        Array.Sort(allcarsa, new CarComparer(6));
        Console.WriteLine("Sort by name");
        for (int i = 0; i < allcarsa.Length; i++)
        {
            Console.WriteLine(allcarsa[i].Name);
            Console.WriteLine(allcarsa[i].ProductionYear);
            Console.WriteLine(allcarsa[i].MaxSpeed);
        }
        Array.Sort(allcarsa, new CarComparer(8));
        Console.WriteLine("Sort by prod year");
        for (int i = 0; i < allcarsa.Length; i++)
        {
            Console.WriteLine(allcarsa[i].Name);
            Console.WriteLine(allcarsa[i].ProductionYear);
            Console.WriteLine(allcarsa[i].MaxSpeed);
        }
        Array.Sort(allcarsa, new CarComparer(98));
        Console.WriteLine("Sort by speed");
        for (int i = 0; i < allcarsa.Length; i++)
        {
            Console.WriteLine(allcarsa[i].Name);
            Console.WriteLine(allcarsa[i].ProductionYear);
            Console.WriteLine(allcarsa[i].MaxSpeed);
        }

        Car aa20 = new Car("aawweerr", 2088, 202);
        Car aa28 = new Car("aa", 1998, 292);
        Car aa38 = new Car("aa", 2098, 292);
        Car aa48 = new Car("aa", 2008, 292);

        CarCatalor aaaa = new CarCatalor(aa20,aa28, aa38,aa48);
    }
}







































