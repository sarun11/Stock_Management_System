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
    public partial class Items : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection("Data Source=DESKTOP-CBUUD35;Initial Catalog=MEDIC_DB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_Delete.Enabled = false;

                //A method called to Fill Data from GridView
                displayDataGrid();

                //Populating the DropDown List
                list_user.Items.FindByValue("-1").Selected = true;
                list_category.Items.FindByValue("-1").Selected = true;
                list_supplier.Items.FindByValue("-1").Selected = true;

                FillDropDownListUser();
                FillDropDownListCategory();
                FillDropDownListSupplier();

            }
            else
            {
                lblSuccessMsg.Text = "";
                lblErrorMsg.Text = "";
            }
        }

        //A method to Clear Values
        protected void Clear()
        {
            hfItemID.Value = "";
            tb_itemCode.Text = "";
            tb_itemName.Text = "";
            tb_description.Text = "";
            tb_price.Text = "";
            tb_purchaseDate.Text = "";
            lblSuccessMsg.Text = "";
            lblErrorMsg.Text = "";
            btn_Delete.Enabled = false;
            tb_itemName.Focus();
            btn_Save.Text = "Save";


            list_user.ClearSelection();
            list_user.Items.FindByValue("-1").Selected = true;

            list_supplier.ClearSelection();
            list_supplier.Items.FindByValue("-1").Selected = true;

            list_category.ClearSelection();
            list_category.Items.FindByValue("-1").Selected = true;
        }



        //A method defined to populate the GridView
        protected DataTable displayData( string stored_procedure)
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
            grid_User.DataSource = displayData("ItemViewAll");
            grid_User.DataBind();
        }
        

        //A method to fill the values to the DropDown List
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

        protected void FillDropDownListCategory()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created
            list_category.DataTextField = "categoryname";
            list_category.DataValueField = "categoryid";

            list_category.DataSource = FillDropDownList("categoryViewAll");
            list_category.DataBind();
        }

        protected void FillDropDownListSupplier()
        {
            //Selecting the datasource of the dropdown list to be the datalist so created
            list_supplier.DataTextField = "suppliername";
            list_supplier.DataValueField = "supplierid";

            list_supplier.DataSource = FillDropDownList("SupplierViewAll");
            list_supplier.DataBind();
        }


        protected void btn_Save_Click(object sender, EventArgs e)
        {

            //Validation to determine whether the user has entered an empty value/ Valid Values or not
            if (String.IsNullOrEmpty(tb_itemCode.Text))
            {
                lblErrorMsg.Text = "Please enter an Item Code!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_itemName.Text))
            {
                lblErrorMsg.Text = "Please enter an Item Name!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_description.Text))
            {
                lblErrorMsg.Text = "Please enter the Item's Description!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_price.Text))
            {
                lblErrorMsg.Text = "Item's Price Field Cannot be Empty!!";
                return;
            }

            if (String.IsNullOrEmpty(tb_purchaseDate.Text))
            {
                lblErrorMsg.Text = "Item's Purchase Date Field Cannot be Empty!!";
                return;
            }

            string selected_user = list_user.SelectedValue.ToString();
            if (selected_user == "-1")
            {

                lblErrorMsg.Text = "Please Select a Handling User";
                list_user.Focus();
                return;
            }

            string selected_supplier = list_supplier.SelectedValue.ToString();
            if (selected_supplier == "-1")
            {

                lblErrorMsg.Text = "Please Select a Supplier's Name";
                list_supplier.Focus();
                return;
            }

            string selected_category = list_category.SelectedValue.ToString();
            if (selected_category == "-1")
            {

                lblErrorMsg.Text = "Please Select an Item's Category";
                list_category.Focus();
                return;
            }


            //If Connection is Closed, opening the Sql Connection
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();

            //Creating a SQL command and passing the name of the Stored Procedure as well as the Connection string in parameter
            SqlCommand sqlCmd = new SqlCommand("ItemCreateOrUpdate", sqlConn);

            //Declaring that the sql commandtype is a stored procedure
            sqlCmd.CommandType = CommandType.StoredProcedure;

            // Passing the respective values as parameters to the Stored Procedure
            sqlCmd.Parameters.AddWithValue("@itemid", (hfItemID.Value == "" ? 0 : Convert.ToInt32(hfItemID.Value)));
            sqlCmd.Parameters.AddWithValue("@itemcode", tb_itemCode.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@itemname", tb_itemName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@itemdescription", tb_description.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@price", tb_price.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@purchasedate", tb_purchaseDate.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@userid", list_user.SelectedValue);
            sqlCmd.Parameters.AddWithValue("@supplierid", list_supplier.SelectedValue);
            sqlCmd.Parameters.AddWithValue("@categoryid", list_category.SelectedValue);

            //Executing the SQL Query in the Database
            sqlCmd.ExecuteNonQuery();

            //Closing the Connection 
            sqlConn.Close();


            //Since the Clear() function changes the value of hiddenfield to Empty, we need to store the value of hiddenField to a new Variable in order to show the correct message to the users
            string usrId = hfItemID.Value;

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
            displayDataGrid();

        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if the Connection is Open or not. If closed, open the Connection
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }

                //Create an Object of SQL command and pass the stored Procedure and Connection String as parameters
                SqlCommand cmd = new SqlCommand("ItemDeleteById", sqlConn);

                //Declare the Sql Command to be a Stored Procedure
                cmd.CommandType = CommandType.StoredProcedure;

                //Sending the Value of userTypeID as parameter to the Stored Procedure
                cmd.Parameters.AddWithValue("@itemid", Convert.ToInt32(hfItemID.Value));

                //Execute the Query in the database
                cmd.ExecuteNonQuery();

                //Close the Connection
                sqlConn.Close();

                //Calling a method to clear all the values in the Front End
                Clear();

                //Calling a method to display data into the gridview
                displayDataGrid();

                lblSuccessMsg.Text = "Item Deleted Successfully!!";
               

            }
            catch (Exception ex)
            {
                //Calling a method to clear all the values in the Front End
                Clear();

                //Calling a method to display data into the gridview
                displayDataGrid();

                lblErrorMsg.Text = "Item Existing in Stock Cannot be Deleted!!"; 
            }

        }

        protected void btn_CLear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void link_Select_Click(object sender, EventArgs e)
        {
            //Store the Id sent by the Link Button into a new Variable
            int itemID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //Check if the Connection is Open or not. If closed, open the Connection
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

            //Creating a Sql Data Adapter in order to Connect to the stored procedure so created by us in the Sql Server using the Connection String
            SqlDataAdapter sqlDA = new SqlDataAdapter("ItemViewById", sqlConn);

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

            //Storing the ID into the HiddenField ID and a new String variable
            hfItemID.Value = itemID.ToString();
            string dropDownlist_user_text = dt.Rows[0]["user_name"].ToString();
            string dropDownlist_category_text = dt.Rows[0]["category_name"].ToString();
            string dropDownlist_supplier_text = dt.Rows[0]["supplier_name"].ToString();

            //Set the values stored in the DataTables to the Textboxes
            tb_itemCode.Text = dt.Rows[0]["itemcode"].ToString();
            tb_itemName.Text = dt.Rows[0]["itemname"].ToString();
            tb_description.Text = dt.Rows[0]["itemdescription"].ToString();
            tb_price.Text = dt.Rows[0]["price"].ToString();
            tb_purchaseDate.Text = dt.Rows[0]["purchasedate"].ToString();

            tb_itemName.Focus();

            list_user.ClearSelection();
            list_user.Items.FindByText(dropDownlist_user_text).Selected = true;

            list_category.ClearSelection();
            list_category.Items.FindByText(dropDownlist_category_text).Selected = true;

            list_supplier.ClearSelection();
            list_supplier.Items.FindByText(dropDownlist_supplier_text).Selected = true;

            //Change the Text of Save Button to Update
            btn_Save.Text = "Update";

            //Enable the Delete Button
            btn_Delete.Enabled = true;
        }
    }
}