using System;

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
}                                                          // Сделал не с первого раза, просто забыл репнуть его, и сохранилась только готовая персия


