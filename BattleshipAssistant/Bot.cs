using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipAssistant
{
    public class Bot
    {
        public int[,] myMap = new int[GameForm.mapSize, GameForm.mapSize]; // bot`s map
        public int[,] enemyMap = new int[GameForm.mapSize, GameForm.mapSize]; // player`s map

        public Button[,] myButtons = new Button[GameForm.mapSize, GameForm.mapSize];
        public Button[,] enemyButtons = new Button[GameForm.mapSize, GameForm.mapSize];

        public Bot(int[,] myMap, int[,] enemyMap, Button[,] myButtons, Button[,] enemyButtons)
        {
            this.myMap = myMap;
            this.enemyMap = enemyMap;
            this.myButtons = myButtons;
            this.enemyButtons = enemyButtons;
        }

        public void ConfigureShips()
        {
            Random random = new Random();
            int numberOfShips = 10;

            while (numberOfShips > 0)
            {
                int x = random.Next(1, GameForm.mapSize);
                int y = random.Next(1, GameForm.mapSize);
                if (myMap[x, y] == 0)
                {
                    myMap[x, y] = 1;
                    numberOfShips--;
                }
            }
        }

        public void Shoot()
        {
            Random random = new Random();
            bool hit = false;

            while (!hit)
            {
                int x = random.Next(1, GameForm.mapSize);
                int y = random.Next(1, GameForm.mapSize);

                if (enemyMap[x, y] != 0)
                {
                    hit = true;
                    enemyMap[x, y] = 0;
                    enemyButtons[x, y].BackColor = Color.Blue;
                    enemyButtons[x, y].Text = "X";
                }
                else if (enemyMap[x, y] == 0)
                {
                    hit = true;
                    enemyButtons[x, y].BackColor = Color.Black;
                }
            }
        }
    }
}