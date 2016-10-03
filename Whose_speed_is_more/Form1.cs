using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace Whose_speed_is_more
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int ALL = 100;
        const int firstkey = 87, secondkey = 80;
        bool player1_keypressed, player2_keypressed;
        int player1 = 0, player2 = 0;
        Thread game;
        bool show_color = false;
        Graphics gr;
        Thread timer;
        int TIME_WAITING = 5;
        bool new_game;
        Bitmap toright, toleft;
        Thread pretty_draw;

        TrackBar tplayer1, tplayer2;
        bool pause_before_color = false;


        void Update_The_Game(Color c)
        {
            gr.Clear(c);
            tplayer1.Draw(gr);
            tplayer2.Draw(gr);
            label1.BackColor = c;
            label2.BackColor = c;
        }

        Thread start_game;

        Thread delayy;
        int TIME_PAUSE;

        void generic()
        {
            Random e = new Random();
            Random col = new Random();
            show_color = false;
            while (true)
            {
                if (show_color) break;
                if (e.Next() % 100 == 17)
                {
                    Color c = Color.Red;
                    if (e.Next() % 2 == 0) c = Color.Green;
                    if (c == tplayer1.color || c == tplayer2.color) continue;
                    show_color = true;

                    new_game = true;
                    Update_The_Game(c);
                    label1.BackColor = c;
                    label2.BackColor = c;
                }
            }
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label1.Text = "0";
            label2.Text = "0";
            label1.Left = label1.ClientSize.Width + 20;
            label1.Top = this.ClientSize.Height - label1.Height - 10;
            

            label2.Left = this.ClientSize.Width - label2.ClientSize.Width - 20;
            label2.Top = this.ClientSize.Height - label2.Height - 10;
            gr = this.CreateGraphics();

            //DOWNLOAD PNG
            toright = new Bitmap(Image.FromFile(@"toright.png")); toright.MakeTransparent(Color.White);
            toleft = new Bitmap(Image.FromFile(@"toleft.png")); toleft.MakeTransparent(Color.White);


            tplayer1 = new TrackBar(new Point(10, this.ClientSize.Height - 300), 30, 0, 300, 50, Color.Yellow);
            tplayer2 = new TrackBar(new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 300), 30, 0, 300, 50, Color.Blue);

            //game.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null) timer.Abort();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            show_color = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1;
            this.Invalidate();
            player1_keypressed = false;
            player2_keypressed = false;

            pause_before_color = false;
            TIME_WAITING = 5;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            TIME_WAITING--;

            if (TIME_WAITING <= 0)
            {

                Update_The_Game(Color.White);
                Random ee = new Random();

                timer1.Enabled = false;
                TIME_PAUSE = ee.Next() % 100;

                timer2.Enabled = true;

                return;
            }
            Update_The_Game(Color.White);
            gr.DrawString("lost " + Convert.ToString(TIME_WAITING) + " seconds", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold), new SolidBrush(Color.Red), new Point(this.ClientSize.Width / 2 - 50, this.ClientSize.Height / 2));

        }

        void draw_bars()
        {
            TrackBar n;
            if (player1_keypressed) n = tplayer1; else n = tplayer2;
            for (int i = 1; i < n.one_move + 1; i++)
            {
                n.Draw(gr, 1);
                Thread.Sleep(10);
            }

             if (player1_keypressed) tplayer1 = n; else tplayer2 = n;
             if (pretty_draw != null) pretty_draw.Abort();
        }

        void Update_The_Bars()
        {
            pretty_draw = new Thread(draw_bars);
            pretty_draw.Start();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            button1.Left = this.ClientSize.Width / 2 - button1.Width/2;
            button1.Top = this.ClientSize.Height - button1.Height - 10;
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            int code = (int)e.KeyCode;
            Bitmap bmp =  new Bitmap(this.ClientSize.Width, this.ClientSize.Height, gr);
            if (timer1.Enabled || timer2.Enabled)
            {
                if (player1_keypressed || player2_keypressed) return;
                if (code == firstkey) player2_keypressed = true;
                if (code == secondkey) player1_keypressed = true;
                timer1.Enabled = false;
                Update_The_Game(Color.White);
                if (player1_keypressed)
                {
                    gr.DrawImage(toleft, this.ClientSize.Width / 2 - 120, this.ClientSize.Height / 2 - 100);
                    label1.Text = Convert.ToString(Convert.ToInt32(label1.Text) + 1);
                }
                if (player2_keypressed)
                {
                    gr.DrawImage(toright, this.ClientSize.Width / 2 - 120, this.ClientSize.Height / 2 - 100);
                    label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) + 1);
                }
                if (player1_keypressed || player2_keypressed)
                {
                    Update_The_Bars();
                    new_game = false;

                    pause_before_color = false;
                }
                
            }
            if (player1_keypressed || player2_keypressed) return;
            if (code == firstkey) player1_keypressed = true;
            if (code == secondkey) player2_keypressed = true;


            if (player1_keypressed)
            {
                gr.DrawImage(toleft, this.ClientSize.Width / 2 - 120, this.ClientSize.Height / 2 - 100);
                label1.Text = Convert.ToString(Convert.ToInt32(label1.Text) + 1);
            }
            if (player2_keypressed)
            {
                gr.DrawImage(toright, this.ClientSize.Width / 2 - 120, this.ClientSize.Height / 2 -100);
                label2.Text = Convert.ToString(Convert.ToInt32(label2.Text) + 1);
            }
            if (player1_keypressed || player2_keypressed)
            {
                Update_The_Bars();
                new_game = false;
            }
        }

        private void authorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Vadym Prokopets, September 2014\n");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TIME_PAUSE--;
            if (TIME_PAUSE <= 0)
            {
                generic();
                timer2.Enabled = false;
            }
        }

        
    }
}
