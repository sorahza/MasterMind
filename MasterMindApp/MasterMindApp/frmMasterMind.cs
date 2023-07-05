using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*AF It would be nicer if the "Give up" and "done selection" buttons would be disabled on start and disabled when the game is over - it's a clearer UI
 Also, when the game is over, the user should see some sort of message letting him know that he won.
Also, to make it clear that you can play again, the "start button" should change to say "Start over" to start a new game*/
namespace MasterMindApp
{
    public partial class frmMasterMind : Form
    {
        int ncount = 0;
        List<TableLayoutPanel> lstpanel;
        List<Color> lstcolor;
        List<Button> lstbuttons;
        Color secretcolor1;
        Color secretcolor2;
        Color secretcolor3;
        Color secretcolor4;


        public frmMasterMind()
        {
            InitializeComponent();
            btnStart.Click += BtnStart_Click;
            btnDone.Click += BtnDone_Click;
            lstpanel = new() { tblGuess1, tblGuess2, tblGuess3,tblGuess4,tblGuess5,tblGuess6,tblGuess7,tblGuess8,tblGuess9 ,tblGuess10 };
            lstcolor = new() { Color.Red, Color.Blue, Color.Yellow, Color.Green, Color.Orange, Color.Purple };
            lstbuttons = new();
            CreateButtonList();
            lstbuttons.ForEach(b => b.Click += BtnSpot_Click);
            lstbuttons.ForEach(b => b.MouseDown += BtnSpot_MouseDown);
            lstbuttons.ForEach(b => b.Enabled = false);
            btnGiveUp.Click += BtnGiveUp_Click;

        }

      

        private void CreateButtonList()
        {
            foreach (TableLayoutPanel t in lstpanel)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is Button)
                    {
                        lstbuttons.Add((Button)c);
                    }
                }
            }
        }

        private Color GetColorChoice()
        {
            Color c = new();
            if (optBlue.Checked == true)
                c = Color.Blue;
            else if (optPurple.Checked == true)
                c = Color.Purple;
            else if (optGreen.Checked == true)
                c = Color.Green;
            else if (optYellow.Checked == true)
                c = Color.Yellow;
            else if (optRed.Checked == true)
                c = Color.Red;
            else if (optOrange.Checked == true)
                c = Color.Orange;

            return c;
        }

        private void StartGame()
        {
            ncount = 0;
            lstbuttons.ForEach(b => b.BackColor = DefaultBackColor);
            GetSecretCode();
            lstpanel.ForEach(t => t.CellBorderStyle = TableLayoutPanelCellBorderStyle.None);
            lstpanel.ForEach(t => ResetLabels(t));
            EnablePanel();
        }
        private void ResetLabels(TableLayoutPanel t) {
            foreach (Control c in t.Controls)
            {
                if (c is TableLayoutPanel)
                    foreach (Control l in c.Controls)
                    {
                        if (l is Label)
                        {
                            l.BackColor = DefaultBackColor;
                       }
                    }
            }
        }
        private void GetSecretCode()
        {
            List<Color> lstcolor2 = new List<Color>(lstcolor);
            secretcolor1 = lstcolor2[new Random().Next(0, lstcolor2.Count() )];
            lstcolor2.Remove(secretcolor1);
            secretcolor2 = lstcolor2[new Random().Next(0, lstcolor2.Count() )];
            lstcolor2.Remove(secretcolor2);
            secretcolor3 = lstcolor2[new Random().Next(0, lstcolor2.Count() )];
            lstcolor2.Remove(secretcolor3);
            secretcolor4 = lstcolor2[new Random().Next(0, lstcolor2.Count() )];
            lstcolor2.Remove(secretcolor4);
            lblCode1.BackColor = Color.DarkGray;
            lblCode2.BackColor = Color.DarkGray;
            lblCode3.BackColor = Color.DarkGray;
            lblCode4.BackColor = Color.DarkGray;

        }
        private void EnablePanel()
        {
            if (ncount > lstpanel.Count() - 1)
            {
                return;
            }
            TableLayoutPanel tblturn = lstpanel[ncount];
            tblturn.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            foreach (Control c in tblturn.Controls)
            {
                if (c is Button)
                {
                    c.Enabled = true;
                }
            }
            btnDone.Enabled = false;
        }
        private void GameOver() {
            lblCode1.BackColor = secretcolor1;
            lblCode2.BackColor = secretcolor2;
            lblCode3.BackColor = secretcolor3;
            lblCode4.BackColor = secretcolor4;
            lstbuttons.ForEach(b => b.Enabled = false);

        }
        private bool CheckForDuplicateColor(Color cchoice)
        {
            
                TableLayoutPanel tblturn = lstpanel[ncount];
                foreach (Control c in tblturn.Controls)
                {
                    if (c is Button)
                    {
                       if (c.BackColor == cchoice)
                    {
                        return true;
                    }

                    }
                }
            return false;
            
        }
        private bool CheckAllColorsFilled()
        {

            TableLayoutPanel tblturn = lstpanel[ncount];
            foreach (Control c in tblturn.Controls)
            {
                if (c is Button)
                {
                    if (c.BackColor == DefaultBackColor)
                        return false;
                }
            }
            return true;

        }

        //AF I would name this procedure a name that explains more cleary what the procedure is doing - GiveREsult sounds vague to me
        private string GiveResult() {
            TableLayoutPanel tblturn = lstpanel[ncount];
            int cntexactmatch = 0;
            int cntcolormatch = 0;
            //AF What is 'ncntbutton' referring to - this variable name sounds confusing to me, as I have no idea what ncnt is
            int ncntbutton = 1;
            string gamestatus = "";
            foreach (Control c in tblturn.Controls)
            {
                
                if (c is Button)
                {
                    Color chk = c.BackColor;
                    Color match = new();
                    switch (ncntbutton) {
                        case 1:
                            match = secretcolor1;
                            break;
                         case 2:
                            match = secretcolor2;
                            break;
                        case 3:
                            match = secretcolor3;
                            break;
                        case 4:
                            match = secretcolor4;
                            break;
                    }
                    if (chk == match)
                    { cntexactmatch = cntexactmatch + 1; }
                    else if (chk == secretcolor1 || chk == secretcolor2 || chk == secretcolor3 || chk == secretcolor4)
                    { cntcolormatch = cntcolormatch + 1; }
                    ncntbutton = ncntbutton + 1;
                }
            }
            if (cntexactmatch == 4)
                gamestatus = "Winner";
            foreach (Control c in tblturn.Controls) {
                if (c is TableLayoutPanel)
                    foreach (Control l in c.Controls)
                    {
                        if (l is Label)
                        {
                            //AF Seems like the curly brace opening below and closing at line 236 is extra
                            {
                                if (cntexactmatch > 0)
                                {
                                    l.BackColor = Color.Red;
                                    cntexactmatch = cntexactmatch - 1;
                                }
                                else if (cntcolormatch > 0)
                                {
                                    //AF It is very hard to tell the difference between the white and the default color, a stronger color should be used in this case
                                    l.BackColor = Color.White;
                                    cntcolormatch = cntcolormatch - 1;
                                }
                            }
                        }
                    }
            }


            return gamestatus;
        }
        private void BtnDone_Click(object? sender, EventArgs e)
        {
            string gamestatus = GiveResult();
            if (gamestatus != "Winner")
            {
                lstbuttons.ForEach(b => b.Enabled = false);
                ncount = ncount + 1;
                EnablePanel();
            }
            else GameOver();
        }
        private void BtnSpot_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button btn = (Button)sender;
                btn.BackColor = DefaultBackColor;
            }
        }
        private void BtnStart_Click(object? sender, EventArgs e)
        {
            StartGame();
        }

        private void BtnSpot_Click(object? sender, EventArgs e)
        {
            Color c = GetColorChoice();
            bool duplicate = false;
            duplicate = CheckForDuplicateColor(c);
            if (duplicate == false) {
                Button btn = (Button)sender;
                btn.BackColor = c;
            }
            bool done = CheckAllColorsFilled();
            if (done == true)
                btnDone.Enabled = true;
        }
        private void BtnGiveUp_Click(object? sender, EventArgs e)
        {
            GameOver();
            
        }
    }
}
