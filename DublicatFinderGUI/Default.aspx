<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DublicatFinderGUI.Default" %>
<%@ Register TagPrefix="asp" Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Default asp page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="p1enal" runat="server"></asp:Panel>
    <div>
    
    </div>
    <asp:Button ID="btnNextDuplicate" Text="Next dublicate" runat="server" onclick="BtnNextDuplicate_Click"/>
    <asp:Button ID="btnMerge" runat="server" onclick="BtnMerge_Click" Text="Merge" />
    <asp:Button ID="btnOtherTables" runat="server" onclick="btnOtherTables_Click" Text="Show other tables" Enabled ="false"/>
    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" Enabled = "false" />
    <asp:Label ID="lbl1" runat="server" />
    
    <asp:Panel runat="server" ID="panelChekboxes">
        <asp:CheckBox ID="CheckBox1" runat="server"  Visible="false" Text="1"/>
        <asp:CheckBox ID="CheckBox2" runat="server"  Visible="false" Text="2" />
        <asp:CheckBox ID="CheckBox3" runat="server"  Visible="false" Text="3" />
        <asp:CheckBox ID="CheckBox4" runat="server"  Visible="false" Text="4" />
        <asp:CheckBox ID="CheckBox5" runat="server"  Visible="false" Text="5" />
        <asp:CheckBox ID="CheckBox6" runat="server"  Visible="false" Text="6" />
        <asp:CheckBox ID="CheckBox7" runat="server"  Visible="false" Text="7" />
        <asp:CheckBox ID="CheckBox8" runat="server"  Visible="false" Text="8" />
        <asp:CheckBox ID="CheckBox9" runat="server"  Visible="false" Text="9" />
        <asp:CheckBox ID="CheckBox10" runat="server"  Visible="false" Text="10" />
     </asp:Panel>
     <asp:Panel runat="server" ID="panelFIODuplFinder">
        <asp:Label runat="server" Text="Last name: " ID="Label7" />
        <asp:TextBox runat="server" ID="tbLastName" width="120"/>
        <asp:Label ID="Label5" runat="server" Text="    First name: " />
        <asp:TextBox runat="server" ID="tbFirsName" width="120"/>
        <asp:Label ID="Label6" runat="server" Text="    Middle name: " />
        <asp:TextBox runat="server" ID="tbMiddleName" width="120"/>
        <asp:Button runat="server" ID="btnFindDuplByFIO" Text=" Find " 
             onclick="btnFindDuplByFIO_Click" />
     </asp:Panel>
    <asp:Panel runat="server" ScrollBars="Both" >
        <asp:GridView runat="server" ID="gridPersons" CellPadding="4"  
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>
 
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" >
    <asp:Label runat="server" ID="label1" Text="Person Contracts" />
        <asp:GridView runat="server" ID="grid1" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>

     <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" >
     <asp:Label runat="server" ID="label2"  />
        <asp:GridView runat="server" ID="grid2" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>

     <asp:Panel ID="Panel3" runat="server" ScrollBars="Both" >
     <asp:Label runat="server" ID="label3"  />
        <asp:GridView runat="server" ID="grid3" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>

    <asp:Panel ID="Panel4" runat="server" ScrollBars="Both" >
    <asp:Label runat="server" ID="label4"  />
        <asp:GridView runat="server" ID="grid4" CellPadding="4" 
            EnableModelValidation="True" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </asp:Panel>
  




    </form>
</body>
</html>
