<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserType.aspx.cs" Inherits="Coursework2_Group5.UserType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>User Type</h1>

    <div>
        <asp:HiddenField ID="hfUserTypeID" runat="server" />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbl_userTypeName" runat="server" Text="Label">User Type: </asp:Label>
                </td>

                <td colspan="2">
                    <asp:TextBox ID="tb_userTypeName" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                </td>

                <td colspan="2">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" />
                    <asp:Button ID="btn_Delete" runat="server" Text="Delete" OnClick="btn_Delete_Click" />
                    <asp:Button ID="btn_CLear" runat="server" Text="Clear" OnClick="btn_CLear_Click" />
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

        <asp:GridView ID="grid_UserType" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="link_Select" runat="server" CommandArgument='<%# Eval("usertypeid") %>' OnClick="link_Select_Click">Select</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            
    </div>
</asp:Content>
