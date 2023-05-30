using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipAssistant
{
    public partial class GameForm : Form
    {
        public const int mapSize = 10;
        public int cellSize = 30;
        public string alphabet = "ABCDEFGHIJ";

        public int[,] myMap = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];

        public Button[,] myButtons = new Button[mapSize, mapSize];
        public Button[,] enemyButtons = new Button[mapSize, mapSize];

        public bool isPlaying = false;

        public Bot bot;

        public GameForm()
        {
            InitializeComponent();
            this.Text = "Sea Battle";
            Init();
        }

        public void Init()
        {
            isPlaying = false;
            CreateMaps();
            bot = new Bot(enemyMap, myMap, enemyButtons, myButtons);
            bot.ConfigureShips();
        }

        public void CreateMaps()
        {
            this.Width = mapSize * 2 * cellSize + 50;
            this.Height = (mapSize + 3) * cellSize + 20;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    else
                    {
                        button.Click += new EventHandler(ConfigureShips);
                    }
                    myButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(320 + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.BackColor = Color.White;
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;
                        if (i == 0 && j > 0)
                            button.Text = alphabet[j - 1].ToString();
                        if (j == 0 && i > 0)
                            button.Text = i.ToString();
                    }
                    else
                    {
                        button.Click += new EventHandler(PlayerShoot);
                    }
                    enemyButtons[i, j] = button;
                    this.Controls.Add(button);
                }
            }
            Label map1 = new Label();
            map1.Text = "Player card";
            map1.Location = new Point(mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map1);

            Label map2 = new Label();
            map2.Text = "Enemy card";
            map2.Location = new Point(350 + mapSize * cellSize / 2, mapSize * cellSize + 10);
            this.Controls.Add(map2);

            Button startButton = new Button();
            startButton.Text = "Start";
            startButton.Click += new EventHandler(Start);
            startButton.Location = new Point(0, mapSize * cellSize + 20);
            this.Controls.Add(startButton);
        }

        public void Start(object sender, EventArgs e)
        {
            isPlaying = true;
        }

        public bool CheckIfMapIsNotEmpty(int[,] checkMap)
        {
            bool isEmpty = true;
            
            for (int i = 1; i < mapSize; i++)
            {
                for (int j = 1; j < mapSize; j++)
                {
                    if (checkMap[i, j] != 0)
                        isEmpty = false;
                    
                }
            }
            if (isEmpty)
                return false;
            else return true;
        }

        public void ConfigureShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (!isPlaying)
            {
                if (myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == 0)
                {
                    pressedButton.BackColor = Color.Red;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 1;
                }
                else
                {
                    pressedButton.BackColor = Color.White;
                    myMap[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = 0;
                }
            }
        }

        public void PlayerShoot(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            if (pressedButton.BackColor == Color.Blue || pressedButton.BackColor == Color.Black)
            {
                return;
            }

            bool playerTurn = Shoot(enemyMap, pressedButton);
            if (!playerTurn)
            {
                bot.Shoot();
            }
                
            if (!CheckIfMapIsNotEmpty(myMap))
            {
                //this.Controls.Clear();
                DialogResult result = MessageBox.Show("Enemy won, Do you want to play again?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if(result == DialogResult.Yes)
                {
                    GameForm newGameForm = new GameForm();  // Create a new instance 
                    newGameForm.Show();  // Show the new instance 
                    this.Close();
                }
                else
                {
                    Application.Exit();
                }
                
            }
            if(!CheckIfMapIsNotEmpty(enemyMap))
            {

                DialogResult result = MessageBox.Show("You won, Do you want to play again?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if(result == DialogResult.Yes)
                {
                    GameForm newGameForm = new GameForm();  // Create a new instance 
                    newGameForm.Show();  // Show the new instance 
                    this.Close();
                }
                else
                {
                    Application.Exit();
                }
            }

        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            bool hit = false;
            if (isPlaying)
            {
                int delta = 0;
                if (pressedButton.Location.X > 320)
                    delta = 320;
                if (map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] != 0 )
                {
                    hit = true;
                    map[pressedButton.Location.Y / cellSize, (pressedButton.Location.X - delta) / cellSize] = 0;
                    pressedButton.BackColor = Color.Blue;
                    pressedButton.Text = "X";
                }
                else
                {

                    hit = false;
                    pressedButton.BackColor = Color.Black;
                }
            }
            return hit;
        }
    }
}
