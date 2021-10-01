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
    public partial class Description : MaterialForm
    {
        private MarvelContext context = new MarvelContext();
        private MarvelManager manager = new MarvelManager();
        private ItemPrototype item;
        public Description(ItemPrototype item)
        {
            InitializeComponent();
            pictureBox1.Load(item.ImageUrl);
            label1.Text = item.Name;
            this.item = item;
            comboBox1.Items.Add("characters");
            comboBox1.Items.Add("comics");
            comboBox1.Items.Add("series");
            if (item.GetType() == typeof(Character))
            {
                comboBox1.Items.RemoveAt(0);
                if (context.Characters.Where(x => x.RealId == item.RealId).Any())
                {
                    materialFlatButton1.Enabled = false;
                    materialFlatButton2.Enabled = true;
                }
            }
            else if (item.GetType() == typeof(Comic))
            {
                comboBox1.Items.RemoveAt(1);
                comboBox1.Items.RemoveAt(1);
                if (context.Comics.Where(x => x.RealId == item.RealId).Any())
                {
                    materialFlatButton1.Enabled = false;
                    materialFlatButton2.Enabled = true;
                }
            }
            else
            {
                comboBox1.Items.RemoveAt(2);
                if (context.Movies.Where(x => x.RealId == item.RealId).Any())
                {
                    materialFlatButton1.Enabled = false;
                    materialFlatButton2.Enabled = true;
                }
            }
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string name = item.GetType().Name.ToLower() + 's';
            if (name.Contains("movie"))
            {
                name = "series";
            }
            var items = manager.GetItemsById(item.RealId, name, comboBox1.SelectedItem.ToString());
            Task.Run(() => AddToForm(items));
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (item.GetType() == typeof(Character))
            {
                context.Characters.Add((Character)item);
            }
            else if (item.GetType() == typeof(Comic))
            {
                context.Comics.Add((Comic)item);
            }
            else
            {
                context.Movies.Add((Movie)item);
            }
            context.SaveChanges();
            materialFlatButton1.Enabled = false;
            materialFlatButton2.Enabled = true;
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            if (item.GetType() == typeof(Character))
            {
                context.Characters.Remove(context.Characters.Where(x => x.RealId == item.RealId).FirstOrDefault());
            }
            else if (item.GetType() == typeof(Comic))
            {
                context.Comics.Remove(context.Comics.Where(x => x.RealId == item.RealId).FirstOrDefault());
            }
            else
            {
                context.Movies.Remove(context.Movies.Where(x => x.RealId == item.RealId).FirstOrDefault());
            }
            context.SaveChanges();
            materialFlatButton1.Enabled = true;
            materialFlatButton2.Enabled = false;
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
