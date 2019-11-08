using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KardanoSquare
{
    class EncryptionTextHandler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fillMatrix">Матриця bool, показує, з яких клітинок текстової матриці необхідно зчитувати символи</param>
        /// <param name="textMatrix">Текстова матриця</param>
        /// <param name="size">Розмір матриці</param>
        /// <returns></returns>
        public string CreateEncryptText(bool[,] fillMatrix, char[,] textMatrix, int size)
        {
            string encryptedText = "";
            int textIndex = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (fillMatrix[i, j] == true)
                    {
                        char character = textMatrix[i, j];
                        encryptedText = encryptedText.Insert(textIndex, character.ToString());
                        textIndex++;
                    }
                }
            }
            return encryptedText;
        }
    }
}
