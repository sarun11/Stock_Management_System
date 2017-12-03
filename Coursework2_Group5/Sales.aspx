<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sales.aspx.cs" Inherits="Coursework2_Group5.Sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sales </h1>

    <div>
        <asp:HiddenField ID="hfSaleID" runat="server" />
        <asp:HiddenField ID="hfItemID" runat="server" />
        <table>

            <tr>
                <td>
                    <asp:Label ID="lbl_billNo" runat="server" Text="Billing Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_billingNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="tb_billingNo" 
                        runat="server" ErrorMessage="Billing Number is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lbl_customerName" runat="server" Text="Customer Name: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_customer" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text=" ---- Select a customer ----" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="list_customer"
                        InitialValue="-1" ErrorMessage="Please select a customer!!" ForeColor="Red">
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
                    <asp:Label ID="lbl_date" runat="server" Text="Date: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_billingDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tb_billingDate" 
                        runat="server" ErrorMessage="Billing Date is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td></td>
                <td></td>
            </tr>


            <tr>
                <td>
                    <asp:Label ID="lbl_itemCode" runat="server" Text="Item Code: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_itemCode" runat="server" OnTextChanged="tb_itemCode_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:Label ID="lbl_itemName" runat="server"></asp:Label>


                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_itemCode"
                        InitialValue="-1" ErrorMessage="Please enter an item code!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    
                </td>
            </tr>



            <tr>
                <td>
                    <asp:Label ID="lbl_quantity" runat="server" Text="Quantity: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_quantity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="tb_quantity" 
                        runat="server" ErrorMessage="Quantity is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
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

         <asp:GridView ID="grid_Sales" runat="server" AutoGenerateColumns="false">
            <Columns>
                 <asp:BoundField DataField="billingdate" HeaderText="Billing Date" DataFormatString="{0:yyyy/MM/dd}"/>
                <asp:BoundField DataField="billingnumber" HeaderText="Billing Number" />
                <asp:BoundField DataField="membername" HeaderText="Customer Name" />
                <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="username" HeaderText="User Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Delete" runat="server" CommandArgument='<%# Eval("salesid") + "," + Eval("itemid") %>' OnClick="link_Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete This Customer?');">Delete</asp:LinkButton>
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
