using System;
using System.Drawing;

public class SquareMatrix
{
    private double[,] matrix;
    private int size;

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
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result += matrix[i, j].ToString("F2") + "\t";                                   // Форматируем числа (подсмотрено на просторах инэта)
            }
            result += "\n";
        }
        return result;
    }
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
    // Подсмотрел на гитхабе у одного типа, он сначала переопределяет equals и get hash code, а потом уже ищет детерминант, в общем сделал так же
    public override bool Equals(object obj)
    {
        if (obj is SquareMatrix)
        {
            SquareMatrix other = (SquareMatrix)obj;
            if (size != other.size) return false;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] != other.matrix[i, j]) return false;
                }
            }
            return true;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(matrix, size);
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
    public SquareMatrix DeepCopy()
    {
        SquareMatrix copy = new SquareMatrix(size);
        Array.Copy(matrix, copy.matrix, matrix.Length);
        return copy;
    }
    public class MatrixOperationException : Exception                               // Добавляем классы исключений
    {
        public MatrixOperationException(string message) : base(message)
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SquareMatrix matrixA = new SquareMatrix(2);
            SquareMatrix matrixB = new SquareMatrix(2);

            Console.WriteLine("Матрица A:");
            Console.WriteLine(matrixA.ToString());

            Console.WriteLine("Матрица B:");
            Console.WriteLine(matrixB.ToString());
                                                                        
            SquareMatrix matrixC = matrixA + matrixB;
            Console.WriteLine("A + B:");
            Console.WriteLine(matrixC.ToString());
           
            SquareMatrix matrixD = matrixA * matrixB;
            Console.WriteLine("A * B:");
            Console.WriteLine(matrixD.ToString());
           
            Console.WriteLine($"Детерминант A: {matrixA.GetDeterminant()}");
            Console.WriteLine($"Детерминант B: {matrixB.GetDeterminant()}");
            Console.WriteLine($"A == B: {matrixA == matrixB}");
        }
    }
}           // В общем это какая то жесть, долго думал как сделать что б матрица и ответы отображались при колмпилировании, по итогу все готово.