using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMindApp
{
    public partial class GuessControl : UserControl
    {
        public GuessControl()
        {
            InitializeComponent();
            btnGuess1.DoubleClick += BtnGuess_DoubleClick;
            btnGuess2.DoubleClick += BtnGuess_DoubleClick;
            btnGuess3.DoubleClick += BtnGuess_DoubleClick;
            btnGuess4.DoubleClick += BtnGuess_DoubleClick;
        }

        private void BtnGuess_DoubleClick(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = DefaultBackColor;
        }
    }
}
