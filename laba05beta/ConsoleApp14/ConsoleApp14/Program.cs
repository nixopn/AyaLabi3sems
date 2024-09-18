using System;

class MyMatrix
{
    int[,] matrixaa;
    private Random randemor = new Random();
    int n;
    int m;

    public MyMatrix(int m, int n, int minzap, int maxzap)
    {
        matrixaa = new int[m, n];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrixaa[i, j] = randemor.Next(minzap, maxzap);
            }
        }
        this.n = n;
        this.m = m;
    }

    public int this[int m, int n]
    {
        get { return matrixaa[m, n]; }
        set { matrixaa[m, n] = value; }

    }

    public static MyMatrix operator +(MyMatrix m1, MyMatrix m2)
    {
        if (m1.n == m2.n && m1.m == m2.m)
        {
            MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
            for (int i = 0; i < m1.m; i++)
            {
                for (int j = 0; j < m2.n; j++)
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

    public void Fill(int minzap, int maxzap)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrixaa[i, j] = randemor.Next(minzap, maxzap);
            }
        }
    }

    public static MyMatrix operator -(MyMatrix m1, MyMatrix m2)
    {
        if (m1.n == m2.n && m1.m == m2.m)
        {
            MyMatrix result = new MyMatrix(m1.n, m1.m, 0, 0);
            for (int i = 0; i < m1.m; i++)
            {
                for (int j = 0; j < m2.n; j++)
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
        MyMatrix result = new MyMatrix(m1.m, m1.n, 0, 0);
        for (int i = 0; i < m1.m; i++)
        {
            for (int j = 0; j < m1.n; j++)
            {
                result[i, j] = m1[i, j] * m2;
            }
        }
        return result;
    }

    public static MyMatrix operator /(MyMatrix m1, int m2)
    {
        MyMatrix result = new MyMatrix(m1.m, m1.n, 0, 0);
        for (int i = 0; i < m1.m; i++)
        {
            for (int j = 0; j < m1.n; j++)
            {
                result[i, j] = m1[i, j] / m2;
            }
        }
        return result;
    }

    public void ChangeSize(int n, int m)
    {
        int[,] newMatrix = new int[m, n];

        for (int i = 0; i < Math.Min(m, matrixaa.GetLength(0)); i++)
        {
            for (int j = 0; j < Math.Min(n, matrixaa.GetLength(1)); j++)
            {
                newMatrix[i, j] = matrixaa[i, j];
            }
        }


        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i >= matrixaa.GetLength(0) || j >= matrixaa.GetLength(1))
                {
                    newMatrix[i, j] = randemor.Next();
                }
            }
        }

        matrixaa = newMatrix;
    }

    public void ShowPartially(int aa, int bb)
    {
        for(int i = 0; i < this.m; i++)
        {
            for(int j = 0; j < this.n; j++) {
                if (matrixaa[i, j] >= aa && matrixaa[i,j] <= bb) {
                    Console.WriteLine(matrixaa[i, j]);
                }
            }
        }
    }

    public void Show()
    {
        for (int i = 0; i < this.m; i++)
        {
            for (int j = 0; j < this.n; j++)
            {
                Console.WriteLine(matrixaa[i, j]);
            }
        }
    }
}

class MyList<T>
{
    private T[] arreyuio; 
    private int indeopio;


    public T this[int indexaer]
    {
        get { return arreyuio[indexaer]; }
        set { arreyuio[indexaer] = value; }

    }

    public MyList( int indeopio, params T[] arreyuio)
    {
        this.arreyuio = arreyuio;
        this.indeopio = indeopio;
    }

    public void Resize()
    {
        T[] ararerer = new T[this.arreyuio.Length*2];
        for(int i = 0;i < arreyuio.Length; i++)
        {
            ararerer[i] = this.arreyuio[i];
        }
        arreyuio = ararerer;
        indeopio = ararerer.Length/2;
    }









    public void add(T aaee)
    {
        if(arreyuio.Length == indeopio)
        {
            Resize();
        }

        arreyuio[indeopio] = aaee;
        indeopio += 1;




    }

    public int lengthiuoo
    {
        get
        {
            return indeopio;
        }
    }

}


class MyDictionary<Tkey, TValue>
{
    Tkey[] aaaa;
    TValue[] bbbb;

}

class Programm
{
    public static void Main()
    {
        MyList<int> aaaa = new MyList<int>(4, 1, 2, 3, 4);
        aaaa.add(1);

        Console.WriteLine( aaaa.lengthiuoo );

        for(int i = 0; i < aaaa.lengthiuoo; i++)
        {
            Console.WriteLine(aaaa[i] );
        }


    }
}