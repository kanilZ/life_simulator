using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame
{
    public class Life
    {
        public Graphics Graphics { get; set; }
        public int Resolution { get; set; }
        public bool[,] Field { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int CurrentGeneration { get; set; }
        public Life()
        {
            CurrentGeneration = 0;
        }
        public void SetFieldSize(int numResolution, int height, int width)
        {
            Resolution = numResolution;
            Rows = height / Resolution;
            Cols = width / Resolution;
            Field = new bool[Cols, Rows];
        }
        public void RandomField(int numDensity)
        {
            Random random = new Random();
            for (int x = 0; x < Cols; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    Field[x, y] = random.Next(numDensity) == 0;
                }
            }
        }

        public void NextGeneration()
        {
            Graphics.Clear(Color.Black);

            var newField = new bool[Cols, Rows];

            for (int x = 0; x < Cols; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    CheckNeighbours(x, y, ref newField);
                }
            }
            Field = newField;
        }
        private void CheckNeighbours(int x, int y, ref bool[,] newField)
        {
            var neighboursCount = CountNeighbours(x, y);
            var hasLife = Field[x, y];

            if (!hasLife && neighboursCount == 3)
                newField[x, y] = true;

            else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                newField[x, y] = false;

            else if (hasLife && (neighboursCount == 2 || neighboursCount == 3))
                newField[x, y] = true;
            else
                newField[x, y] = Field[x, y];

            if (hasLife)
                Graphics.FillRectangle(Brushes.Crimson, x * Resolution, y * Resolution, Resolution, Resolution);
        }
        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + Cols) % Cols;
                    var row = (y + j + Rows) % Rows;

                    var isSelfChecking = col == x && row == y;
                    var hasLife = Field[col, row];

                    if (hasLife && !isSelfChecking)
                        count++;
                }
            }
            return count;
        }
        public void CreateCells(int locationX,int locationY,bool hasCreated)
        {
            var x = ComputeCoords(locationX);
            var y = ComputeCoords(locationY);

            if (ValidateMousePosition(x, y))
            {
                Field[x, y] = hasCreated;
            }
        }
        private int ComputeCoords(int location)
        {
            return location / Resolution;
        }

        private bool ValidateMousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Cols && y < Rows;
        }
    }
}
