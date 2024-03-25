using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevExProgramacaoLaura
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void JuridicalP_Click(object sender, EventArgs e)
        {
            var JuridicalP = new JuridicalP();
            JuridicalP.Show();
        }

        private void NaturalP_Click(object sender, EventArgs e)
        {
            var NaturalP = new NaturalP();
            NaturalP.Show();
        }
    }
}
