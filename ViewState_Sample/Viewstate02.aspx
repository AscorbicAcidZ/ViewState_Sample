<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewstate02.aspx.cs" Inherits="ViewState_Sample.Viewstate02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <<form id="form1" runat="server">
    <div>
        Enter Name
        <asp:TextBox ID="txtName" runat="server" />
        <br />
        Enter City
        <asp:TextBox ID="txtCity" runat="server" />
        <br />
        <asp:Button ID="btnAdd" Text="Add" OnClick="Add" runat="server" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="City" HeaderText="City" />
            </Columns>
        </asp:GridView>
        <asp:Button runat="server" OnClick="Save" Text="Save" />
    </div>
    </form>
</body>
</html>
