using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GiftaidDB
{
    public partial class Form2 : Form
    {
        public string title,forename, surname,address;
        public string town,postcode,telephone,email,old_sheetno, new_sheetno,barcode;
        public string postme,teleme,emailme,old_shop, new_shop, localshop;

        public Form1 f1;

        public int movieID;
        public Form2()
        { 
            InitializeComponent();
            f1 = new Form1();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = address;
            textBox2.Text = forename ;
            textBox3.Text = surname;
            textBox7.Text = town;
            textBox8.Text = postcode;
            textBox9.Text = telephone;
            textBox10.Text = email;
            textBox11.Text = old_sheetno;
            textBox12.Text = barcode;
            comboBox1.Text = title;
            comboBox2.Text = old_shop;


            if (postme == "Yes") checkBox1.Checked = true;
  //          else if (postme == "No") checkBox1.Checked = true;

            if (teleme == "Yes") checkBox2.Checked = true;
 //           else if (teleme == "No") checkBox2.Checked = true;

            if (emailme == "Yes") checkBox3.Checked = true;
 //           else if (emailme == "No") checkBox3.Checked = true;



            if (f1.localshop != old_shop)
            {
                // disable update capability in form2  because user 
                // is not allowed to change customer from another shop
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox11.ReadOnly = true;
                textBox12.ReadOnly = true;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                button6.Enabled = false;
            }
            else
            { l_unable_toupdate.Visible = false; }

        }

        #region Update
        private void button6_Click(object sender, EventArgs e)
        {

            //sql query 
         //   Form1 f1 = new Form1();
      //      string typeString=null;
             address = textBox1.Text.ToString();
             forename = textBox2.Text.ToString();
             surname = textBox3.Text.ToString();
             town = textBox7.Text.ToString();
             postcode = textBox8.Text.ToString();
             telephone = textBox9.Text.ToString();
             email = textBox10.Text.ToString();
             new_sheetno = textBox11.Text.ToString();
             barcode = textBox12.Text.ToString();
             title = comboBox1.Text.ToString();
             new_shop = comboBox2.Text.ToString();

            //
            // validate required fields
            //

    //         MessageBox.Show("new shop= "+new_shop, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //         MessageBox.Show("new sheetno= "+new_sheetno, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

             if (new_shop != localshop)
             {
                 MessageBox.Show("You can only change a customer to your local shop ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (new_shop == "")
             {
                 MessageBox.Show("You must enter a Shop", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (title == "")
             {
                 MessageBox.Show("You must enter a Title", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (forename == "")
             {
                 MessageBox.Show("You must enter a Forename", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (surname == "")
             {
                 MessageBox.Show("You must enter a surname", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (address == "")
             {
                 MessageBox.Show("You must enter a address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (postcode == "")
             {
                 MessageBox.Show("You must enter a postcode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (town == "")
             {
                 MessageBox.Show("You must enter a Town", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

             if (new_sheetno == "")
             {
                 MessageBox.Show("You must enter a Sheetno", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }
             string postme, emailme, teleme;  
       
            if (checkBox1.Checked == true)
            { postme = "Yes";}
            else
            {postme = "No";}

            if (checkBox2.Checked == true)
            { teleme = "Yes"; }
            else
            { teleme = "No"; }

            if (checkBox3.Checked == true)
            { emailme = "Yes"; }
            else
            { emailme = "No"; }
        
                //SQLUpdateString = "UPDATE movie SET Title ='" + title.Replace("'", "''") + "', MovieYear=NULL, Publisher='" + publisher + "', typeID=" + type + ", Previewed='" + previewed + "' WHERE movieID=" + movieID + "";
            string SQLUpdateString = "UPDATE customer SET Title ='" + title + "', forename='" + forename + "', surname='" + surname + "', address='" + address + "' , town='" + town + "', postcode='" + postcode + "', telephone='" + telephone + "', email='" + email + "', barcode='" + barcode + "', post_me='" + postme + "', tele_me='" + teleme + "', email_me='" + emailme + "',shop='" + new_shop + "' ,sheetno='" + new_sheetno + "' WHERE sheetno='" + old_sheetno + "' and shop='" + old_shop + "' ";

            try
            {

                OleDbCommand SQLCommand = new OleDbCommand();
                SQLCommand.CommandText = SQLUpdateString;
                SQLCommand.Connection = f1.database;
                SQLCommand.Connection = f1.database;
                int response = SQLCommand.ExecuteNonQuery();
                MessageBox.Show("Update successful!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
 //               string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone,address,town,email,post_me,tele_me,email_me FROM customer";
 //               f1.loadDataGrid(queryString);
   
        //        Close();

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }

  //          if (response >= 1)
  //          {
  //              MessageBox.Show("Update successful!","Message",MessageBoxButtons.OK, MessageBoxIcon.Information);
   //             string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer";
   //             f1.loadDataGrid(queryString);
   //             Close();
   //         }

 //           string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer";
 //           f1.loadDataGrid(queryString);

           Close();

         //   button1.PerformClick();

        }

        #endregion

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(null, null);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}