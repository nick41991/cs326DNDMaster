﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dnd_start_load
{
    public partial class EncounterScreen : UserControl
    {

        //Form2 form;
        List<Player> players;
        List<Monster> monsters;

        List<Character> turnOrder;
        int turnPosition;
        string selectedMonster;

        List<Button> buttons = new List<Button>();

        public EncounterScreen()
        {
            InitializeComponent();
            updatebutton.Hide();
            updateTurnOrder.Hide();
        }

        public void place_button(int x, int y, String img_target, Monster title)
        {

            Point newLoc = new Point(x, y); // Set whatever you want for initial location
            bool same_loc_flag = true;
            bool same_but_flag = true;
            Button b = new Button();
            string curdir = AppDomain.CurrentDomain.BaseDirectory;

                b.Name = title.getName();
                b.Text = title.getName();
            b.TextAlign = ContentAlignment.BottomLeft;
            b.TextImageRelation = TextImageRelation.TextAboveImage;

            if (img_target != "" && img_target != null)
            {
                b.BackgroundImage = System.Drawing.Image.FromFile(curdir + "Images\\" + img_target + ".jpg");
                b.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else {

                b.BackgroundImage = System.Drawing.Image.FromFile(curdir + "Images\\" + "Goblin" + ".jpg");
                b.BackgroundImageLayout = ImageLayout.Stretch;

            }
            b.Location = newLoc;
           

            foreach (Button n in buttons)
            {

                if (n.Location == b.Location)
                {

                    same_loc_flag = false;
                    break;
                }

            }
            foreach (Button n in buttons)
            {

                if (n.Name == b.Name)
                {

                    same_but_flag = false;
                   
                    
                }

            }

            if (same_loc_flag && same_but_flag)
            {

                buttons.Add(b);

            }
            

            b.MouseClick += (sender, e) => button_opt(sender, e, title);
            //Controls.Add(b);
            //*
            foreach (Button z in buttons)
            {
                Controls.Add(z);
               
                z.Size = new Size(175, 125);

            }
            //*/
        }
        public void button_opt(object s, EventArgs e, Monster monster)
        {
            selectedMonster = monster.getName();
            textBox1.Text = null;
            textBox1.Text = monster.getName() + Environment.NewLine + "Health: " + monster.getHp() + Environment.NewLine +"init:" + monster.getinit();

        }
        public void remove_button(String name) {

                 bool flag = false;
                 Button n = null;
            foreach (Button b in buttons)
            {

                if (b.Name.Equals(name))
                {

                    //b.Hide();
                    
                     flag = true;
                     n = b;
                }
            }
            //*
            if (flag) {

                buttons.Remove(n);
                this.Controls.Remove(n);


            }
            //*/
        }

        

        private void startbutton_Click(object sender, EventArgs e)
        {
            
            Form2 form = (Form2)this.Parent;
            players = form.game.getPlayers();
            monsters = form.game.getMonsters();
            if (players.Count != 0 || monsters.Count != 0)
            {
                updateTurnOrder.Visible = true;
                resetbutton.Visible = true;
                draw_update();

                string s = "";
                foreach (Player p in players)
                {
                    s = String.Concat(s, "player: " + p.getName() + "\tinit: " + p.getinit() + Environment.NewLine + Environment.NewLine);
                }

                turnOrder = form.game.generateTurnOrder();
                turnPosition = 0;
                playerRotationDisplay.Text = turnOrder.ElementAt(turnPosition).getName() + "'s Turn";

                playernames.Text = s;
                startbutton.Hide();
                updatebutton.Visible = true;
            }
        }
        public void draw_update() {

            Form2 form = (Form2)this.Parent;
            monsters = form.game.getMonsters();
            int x = 105;
            int y = 70;

            foreach (Monster n in monsters)
            {

                if (buttons.Count() < 16 ) {
                    if (x + 180 <= 1000)
                    {
                        x = x + 180;
                    }
                    else
                    {

                        x = 285;
                        y = y + 125;
                    }
                    if (n.getHp() != 0){
                        place_button(x, y, n.getimg(), n);
                    }
                    else {

                        place_button(x, y, "Death_screen", n);

                    }
                }
            }
            foreach (Button n in buttons)
            {

                n.BringToFront();

            }






        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            Form2 form = (Form2)this.Parent;
           
            players = form.game.getPlayers();
            monsters = form.game.getMonsters();
           draw_update();
            int i = 0;
            
            for(i = 0;i< buttons.Count();i++)
            {
                remove_button(buttons.ElementAt(i).Name);
            }
                draw_update();

            string s = "";
            foreach (Player p in players)
            {
                s = String.Concat(s, "player: " + p.getName() + "\tinit: " + p.getinit() + Environment.NewLine + Environment.NewLine);
            }

            playernames.Text = s;

            String cur = "";
            if(turnPosition >= 0)
                cur = turnOrder.ElementAt(turnPosition).getName();

            turnOrder = form.game.generateTurnOrder();
            turnPosition = -1;
            foreach(Character c in turnOrder) /*Dom't skip the player who's turn it is*/
            {
                if (cur.Equals(c.getName()))
                {
                    break;
                }
                turnPosition++;
            }
            if(turnPosition >= 0)
                playerRotationDisplay.Text = turnOrder.ElementAt(turnPosition).getName() + "'s Turn";

        }

        private void playernames_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void playerRotationDisplay_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateTurnOrder_Click(object sender, EventArgs e)
        {
            turnPosition++;
            if(turnPosition >= turnOrder.Count)
            {
                turnPosition = 0;
            }

            if (turnOrder.Count <= 0)
                turnPosition = -1;

            if(turnPosition >= 0)
                playerRotationDisplay.Text = turnOrder.ElementAt(turnPosition).getName() + "'s Turn";
        }

        private void hpbutton_Click(object sender, EventArgs e)
        {
            Form2 form = (Form2)this.Parent;
            
            foreach (Monster m in form.game.getMonsters())
            {
                if (m.getName().Equals(selectedMonster))
                {
                    m.setHp(Convert.ToInt32(hpbox.Text));

                    if (m.getHp() == 0)
                    {
                        m.isdead = true;
                    }
                    else {
                        m.isdead = false;

                    }
                    
                      
                    textBox1.Text = m.getName() + Environment.NewLine + "Health: " + m.getHp() + Environment.NewLine + "init:" + m.getinit();
                    updatebutton.PerformClick();
                    break;
                }
                draw_update();
            }


        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            playernames.Text = "";
            playerRotationDisplay.Text = "";
            updatebutton.Hide();
            updateTurnOrder.Hide();
            startbutton.Visible = true;
            Form2 form = (Form2)this.Parent;
            int i = 0;
            if (buttons.Count > 0)
            {


                Button buttonToRemove = buttons[0];
                buttons.Remove(buttonToRemove);
                this.Controls.Remove(buttonToRemove);
                resetbutton.PerformClick();


            }
            //draw_update();
            form.game.getPlayers().Clear();
            form.game.getMonsters().Clear();
            resetbutton.Hide();
        }
    }

    
}
