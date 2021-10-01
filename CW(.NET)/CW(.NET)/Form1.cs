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
    public partial class Form1 : Form
    {
        MarvelContext context = new MarvelContext();
        MarvelManager manager = new MarvelManager();

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Replace(" ",String.Empty) != "")
            {
                flowLayoutPanel1.Controls.Clear();

                if(comboBox1.SelectedItem == "Character")
                {
                    var items = manager.GetCharacters(textBox1.Text);
                    Task.Run(() => AddToForm(items));
                }
                else if(comboBox1.SelectedItem == "Comic")
                {
                    var items = manager.GetComics(textBox1.Text);
                    Task.Run(() => AddToForm(items));
                }
                else
                {
                    var items = manager.GetMovies(textBox1.Text);
                    Task.Run(() => AddToForm(items));
                }
                
            }
            else
            {
                MessageBox.Show("Type in name or title");
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            ProfileForm form = new ProfileForm();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
