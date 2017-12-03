<%@ Page Title="Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="Coursework2_Group5.Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Items List</h1>


      <div>
        <asp:HiddenField ID="hfItemID" runat="server" />
        <table>
             <tr>
                <td>
                    <asp:Label ID="Lbl_itemCode" runat="server" Text="Item Code: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_itemCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="itemCode_Validator" ControlToValidate="tb_itemName"
                        runat="server" ErrorMessage="Item Code is Required is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Lbl_itemName" runat="server" Text="Item Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_itemName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_itemName" ControlToValidate="tb_itemName"
                        runat="server" ErrorMessage="Item Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_description" runat="server" Text="Description: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_description" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_description" ControlToValidate="tb_description" 
                        runat="server" ErrorMessage="Item Description is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_price" runat="server" Text="Unit Price: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_price" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tb_price" 
                        runat="server" ErrorMessage="Item's price is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_purchaseDate" runat="server" Text="PurchaseDate: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_purchaseDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="tb_purchaseDate" 
                        runat="server" ErrorMessage="Item's Purchase Date is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_supplier" runat="server" Text="Supplier Name: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_supplier" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text=" ---- Select a Supplier ----" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="list_validator1" runat="server" ControlToValidate="list_supplier"
                        InitialValue="-1" ErrorMessage="A Supplier must be provided!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="Lbl_user" runat="server" Text="Handling User: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_user" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text=" ---- Select a User ----" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="list_user"
                        InitialValue="-1" ErrorMessage="A Handling User has to be Defined!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Lbl_category" runat="server" Text="Item Category: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_category" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text=" ---- Select a Category ----" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="list_category"
                        InitialValue="-1" ErrorMessage="The item's Category must be provided" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>


            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete This Customer?');"/>
                    <asp:Button ID="btn_CLear" runat="server" Text="Clear" OnClick="btn_CLear_Click" CausesValidation="false" />
                </td>
            </tr>

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Label ID="lblSuccessMsg" runat="server" Text="" ForeColor="Green"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                </td>
            </tr>

        </table>
        <br />

         <asp:GridView ID="grid_User" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="itemcode" HeaderText="Item Code" />
                <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                <asp:BoundField DataField="itemdescription" HeaderText="Item Description" />
                <asp:BoundField DataField="price" HeaderText="Unit Price" />
                <asp:BoundField DataField="purchasedate" HeaderText="Purchase Date" DataFormatString="{0:yyyy/MM/dd}"/>
                <asp:BoundField DataField="supplier_name" HeaderText="Supplier Name" />
                <asp:BoundField DataField="category_name" HeaderText="Category Name" />
                <asp:BoundField DataField="quantity" HeaderText="In Stock Quantity" />
                 <asp:BoundField DataField="user_name" HeaderText="User Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("itemid") %>' OnClick="link_Select_Click" CausesValidation="false">Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
     
    </div>

    <script type="text/javascript">
        function HideLabel() {
            document.getElementById('<%= lblErrorMsg.ClientID %>').style.display = "none";
            document.getElementById('<%= lblSuccessMsg.ClientID %>').style.display = "none";
        }
        setTimeout("HideLabel();", 4000);
    </script>
</asp:Content>
