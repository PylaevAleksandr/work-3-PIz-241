using System;
using System.Drawing;

public class SquareMatrix
{
    private double[,] matrix;
    private int size;

    // Конструктор случайной матрицы
    public SquareMatrix(int size)
    {
        this.size = size;
        matrix = new double[size, size];
        Random rand = new Random();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = rand.NextDouble() * 10;
            }
        }
    }
    // Сделал не с первого раза, просто забыл репнуть его, и сохранилась только готовая персия
    // Делаем перезагрузку операторов
    public static SquareMatrix operator +(SquareMatrix a, SquareMatrix b)
    {
        if (a.size != b.size) throw new InvalidOperationException("Матрицы должны быть одинакового размера.");
        SquareMatrix result = new SquareMatrix(a.size);
        for (int i = 0; i < a.size; i++)
        {
            for (int j = 0; j < a.size; j++)
            {
                result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
            }
        }
        return result;
    }

    public static SquareMatrix operator *(SquareMatrix a, SquareMatrix b)
    {
        if (a.size != b.size) throw new InvalidOperationException("Матрицы должны быть одинакового размера.");
        SquareMatrix result = new SquareMatrix(a.size);
        for (int i = 0; i < a.size; i++)
        {
            for (int j = 0; j < a.size; j++)
            {
                for (int k = 0; k < a.size; k++)
                {
                    result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                }
            }
        }
        return result;
    }

    // Прочие перегрузки операторов (>, <, ==, !=, >=, <=)
    public static bool operator >(SquareMatrix a, SquareMatrix b)
    {
        return a.GetDeterminant() > b.GetDeterminant();
    }

    public static bool operator <(SquareMatrix a, SquareMatrix b)
    {
        return a.GetDeterminant() < b.GetDeterminant();
    }

    public static bool operator >=(SquareMatrix a, SquareMatrix b)
    {
        return a.GetDeterminant() >= b.GetDeterminant();
    }

    public static bool operator <=(SquareMatrix a, SquareMatrix b)
    {
        return a.GetDeterminant() <= b.GetDeterminant();
    }

    public static bool operator ==(SquareMatrix a, SquareMatrix b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(SquareMatrix a, SquareMatrix b)
    {
        return !a.Equals(b);
    }
                                                                        // Находим детерминант
    public double GetDeterminant()
    {
        if (size == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        throw new NotImplementedException("Метод детерминанта для больших матриц не реализован.");
    }
}



