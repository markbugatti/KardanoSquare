using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KardanoSquare
{
    class MatrixHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix">Матриця, яку необхідно повернути вправо</param>
        /// <param name="size">Розмір матриці</param>
        public void TurnStencilRight(int[,] matrix, int size)
        {
            // копія матриці-трафарету, для повертання матриці-трафарету праворуч
            int[,] stencilCopyMatrix;
            stencilCopyMatrix = new int[size, size];
            // скопіювати матрицю трафарет у додаткову матрицю
            CopyMatrix(stencilCopyMatrix, matrix, size);
            // онулить stencilMatrix
            restoreMatrix(matrix, size);
            int k = size - 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int digit = stencilCopyMatrix[i, j];
                    matrix[j, k] = digit;
                }
                k--;
            }
        }

        public void TurnStencilLeft(int[,] matrix, int size)
        {
            // допоміжна матриця, щоб повернути трафарет вліво
            int[,] stencilCopyMatrix;
            stencilCopyMatrix = new int[size, size];
            CopyMatrix(stencilCopyMatrix, matrix, size);
            // очистить stencil матрицу
            restoreMatrix(matrix, size);
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int digit = stencilCopyMatrix[i, j];
                    matrix[j, k] = digit;
                }
                k++;
            }
        }

        public void restoreMatrix(int[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public void restoreMatrix(bool[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Функція здійснює копіювання матриці
        /// </summary>
        /// <param name="matrixA">Матриця в яку копіюють</param>
        /// <param name="matrixB">Матриця з якої копіюють</param>
        void CopyMatrix(int[,] matrixA, int[,] matrixB, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrixA[i, j] = matrixB[i, j];
                }
            }
        }
    }
}
