using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Raketa
{
    public partial class PostavkeForm : Form
    {
        string bodovi, prepreke, vrijeme;
        public List<string> Odgovori { get => new List<string> { bodovi, prepreke, vrijeme }; }
        public PostavkeForm()
        {
            InitializeComponent();
        }

        private void PostavkeForm_Load(object sender, EventArgs e)
        {
            ForeColor = Color.White;

            Label label1 = new Label();
            label1.Text = "Odaberite željenu težinu igre.";
            label1.Location = new Point(20, 20);
            label1.Size = new Size(300, 20);
            label1.Font = new Font("Arial", 12, FontStyle.Bold);
            label1.BackColor = Color.RoyalBlue;
            Controls.Add(label1);

            GroupBox box1 = new GroupBox();
            box1.Location = new Point(20, 70);
            box1.Size = new Size(300, 70);
            box1.Font = new Font("Arial", 10, FontStyle.Bold);
            box1.Text = "1. Bonus bodovi:";
            box1.Name = "box1";
            box1.ForeColor = Color.White;
            box1.BackColor = Color.RoyalBlue;

            Controls.Add(box1);

            RadioButton r1_1 = new RadioButton();
            r1_1.Location = new Point(20, 20);
            r1_1.Size = new Size(100, 40);
            r1_1.Font = new Font("Arial", 8);
            r1_1.Text = "Lagano";
 
            RadioButton r1_2 = new RadioButton();
            r1_2.Location = new Point(120, 20);
            r1_2.Size = new Size(100, 40);
            r1_2.Font = new Font("Arial", 8);
            r1_2.Text = "Srednje";

            RadioButton r1_3 = new RadioButton();
            r1_3.Location = new Point(220, 20);
            r1_3.Size = new Size(70, 40);
            r1_3.Font = new Font("Arial", 8);
            r1_3.Text = "Teško";

            box1.Controls.Add(r1_1);
            box1.Controls.Add(r1_2);
            box1.Controls.Add(r1_3);

            foreach (RadioButton radioButton in box1.Controls.OfType<RadioButton>())
                radioButton.CheckedChanged += (s, e1) =>
                {
                    if (radioButton.Checked)
                        bodovi = radioButton.Text;
                };

            GroupBox box2 = new GroupBox();
            box2.Location = new Point(20, 170);
            box2.Size = new Size(300, 70);
            box2.Font = new Font("Arial", 10, FontStyle.Bold);
            box2.Text = "2. Brzina prepreke:";
            box2.Name = "box1";
            box2.ForeColor = Color.White;
            box2.BackColor = Color.RoyalBlue;

            Controls.Add(box2);

            RadioButton r2_1 = new RadioButton();
            r2_1.Location = new Point(20, 20);
            r2_1.Size = new Size(100, 40);
            r2_1.Font = new Font("Arial", 8);
            r2_1.Text = "Lagano";

            RadioButton r2_2 = new RadioButton();
            r2_2.Location = new Point(120, 20);
            r2_2.Size = new Size(100, 40);
            r2_2.Font = new Font("Arial", 8);
            r2_2.Text = "Srednje";

            RadioButton r2_3 = new RadioButton();
            r2_3.Location = new Point(220, 20);
            r2_3.Size = new Size(100, 40);
            r2_3.Font = new Font("Arial", 8);
            r2_3.Text = "Teško";

            box2.Controls.Add(r2_1);
            box2.Controls.Add(r2_2);
            box2.Controls.Add(r2_3);

            foreach (RadioButton radioButton in box2.Controls.OfType<RadioButton>())
                radioButton.CheckedChanged += (s, e1) =>
                {
                    if (radioButton.Checked)
                        prepreke = radioButton.Text;
                };

            GroupBox box3 = new GroupBox();
            box3.Location = new Point(20, 270);
            box3.Size = new Size(300, 70);
            box3.Font = new Font("Arial", 10, FontStyle.Bold);
            box3.Text = "3. Broj kometa:";
            box3.Name = "box1";
            box3.ForeColor = Color.White;
            box3.BackColor = Color.RoyalBlue;

            Controls.Add(box3);

            RadioButton r3_1 = new RadioButton();
            r3_1.Location = new Point(20, 20);
            r3_1.Size = new Size(100, 40);
            r3_1.Font = new Font("Arial", 8);
            r3_1.Text = "Lagano";

            RadioButton r3_2 = new RadioButton();
            r3_2.Location = new Point(120, 20);
            r3_2.Size = new Size(70, 40);
            r3_2.Font = new Font("Arial", 8);
            r3_2.Text = "Srednje";

            RadioButton r3_3 = new RadioButton();
            r3_3.Location = new Point(220, 20);
            r3_3.Size = new Size(70, 40);
            r3_3.Font = new Font("Arial", 8);
            r3_3.Text = "Teško";

            box3.Controls.Add(r3_1);
            box3.Controls.Add(r3_2);
            box3.Controls.Add(r3_3);

            foreach (RadioButton radioButton in box3.Controls.OfType<RadioButton>())
                radioButton.CheckedChanged += (s, e1) =>
                {
                    if (radioButton.Checked)
                        vrijeme = radioButton.Text;
                };
        }

        private void gumbZatvori_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
