using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Raketa
{
    public partial class Form1 : Form
    {
        public Form1(List<string> postavke)
        {
            tezinaBodovi = tezinaPrepreke = tezinaKometi = "Lagano";
            if (postavke != null)
            {
                if (postavke[0] != null)
                    tezinaBodovi = postavke[0];
                if (postavke[1] != null)
                    tezinaPrepreke = postavke[1];
                if (postavke[2] != null)
                    tezinaKometi = postavke[2];
            }

            InitializeComponent();
            sirina = ClientSize.Width;
            visina = ClientSize.Height;
            PocetnePostavke();
            timer1.Start();
            DoubleBuffered = true;
            labelaBodovi.Location = labelaBodovi1.Location = new Point(4, 4);
            progress1.Parent = prepreka1;
            progress2.Parent = prepreka2;           
            progress1.Maximum = hpPrepreka;
            progress2.Maximum = hpPrepreka;
        }

        ProgressBar progress1 = new ProgressBar();
        ProgressBar progress2 = new ProgressBar();

        StatusStrip bar = new StatusStrip();
        ToolStripStatusLabel zivotiStatusLabel;
        ToolStripStatusLabel bodoviStatusLabel;

        Image zid = Properties.Resources.zidNovi;
        float sirina, visina;
        float brzinaPozadine, brzinaZida, brzinaBroda, brzinaPrepreka;
        float[] koordPozadina, koordZid;
        bool lijevo, desno;
        bool krajIgre, krajZivota, pauza;
        bool kretanje, preprekaBocno;

        int pomakBocno;
        int bodovi = 0, brojZivota = 3, pomocniProgress = 1000;
        string tezinaBodovi, tezinaPrepreke, tezinaKometi;

        int mozeRaketa; // svakih 'mozeRaketa' tickova mozemo ispaliti raketu, odbrojavamo do 0 ( na 0 se moze ispaliti )
        bool ispaljenaRaketa;
        bool unistenKomet, unistenaPrepreka1, unistenaPrepreka2;
        int hpPrepreka = 5;

        Random random = new Random();
        Random random2 = new Random();

        private void PocetnePostavke()
        {
            progress1.Location = new Point(16, 10);
            progress1.Width = 150;
            progress1.Height = 15;
            progress2.Location = new Point(16, 10);
            progress2.Width = 150;
            progress2.Height = 15;

            // pocetne postavke za varijable sa status bar
            bar.Items.Clear();
            progressBar1.Value = pomocniProgress;

            // pocetne postavke za progress bar na preprekama i raketu
            progress1.Value = hpPrepreka;
            progress2.Value = hpPrepreka;
            mozeRaketa = 0;
            ispaljenaRaketa = false;

            if (krajIgre) // resetiramo vrijednosti za cijelu novu igru
            {
                progressBar1.Value = 1000;
                brojZivota = 3;
                bodovi = 0;
                povecajBodove(0);
            }

            PostaviStatusBar();

            // pocetne postavke za ispaljivanje raketa
            unistenKomet = unistenaPrepreka1 = unistenaPrepreka2 = false;

            krajIgre = krajZivota = pauza = false;
            labelaRestartPoruka.Visible = false;
            labelNoviZivotPoruka.Visible = false;
            labelaPauza.Visible = false;

            // pocetne postavke za horizontalno kretanje prepreka
            preprekaBocno = false;
            pomakBocno = 0;
            prepreka1.Location = new Point(10, 205);
            prepreka2.Location = new Point(205, 10);

            lijevo = desno = false;
            kretanje = false;
            koordPozadina = new float[] { -visina, 0 };
            koordZid = new float[] { -visina, 0 };
            brzinaPozadine = 0.5f;
            brzinaZida = 4;
            brzinaBroda = 5;

            // brzina kretanja prepreka ovisno o tome koju težinu je igrač odabrao
            switch (tezinaPrepreke)
            {
                case "Lagano": brzinaPrepreka = 1; break;
                case "Srednje": brzinaPrepreka = 2; break;
                case "Teško": brzinaPrepreka = 4; break; // nisam stavio 3 jer onda se poklopi da se moze ici samo ravno
                                                         // i izbjegavaju se sve prepreke
                default: break;
            }

            // početna lokacija broda
            brod.Location = new Point
                ((int)sirina / 2 - brod.Size.Width / 2,
                (int)visina - brod.Size.Height - 40);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PomakniPozadinu();
            PomakniPrepreke();
            if (lijevo && !desno &&
                brod.Left - brzinaBroda >= 0.1 * sirina)
                brod.Left -= (int)brzinaBroda;
            if (desno && !lijevo &&
                brod.Right + brzinaBroda <= 0.9 * sirina)
                brod.Left += (int)brzinaBroda;
            if (kretanje)
            {
                prepreka1.Top += (int)brzinaZida;
                prepreka2.Top += (int)brzinaZida;
                if (prepreka1.Top > visina)
                {
                    if (unistenaPrepreka1)
                    {
                        prepreka1.Visible = true;
                        unistenaPrepreka1 = false;
                    }
                    progress1.Value = hpPrepreka;
                    povecajBodove(1);
                    prepreka1.Top = -prepreka1.Height;
                    progressBar1.Value = Math.Min(progressBar1.Value + 60, 1000);
                }
                if (prepreka2.Top > visina)
                {
                    if (unistenaPrepreka2)
                    {
                        prepreka2.Visible = true;
                        unistenaPrepreka2 = false;
                        progress2.Value = hpPrepreka;
                    }
                    povecajBodove(1);
                    prepreka2.Top = -prepreka2.Height;
                    progressBar1.Value = Math.Min(progressBar1.Value + 60, 1000);
                }
            }

             progressBar1.Value -= 1;

            switch (tezinaKometi) // povećavamo pojavljivanje kometa prema postavkama za komete
            {
                case "Lagano": if (random.Next() % 100 == 0) StvoriKomet(); break;
                case "Srednje": if (random.Next() % 100 == 0 || random.Next() % 100 == 2) StvoriKomet(); break;
                case "Teško": if (random.Next() % 100 == 0 || random.Next() % 100 == 2 || random.Next() % 100 == 3) StvoriKomet(); break;
                default: break;
            }
                
            if (random2.Next() % 100 == 1)
                StvoriBonus();

            if (ispaljenaRaketa) StvoriRaketu();

            foreach (Control kontrola in Controls)
            {
                if (kontrola is PictureBox x && (string)x.Tag == "komet")
                {
                    x.Top += (int)(kretanje ? (brzinaZida + brzinaBroda) : brzinaZida);
                    if (x.Top > visina)
                    {
                        Controls.Remove(kontrola);
                        x.Dispose();
                    }
                    foreach(Control kontrola2 in Controls)
                    {
                        if (kontrola2 is PictureBox y && (string)y.Tag == "raketa" && x.Bounds.IntersectsWith(y.Bounds))
                        {
                            if(unistenKomet)
                            {
                                Controls.Remove(kontrola);
                                Controls.Remove(kontrola2);
                                x.Dispose();
                                y.Dispose();
                                povecajBodove(1);
                            }
                            unistenKomet = !unistenKomet;
                        }
                    }
                }
                else if (kontrola is PictureBox y && (string)y.Tag == "bonus")
                {
                    y.Top += (int)(kretanje ? (brzinaZida + brzinaBroda) : brzinaZida);
                    if (y.Top > visina)
                    {
                        Controls.Remove(kontrola);
                        y.Dispose();
                    }
                }
                else if (kontrola is PictureBox z && (string)z.Tag == "raketa")
                {
                    z.Top -= (int)(kretanje ? brzinaZida : (brzinaZida + brzinaBroda));

                    bool first = z.Bounds.IntersectsWith(prepreka1.Bounds);
                    bool second = z.Bounds.IntersectsWith(prepreka2.Bounds);
                    if ((first || second))
                    {
                        if (first) progress1.Value = Math.Max(progress1.Value - 1, 0);
                        else progress2.Value = Math.Max(progress2.Value - 1, 0);
                        Controls.Remove(kontrola);
                        z.Dispose();
                    }

                    if (z.Top == 0)
                    {
                        Controls.Remove(kontrola);
                        z.Dispose();
                    }
                }
            }

            Invalidate();

            if ((brod.Bounds.IntersectsWith(prepreka1.Bounds) && !unistenaPrepreka1)
                || (brod.Bounds.IntersectsWith(prepreka2.Bounds) && !unistenaPrepreka2)
                || progressBar1.Value == 0)
            {
                brojZivota--;
                if (brojZivota == 0 || progressBar1.Value == 0) GameOver();
                else NoviZivot();
                return;
            }

            foreach (Control kontrola in Controls)
            {
                if (kontrola is PictureBox x && (string)x.Tag == "komet")
                {
                    if (brod.Bounds.IntersectsWith(x.Bounds))
                    {
                        brojZivota--;
                        if (brojZivota == 0) GameOver();
                        else NoviZivot();
                        return;
                    }
                }
                else if (kontrola is PictureBox y && (string)y.Tag == "bonus")
                {
                    if (brod.Bounds.IntersectsWith(y.Bounds))
                    {
                        Controls.Remove(kontrola);
                        y.Dispose();
                        povecajBodove(1);
                    }
                }
            }

            if (progress1.Value == 0)
            {
                prepreka1.Visible = false;
                unistenaPrepreka1 = true;
                progress1.Value = hpPrepreka;
                povecajBodove(3);
            }
            if (progress2.Value == 0)
            {
                prepreka2.Visible = false;
                unistenaPrepreka2 = true;
                progress2.Value = hpPrepreka;
                povecajBodove(3);
            }

            ispaljenaRaketa = false;
            mozeRaketa = Math.Max(mozeRaketa - 1, 0);
            bodoviStatusLabel.Text = $"Bodovi: {bodovi}";
        }

        private void PostaviStatusBar()
        {
            bar.BackColor = Color.Black;
            bar.AutoSize = false;
            bar.Dock = DockStyle.Bottom;
            bar.Height = 35;
            bar.BringToFront();
            UpdateZivoti();
            UpdateBodovi();
            Controls.Add(bar);
        }

        private void UpdateZivoti()
        {
            for (int i = 0; i < brojZivota; i++)
            {
                zivotiStatusLabel = new ToolStripStatusLabel();
                Image zivot = Properties.Resources.heart;
                zivotiStatusLabel.ImageScaling = ToolStripItemImageScaling.None;
                zivotiStatusLabel.Image = zivot;
                bar.Items.Add(zivotiStatusLabel);
            }
        }

        private void UpdateBodovi()
        {
            bodoviStatusLabel = new ToolStripStatusLabel();
            bodoviStatusLabel.ForeColor = Color.White;
            bodoviStatusLabel.Spring = true;
            bodoviStatusLabel.TextAlign = ContentAlignment.TopRight;
            bodoviStatusLabel.Font = new Font("Arial", 24);
            bodoviStatusLabel.Margin = new Padding(0, -2, 0, 0);
            bar.Items.Add(bodoviStatusLabel);
        }

        private void PomakniPozadinu()
        {
            for (int i = 0; i < 2; i++)
            {
                if (koordPozadina[i] > visina)
                    koordPozadina[i] -= 2 * visina;
                if (koordZid[i] > visina)
                    koordZid[i] -= 2 * visina;
                if (kretanje)
                {
                    koordPozadina[i] += brzinaPozadine;
                    koordZid[i] += brzinaZida;
                }
            }
        }

        private void PomakniPrepreke()
        {
            if (pomakBocno < 205 - 10)
            {
                if (!preprekaBocno)
                {
                    prepreka1.Left += (int)brzinaPrepreka;
                    prepreka2.Left -= (int)brzinaPrepreka;
                }
                else
                {
                    prepreka1.Left -= (int)brzinaPrepreka;
                    prepreka2.Left += (int)brzinaPrepreka;
                }
                pomakBocno += (int)brzinaPrepreka;
            }
            else
            {
                preprekaBocno = !preprekaBocno;
                pomakBocno = 0;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && !kretanje)
                kretanje = true;
            if (e.KeyCode == Keys.Left)
                lijevo = true;
            if (e.KeyCode == Keys.Right)
                desno = true;
            if (e.KeyCode == Keys.P)
            {
                if (!pauza)
                {
                    Form1_Deactivate(sender, e);
                    pauza = true;
                }
                else
                {
                    Form1_Activated(sender, e);
                    pauza = false;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && kretanje)
                kretanje = false;
            if (e.KeyCode == Keys.Left)
                lijevo = false;
            if (e.KeyCode == Keys.Right)
                desno = false;
            if (e.KeyCode == Keys.R && krajIgre)
            {
                PocetnePostavke();
                timer1.Start();
            }
            if (e.KeyCode == Keys.R && krajZivota)
            {
                PocetnePostavke();
                timer1.Start();
            }
            if (e.KeyCode == Keys.Space && mozeRaketa == 0)
            {
                mozeRaketa = 40;
                ispaljenaRaketa = true;
            }

        }

        void povecajBodove(int dobiveniBodovi)
        {
            switch (tezinaBodovi)
            {
                case "Lagano": bodovi += (dobiveniBodovi * 3); break;
                case "Srednje": bodovi += (dobiveniBodovi * 2); break;
                case "Teško": bodovi += dobiveniBodovi; break;
                default: break;
            }
            labelaBodovi.Text = labelaBodovi1.Text
                = "Bodovi: " + bodovi;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (!krajZivota)
            {
                timer1.Stop();
                if (kretanje || lijevo || desno)
                    kretanje = lijevo = desno = false;
                if (!krajZivota)
                    labelaPauza.Visible = true;
                else
                {
                    PocetnePostavke();
                    timer1.Start();
                }
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (!krajIgre)
            {
                labelaPauza.Visible = false;
                timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NoviZivot()
        {
            timer1.Stop();
            krajZivota = true;
            bar.Items.RemoveAt(bar.Items.Count - 2);
            labelNoviZivotPoruka.Visible = true;
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is PictureBox x && 
                    ((string)x.Tag == "komet" || (string)x.Tag == "bonus" || (string)x.Tag == "raketa"))
                {
                    x.Visible = false;
                    Controls.Remove(Controls[i]);
                    x.Dispose();
                }
            }
            pomocniProgress = progressBar1.Value; // zbog početka novog života pamtimo progress u pomocnoj varijabli
        }

        private void GameOver()
        {
            timer1.Stop();
            krajIgre = krajZivota = true;
            labelaRestartPoruka.Visible = true;
            MessageBox.Show("Osvojeni bodovi: " + bodovi,
                "Igra je završila!");
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is PictureBox x &&
                    ((string)x.Tag == "komet" || (string)x.Tag == "bonus" || (string)x.Tag == "raketa"))
                {
                    x.Visible = false;
                    Controls.Remove(Controls[i]);
                    x.Dispose();
                }
            }
            bodovi = 0; // zbog početka nove igre
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 2; ++i)
            {
                e.Graphics.DrawImage(zid, 0, koordZid[i],
                    0.1f * sirina, visina);
                e.Graphics.DrawImage(zid, 0.9f * sirina, koordZid[i],
                    0.1f * sirina, visina);
            }
        }

        private void StvoriKomet()
        {
            PictureBox komet = new PictureBox();
            komet.Size = new Size(20, 20);
            komet.BackColor = Color.Red;
            komet.BorderStyle = BorderStyle.FixedSingle;
            komet.Top = -komet.Height;
            komet.Left = (int)(0.1 * sirina + 1)
                + random.Next(0, (int)(0.8 * sirina - komet.Width));
            komet.Tag = "komet";

            Controls.Add(komet);
            komet.BringToFront();
        }

        // bonus stvari se pojavljuju isto kao i kometi, padaju niz ekran
        // i donose dodatne bodove igraču (ovisno o izabranoj težini)
        private void StvoriBonus()
        {
            PictureBox bonus = new PictureBox();
            bonus.Size = new Size(20, 20);
            bonus.BackColor = Color.DarkGreen;
            bonus.BorderStyle = BorderStyle.FixedSingle;
            bonus.Top = -bonus.Height;
            bonus.Left = (int)(0.1 * sirina + 1)
                + random2.Next(0, (int)(0.8 * sirina - bonus.Width));
            bonus.Tag = "bonus";

            Controls.Add(bonus);
            bonus.BringToFront();
        }

        private void StvoriRaketu()
        {
            PictureBox raketa = new PictureBox();
            raketa.Image = Properties.Resources.laser;
            raketa.BackColor = Color.White;
            raketa.Size = new Size(15, 15);
            raketa.Top = brod.Top;
            raketa.Left = brod.Left + 15;
            raketa.Tag = "raketa";

            Controls.Add(raketa);
            raketa.BringToFront();
        }
    }
}
