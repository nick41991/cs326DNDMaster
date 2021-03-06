﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace dnd_start_load
{
    
    public partial class Form2 : Form
    {
        int signal = 0;
        public Game game;
        Thread dieRoller;

        List<Button> buttons = new List<Button>();

        public Form2()
        {
            game = new Game();
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Encounter Button
        {
            signal = 1;
            encounterScreen1.BringToFront();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) //Treasure Generator Button
        {
            signal = 5;
            treasureGenScreen1.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e) //Game Manager Button
        {
            signal = 7;
            gameManager1.BringToFront();
        }

        private void button6_Click(object sender, EventArgs e) //Die Roller Button
        {
            signal = 6;
            //Need to check if die roller window is open and if not open new one, else bring to focus
            /* Possibly will work
            if (Application.OpenForms.OfType<DieRoller>.Count() == 1)
            {
                /*foreach(DieRoller roller in Application.OpenForms.OfType<DieRoller>) {
                    roller.WindowState = FormWindowState.Minimized;
                    roller.Show();
                    roller.WindowState = FormWindowState.Normal;
                }*/

            // } else
            // */
            if (dieRoller != null && dieRoller.IsAlive)
            {
                dieRoller.Abort();
                dieRoller = new Thread(() => new DieRoller().ShowDialog());
                dieRoller.Start();
            } else
            {
                dieRoller = new Thread(() => new DieRoller().ShowDialog());
                dieRoller.Start();
            }

            // }
            /**/
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        


        private void button_MouseClick(object sender, MouseEventArgs e)
        {


            if (signal == 1)
            {
                button1.ForeColor = System.Drawing.Color.Orange;
                button5.ForeColor = System.Drawing.Color.Black;
                button6.ForeColor = System.Drawing.Color.Black;
                button7.ForeColor = System.Drawing.Color.Black;

                



            }
            if (signal == 5)
            {
                button1.ForeColor = System.Drawing.Color.Black;
                button5.ForeColor = System.Drawing.Color.Orange;
                button6.ForeColor = System.Drawing.Color.Black;
                button7.ForeColor = System.Drawing.Color.Black;

                
            }
            if (signal == 6)
            {
                button1.ForeColor = System.Drawing.Color.Black;
                button5.ForeColor = System.Drawing.Color.Black;
                button6.ForeColor = System.Drawing.Color.Orange;
                button7.ForeColor = System.Drawing.Color.Black;
                
            }
            if (signal == 7)
            {
                button7.ForeColor = System.Drawing.Color.Orange;
                button5.ForeColor = System.Drawing.Color.Black;
                button6.ForeColor = System.Drawing.Color.Black;
                button1.ForeColor = System.Drawing.Color.Black;
                
            }
        }
        

        private void gameManager1_Load(object sender, EventArgs e)
        {

        }

        private void encounterScreen1_Load(object sender, EventArgs e)
        {

        }

        private void encounterScreen2_Load(object sender, EventArgs e)
        {

        }
    }
}
