using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_.NET_
{
    public partial class ProfileForm : MaterialForm
    {
        private MarvelContext context = new MarvelContext();
        public ProfileForm()
        {
            InitializeComponent();
            comboBox1.Items.Add("characters");
            comboBox1.Items.Add("comics");
            comboBox1.Items.Add("series");
            comboBox1.SelectedIndex = 0;
        }

        private void AddToForm(IEnumerable<ItemPrototype> items)
        {
            if (items.Count() == 0)
            {
                Label label = new Label
                {
                    Text = "Nothing was found",
                    Font = new Font(FontFamily.GenericSerif, 14),
                    Width = 300
                };
                flowLayoutPanel1.BeginInvoke(new Action(() =>
                {
                    flowLayoutPanel1.Controls.Add(label);
                }));
            }

            foreach (var item in items)
            {
                Panel panel = new Panel
                {
                    Width = 300,
                    Height = 300
                };
                PictureBox pictureBox = new PictureBox
                {
                    Width = 250,
                    Height = 250,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                Label label = new Label
                {
                    Text = item.Name,
                    Font = new Font(FontFamily.GenericSerif, 14),
                    Width = 150
                };

                pictureBox.Load(item.ImageUrl);
                panel.Controls.Add(label);
                panel.Controls.Add(pictureBox);
                pictureBox.Click += new EventHandler((sender, e) => PictureBox_Click(sender, e, item));


                flowLayoutPanel1.BeginInvoke(new Action(() =>
                {
                    flowLayoutPanel1.Controls.Add(panel);
                }));
            }
        }

        private void PictureBox_Click(object sender, EventArgs e, ItemPrototype item)
        {
            Description form = new Description(item);
            form.Show();
        }


        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            if (comboBox1.SelectedItem.ToString() == "characters")
            {
                Task.Run(() => AddToForm(context.Characters));
            }
            else if (comboBox1.SelectedItem.ToString() == "comics")
            {
                Task.Run(() => AddToForm(context.Comics));
            }
            else
            {
                Task.Run(() => AddToForm(context.Movies));
            }
        }
    }
}
