<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierDetails.aspx.cs" Inherits="Coursework2_Group5.SupplierDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>supplier Details</h1>
    <div>
        <asp:HiddenField ID="hfSupplierID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Lbl_supplierName" runat="server" Text="Supplier Name: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_supplierName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_supplierName" ControlToValidate="tb_supplierName"
                        runat="server" ErrorMessage="Supplier Name is required!" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_supplierAddress" runat="server" Text="Supplier Address: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_supplierAddress" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_fullName" ControlToValidate="tb_supplierAddress" 
                        runat="server" ErrorMessage="Supplier address is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_contactNumber" runat="server" Text="Contact Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_contactNumber" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="list_validator" runat="server" ControlToValidate="tb_contactNumber"
                        InitialValue="-1" ErrorMessage="Contact number is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_email" runat="server" Text="Email: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_email"
                        InitialValue="-1" ErrorMessage="Email is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_url" runat="server" Text="URL: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_url" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_url"
                        InitialValue="-1" ErrorMessage="URL is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_contactPerson" runat="server" Text="Contact Person: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_contactPerson" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_contactPerson"
                        InitialValue="-1" ErrorMessage="supplier Type is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_user" runat="server" Text="User: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_user" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text="---Select User---" Value="-1" />
                    </asp:DropDownList>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="list_user"
                        InitialValue="-1" ErrorMessage="User is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete This supplier?');"/>
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

         <asp:GridView ID="grid_supplier" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="suppliername" HeaderText="Supplier Name" />
                <asp:BoundField DataField="supplieraddress" HeaderText="Supplier Address" />
                <asp:BoundField DataField="contactnumber" HeaderText="Contact Number" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:BoundField DataField="url" HeaderText="URL" />
                <asp:BoundField DataField="contactperson" HeaderText="Contact Person" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("supplierid") %>' OnClick="link_Select_Click" CausesValidation="false">Select</asp:LinkButton>
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
