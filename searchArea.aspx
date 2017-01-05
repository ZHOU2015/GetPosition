<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchArea.aspx.cs" Inherits="searchArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>省
    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>市
    </form>
</body>
<script src="http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js" charset="GB2312" ></script>
<script type="text/javascript" charset="GB2312">
    var textBox1 = document.getElementById('TextBox1');
    var textBox2 = document.getElementById('TextBox2');
    textBox1.value = remote_ip_info.province;
    textBox2.value = remote_ip_info.city;
</script>
</html>
