using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TicTacMatic
{
    public partial class player_vs_bot : Form
    {
        bool turnX;
        bool victory;
        againsabot xbot;
        againsabot obot;

        public PictureBox[][] fields;

        PictureBox[] sky;
        PictureBox[] earth;
        PictureBox[] lava;

        PictureBox[] left;
        PictureBox[] middle;
        PictureBox[] right;

        PictureBox[] northwest;
        PictureBox[] northeast;



        public player_vs_bot()
        {
            InitializeComponent();
            TurnX();

            xbot = new againsabot(true);
            obot = new againsabot(false);

            sky = new PictureBox[] { field1, field2, field3 };
            earth = new PictureBox[] { field4, field5, field6 };
            lava = new PictureBox[] { field7, field8, field9 };

            left = new PictureBox[] { field1, field4, field7 };
            middle = new PictureBox[] { field2, field5, field8 };
            right = new PictureBox[] { field3, field6, field9 };

            northwest = new PictureBox[] { field1, field5, field9 };
            northeast = new PictureBox[] { field3, field5, field7 };

            fields = new PictureBox[][] { sky, earth, lava, left, middle, right, northwest, northeast };

            //if (File.Exists("X.txt"))
            //{
            //    using (StreamReader sr = new StreamReader("X.txt"))
            //    {
            //        List<int> moves = new List<int>();
            //        string line = " ";
            //        do
            //        {
            //            line = sr.ReadLine();
            //            moves.Add(Convert.ToInt32(line));
            //        } while (line != null);
            //
            //        xbot = new Bot(moves);
            //    }
            //    File.Delete("X.txt");
            //}
            //// If no pre-defined moves, empty bot
            //else xbot = new Bot();
            //File.WriteAllText("X.txt", "");
            //
            //
            //if (File.Exists("O.txt"))
            //{
            //    using (StreamReader sr = new StreamReader("O.txt"))
            //    {
            //        List<int> moves = new List<int>();
            //        string line = " ";
            //        do
            //        {
            //            line = sr.ReadLine();
            //            moves.Add(Convert.ToInt32(line));
            //        } while (line != null);
            //
            //        obot = new Bot(moves);
            //    }
            //    File.Delete("O.txt");
            //}
            //else obot = new Bot();
            //File.WriteAllText("O.txt", "");

            botTimer.Start();
        }

    

      
        public void ResetGame()
        {
            player_vs_bot tictactoe = new player_vs_bot();
            tictactoe.Show();
            this.Dispose(false);
        }

        private void TurnX()
        {
            this.Text = "Turn: X";
            turnX = true;
        }

        private void TurnO()
        {
            this.Text = "Turn: O";
            turnX = false;
        }

        public void Play(object sender, EventArgs e)
        {
            PictureBox clickedField = (PictureBox)sender;

            clickedField.Click -= Play;

            int clickedInt = Convert.ToInt32(clickedField.Name.Replace("field", ""));

            // Place X/O
            if (turnX)
            {
                clickedField.ImageLocation = "X.png";
                //if (File.Exists("X.txt") && File.ReadLines("X.txt").Count() < 5)
                //{
                //    using (StreamWriter sw = new StreamWriter("X.txt", true))
                //    { sw.WriteLine(clickedInt); }
                //}
            }
            else
            {
                clickedField.ImageLocation = "O.png";
                //if (File.Exists("O.txt") && File.ReadLines("O.txt").Count() < 5)
                //{
                //    using (StreamWriter sw = new StreamWriter("O.txt", true))
                //    { sw.WriteLine(clickedInt); }
                //}
            }


            switch (clickedInt)
            {
                case 1:
                    leftField(clickedInt);
                    highfield(clickedInt);
                    CornerOne(clickedInt);
                    break;

                case 2:
                    middlefield(clickedInt);
                    highfield(clickedInt);
                    break;

                case 3:
                    rightfield(clickedInt);
                    highfield(clickedInt);
                    CornerThree(clickedInt);
                    break;

                case 4:
                    leftField(clickedInt);
                    mediumfield(clickedInt);
                    break;

                case 5:
                    middlefield(clickedInt);
                    mediumfield(clickedInt);
                    MiddleFive(clickedInt);
                    break;

                case 6:
                    rightfield(clickedInt);
                    mediumfield(clickedInt);
                    break;

                case 7:
                    leftField(clickedInt);
                    lowfield(clickedInt);
                    CornerSeven(clickedInt);
                    break;

                case 8:
                    middlefield(clickedInt);
                    lowfield(clickedInt);
                    break;

                case 9:
                    rightfield(clickedInt);
                    lowfield(clickedInt);
                    CornerNine(clickedInt);
                    break;
            }

            if (victory)
            {
                //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                //else File.Delete("X.txt");

                DialogResult dialogresult =
                    MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                    "Do you want to play again?",
                    "Victory!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (dialogresult == DialogResult.Yes)
                {
                    ResetGame();
                    return;
                }
                else Close();
            }

            // Switch turns
            if (turnX) TurnO();
            else TurnX();
        }

        private void leftField(int clickedInt)
        {
            if (
                GetField(clickedInt + 1).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 2).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void middlefield(int clickedInt)
        {
            if (
                GetField(clickedInt - 1).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 1).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void rightfield(int clickedInt)
        {
            if (
                GetField(clickedInt - 1).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt - 2).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }


        private void highfield(int clickedInt)
        {
            if (
                GetField(clickedInt + 3).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 6).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void mediumfield(int clickedInt)
        {
            if (
                GetField(clickedInt - 3).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 3).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void lowfield(int clickedInt)
        {
            if (
                GetField(clickedInt - 3).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt - 6).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }


        private void CornerOne(int clickedInt)
        {
            if (
                GetField(clickedInt + 4).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 8).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void CornerNine(int clickedInt)
        {
            if (
                GetField(clickedInt - 4).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt - 8).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void CornerThree(int clickedInt)
        {
            if (
                GetField(clickedInt + 2).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 4).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void CornerSeven(int clickedInt)
        {
            if (
                GetField(clickedInt - 2).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt - 4).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        private void MiddleFive(int clickedInt)
        {
            if (
                GetField(clickedInt - 4).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 4).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
            else if (
                GetField(clickedInt - 2).ImageLocation == GetField(clickedInt).ImageLocation &&
                GetField(clickedInt + 2).ImageLocation == GetField(clickedInt).ImageLocation
                ) victory = true;
        }

        public PictureBox GetField(int index)
        {
            return (PictureBox)this.Controls.Find("field" + index.ToString(), true)[0];
        }

      

        private void player_vs_bot_Load_1(object sender, EventArgs e)
        {

        }

        private void botTimer_Tick_1(object sender, EventArgs e)
        {
            if (victory == false)
            {

                if (turnX)
                {
                    return;
                }
                else obot.Play(this);
            }
        }

        private void field1_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field1.ImageLocation = "X.png";
             
                controleren(1);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field2_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field2.ImageLocation = "X.png";
              
                controleren(2);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                    
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field3_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field3.ImageLocation = "X.png";
               
                controleren(3);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field4_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field4.ImageLocation = "X.png";
               
                controleren(4);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();

                    
                }
                turnX = false;
                this.Text = "Turn: O";
            }
        }

        private void field5_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field5.ImageLocation = "X.png";

                controleren(5);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field6_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field6.ImageLocation = "X.png";
             
                controleren(6);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field7_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field7.ImageLocation = "X.png";
             
                controleren(7);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field8_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field8.ImageLocation = "X.png";
              
                controleren(8);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }

        private void field9_Click(object sender, EventArgs e)
        {
            if (turnX)
            {
                field9.ImageLocation = "X.png";
              
                controleren(9);
                if (victory)
                {
                    //if (this.Text.Split(' ')[1] == "X") File.Delete("O.txt");
                    //else File.Delete("X.txt");

                    DialogResult dialogresult =
                        MessageBox.Show(this.Text.Split(' ')[1] + " won!\n\n" +
                        "Do you want to play again?",
                        "Victory!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);
                    if (dialogresult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }
                    else Close();
                }
                this.Text = "Turn: O";
                turnX = false;
            }
        }
        public void controleren(int clickedInt)
        {
            switch (clickedInt)
            {
                case 1:
                    leftField(clickedInt);
                    highfield(clickedInt);
                    CornerOne(clickedInt);
                    break;

                case 2:
                    middlefield(clickedInt);
                    highfield(clickedInt);
                    break;

                case 3:
                    rightfield(clickedInt);
                    highfield(clickedInt);
                    CornerThree(clickedInt);
                    break;

                case 4:
                    leftField(clickedInt);
                    mediumfield(clickedInt);
                    break;

                case 5:
                    middlefield(clickedInt);
                    mediumfield(clickedInt);
                    MiddleFive(clickedInt);
                    break;

                case 6:
                    rightfield(clickedInt);
                    mediumfield(clickedInt);
                    break;

                case 7:
                    leftField(clickedInt);
                    lowfield(clickedInt);
                    CornerSeven(clickedInt);
                    break;

                case 8:
                    middlefield(clickedInt);
                    lowfield(clickedInt);
                    break;

                case 9:
                    rightfield(clickedInt);
                    lowfield(clickedInt);
                    CornerNine(clickedInt);
                    break;
            }
        }
    }
}
