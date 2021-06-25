﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GiftaidDB
{
    public partial class export : Form
    {
        public OleDbConnection database;
        public export()
        {
            InitializeComponent();
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            string shop = c_shop.Text;
            string deleteString = "DELETE FROM cust_export where shop not in ( '" + shop + "')";
            string createTableString = "select * into cust_export from customer";
            string dropTableString = "drop table cust_export";


            // drop  copy of the customer table and data ;
            try
            {
                OleDbCommand sqlDroptable = new OleDbCommand();
                sqlDroptable.CommandText = dropTableString;
                sqlDroptable.Connection = database;
                sqlDroptable.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }


            // create a copy of the customer table and data ;
            OleDbCommand sqlCreatetable = new OleDbCommand();
            sqlCreatetable.CommandText = createTableString;
            sqlCreatetable.Connection = database;
            sqlCreatetable.ExecuteNonQuery();

            // delete all other shop data except that required 
            OleDbCommand sqlDelete = new OleDbCommand();
            sqlDelete.CommandText = deleteString;
            sqlDelete.Connection = database;
            sqlDelete.ExecuteNonQuery();

            MessageBox.Show("Customer exported to table cust_export", "", MessageBoxButtons.OK, MessageBoxIcon.None);
               
            Close();

  //          loadDataGrid(queryString); // 
        }

        private void b_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
