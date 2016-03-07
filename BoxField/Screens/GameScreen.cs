using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;

        //used to draw boxes on screen
        SolidBrush[] boxBrush = new SolidBrush[4];

        //Character Brush
        SolidBrush charBrush = new SolidBrush(Color.White);

        //Create a list of Boxes
        List<Box> boxes = new List<Box>();
        List<Box> boxes2 = new List<Box>();

        int waitTime = 18;

        int x = 500;

        Random randGen = new Random();

        int randColor, randX, randX2, waitForRandom, charX;

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //Create initial box object and add it to list of Boxes
            Box b = new Box(x, -35, randColor);
            boxes.Add(b);

            Box b2 = new Box(x + 200, -35, randColor);
            boxes2.Add(b2);

            //Draw Initial Character
            charX = x + 100;
            Character main = new Character(charX, this.Height - 20);

            //Add Box Colors
            boxBrush[0] = new SolidBrush(Color.Red);
            boxBrush[1] = new SolidBrush(Color.Blue);
            boxBrush[2] = new SolidBrush(Color.Yellow);
            boxBrush[3] = new SolidBrush(Color.Green);

            randX = randGen.Next(5, 16);
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.B:
                    bDown = true;
                    break;
                case Keys.N:
                    nDown = true;
                    break;
                case Keys.M:
                    mDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.B:
                    bDown = false;
                    break;
                case Keys.N:
                    nDown = false;
                    break;
                case Keys.M:
                    mDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            randX2 = randGen.Next(10, this.Width - 50);

            randColor = randGen.Next(0, 4);

            if(randX % 4  < 2)
            {
                if(x > 10)
                {
                    x -= 5;
                }
            }

            if(randX % 4 >= 2)
            {
                if (x < this.Width - 220)
                {
                    x += 5;
                }
            }

            randX = randGen.Next(5, 15);

            waitTime--;

            if (waitForRandom < 200)
            {
                waitForRandom++;
                if (waitTime == 0)
                {
                    //Add boxes 
                    Box b = new Box(x, -35, randColor);
                    boxes.Add(b);
                    waitTime = 18;

                    Box b2 = new Box(x + 200, -35, randColor);
                    boxes2.Add(b2);
                }
            }

            if(waitForRandom >= 200)
            {
                if(waitTime == 0)
                {
                    //Add boxes 
                    Box b = new Box(randX2, -35, randColor);
                    boxes.Add(b);
                    waitTime = 18;

                    Box b2 = new Box((x + randX2) / 2, -35, randColor);
                    boxes2.Add(b2);
                }
            }

            //Update position of each box
            foreach (var Box in boxes)
            {
                Box.y += 2;
            }

            foreach (var Box in boxes2)
            {
                Box.y += 2;
            }

            //Remove box from list if it is off screen
            if (boxes[0].y > this.Height)
            {
                boxes.RemoveAt(0);
            }

            if (boxes2[0].y > this.Height)
            {
                boxes2.RemoveAt(0);
            }

            if(rightArrowDown ==  true && charX < this.Width - 50)
            {
                charX += 5;
            }

            if(leftArrowDown == true && charX > 50)
            {
                charX -= 5;
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //Draw each box to the screen
            foreach (var Box in boxes)
            {
                e.Graphics.FillRectangle(boxBrush[Box.color], Box.x, Box.y, 30, 30);
            }

            foreach (var Box in boxes2)
            {
                e.Graphics.FillRectangle(boxBrush[Box.color], Box.x, Box.y, 30, 30);
            }

            e.Graphics.FillRectangle(charBrush, charX, this.Height - 20, 10, 10);
        }
    }
}
