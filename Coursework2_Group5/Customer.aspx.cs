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
    public partial class Customer : System.Web.UI.Page
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


        //A method to Clear Values
        protected void Clear()
        {
            hfCustomerID.Value = "";
            tb_CustomerName.Text = "";
            tb_contactNo.Text = "";
            tb_email.Text = "";
            tb_address.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            btn_Delete.Enabled = false;
            tb_CustomerName.Focus();
            btn_Save.Text = "Save";
            list_user.ClearSelection();
            list_user.Items.FindByValue("-1").Selected = true;
        }


        //A method defined to populate the GridView
        protected void displayData()
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("CustomerViewAll", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataSet and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();


            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_User.DataSource = dt;
            grid_User.DataBind();
        }



        //A method to fill the values to the DropDown List
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



        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_CustomerName.Text))
            {
                lblErrorMsg.Text = "Please enter a Customer Name!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_address.Text))
            {
                lblErrorMsg.Text = "Please enter the Customer's Address!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_contactNo.Text))
            {
                lblErrorMsg.Text = "Contact Number Field Cannot be Empty!!";
                return;
            }

            if(String.IsNullOrEmpty(tb_email.Text))
            {
                lblErrorMsg.Text = "Contact Email Cannot be Empty!!";
                return;
            }

            string selected = list_user.SelectedValue.ToString();
            if (selected == "-1")
            {

                lblErrorMsg.Text = "Please Select a Handling User";
                list_user.Focus();
                return;
            }


            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("CustomerCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@memberid", (hfCustomerID.Value == "" ? 0 : Convert.ToInt32(hfCustomerID.Value)));
            sqlCmd.Parameters.AddWithValue("@membername", tb_CustomerName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@address", tb_address.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@contactnumber", tb_contactNo.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@email", tb_email.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@userid", list_user.SelectedValue);

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrId = hfCustomerID.Value;

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
            SqlCommand cmd = new SqlCommand("CustomerDeleteById", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@memberid", Convert.ToInt32(hfCustomerID.Value));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            displayData();

            lblSuccessMsg.Text = "Customer Deleted Successfully!!";
        }


        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            int customerID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("CustomerViewById", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@memberid", customerID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID and a new String 
            hfCustomerID.Value = customerID.ToString();
            string dropDownlist_text = dt.Rows[0]["UserName"].ToString();

            //Set the values stored in the DataTables to the Textboxes
            tb_CustomerName.Text = dt.Rows[0]["membername"].ToString();
            tb_address.Text = dt.Rows[0]["address"].ToString();
            tb_contactNo.Text = dt.Rows[0]["contactnumber"].ToString();
            tb_email.Text = dt.Rows[0]["email"].ToString();

            tb_CustomerName.Focus();

            list_user.ClearSelection();
            list_user.Items.FindByText(dropDownlist_text).Selected = true;

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;

        }
    }
}