using System.Media;

namespace SpaceMod
{
    public partial class Form1 : Form
    {

        int gravity;
        int gravityValue = 8;
        int obstacleSpeed = 10;
        int coinSpeed = 10;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        Random random= new Random();

 
        public Form1()
        {
            InitializeComponent();
            SoundPlayer splayer = new SoundPlayer(@"C:\Users\lycmn\Downloads\Wii.wav");
            splayer.Play();
            splayer.PlayLooping();

            RestartGame();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            lblScore.Text = "Score: " + score;
            lblhighScore.Text = "High Score: " + highScore;
            player.Top += gravity;

            if (player.Top > 337)
            {
                gravity = 0;
                player.Top = 337;
                player.Image = Properties.Resources.run_down0_1_;
            }
            else if (player.Top < 38)
            {
                gravity = 0;
                player.Top = 38;
                player.Image = Properties.Resources.run_up0_1_;
            }

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = random.Next(1200, 3000);
                        score += 1;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameTimer.Stop();
                        lblScore.Text += "  GAME OVER!!! PRESS ENTER TO RESTART";
                        gameOver = true;

                        if (score > highScore)
                        {
                            highScore = score;
                        }
                    }
                }
            }

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "coins")
                {
                    x.Left -= coinSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = random.Next(1200, 3000);
                        
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {

                        x.Left = random.Next(1200, 3000);
                        score+=3;
                        
                        
                        if (score > highScore && gameOver)
                        {
                            highScore = score;
                        }
                    }                                                            
                }
            }

            if(score==20)
            {
                obstacleSpeed = 20;
                coinSpeed= 20;
                gravityValue = 12;
            }


        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Space) 
            {
                if (player.Top == 337)
                {
                    player.Top -= 10;
                    gravity = -gravityValue;
                }
                else if (player.Top == 38)
                {
                    player.Top += 10;
                    gravity = gravityValue;
                }
            }

            if(e.KeyCode == Keys.Enter && gameOver == true) 
            {
                RestartGame();
            }
         
        }

       private void RestartGame()
        {
            lblScore.Parent = pictureBox1;
            lblhighScore.Parent = pictureBox2;
            lblhighScore.Top = 0;
            player.Location = new Point(180, 149);
            player.Image = Properties.Resources.run_down0_1_;
            score = 0;
            gravityValue = 8;
            gravity = gravityValue;
            obstacleSpeed = 10;
            coinSpeed = 10;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left = random.Next(1200, 3000);
                }

            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "coins")
                {
                    x.Left = random.Next(1200, 3000);
                }

            }

            gameTimer.Start();




        }
    }
}