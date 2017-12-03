using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Coursework2_Group5
{
    public partial class StockDetails : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");
        public int itemID;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Delete.Enabled = false;

                //A method called to Fill Data from GridView
                FillGridView();

                //Populating the DropDown List
                list_item.Items.FindByValue("-1").Selected = true;
                list_supplier.Items.FindByValue("-1").Selected = true;

                FillDropDownListItem();
                FillDropDownListSupplier();


                //Testing
                list_supplier.Enabled = false;
            }
            else
            {
                lblErrorMsg.Text = "";
                lblSuccessMsg.Text = "";
            }
        }

        public void Clear()
        {
            hfSupplierID.Value = "";
            hfItemID.Value = "";
            tb_location.Text = "";
            tb_quantity.Text = "";
            tb_manufactureDate.Text = "";
            tb_expiryDate.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            lbl_itemCode.Text = "";
            btn_Delete.Enabled = false;
            tb_location.Focus();
            btn_Save.Text = "Save";
            list_supplier.ClearSelection();
            list_supplier.Items.FindByValue("-1").Selected = true;
            list_item.ClearSelection();
            list_item.Items.FindByValue("-1").Selected = true;
        }

        protected DataSet FillDropDownList(string stored_procedure)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter(stored_procedure, sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataSet ds = new DataSet();
            sqlDA.Fill(ds);

            //Finally Close the Sql connection
            sqlConn.Close();

            return ds;
        }

        protected void FillDropDownListSupplier()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created

            list_supplier.DataTextField = "SupplierName";
            list_supplier.DataValueField = "supplierid";

            list_supplier.DataSource = FillDropDownList("StockViewAll");
            list_supplier.DataBind();

        }

        protected void FillDropDownListItem()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created

            list_item.DataTextField = "itemname";
            list_item.DataValueField = "itemid";

            list_item.DataSource = FillDropDownList("ItemNamesInStockDropDown");
            list_item.DataBind();

        }

        public DataTable displayData(string stored_procedure)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter(stored_procedure, sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataSet and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            return dt;
        }

        public void FillGridView()
        {
            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_supplier.DataSource = displayData("StockViewAll");
            grid_supplier.DataBind();
        }


        //A method to determine whether a particular item is in stock or not
        protected int isItemInStock(int itemId)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("IsItemInStock", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@itemid", itemId);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Return value 0 if item not in stock otherwise return the item ID
            string result = null;

            if (dt.Rows.Count!= 0)
            {
                result = dt.Rows[0]["itemid"].ToString();
            }

            if(result!= null)
            {
                return int.Parse(result);
             
            }
            else
            {
                
                return 0;  
            }

        }


        //Stock Items CLearing if item is not stocked! protect the item Name
        protected void stockFormClear()
        {
            hfSupplierID.Value = "";
            hfItemID.Value = "";
            tb_location.Text = "";
            tb_quantity.Text = "";
            tb_manufactureDate.Text = "";
            tb_expiryDate.Text = "";
            lblSuccessMsg.Text = "";
            lbl_itemCode.Text = "";
            lblErrorMsg.Text = "";
            btn_Delete.Enabled = false;
            tb_location.Focus();
            btn_Save.Text = "Save";   
        }

        protected void list_item_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Updating the Value of item ID to the Selected Value of DropDownList
            itemID = int.Parse(list_item.SelectedValue);

            //Calling a to check if item is present in stock or not
            int instock = isItemInStock(itemID);

            //If already in Stock
            if(instock!= 0)
            {
                //Check if the Connection is Open or not. If closed, open the Connection
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
                SqlDataAdapter sqlDA = new SqlDataAdapter("StockViewById", sqlConn);

                //Define that the Sql Command is a stored Procedure
                sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

                //Pass the userType ID as parameter to the Stored procedure
                sqlDA.SelectCommand.Parameters.AddWithValue("@itemid", itemID);
                sqlDA.SelectCommand.Parameters.AddWithValue("@supplierid", getSupplierID());

                //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
                DataTable dt = new DataTable();
                sqlDA.Fill(dt);

                //Finally Close the Sql connection
                sqlConn.Close();

                //Now, Showing these Values into the Edit Form

                //Storing the ID into the HiddenField ID and a new String 
                hfItemID.Value = itemID.ToString();
                string dropDownlistItem_text = dt.Rows[0]["ItemName"].ToString();

                //Set the values stored in the DataTables to the Textboxes
                lbl_itemCode.Text = dt.Rows[0]["itemcode"].ToString();
                tb_location.Text = dt.Rows[0]["location"].ToString();
                tb_quantity.Text = dt.Rows[0]["quantity"].ToString();
                tb_manufactureDate.Text = dt.Rows[0]["manufacturedate"].ToString();
                tb_expiryDate.Text = dt.Rows[0]["expirydate"].ToString();
                tb_location.Focus();

                list_item.ClearSelection();
                list_item.Items.FindByText(dropDownlistItem_text).Selected = true;

                //Change the Text of Save Button to Update
                btn_Save.Text = "Update";
            }
            else
            {
                stockFormClear();
            }
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //Validation to determine whether the user has entered an empty value/ Valid Values or not
            /*
            string selectedSupplier = list_supplier.SelectedValue.ToString();
            if (selectedSupplier == "-1")
            {
                lblErrorMsg.Text = "Please select a Supplier!!";
                return;
            }
            */

            string selectedItem = list_item.SelectedValue.ToString();
            if (selectedItem == "-1")
            {
                lblErrorMsg.Text = "Please select an Item!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_location.Text))
            {
                lblErrorMsg.Text = "Please enter the stock's address!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_quantity.Text))
            {
                lblErrorMsg.Text = "Quantity Field Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_manufactureDate.Text))
            {
                lblErrorMsg.Text = "Manufacturing Date Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_expiryDate.Text))
            {
                lblErrorMsg.Text = "Expiry Date Cannot be Empty!!";
                return;
            }

            //If Connection is Closed, opening the Sql Connection

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }


            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("StockCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            

            //Updating the Value of item ID to the Selected Value of DropDownList
            itemID = int.Parse(list_item.SelectedValue);

            
            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@flagValue", isItemInStock(itemID));
            sqlCmd.Parameters.AddWithValue("@supplierid", getSupplierID());
            sqlCmd.Parameters.AddWithValue("@itemid", itemID);
            sqlCmd.Parameters.AddWithValue("@location", tb_location.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@quantity", tb_quantity.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@manufacturedate", tb_manufactureDate.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@expirydate", tb_expiryDate.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@purchasedate", DateTime.Now);


            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string supplierId = hfSupplierID.Value;
            string itemId = hfItemID.Value;

            //Method Clear called to reset all the values 
            Clear();

            if (supplierId == "" && itemId == "")
            {
                lblSuccessMsg.Text = "Saved Successfully!!";
            }
            else
            {
                lblSuccessMsg.Text = "Updated Successfully";
            }


            //A method Called to display the updated data into the GridView
            FillGridView();
        }

        //A Method to Find the Name of the supplier who supplies the item
        protected int getSupplierID()
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("GetSupplierForStockTable", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@itemid", itemID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //pass the desired supplier id
            
             return int.Parse(dt.Rows[0]["supplierid"].ToString());
            
        }


        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
            SqlCommand cmd = new SqlCommand("StockDeleteById", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@supplierid", Convert.ToInt32(hfSupplierID.Value));
            cmd.Parameters.AddWithValue("@itemid", Convert.ToInt32(hfItemID.Value));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            FillGridView();

            lblSuccessMsg.Text = "Stock Deleted Successfully!!";
        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            string[] commandArgs = (sender as LinkButton).CommandArgument.ToString().Split(new char[] { ',' });
            int supplierID = Convert.ToInt32(commandArgs[0]);
            int itemID = Convert.ToInt32(commandArgs[1]);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("StockViewById", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@supplierid", supplierID);
            sqlDA.SelectCommand.Parameters.AddWithValue("@itemid", itemID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID and a new String 
            hfSupplierID.Value = supplierID.ToString();
            hfItemID.Value = itemID.ToString();
            string dropDownlistSupplier_text = dt.Rows[0]["SupplierName"].ToString();
            string dropDownlistItem_text = dt.Rows[0]["ItemName"].ToString();

            //Set the values stored in the DataTables to the Textboxes
            lbl_itemCode.Text = dt.Rows[0]["itemcode"].ToString();
            tb_location.Text = dt.Rows[0]["location"].ToString();
            tb_quantity.Text = dt.Rows[0]["quantity"].ToString();
            tb_manufactureDate.Text = dt.Rows[0]["manufacturedate"].ToString();
            tb_expiryDate.Text = dt.Rows[0]["expirydate"].ToString();
            tb_location.Focus();

            list_supplier.ClearSelection();
            list_supplier.Items.FindByText(dropDownlistSupplier_text).Selected = true;

            list_item.ClearSelection();
            list_item.Items.FindByText(dropDownlistItem_text).Selected = true;

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;
        }


        
    }
}