using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Raketa
{
    public partial class StartForm : Form
    {
        List<string> odabranePostavke;

        public StartForm()
        {
            InitializeComponent();
        }

        private void gumbZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gumbPokreni_Click(object sender, EventArgs e)
        {
            Form1 formaZaIgru = new Form1(odabranePostavke);
            Visible = false;
            formaZaIgru.ShowDialog();
            Visible = true;
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void gumbPostavke_Click(object sender, EventArgs e)
        {
            PostavkeForm postavke = new PostavkeForm();
            Visible = false;
            postavke.ShowDialog();
            Visible = true;

            odabranePostavke = postavke.Odgovori;
        }
    }
}
