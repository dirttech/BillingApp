<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Loggin.aspx.cs" Inherits="LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" type="text/css" href="Scripts/LoginStyle.css" />
    <link rel="shortcut icon" href="images/dashboard_icon.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="LoginContainer">
  <div class="login" >
    <h1>Login</h1>

    <p><input type="text" name="login" value="" placeholder="Username   " runat="server" id="usrName" /></p>
      <p><input type="password" name="password" value="" placeholder="Password" runat="server" id="pwd" /></p>
     
      <p class="submit">
          <asp:Button ID="loginUser" runat="server" Text="Login" 
              onclick="loginUser_Click" /></p>
              <p><asp:Label ID="msg" Text="" runat="server" style="color:#c4376b; font-weight:bold;"></asp:Label></p>
   
  </div>
 
</div>



   





</asp:Content>

