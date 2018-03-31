<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Coursework2_Group5.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Customer Details</h1>

       <div>
        <asp:HiddenField ID="hfCustomerID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Lbl_CustomerName" runat="server" Text="Customer Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_CustomerName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_userName" ControlToValidate="tb_CustomerName"
                        runat="server" ErrorMessage="Customer Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_Address" runat="server" Text="Address: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_address" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_address" ControlToValidate="tb_address" 
                        runat="server" ErrorMessage="Address Field is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_ContactNo" runat="server" Text="Contact Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_contactNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tb_contactNo" 
                        runat="server" ErrorMessage="Customer's Contact Number is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_email" runat="server" Text="Email: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="tb_email" 
                        runat="server" ErrorMessage="Customer's Email is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_user" runat="server" Text="Handling User: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_user" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text=" ---- Select a User ----" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="list_validator" runat="server" ControlToValidate="list_user"
                        InitialValue="-1" ErrorMessage="A Handling User has to be Defined!!" ForeColor="Red">
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
                <asp:BoundField DataField="membername" HeaderText="Customer Name" />
                <asp:BoundField DataField="address" HeaderText="Full Address" />
                <asp:BoundField DataField="contactnumber" HeaderText="Contact No." />
                <asp:BoundField DataField="email" HeaderText="Email Address" />
                <asp:BoundField DataField="UserName" HeaderText="Handling User" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("memberid") %>' OnClick="link_Select_Click" CausesValidation="false">Select</asp:LinkButton>
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
