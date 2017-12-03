<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockDetails.aspx.cs" Inherits="Coursework2_Group5.StockDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Stock Details</h1>
    <div>
        <asp:HiddenField ID="hfSupplierID" runat="server" />
        <asp:HiddenField ID="hfItemID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_supplier" runat="server" Text="Supplier"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_supplier" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true">
                        <asp:ListItem Enabled="true" Text="---Select Supplier---" Value="-1" />
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_item" runat="server" Text="Item"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="list_item" runat="server" Height="36px" Width="180px" AppenddataBoundItems="true" OnSelectedIndexChanged="list_item_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Enabled="true" Text="---Select Item---" Value="-1" />
                    </asp:DropDownList>
                    
                    <asp:Label ID="lbl_itemCode" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_location" runat="server" Text="Location: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_location" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="list_validator" runat="server" ControlToValidate="tb_location"
                        InitialValue="-1" ErrorMessage="Location is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_quantity" runat="server" Text="Quantity: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_quantity" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_quantity"
                        InitialValue="-1" ErrorMessage="Quantity is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_manufactureDate" runat="server" Text="Manufacture Date: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_manufactureDate" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_manufactureDate"
                        InitialValue="-1" ErrorMessage="Manufacture Date is Required!!" ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lbl_expiryDate" runat="server" Text="Expiry Date: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tb_expiryDate" runat="server"></asp:TextBox>

                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_expiryDate"
                        InitialValue="-1" ErrorMessage="Expiry Date is Required!!" ForeColor="Red">
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
                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                <asp:BoundField DataField="location" HeaderText="Location" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="purchasedate" HeaderText="Purchase Date" DataFormatString="{0:yyyy/MM/dd}"/>
                <asp:BoundField DataField="manufacturedate" HeaderText="Manufacture Date" DataFormatString="{0:yyyy/MM/dd}"/>
                <asp:BoundField DataField="expirydate" HeaderText="Expiry Date" DataFormatString="{0:yyyy/MM/dd}"/>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("supplierid") + "," + Eval("itemid") %>' OnClick="link_Select_Click" CausesValidation="false">Select</asp:LinkButton>
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
