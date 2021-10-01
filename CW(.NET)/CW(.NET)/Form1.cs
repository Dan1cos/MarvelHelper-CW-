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
    public partial class Form1 : MaterialForm
    {
        MarvelContext context = new MarvelContext();
        MarvelManager manager = new MarvelManager();
        private int offset = 0;
        private string name = "aaa";
        private string type = "Character";

        public Form1()
        {
            InitializeComponent();
            if (!context.Users.Any())
            {
                User u1 = new User { Name = "Test" };
                context.Users.Add(u1);
                context.SaveChanges();
            }
            comboBox1.Items.Add("Character");
            comboBox1.Items.Add("Comic");
            comboBox1.Items.Add("Series");
            comboBox1.SelectedIndex=0;
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
                pictureBox.Click += new EventHandler((sender,e) => PictureBox_Click(sender,e,item));


                flowLayoutPanel1.BeginInvoke(new Action(() =>
                {
                    flowLayoutPanel1.Controls.Add(panel);
                }));
            }
        }

        private void PictureBox_Click(object sender, EventArgs e,ItemPrototype item)
        {
            Description form = new Description(item);
            form.Show();
        }


        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            ProfileForm form = new ProfileForm();
            form.ShowDialog();
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Replace(" ", String.Empty) != "")
            {
                flowLayoutPanel1.Controls.Clear();
                offset = 0;
                name = textBox1.Text;
                type = comboBox1.SelectedItem.ToString();
                if (comboBox1.SelectedItem.ToString() == "Character")
                {
                    var items = manager.GetCharacters(textBox1.Text, offset);
                    Task.Run(() => AddToForm(items));
                }
                else if (comboBox1.SelectedItem.ToString() == "Comic")
                {
                    var items = manager.GetComics(textBox1.Text, offset);
                    Task.Run(() => AddToForm(items));
                }
                else
                {
                    var items = manager.GetMovies(textBox1.Text, offset);
                    Task.Run(() => AddToForm(items));
                }

            }
            else
            {
                MessageBox.Show("Type in name or title");
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (offset >= 10)
            {
                offset -= 10;
                if (type == "Character")
                {
                    flowLayoutPanel1.Controls.Clear();
                    var items = manager.GetCharacters(name, offset);
                    Task.Run(() => AddToForm(items));
                }
                else if (type == "Comic")
                {
                    flowLayoutPanel1.Controls.Clear();
                    var items = manager.GetComics(name, offset);
                    Task.Run(() => AddToForm(items));
                }
                else
                {
                    flowLayoutPanel1.Controls.Clear();
                    var items = manager.GetMovies(name, offset);
                    Task.Run(() => AddToForm(items));
                }
            }
            else
            {
                MessageBox.Show("Nothing");
                offset += 10;
            }
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            offset += 10;
            if (type == "Character")
            {
                var items = manager.GetCharacters(name, offset);
                if (items.Count() != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    Task.Run(() => AddToForm(items));
                }
                else
                {

                    MessageBox.Show("Nothing");
                    offset -= 10;
                }
            }
            else if (type == "Comic")
            {
                var items = manager.GetComics(name, offset);
                if (items.Count() != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    Task.Run(() => AddToForm(items));
                }
                else
                {
                    MessageBox.Show("Nothing");
                    offset -= 10;
                }
            }
            else
            {
                var items = manager.GetMovies(name, offset);
                if (items.Count() != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    Task.Run(() => AddToForm(items));
                }
                else
                {
                    MessageBox.Show("Nothing");
                    offset -= 10;
                }
            }
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
