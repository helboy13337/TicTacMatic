using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacMatic
{
    public partial class startup : Form
    {
        public startup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            player_vs_bot frm2 = new player_vs_bot();
         
            frm2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            TicTacMatic frm2 = new TicTacMatic();
        
            frm2.ShowDialog();
            
        }

        private void startup_Load(object sender, EventArgs e)
        {

        }
    }
}
