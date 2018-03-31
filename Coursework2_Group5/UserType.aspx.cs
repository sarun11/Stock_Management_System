using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace Coursework2_Group5
{
    public partial class UserType : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Delete.Enabled = false;
                displayData();
            }
        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            hfUserTypeID.Value = "";
            tb_userTypeName.Text = " ";
            btn_Save.Text = "Save";
            btn_Delete.Enabled = false;
            lblSuccessMsg.Text = lblErrorMsg.Text = "";
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //Validation to determine whether the user has entered an empty value or not
            if (tb_userTypeName.Text == "")
            {
                lblErrorMsg.Text = "Please enter a UserType Name!!";
                return;
            }
            
            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("UserTypeCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@usertypeid",(hfUserTypeID.Value == "" ? 0 : Convert.ToInt32(hfUserTypeID.Value)));
            sqlCmd.Parameters.AddWithValue("@Name", tb_userTypeName.Text.Trim());

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrTypeId = hfUserTypeID.Value;

            //Method Clear called to reset all the values 
            Clear();

            if (usrTypeId == "")
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


        //A Method to Display the values from the database to the GridView
        void displayData()
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("UserTypeViewAll", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_UserType.DataSource = dt;
            grid_UserType.DataBind();


        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            int userTypeID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("UserTypeViewById", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@usertypeid", userTypeID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID
            hfUserTypeID.Value = userTypeID.ToString();

            //Set the values stored in the DataTables to the Textboxes
            tb_userTypeName.Text = dt.Rows[0]["Name"].ToString();

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;


        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
            SqlCommand cmd = new SqlCommand("UserTypeDeleteById", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@usertypeid", Convert.ToInt32(hfUserTypeID.Value));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            displayData();

            lblSuccessMsg.Text = "User Type Deleted Successfully!!";

        }

        
    }
}