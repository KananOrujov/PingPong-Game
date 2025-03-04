using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pong
{
    public partial class Form1 : Form
    {
       
        int xspeed;
        int yspeed;
        int lastx;
        int lastx_cpu;
        int score_player;
        int score_cpu;
        int topBounds;
        int bottomBounds;
        int leftBounds;
        int rightBounds;
        bool paused = false;

        public Form1()
        {
            InitializeComponent();

          
            xspeed = 2;
            yspeed = 2;

           
            ball.Enabled = false;
            paddle.Enabled = false;

         
            lastx = MousePosition.X;
            lastx_cpu = paddle2.Location.X;

        
            score_player = 0;
            score_cpu = 0;

         
            topBounds = 0;
            bottomBounds = this.Height;
            leftBounds = 0;
            rightBounds = this.Width;

          
            pause_txt.Visible = false;

        
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

       
        private void moveBall(object sender, EventArgs e)
        {
           
            topBounds = 0;
            bottomBounds = this.Height-23;
            leftBounds = 0;
            rightBounds = this.Width;

            
            if (!paused)
            {
              
                paddle.Location = new Point((int)(MousePosition.X - paddle.Width), paddle.Location.Y);
                ball.Location = new Point(ball.Location.X + xspeed, ball.Location.Y + yspeed);

               
                if (ball.Location.X > paddle2.Location.X)
                {
                    paddle2.Location = new Point(paddle2.Location.X + 3, paddle2.Location.Y);
                }
                else
                {
                    paddle2.Location = new Point(paddle2.Location.X - 3, paddle2.Location.Y);
                }

                
                if (ball.Location.X < leftBounds)
                {
                    xspeed *= -1;
                    while (ball.Location.X - 1 < leftBounds)
                    {
                        ball.Location = new Point(ball.Location.X + 1, ball.Location.Y);
                    }
                }

               
                if (ball.Location.X + ball.Width > rightBounds)
                {
                    xspeed *= -1;
                    while (ball.Location.X + 1 > rightBounds)
                    {
                        ball.Location = new Point(ball.Location.X - 1, ball.Location.Y);
                    }
                }

                
                if (ball.Location.Y + ball.Height > paddle.Location.Y && ball.Location.X > (int)(paddle.Location.X - ball.Width / 2) && ball.Location.X + ball.Width < (int)(paddle.Location.X + paddle.Width + ball.Width / 2) && ball.Location.Y < (int)(paddle.Location.Y + paddle.Height / 2))
                {
                    yspeed *= -1;
                    xspeed = Math.Abs(MousePosition.X - lastx);
                    if (xspeed > 4)
                    {
                        xspeed = 4;
                    }
                    else if (xspeed < -4)
                    {
                        xspeed = -4;
                    }
                    else if (xspeed == 0)
                    {
                        Random a = new Random();
                        if (a.NextDouble() > .5)
                        {
                            xspeed = 2;
                        }
                        else
                        {
                            xspeed = -2;
                        }
                    }
                    while (ball.Location.Y + 1 + ball.Height > paddle.Location.Y)
                    {
                        ball.Location = new Point(ball.Location.X, ball.Location.Y - 1);
                    }
                }

                
                if (ball.Location.Y < paddle2.Location.Y + paddle2.Height && ball.Location.X > (int)(paddle2.Location.X - ball.Width / 2) && ball.Location.X + ball.Width < (int)(paddle2.Location.X + paddle.Width + ball.Width / 2) && ball.Location.Y > (int)(paddle2.Location.Y + paddle2.Height / 2))
                {
                    yspeed *= -1;
                    xspeed = Math.Abs(paddle.Location.X - lastx_cpu);
                    if (xspeed > 4)
                    {
                        xspeed = 4;
                    }
                    else if (xspeed < -4)
                    {
                        xspeed = -4;
                    }
                    else if (xspeed == 0)
                    {
                        Random a = new Random();
                        if (a.NextDouble() > .5)
                        {
                            xspeed = 2;
                        }
                        else
                        {
                            xspeed = -2;
                        }
                    }
                    while (ball.Location.Y - 1 < paddle2.Location.Y + paddle2.Height)
                    {
                        ball.Location = new Point(ball.Location.X, ball.Location.Y + 1);
                    }
                }

                
                if (ball.Location.Y > bottomBounds)
                {
                    ball.Location = new Point(120, 100);
                    Random b = new Random();
                    if (b.NextDouble() > .5)
                    {
                        xspeed = 2;
                    }
                    else
                    {
                        xspeed = -2;
                    }
                    yspeed = -2;
                    score_cpu++;
                    points2.Text = "CPU: " + score_cpu;
                } 
                else if (ball.Location.Y < topBounds)
                {
                    ball.Location = new Point(120, 100);
                    Random b = new Random();
                    if (b.NextDouble() > .5)
                    {
                        xspeed = 2;
                    }
                    else
                    {
                        xspeed = -2;
                    }
                    yspeed = 2;
                    score_player++;
                    points1.Text = "Player: " + score_player;
                }
                lastx = MousePosition.X;
                lastx_cpu = paddle2.Location.X;
            }
        }


        private void movePaddles(object sender, EventArgs e)
        {
            
            paddle.Location = new Point(paddle.Location.X, bottomBounds - 46);
            paddle2.Location = new Point(paddle2.Location.X,topBounds + 12);
            pause_txt.Location = new Point((int)rightBounds / 2 - pause_txt.Width / 2, (int)bottomBounds / 2 - pause_txt.Height / 2);
        }

        private void pause(object sender, EventArgs e)
        {
            
            if (!paused)
            {
                paused = true;
                pause_txt.Visible = true;
            }
            else
            {
                paused = false;
                pause_txt.Visible = false;
            }
        }

        
        protected override void OnPaint(PaintEventArgs pe)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}