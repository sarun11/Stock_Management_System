<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDetails.aspx.cs" Inherits="Coursework2_Group5.UserDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>User Details</h1>
    <div>
        <asp:HiddenField ID="hfUserID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Lbl_userName" runat="server" Text="User Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_userName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_userName" ControlToValidate="tb_userName"
                        runat="server" ErrorMessage="User Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_fullName" runat="server" Text="Full Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_fullName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_fullName" ControlToValidate="tb_fullName" 
                        runat="server" ErrorMessage="User's Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_password" runat="server" Text="Password: "></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="tb_password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" ID="regex_password" ControlToValidate="tb_password"
                        ErrorMessage="Password should be of length atleast 8 with one capital letter and one digit "
                         ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$" ForeColor="Red"></asp:RegularExpressionValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_rePassword" runat="server" Text="ReType Password: " ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_rePassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_rePassword" ControlToValidate="tb_rePassword"
                        runat="server" ErrorMessage="Re-Enter Password field is required!!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_userType" runat="server" Text="User Type: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_userType" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text="Select User Type" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="list_validator" runat="server" ControlToValidate="list_userType"
                        InitialValue="-1" ErrorMessage="User Type is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete This User?');"/>
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
                <asp:BoundField DataField="username" HeaderText="User Name" />
                <asp:BoundField DataField="fullname" HeaderText="Full Name" />
                <asp:BoundField DataField="createdon" HeaderText="Created On" DataFormatString="{0:yyyy/MM/dd}" />
                <asp:BoundField DataField="UserType" HeaderText="User Type" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("userid") %>' OnClick="link_Select_Click" CausesValidation="false">Select</asp:LinkButton>
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



