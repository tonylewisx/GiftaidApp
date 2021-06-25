using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Data.OleDb; // <- for database methods

namespace GiftaidDB
{
    public partial class Form1 : Form
    {
        public OleDbConnection database;
        DataGridViewButtonColumn editButton;
        DataGridViewButtonColumn deleteButton;
        public string localshop;
        public Form2 f2;
 
  //      int movieIDInt;

        #region Form1 constructor
        public Form1()
        {

            InitializeComponent();
            // iniciate DB connection
    //        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=moviedb.mdb";
     //       string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=giftaiddb.mdb";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=giftaiddb.mdb";
  
            try
            {

                database = new OleDbConnection(connectionString);
                database.Open();

                // get the local shop name
                string queryString = "SELECT value from config where parameter='localshop'";
                OleDbCommand SQLQuery = new OleDbCommand();      
                SQLQuery.Connection = null;
                SQLQuery.CommandText = queryString;
                SQLQuery.Connection = database;
                localshop = Convert.ToString(SQLQuery.ExecuteScalar());

                l_shopid.Text = " Local SVP Shop : "+localshop;

     //           MessageBox.Show("localshop => " + localshop);

                //SQL query to customers
                queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone,address,town,email,post_me,tele_me,email_me,shop FROM customer";

                loadDataGrid(queryString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        
        #region Load dataGrid
        public void loadDataGrid(string sqlQueryString) {

            OleDbCommand SQLQuery = new OleDbCommand();
            DataTable data = null;
            dataGridView1.DataSource = null;
            SQLQuery.Connection = null;
            OleDbDataAdapter dataAdapter = null;
            dataGridView1.Columns.Clear(); // <-- clear columns
           
            //---------------------------------
            SQLQuery.CommandText = sqlQueryString;
            SQLQuery.Connection = database;
            data = new DataTable();
            dataAdapter = new OleDbDataAdapter(SQLQuery);
            dataAdapter.Fill(data);
            dataGridView1.DataSource = data;
            dataGridView1.AllowUserToAddRows = false; // remove the null line
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            //dataGridView1.Columns[6].Width = 50;
            //dataGridView1.Columns[7].Width = 50;

            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;


            // insert edit button into datagridview
            editButton = new DataGridViewButtonColumn();
            editButton.HeaderText = "Edit";
            editButton.Text = "Edit";
            editButton.UseColumnTextForButtonValue = true;
            editButton.Width = 80;
            dataGridView1.Columns.Add(editButton);
            // insert delete button to datagridview
            deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 80;
            dataGridView1.Columns.Add(deleteButton);
        }
        #endregion

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        #region Close database connection
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.Close();
        }
        #endregion

        #region refresh button
  //      private void button2_Click(object sender, EventArgs e)
 //       {
  //          textBox4.Clear();
  //          string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone,address,town,email,post_me,tele_me,email_me FROM customer";
   //         loadDataGrid(queryString);
   //     }
        #endregion

        #region Input
        private void button6_Click(object sender, EventArgs e)
        {
  
            string address = textBox1.Text;
            string forename = textBox2.Text;
            string surname = textBox3.Text;
            string town = textBox7.Text;
            string postcode = textBox8.Text;
            string telephone = textBox9.Text;
            string email = textBox10.Text;
            string sheetno = textBox11.Text;
            string barcode = textBox12.Text;
            string title = comboBox1.Text;
            string shop = comboBox3.Text;

            if (shop != localshop)
            {
                MessageBox.Show("You can ONLY add customers to your own shop ! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //
            // validate entry input 
            //

 //           MessageBox.Show("Title length = "+ title.Length.ToString());


            if (sheetno == "")
            {
                MessageBox.Show("You must enter a Sheetno ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (shop == "")
            {
                MessageBox.Show("You must enter a Shop", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if ( title == "" )
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

            if ( address == "")
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

            string postme,emailme,teleme;
            if (checkBox1.Checked == true)
            {
                postme = "Yes";
            }
            else
            {
                postme = "No";
            }
            if (checkBox2.Checked == true)
            {
                teleme = "Yes";
            }
            else
            {
                teleme = "No";
            }
            if (checkBox3.Checked == true)
            {
                emailme = "Yes";
            }
            else
            {
                emailme = "No";
            }


            string SQLString = "INSERT INTO customer(Title, forename,surname,address,town,postcode,telephone,email,sheetno,barcode,post_me,tele_me,email_me,shop) VALUES('" + title + "','" + forename + "','" + surname + "','" + address + "','" + town + "','" + postcode + "','" + telephone + "','" + email + "','" + sheetno + "','" + barcode + "','" + postme + "','" + teleme + "','" + emailme + "','" + shop + "');";

           OleDbCommand SQLCommand = new OleDbCommand();
           SQLCommand.CommandText = SQLString;
           SQLCommand.Connection = database;
           int response = -1;
           try
             {
                response = SQLCommand.ExecuteNonQuery();
             }
             catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
           if (response >= 1)
             {
               MessageBox.Show("Customer is added to database","Successful",MessageBoxButtons.OK, MessageBoxIcon.Information);
               textBox1.Clear();
               textBox2.Clear();
               textBox3.Clear();
               textBox7.Clear();
               textBox8.Clear();
               textBox9.Clear();
               textBox10.Clear();
               textBox11.Clear();
               textBox12.Clear();
               comboBox1.ResetText();
               comboBox3.ResetText();
               checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = false;
            }
            else
            {
               MessageBox.Show("Error on Inserting Customer to database", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
   //            textBox3.Clear();
    //           textBox3.Focus();
            }
        }

        #endregion

        #region Delete/Edit button handling
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer";

            int currentRow = int.Parse(e.RowIndex.ToString());
    
            string sheetno = dataGridView1[0, currentRow].Value.ToString(); // primary key

            string title = dataGridView1[1, currentRow].Value.ToString();
            string surname = dataGridView1[2, currentRow].Value.ToString();
            string forename = dataGridView1[3, currentRow].Value.ToString();
            string barcode = dataGridView1[4, currentRow].Value.ToString();
            string postcode = dataGridView1[5, currentRow].Value.ToString();
            string telephone = dataGridView1[6, currentRow].Value.ToString();

            string town = dataGridView1[8, currentRow].Value.ToString();
            string email = dataGridView1[9, currentRow].Value.ToString();
            string postme = dataGridView1[10, currentRow].Value.ToString();
            string teleme = dataGridView1[11, currentRow].Value.ToString();
            string emailme = dataGridView1[12, currentRow].Value.ToString();
            string address = dataGridView1[7, currentRow].Value.ToString();
            string shop = dataGridView1[13, currentRow].Value.ToString();

             // edit button
            if (dataGridView1.Columns[e.ColumnIndex] == editButton && currentRow >= 0)
            {
               // string address = dataGridView1[6, currentRow].Value.ToString();

                //runs form 2 for editing    
                f2 = new Form2();
                f2.title = title;
                f2.forename = forename;
                f2.surname = surname;
                f2.barcode = barcode;
                f2.postcode  = postcode;
                f2.telephone = telephone;
                f2.address = address;
                f2.old_sheetno = sheetno;

                f2.town = town;
                f2.email = email;
                f2.postme = postme;
                f2.teleme = teleme;
                f2.emailme = emailme;
                f2.old_shop = shop;
                f2.localshop = localshop;

    //            ProcessStartInfo pinfo = new ProcessStartInfo();
    //            pinfo.Arguments = "f2.Show()";
    //            Process p = Process.Start(pinfo);
    //            p.WaitForInputIdle();
     //           p.WaitForExit();

             //   Thread t_form2 = new Thread(new ThreadStart(f2.Show));
             //   t_form2.Start();
             //   t_form2.Join(10000);

          //      MessageBox.Show("before call form2 update");

                f2.ShowDialog();

           //     MessageBox.Show("after call form2 update");

             //   f2.Show();

           //    System.Threading.Thread.Sleep(1000);

               loadDataGrid(queryString);

            //   MessageBox.Show("loaded datagrid");
           
      //         dataGridView1.Update();
      //         dataGridView1.Refresh();

                // go back to front screen inorder to refersh grid 
                tabControl1.SelectTab(0);

             //   tabControl1.SelectTab(1);
               // tabControl2.Select();

        //        tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        //        tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];

        
            }
            // delete button
            else if (dataGridView1.Columns[e.ColumnIndex] == deleteButton && currentRow >= 0)
            {

                if (shop == localshop)
                {
                    if (MessageBox.Show("Are you sure you want to delete this Customer", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {

                        // delete sql query
                        string queryDeleteString = "DELETE FROM customer where sheetno = '" + sheetno + "'";
                        //string queryDeleteString = null;
                        OleDbCommand sqlDelete = new OleDbCommand();
                        sqlDelete.CommandText = queryDeleteString;
                        sqlDelete.Connection = database;
                        sqlDelete.ExecuteNonQuery();
                        loadDataGrid(queryString); // refresh screen after delete
                    }
                }
                else
                { MessageBox.Show("You are not allowed to delete customers from another shop", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    
            }
             
         }
        #endregion
         
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region search by postcode
        private void button1_Click(object sender, EventArgs e)
        {
            string postcode = textBox4.Text.ToString();
            if (postcode != "")
            {
                string queryString = "SELECT sheetno,title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer where postcode LIKE '" + postcode + "%'";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter a Postcode","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region search by surname
        private void button5_Click(object sender, EventArgs e)
        {
            string surname = textBox13.Text.ToString();
            if (surname != "")
            {
                string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer where surname LIKE '" + surname + "%'";
                //"SELECT title, surname, forename, barcode, postcode, Telephone FROM customer";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter a Surname", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        
        }
        #endregion

        #region search by forename
        private void button4_Click(object sender, EventArgs e)
        {
            string forename = textBox5.Text.ToString();
            if (forename != "")
            {
                string queryString = "SELECT sheetno,title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer where forename LIKE '" + forename + "%'";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter a forename", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

 
  //      }
        #endregion

        #region search telephone
        private void button3_Click(object sender, EventArgs e)
        {
           // string previewed;
           // if (radioButton3.Checked == true) previewed = "Yes";
           // else previewed = "No";
            string telephone = textBox6.Text.ToString();
            if (telephone != "")
            {
                string queryString = "SELECT sheetno,title, surname, forename, barcode, postcode, Telephone, address, town,email,post_me,tele_me,email_me,shop FROM customer where telephone LIKE '" + telephone + "%'";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter a telephone number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(null, null);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
   //         string queryString = "SELECT movieID, Title, Publisher, Previewed, MovieYear, Type FROM movie,movieType WHERE movietype.typeID = movie.typeID";
            string queryString = "SELECT sheetno, title, surname, forename, barcode, postcode, Telephone, address, town,email,post_me,tele_me,email_me,shop FROM customer";
            loadDataGrid(queryString);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lab_search_bar_Click(object sender, EventArgs e)
        {

        }
        #region search barcode
        private void button7_Click(object sender, EventArgs e)
        {
            string barcode = ent_search_bar.Text.ToString();
            if (barcode != "")
            {
                string queryString = "SELECT sheetno,title, surname, forename, barcode, postcode, Telephone, address,town,email,post_me,tele_me,email_me,shop FROM customer where barcode LIKE '" + barcode + "%'";
                loadDataGrid(queryString);
            }
            else
            {
                MessageBox.Show("You must enter a Barcode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void tabPage4_Click(object sender, EventArgs e)
        {
            textBox13.Clear();
        }
        private void tabControl2_Click(object sender, EventArgs e)
        {
            textBox13.Clear();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void synvToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I am sorry, This Functionality is under construction , it will be available in a Future release", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
  //          MessageBox.Show("I am sorry, This Functionality is under construction , it will be available in a Future release", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
   //         string shop = c_shop.Text;

         

            string createTableString = "select * into cust_full_backup from customer";

            try
            {
                // create a copy of the customer table and data ;
                OleDbCommand sqlCreatetable = new OleDbCommand();
                sqlCreatetable.CommandText = createTableString;
                sqlCreatetable.Connection = database;
                sqlCreatetable.ExecuteNonQuery();

                MessageBox.Show("The Customers have been Backed up ( table cust_full_backup)", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
  
                
           //     return;
            }

   //         MessageBox.Show("Till in routine", "", MessageBoxButtons.OK);
    
        }

        private void exportCustomerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
 //           MessageBox.Show("I am sorry, This Functionality is under construction , it will be available in a Future release", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            export fexp = new export();
            fexp.database = database;
            fexp.Show();
        }

        private void importCustomerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import fimp = new Import();
            fimp.database = database;
            fimp.Show(); 
  //          MessageBox.Show("I am sorry, This Functionality is under construction , it will be available in a Future release", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void removeBacupOfCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dropTableString = "drop table cust_full_backup";

            try
            {
                // remove  backup of the customer table and data ;
                OleDbCommand sqltable = new OleDbCommand();
                sqltable.CommandText = dropTableString;
                sqltable.Connection = database;
                sqltable.ExecuteNonQuery();

                MessageBox.Show("Backup of Customer has been removed", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void aboutGiftadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutGiftaid ga = new AboutGiftaid();
            ga.Show();
        }
    }
}