<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="APICallerTemplate.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your NetroMedia username and password.
    </p>
    <p>
        <label ID="UserNameLabel">Username:</label>
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
    </p>
    <p>
        <label ID="PasswordLabel">Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
    </p>

    <asp:Button ID="LoginButton" runat="server" OnCommand="Page_Command" CommandName="Login" Text="Log In"/>

    <p runat="server" id="loginfeedback"></p>
</asp:Content>
