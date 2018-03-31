using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

namespace Coursework2_Group5
{
    public partial class UserDetails : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Delete.Enabled = false;
                displayData();

                //Populating the DropDown List
                list_userType.Items.FindByValue("-1").Selected = true;
                FillDropDownList();
                
            }
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
            SqlDataAdapter sqlDA = new SqlDataAdapter("UserTypeViewAll", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataSet ds = new DataSet();
            sqlDA.Fill(ds);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Selecting the datasource of the dropdown list to be the datalist so created
            
            list_userType.DataTextField = "name";
            list_userType.DataValueField = "usertypeid";

            list_userType.DataSource = ds;
            list_userType.DataBind();

        }


        //A method to Clear Values
        public void Clear()
        {
            hfUserID.Value = "";
            tb_userName.Text = "";
            tb_fullName.Text = "";
            tb_password.Text = "";
            tb_rePassword.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            btn_Delete.Enabled = false;
            tb_userName.Focus();
            btn_Save.Text = "Save";
            list_userType.ClearSelection();
            list_userType.Items.FindByValue("-1").Selected = true;
        }

        public void displayData()
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

            //Creating a new DataSet and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();


            //Select such dataTable as the datasource for the GridView and bind the Gridview 
            grid_User.DataSource = dt;
            grid_User.DataBind();
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {
            //Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_userName.Text))
            {
                lblErrorMsg.Text = "Please enter a User Name!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_fullName.Text))
            {
                lblErrorMsg.Text = "Please enter the User's Full Name!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_password.Text))
            {
                lblErrorMsg.Text = "Password Field Cannot be Empty!!";
                return;
            }

            if (tb_password.Text.Trim() != tb_rePassword.Text.Trim())
            {

                lblErrorMsg.Text = "Password and Repassword Mismatch";
                tb_password.Focus();
                return;
            }

            string selected = list_userType.SelectedValue.ToString();
            if (selected == "-1")
            {

                lblErrorMsg.Text = "Please Enter a User Type";
                list_userType.Focus();
                return;
            }

            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("UserCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@userid", (hfUserID.Value == "" ? 0 : Convert.ToInt32(hfUserID.Value)));
            sqlCmd.Parameters.AddWithValue("@username", tb_userName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@fullname", tb_fullName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@createdon", System.DateTime.Now);
            sqlCmd.Parameters.AddWithValue("@passwordashash", MD5Hash(tb_password.Text.Trim()));
            sqlCmd.Parameters.AddWithValue("@usertypeid", list_userType.SelectedValue);

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrId = hfUserID.Value;

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

        //Convert password to HaSH
        protected static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
            SqlCommand cmd = new SqlCommand("UserDeleteById", sqlConn);

            //Declare the Sql Command to be a Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;

            //Sending the Value of userTypeID as parameter to the Stored Procedure
            cmd.Parameters.AddWithValue("@userid", Convert.ToInt32(hfUserID.Value));

            //Execute the Query in the database
            cmd.ExecuteNonQuery();

            //Close the Connection
            sqlConn.Close();

            //Calling a method to clear all the values in the Front End
            Clear();

            //Calling a method to display data into the gridview
            displayData();

            lblSuccessMsg.Text = "User Deleted Successfully!!";
        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            int userID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("userViewById", sqlConn);

            //Define that the Sql Command is a stored Procedure
            sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;

            //Pass the userType ID as parameter to the Stored procedure
            sqlDA.SelectCommand.Parameters.AddWithValue("@userid", userID);

            //Creating a new DataTable and Linking such DataTable to the Stored procedure SQL command
            DataTable dt = new DataTable();
            sqlDA.Fill(dt);

            //Finally Close the Sql connection
            sqlConn.Close();

            //Now, Showing these Values into the Edit Form

            //Storing the ID into the HiddenField ID and a new String 
            hfUserID.Value = userID.ToString();
            string dropDownlist_text = dt.Rows[0]["UserType"].ToString();

            //Set the values stored in the DataTables to the Textboxes
            tb_userName.Text = dt.Rows[0]["username"].ToString();
            tb_fullName.Text = dt.Rows[0]["fullname"].ToString();
            tb_userName.Focus();

            list_userType.ClearSelection();
            list_userType.Items.FindByText(dropDownlist_text).Selected = true;

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;
        }
    }
}