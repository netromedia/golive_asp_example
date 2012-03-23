<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewChannelInfo.aspx.cs" Inherits="APICallerTemplate.ViewChannelInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        View Channel Information
    </h2>

    <asp:Panel runat="server" ID="pnlNotLoggedIn">
        <p>You are not logged in.  Please log in to view information about a channel.</p>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlDefault">

        <asp:DataList ID="dlMyChannelInfo" runat="server" ShowHeader="True" BorderWidth="0px" Width="100%" BorderStyle="None" AutoGenerateColumns="False">
            <HeaderTemplate>
                <h3>Channel Properties</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <%# ((System.Data.DataRowView)Container.DataItem)["ChannelPropertyName"] %>: <%# ((System.Data.DataRowView)Container.DataItem)["ChannelPropertyValue"]%>
            </ItemTemplate>
        </asp:DataList>

        <asp:DataList ID="dlMyChannelStats" runat="server" ShowHeader="True" BorderWidth="0px" Width="100%" BorderStyle="None" AutoGenerateColumns="False">
            <HeaderTemplate>
                <h3>Current Statistics</h3>
            </HeaderTemplate>
            <ItemTemplate>
                <%# ((System.Data.DataRowView)Container.DataItem)["ChannelStatPropertyName"]%>: <%# ((System.Data.DataRowView)Container.DataItem)["ChannelStatPropertyValue"]%>
            </ItemTemplate>
        </asp:DataList>

        <p runat="server" id="pError"></p>

    </asp:Panel>
</asp:Content>
