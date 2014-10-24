using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Texture_Grabber
{
    public partial class Form1 : Form
    {
        Search search;
        ListViewItem[] realItems;
        string PictureUrl = "realmofthemadgod";
        int viewLimit = 2000;

        public Form1()
        {
            InitializeComponent();

            search = new Search(this);
            realItems = new ListViewItem[2000];
        }

        public bool HasItems()
        {
            return listViewTags.Items.Count > 0;
        }

        public bool FindItems(string name)
        {
            bool ret = false;
            List<ListViewItem> lvi = new List<ListViewItem>();
            if (HasItems())
            {
                foreach (ListViewItem i in realItems)
                {
                    if (i.Text.ToLower().Contains(name.ToLower()))
                    {
                        ret = true;
                        lvi.Add(i);
                    }
                }
            }
            listViewTags.Items.Clear();
            listViewTags.Items.AddRange(lvi.ToArray());
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listViewTags.Clear();
            WebClient wc = new WebClient();
            string result = wc.DownloadString("http://" + PictureUrl + ".appspot.com/picture/list?num=" + viewLimit + "&tags=" + textBoxSearchTag.Text);
            XDocument xd = XDocument.Parse(result);
            List<Picture> pics = new List<Picture>();
            if (xd.Root.HasElements) foreach (XElement x in xd.Root.Elements("Pic"))
                    pics.Add(Picture.FromXElement(x));
            foreach (var p in pics)
                listViewTags.Items.Add(new ListViewItem()
                {
                    Text = p.Name,
                    Tag = new LVTag(p.ID, radioButton2.Checked)
                });
            listViewTags.Items.CopyTo(realItems, 0);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBoxItem.Load("http://" + ((LVTag)listViewTags.SelectedItems[0].Tag).GetPicUrl() + ".appspot.com/picture/get?id=" + listViewTags.SelectedItems[0].Tag.ToString());
                textBoxStringTag.Text = listViewTags.SelectedItems[0].Tag.ToString();
                saveFileDialog1.FileName = listViewTags.SelectedItems[0].Text.ToString() + " - " + listViewTags.SelectedItems[0].Tag.ToString() + ".png";
            }
            catch { }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("http://" + ((LVTag)listViewTags.SelectedItems[0].Tag).GetPicUrl() + ".appspot.com/picture/get?id=" + listViewTags.SelectedItems[0].Tag.ToString(), saveFileDialog1.FileName);
            }
            catch
            {
                MessageBox.Show("Error when saving file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                search.ShowDialog();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonProduction.Checked) PictureUrl = "realmofthemadgod";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) PictureUrl = "rotmgtesting";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
    }
    public class Picture
    {
        public long ID;
        public string Name;

        public static Picture FromXElement(XElement x)
        {
            return new Picture()
            {
                ID = Convert.ToInt64(x.Attribute("id").Value),
                Name = x.Element("PicName").Value
            };
        }
    }
    public class LVTag
    {
        public LVTag(long id, bool test)
        {
            Testing = test;
            ID = id;
        }

        public bool Testing;
        public long ID;

        public override string ToString()
        {
            return ID.ToString();
        }

        public string GetPicUrl()
        {
            if (Testing) return "rotmgtesting";
            else return "realmofthemadgod";
        }
    }
}