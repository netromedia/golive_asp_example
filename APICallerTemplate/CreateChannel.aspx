<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateChannel.aspx.cs" Inherits="APICallerTemplate.CreateChannel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Create New Channel
    </h2>

    <asp:Panel runat="server" ID="pnlNotLoggedIn">
        <p>You are not logged in.  Please log in to create a new channel.</p>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlDefault">

        <p>
            Please enter the details for the channel you want to create.
        </p>
        <p>
            <label>Channel Name:</label>
            <asp:TextBox ID="txtChannelName" runat="server"></asp:TextBox>
        </p>
        <p>
            <label>Channel Format:</label>
            <asp:DropDownList runat="server" ID="ddlChannelFormat">
                <asp:ListItem Text="Flash Live" value="FLASH_LIVE" ></asp:ListItem>
                <asp:ListItem Text="Flash Live Mobile" value="FLASH_LIVE_MOBILE" ></asp:ListItem>
                <asp:ListItem Text="Flash On-Demand" value="FLASH_OD" ></asp:ListItem>
                <asp:ListItem Text="Windows Media Live Pull" value="WMS_LIVE_PULL" ></asp:ListItem>
                <asp:ListItem Text="Windows Media Live Push" value="WMS_LIVE_PUSH" ></asp:ListItem>
                <asp:ListItem Text="Windows Media On-Demand" value="WMS_OD" ></asp:ListItem>
                <asp:ListItem Text="MPEGTS Stream" value="MPEGTS_STREAM" ></asp:ListItem>
                <asp:ListItem Text="MPEGTS Stream Mobile" value="MPEGTS_STREAM_MOBILE" ></asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            <label>Channel Source:</label>
            <asp:TextBox ID="txtChannelSource" runat="server"></asp:TextBox>
        </p>
        <p>
            <label>Maximum Connections:</label>
            <asp:TextBox runat="server" ID="txtMaxConnections"></asp:TextBox>
         </p>

         <p>
            <label>Maximum Bitrate:</label>
            <asp:TextBox runat="server" ID="txtMaxBitrate"></asp:TextBox>
         </p>

        <asp:Button runat="server" ID="btnCreateChannel" CommandName="CreateChannel" CommandArgument="" OnCommand="Page_Command" Text="Create New Channel" />
        
        <p runat="server" id="pError"></p>
    </asp:Panel>
</asp:Content>
