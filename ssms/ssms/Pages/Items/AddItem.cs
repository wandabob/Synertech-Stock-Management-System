﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ssms.Pages.Items
{
    public partial class AddStock : UserControl
    {
        List<LTS.Brand> listB;
        List<LTS.Category> listC;
        List<LTS.Store> listS;
        List<LTS.Barcode> listBar;
        public AddStock()
        {
            InitializeComponent();
        }

        //to change the content of the small panel
        //Margo
        public void ChangeView<T>() where T : Control, new()
        {
            try
            {

                panel1.Controls.Clear();
                T find = new T();
                find.Parent = panel1;
                find.Dock = DockStyle.Fill;
                find.BringToFront();
            }
            catch
            {

            }
        }


        //Margo
        private void button5_Click(object sender, EventArgs e)
        {
            btnlogin.Enabled = false;
            button2.Enabled = false;
            
            comboBoxStore.Enabled = false;
            comboBox1.Enabled = false;
            ChangeView<Store.AddStoreSmall>();
        }



        //after a store is added in the small panel you need to update the combobox
        //Margo
        public void doneStore()
        {
            panel1.Controls.Clear();
            comboBoxStore.DataSource = null;
            listS.Clear();
            listS = DAT.DataAccess.GetStore().ToList();
            List<string> S = new List<string>();

            for (int x = 0; x < listS.Count; x++)
            {
                S.Add(listS[x].StoreName);
            }
            comboBoxStore.DataSource = S;
            
            btnlogin.Enabled = true;
            button2.Enabled = true;

            comboBoxStore.Enabled = true;
            comboBox1.Enabled = true;
        }

        //Devon
        private void AddStock_Load(object sender, EventArgs e)
        {
           
            //load store names into combo box from db
            listS = DAT.DataAccess.GetStore().ToList();
            List<string> S = new List<string>();

            for (int x = 0; x < listS.Count; x++)
            {
                S.Add(listS[x].StoreName);
            }
            comboBoxStore.DataSource = S;


            //load barcode into combo box from db

            listBar = DAT.DataAccess.GetBarcode().ToList();
            List<string> Bar = new List<string>();

            for (int x = 0; x < listBar.Count; x++)
            {
                Bar.Add(listBar[x].BarcodeNumber);
            }
            comboBox1.DataSource = Bar;


        }

        //Margo
        private void button1_Click(object sender, EventArgs e)
        {
            ((Main)this.Parent.Parent).ChangeView<Pages.Items.Items>();
        }

        //Devon
        private void btnlogin_Click(object sender, EventArgs e)
        {
            LTS.Item i = new LTS.Item();

            int storeIndex = comboBoxStore.SelectedIndex;
            int storeID = listS[storeIndex].StoreID;

            i.StoreID = storeID;

            int barcodeIndex = comboBox1.SelectedIndex;
            int barcodeID = listBar[barcodeIndex].BarcodeID;

            LTS.Product p = DAT.DataAccess.GetProduct().Where(a => a.BarcodeID == barcodeID).FirstOrDefault();
            if (p != null)
            {
                i.ProductID = p.ProductID;
                i.ItemStatus = true;
                i.TagEPC = textBox2.Text;

                int returnedID = DAT.DataAccess.AddItem(i);
                textBox2.Text = "";

                if (returnedID == -1)
                {
                     MessageBox.Show("Item was not added to the database!");  
                }
                else{
                     MessageBox.Show("Item was succesfully added to the database");
                }
                
                label16.Visible = false;
            }
            else {
                label16.Visible = true;
                
            }

            
        }

        //Devon
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            try
            {
            //    int itemID = Int32.Parse(label5.Text);

            //    int sIndex = comboBoxStore.SelectedIndex;
            //    int storeID = listS[sIndex].StoreID;

            //    int bIndex = comboBox1.SelectedIndex;
            //    int barID = listBar[bIndex].BarcodeID;

            //    LTS.Product p = new LTS.Product();
            //    p = DAT.DataAccess.GetProduct().Where(f => f.BarcodeID == barID).FirstOrDefault();

            //    string barcode = comboBox1.Text;
            //    string prod = p.ProductName;
            //    string prodDesc = p.ProductDescription;


                panel1.Controls.Clear();
                //public ShowProductDetails(string pBarcode,string pName,string pDescription,string pBrand, string pCategory)
                Control find = new ShowProductDetails("", "", "","","");
                find.Parent = panel1;
                find.Dock = DockStyle.Fill;
                find.BringToFront();

            }
            catch
            {

            }
        }


        //Devon
        private void comboBoxStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            comboBoxStore.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxStore.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        
    }
}
