<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="csharpEmailClass.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email Class example</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            This example will send an email to the address entered in the textbox with a small text file attachment. <br /> 
            The example makes use of an email class that accepts a list of email addresses and a list of attachments.<br />
            Each recipient will receive all the attachments in the list.<br />
            Any questions, drop me a message at <a href="http://codetux.com">codeTux.com</a>.
        </p>
        Recipient email address: <asp:TextBox ID="tbEmailAddress" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Send email" OnClick="Button1_Click" />
    </div>
        <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>