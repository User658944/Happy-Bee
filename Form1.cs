using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Happy_Bee
{
    public partial class Form1 : Form
    {
        int leafSpeed = 8;
        int gravity = 5;
        int score = 0;
        public int flovers = 0;
        public bool gamePause = false;

        Random rand = new Random();
        int y;

        WindowsMediaPlayer playerMusic = new WindowsMediaPlayer();
        WindowsMediaPlayer playerBeeUp = new WindowsMediaPlayer();
        WindowsMediaPlayer playerBee = new WindowsMediaPlayer();
        
        public Form1()
        {
            InitializeComponent();
            playerMusic.URL = "music.mp3";
            playerBeeUp.URL = "beeup.mp3";
            playerBee.URL = "beedown.mp3";


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            playerMusic.controls.play();
            playerBee.controls.play();

        }

        public void timerTimerEvent(object sender, EventArgs e)
        {
            pictureBoxBee.Top += gravity;
            pictureBoxDown.Left -= leafSpeed;
            pictureBoxUp.Left -= leafSpeed;
            pictureBoxKukka.Left -= leafSpeed;
            pictureBoxKukka2.Left -= leafSpeed;
            pictureBoxKukka3.Left -= leafSpeed;
            labelScore.Text = "Scores  " + score;
            
            int padding = 30;
            Rectangle narrowRectBee = new Rectangle(
                pictureBoxBee.Location.X + padding,
                pictureBoxBee.Location.Y + padding,
                pictureBoxBee.Width - padding * 2,
                pictureBoxBee.Height - padding * 2
            );



            if (pictureBoxDown.Left < -150)
            {
                pictureBoxDown.Left = 1200;
            }
            if (pictureBoxUp.Left < -150)
            {
                pictureBoxUp.Left = 1000;
            }
            if (pictureBoxKukka.Left < -100)
            {
                pictureBoxKukka.Left = 1000;
                y = rand.Next(0, 480);
                pictureBoxKukka.Top = y;
            }
            if (pictureBoxKukka2.Left < -70)
            {
                pictureBoxKukka2.Left = 1400;
                y = rand.Next(0, 480);
                pictureBoxKukka2.Top = y;
            }
            if (pictureBoxKukka3.Left < -200)
            {
                pictureBoxKukka3.Left = 3000;
                y = rand.Next(0, 480);
                pictureBoxKukka3.Top = y;
            }

            if (narrowRectBee.IntersectsWith(pictureBoxDown.Bounds) || narrowRectBee.IntersectsWith(pictureBoxUp.Bounds) || narrowRectBee.IntersectsWith(pictureBoxGround.Bounds))
            {
                endGame();
            }

            if (narrowRectBee.IntersectsWith(pictureBoxKukka.Bounds))
            {
                // toistetaan ja pysäytetään yhden toistokerran jäkeen pienen kukan ääniefekti
                WindowsMediaPlayer playerSmallFlover = new WindowsMediaPlayer();
                playerSmallFlover.URL = "SmallFlover.mp3";
                playerSmallFlover.controls.play();
                playerSmallFlover.PlayStateChange += (newState) =>
                {
                    if ((WMPPlayState)newState == WMPPlayState.wmppsStopped)
                        playerSmallFlover.controls.stop();
                };

                score++;
                pictureBoxKukka.Left = 1000;
                y = rand.Next(0, 500);
                pictureBoxKukka.Top = y;

            }
            if (narrowRectBee.IntersectsWith(pictureBoxKukka2.Bounds))
            {
                // toistetaan ja pysäytetään yhden toistokerran jäkeen pienen kukan ääniefekti
                WindowsMediaPlayer playerSmallFlover2 = new WindowsMediaPlayer();
                playerSmallFlover2.URL = "SmallFlover2.mp3";
                playerSmallFlover2.controls.play();
                playerSmallFlover2.PlayStateChange += (newState) =>
                {
                    if ((WMPPlayState)newState == WMPPlayState.wmppsStopped)
                        playerSmallFlover2.controls.stop();
                };

                score++;
                pictureBoxKukka2.Left = 1100;
                y = rand.Next(0, 500);
                pictureBoxKukka2.Top = y;
            }
            if (narrowRectBee.IntersectsWith(pictureBoxKukka3.Bounds))
            {
                // toistetaan ja pysäytetään yhden toistokerran jäkeen ison kukan ääniefekti
                WindowsMediaPlayer playerBigFlover = new WindowsMediaPlayer();
                playerBigFlover.URL = "BigFlover.mp3";
                playerBigFlover.controls.play();
                playerBigFlover.PlayStateChange += (newState) =>
                {
                    if ((WMPPlayState)newState == WMPPlayState.wmppsStopped)
                        playerBigFlover.controls.stop();
                };

                flovers = flovers + 1;
                winner();

                pictureBoxKukka3.Left = 3000;
                y = rand.Next(0, 480);
                pictureBoxKukka3.Top = y;
            }
            if (pictureBoxKukka2.Bounds.IntersectsWith(pictureBoxUp.Bounds) || pictureBoxKukka2.Bounds.IntersectsWith(pictureBoxDown.Bounds))
            {
                pictureBoxKukka2.Left = 1200;
                y = rand.Next(0, 480);
                pictureBoxKukka2.Top = y;
            }
            if (pictureBoxKukka.Bounds.IntersectsWith(pictureBoxDown.Bounds) || pictureBoxKukka.Bounds.IntersectsWith(pictureBoxUp.Bounds))
            {
                pictureBoxKukka.Left = 1000;
                y = rand.Next(0, 480);
                pictureBoxKukka.Top = y;
            }
            if (pictureBoxKukka3.Bounds.IntersectsWith(pictureBoxDown.Bounds) || pictureBoxKukka3.Bounds.IntersectsWith(pictureBoxUp.Bounds) || pictureBoxKukka3.Bounds.IntersectsWith(pictureBoxKukka.Bounds) || pictureBoxKukka3.Bounds.IntersectsWith(pictureBoxKukka2.Bounds))
            {
                pictureBoxKukka3.Left = 1100;
                y = rand.Next(0, 480);
                pictureBoxKukka3.Top = y;
            }

            if (score == 10)
            {
                leafSpeed = 10;
                labelLevel.Text = "Level 2";
                labelLevel.Visible = true;

                if (flovers == 5)
                {
                    labelLevel.Text = "You WIN!!!";
                    labelLevel.Visible = true;
                }
            }
            else if (score == 20)
            {
                leafSpeed = 12;
                labelLevel.Text = "Level 3";
                labelLevel.Visible = true;

                if (flovers == 5)
                {
                    labelLevel.Text = "You WIN!!!";
                    labelLevel.Visible = true;
                }
            }
            else if (flovers == 5)
            {
                labelLevel.Text = "You WIN!!!";
                labelLevel.Visible = true;
            }
            else
            {
                labelLevel.Visible = false;
            }

        }
        
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // if lauseella estetään äänitiedostojen toiston jos peli on end-tilassa
            if (gamePause == false)
            {
                playerBee.controls.play();
                playerBeeUp.controls.stop();
            }

            if (e.KeyCode == Keys.Space)
            {
                gravity = 5;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if lauseella estetään äänitiedostojen toiston jos peli on end-tilassa
            if (gamePause == false)
            {
                playerBee.controls.stop();
                playerBeeUp.controls.play();
            }

            if (e.KeyCode == Keys.Space)
            {
                gravity = -5;
            }
            if (pictureBoxBee.Top < 0)
            {
                gravity = 5;
            }
        }

        private void endGame()
        {
            gamePause = true;

            timerTimer.Stop();

            // toistetaan end.mp3 vain kerran, ei loopata
            WindowsMediaPlayer playerEnd = new WindowsMediaPlayer();
            playerEnd.URL = "end.mp3";
            playerEnd.controls.play();
            playerEnd.PlayStateChange += (newState) =>
            {
                if ((WMPPlayState)newState == WMPPlayState.wmppsStopped)
                    playerEnd.controls.stop();
            };

            playerMusic.controls.stop();

            if (playerBee.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                playerBee.controls.stop();
            }
            if (playerBeeUp.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                playerBeeUp.controls.stop();
            }




            labelScore.Text += "      Game Over!!!";
            pictureBoxDown.Visible = false;
            pictureBoxUp.Visible = false;
            labelRe.Visible = true;
            labelExit.Visible = true;
        }


        private void labelRe_Click(object sender, EventArgs e)
        {

            labelRe.Visible = false;
            labelExit.Visible = false;

            leafSpeed = 8;
            score = 0;

            pictureBoxKukka.Left = 1000;
            y = rand.Next(0, 600);
            pictureBoxKukka.Top = y;

            pictureBoxKukka2.Left = 700;
            y = rand.Next(0, 500);
            pictureBoxKukka2.Top = y;

            pictureBoxDown.Visible = true;
            pictureBoxDown.Left = 1200;

            pictureBoxUp.Visible = true;
            pictureBoxUp.Left = 1000;

            pictureBoxBee.Top = 350;

            pictureBoxG1.Image = Image.FromFile("kukkaG.png");
            pictureBoxG2.Image = Image.FromFile("kukkaG.png");
            pictureBoxG3.Image = Image.FromFile("kukkaG.png");
            pictureBoxG4.Image = Image.FromFile("kukkaG.png");
            pictureBoxG5.Image = Image.FromFile("kukkaG.png");

            flovers = 0;

            gamePause = false;
            timerTimer.Start();

            playerMusic.controls.play();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void winner()
        {
            

            if (flovers == 1)
            {
                pictureBoxG1.Image = Image.FromFile("kukka.png");
            }
            else if (flovers == 2)
            {
                pictureBoxG2.Image = Image.FromFile("kukka.png");
            }
            else if (flovers == 3)
            {
                pictureBoxG3.Image = Image.FromFile("kukka.png");
            }
            else if (flovers == 4)
            {
                pictureBoxG4.Image = Image.FromFile("kukka.png");
            }
            else
            {
                pictureBoxG5.Image = Image.FromFile("kukka.png");

                timerTimer.Stop();
                pictureBoxDown.Visible = false;
                pictureBoxUp.Visible = false;
                labelRe.Visible = true;
                labelExit.Visible = true;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // käynnistää pelin Enterillä näppikseltä
            if (e.KeyChar == (char)Keys.Enter)
            {
                timerTimer.Enabled = true;
                labelStart.Visible = false;
            }
        }
    }
}
