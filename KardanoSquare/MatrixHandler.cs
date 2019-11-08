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
        private void TurnStencilRight(int[,] matrix, int size)
        {
            // копія матриці-трафарету, для повертання матриці-трафарету праворуч
            int[,] stencilCopyMatrix;
            stencilCopyMatrix = new int[size, size];
            // скопіювати матрицю трафарет у додаткову матрицю
            CopyMatrix(stencilCopyMatrix, stencilMatrix);
            // онулить stencilMatrix
            restoreMatrix(stencilMatrix);
            int k = size - 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int digit = stencilCopyMatrix[i, j];
                    stencilMatrix[j, k] = digit;
                }
                k--;
            }
        }
    }
}
