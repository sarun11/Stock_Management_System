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
    public partial class Sales : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                displayDataGrid();

                list_user.Items.FindByValue("-1").Selected = true;
                //list_item.Items.FindByValue("-1").Selected = true;
                list_customer.Items.FindByValue("-1").Selected = true;

                FillDropDownListUser();
                FillDropDownListItem();
                FillDropDownListCustomer();
                Clear();
            }
        }

        protected void Clear()
        {
            hfSaleID.Value = "";
            hfItemID.Value = "";
            tb_quantity.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            btn_Save.Text = "Save";

            tb_billingNo.Text = "";
            tb_billingDate.Text = "";
            tb_itemCode.Text = "";
            lbl_itemName.Text = "";
            list_user.ClearSelection();
            list_user.Items.FindByValue("-1").Selected = true;

            //list_item.ClearSelection();
            //list_item.Items.FindByValue("-1").Selected = true;

            list_customer.ClearSelection();
            list_customer.Items.FindByValue("-1").Selected = true;
        }

        protected DataTable displayData(string stored_procedure)
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

        protected void displayDataGrid()
        {

            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_Sales.DataSource = displayData("SaleViewAll");
            grid_Sales.DataBind();
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

        protected void FillDropDownListUser()
        {

            //Selecting the datasource of the dropdown list to be the datalist so created
            list_user.DataTextField = "username";
            list_user.DataValueField = "userid";

            list_user.DataSource = FillDropDownList("UserViewAll");
            list_user.DataBind();

        }

        protected void FillDropDownListItem()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created
            //list_item.DataTextField = "itemname";
            //list_item.DataValueField = "itemid";

            //list_item.DataSource = FillDropDownList("ItemViewAll");
            //list_item.DataBind();
        }

        protected void FillDropDownListCustomer()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created
            list_customer.DataTextField = "membername";
            list_customer.DataValueField = "memberid";

            list_customer.DataSource = FillDropDownList("CustomerViewAll");
            list_customer.DataBind();
        }



        protected void btn_Save_Click(object sender, EventArgs e)
        {
            // Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_quantity.Text))
            {
                lblErrorMsg.Text = "Please enter an Item Name!!";
                return;
            }

            // Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_billingDate.Text))
            {
                lblErrorMsg.Text = "Please enter an Billing Date!!";
                return;
            }


            // Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_itemCode.Text))
            {
                lblErrorMsg.Text = "Please enter an Item Code!!";
            }

            string selected_customer = list_customer.SelectedValue.ToString();
            if (selected_customer == "-1")
            {

                lblErrorMsg.Text = "Please Select a Customer";
                list_customer.Focus();
                return;
            }

            string selected_user = list_user.SelectedValue.ToString();
            if (selected_user == "-1")
            {

                lblErrorMsg.Text = "Please Select an Handling User!";
                list_user.Focus();
                return;
            }


            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("SaleCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@billingdate", tb_billingDate.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@billingnumber", tb_billingNo.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@userid", list_user.SelectedValue.ToString());
            sqlCmd.Parameters.AddWithValue("@customerid", list_customer.SelectedValue.ToString());

           
            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //A method is now called to Insert the individual sales to the SaleLine
            insertIntoSaleLine(int.Parse(hfItemID.Value), returnSaleID(tb_billingNo.Text.Trim()), int.Parse(tb_quantity.Text));


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrId = hfItemID.Value;

            //Method Clear called to reset all the values 
            Clear();

            if (usrId == "")
            {
                lblSuccessMsg.Text = "Sales Record Entered Successfully Successfully!!";
            }
            
            //A method Called to display the updated data into the GridView
            displayDataGrid();

        }


        //A method to insert items into the Sale line
        protected void insertIntoSaleLine(int itemID, int saleID, int quantity)
        {
            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("SaleLineInsert", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@itemid", itemID);
            sqlCmd.Parameters.AddWithValue("@saleid", saleID);
            sqlCmd.Parameters.AddWithValue("@quantity", quantity);
          
            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();
        }

        //A method to fetch sales id from a billing number
        protected int returnSaleID(string billingNumber)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("FetchSaleIdFromSales", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@billingnumber", billingNumber);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //returning the sales id value
            return int.Parse(dt.Rows[0]["salesid"].ToString());
            

        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Delete_Click(object sender, EventArgs e)
        {
            string[] commandArgs = (sender as LinkButton).CommandArgument.ToString().Split(new char[] { ',' });
            int salesID = Convert.ToInt32(commandArgs[0]);
            int itemID = Convert.ToInt32(commandArgs[1]);
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
            SqlCommand cmd = new SqlCommand("SaleLineDeleteByID", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@salesid", Convert.ToInt32(salesID));
            cmd.Parameters.AddWithValue("@itemid", Convert.ToInt32(itemID));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            try
            {
                SqlCommand delcmd = new SqlCommand("SaleDeleteById", sqlConn);

                // Declare the Sql Command to be a Stored Procedure
                delcmd.CommandType = CommandType.StoredProcedure;

                //Sending the Value of userTypeID as parameter to the Stored Procedure
                delcmd.Parameters.AddWithValue("@salesid", Convert.ToInt32(salesID));

                //Execute the Query in the database
                delcmd.ExecuteNonQuery();
            }
            catch
            {
                //do nothing
            }

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            displayDataGrid();

            lblSuccessMsg.Text = "Sale line Deleted Successfully!!";
        }
      

        protected void tb_itemCode_TextChanged(object sender, EventArgs e)
        {
            
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("FindItemNameByCode", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@itemcode", tb_itemCode.Text.ToString());

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID and a new String 
            hfItemID.Value = dt.Rows[0]["itemid"].ToString();
        
            lbl_itemName.Text = dt.Rows[0]["itemname"].ToString();
        }
    }
}