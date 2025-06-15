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
    // Сделал не с первого раза, просто забыл репнуть его, и сохранилась только готовая версия
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

    // Остальные перегрузки операторов (>, <, ==, !=, >=, <=)
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

    // Естественно я это делал все не с первого раза, сейчас уже просто поэтапно выгружаю. вообще на этом моменте я чуть не умер,
    // по этому и пошел в гит смотреть че да как. (даже нейронкой чуть пришлось воспользоваться, я ж не всесильный)
    // Добавляем метод глубокого копирования "Прототип" (кстати класное название, игру такую помню)
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
    class Program                                                                   // И делаем приложение матричный калькулятор
    {
        static void Main(string[] args)
        {
            SquareMatrix matrixA = new SquareMatrix(2);
            SquareMatrix matrixB = new SquareMatrix(2);

            Console.WriteLine("Матрица A:");
            Console.WriteLine(matrixA.ToString());

            Console.WriteLine("Матрица B:");
            Console.WriteLine(matrixB.ToString());

            // Сложение матриц
            SquareMatrix matrixC = matrixA + matrixB;
            Console.WriteLine("A + B:");
            Console.WriteLine(matrixC.ToString());

            // Умножение матриц
            SquareMatrix matrixD = matrixA * matrixB;
            Console.WriteLine("A * B:");
            Console.WriteLine(matrixD.ToString());

            // Проверка детерминантов
            Console.WriteLine($"Детерминант A: {matrixA.GetDeterminant()}");
            Console.WriteLine($"Детерминант B: {matrixB.GetDeterminant()}");

            // Проверка на равенство
            Console.WriteLine($"A == B: {matrixA == matrixB}");
        }
    }
}

