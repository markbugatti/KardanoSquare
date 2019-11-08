using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KardanoSquare
{
    class EncryptionHandler
    {
        EncryptionTextHandler EncryptionTextHandler;
        MatrixHandler MatrixHandler;
        public EncryptionHandler(EncryptionTextHandler encryptionTextHandler, MatrixHandler matrixHandler)
        {
            EncryptionTextHandler = encryptionTextHandler;
            MatrixHandler = matrixHandler;
        }
        
        public string Encrypt(string plainText, int[,] stencilMatrix, char[,] textMatrix, bool[,] fillMatrix, int matrixSize)
        {
            // анулювати bool матрицу. Якщо метод Encrypt було виклиано, значить текст треба зашифрувати заново.
            MatrixHandler.restoreMatrix(fillMatrix, matrixSize);
            
            // Окремий індекс для рядка, куди буде заноситись зашифрований текст
            int textIndex = 0;
            
            // Матрицю необхідно буде повернути на 90 градусів 3 рази. 0 - 90 - 180 - 270; 360 - повертається, але вже не обробляється
            int degree = 0;
            
            // алгоритм шифрування
            while (degree < 360)
            {
                // Обробляємо матрицю.
                for (int i = 0; i < matrixSize; i++)
                {
                    for (int j = 0; j < matrixSize; j++)
                    {
                        // Якщо в трафареті стоїть 1, та вихідний текст ще має символи.
                        // Другу умову необхідно перевіряти на той випадок, якщо в трафареті ще є порожні клітинки, але вже весь вихідний текст вже зашифровано.
                        // Інакше буде помилка OutOfRange для вихідного тексту.
                        if (stencilMatrix[i, j] == 1 && textIndex < plainText.Length)
                        {
                            // переносимо наступний символ відкритого повідомлення у матрицю тексту.
                            textMatrix[i, j] = plainText[textIndex];
                            // відмічаємо, що ця клітинка заповнена.
                            fillMatrix[i, j] = true;
                            textIndex++;
                        }
                    }
                }
                // повернути трафарет за годинниковою стрілкою на 90 градусів
                MatrixHandler.TurnStencilRight(stencilMatrix, matrixSize);
                degree += 90;
            }
            // Матриця тексту заповнена, необхідно зчитати з неї текст по рядкам.
            return EncryptionTextHandler.CreateEncryptText(fillMatrix, textMatrix, matrixSize);
        }
        public string Descrypt(string plainText, int[,] stencilMatrix, char[,] textMatrix, bool[,] fillMatrix, int matrixSize)
        {
            int degree = 360;
            plainText = "";
            // При розшифрувані матрицю необхідно повертати проти годинникової. 270 - 180 - 90 - 0 градусів.
            while (degree > 0)
            {
                MatrixHandler.TurnStencilLeft(stencilMatrix, matrixSize);
                degree -= 90;
                // Обробляємо марицю-трафарет зправа наліво, знизу на верх
                for (int i = matrixSize - 1; i >= 0; i--)
                {
                    for (int j = matrixSize - 1; j >= 0; j--)
                    {
                        // Якщо клітинка в трафареті із "діркою" і якщо символ в текстовій матриці введений
                        if (stencilMatrix[i, j] == 1 && fillMatrix[i, j] == true)
                        {
                            // Забрати текст з відповідної клітинки текстової матриці
                            char character = textMatrix[i, j];
                            plainText = plainText.Insert(0, character.ToString());
                        }
                    }
                }
            }
            return plainText;
        }
    }
}
