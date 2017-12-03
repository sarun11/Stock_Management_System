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
    public partial class SupplierDetails : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Delete.Enabled = false;
                displayData();

                //Populating the DropDown List
                list_user.Items.FindByValue("-1").Selected = true;
                FillDropDownList();

            }
        }

        public void Clear()
        {
            hfSupplierID.Value = "";
            tb_supplierName.Text = "";
            tb_supplierAddress.Text = "";
            tb_contactNumber.Text = "";
            tb_email.Text = "";
            tb_url.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            tb_contactPerson.Text = "";
            btn_Delete.Enabled = false;
            tb_supplierName.Focus();
            btn_Save.Text = "Save";
            list_user.ClearSelection();
            list_user.Items.FindByValue("-1").Selected = true;
        }

        protected void FillDropDownList()
        {


            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("UserViewAll", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataSet ds = new DataSet();
            sqlDA.Fill(ds);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Selecting the datasource of the dropdown list to be the datalist so created

            list_user.DataTextField = "username";
            list_user.DataValueField = "userid";

            list_user.DataSource = ds;
            list_user.DataBind();

        }

        public void displayData()
        {


            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("SupplierViewAll", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataSet and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();


            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_supplier.DataSource = dt;
            grid_supplier.DataBind();
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_supplierName.Text))
            {
                lblErrorMsg.Text = "Please enter a Supplier Name!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_supplierAddress.Text))
            {
                lblErrorMsg.Text = "Please enter the supplier's address!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_contactNumber.Text))
            {
                lblErrorMsg.Text = "Contact Number Field Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_email.Text))
            {
                lblErrorMsg.Text = "Email Field Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_url.Text))
            {
                lblErrorMsg.Text = "URL Field Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_contactPerson.Text))
            {
                lblErrorMsg.Text = "Contact Person Field Cannot be Empty!!";
                return;
            }

            string selected = list_user.SelectedValue.ToString();
            if (selected == "-1")
            {

                lblErrorMsg.Text = "Please Select a User";
                list_user.Focus();
                return;
            }

            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("SupplierCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@supplierid", (hfSupplierID.Value == "" ? 0 : Convert.ToInt32(hfSupplierID.Value)));
            sqlCmd.Parameters.AddWithValue("@suppliername", tb_supplierName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@supplieraddress", tb_supplierAddress.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@contactnumber", tb_contactNumber.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@email", tb_email.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@url", tb_url.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@contactperson", tb_contactPerson.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@userid", list_user.SelectedValue);

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrId = hfSupplierID.Value;

            //Method Clear called to reset all the values 
            Clear();

            if (usrId == "")
            {
                lblSuccessMsg.Text = "Saved Successfully!!";
            }
            else
            {
                lblSuccessMsg.Text = "Updated Successfully";
            }


            //A method Called to display the updated data into the GridView
            displayData();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
            SqlCommand cmd = new SqlCommand("SupplierDeleteById", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@supplierid", Convert.ToInt32(hfSupplierID.Value));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            displayData();

            lblSuccessMsg.Text = "Supplier Deleted Successfully!!";
        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            int supplierID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("supplierViewById", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@supplierid", supplierID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID and a new String 
            hfSupplierID.Value = supplierID.ToString();
            string dropDownlist_text = dt.Rows[0]["UserName"].ToString();

            //Set the values stored in the DataTables to the Textboxes
            tb_supplierName.Text = dt.Rows[0]["suppliername"].ToString();
            tb_supplierAddress.Text = dt.Rows[0]["supplieraddress"].ToString();
            tb_contactNumber.Text = dt.Rows[0]["contactnumber"].ToString();
            tb_email.Text = dt.Rows[0]["email"].ToString();
            tb_url.Text = dt.Rows[0]["url"].ToString();
            tb_contactPerson.Text = dt.Rows[0]["contactperson"].ToString();
            tb_supplierName.Focus();

            list_user.ClearSelection();
            list_user.Items.FindByText(dropDownlist_text).Selected = true;

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;
        }
    }
}