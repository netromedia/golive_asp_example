<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="MyChannels.aspx.cs" Inherits="APICallerTemplate.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        My Channels
    </h2>

    <asp:Panel runat="server" ID="pnlNotLoggedIn">
        <p>You are not logged in.  Please log in to see your channels.</p>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlDefault">
        <asp:LinkButton runat="server" ID="lbCreateChannel" Text="Create New Channel" PostBackUrl="~/CreateChannel.aspx"></asp:LinkButton>

        <asp:DataGrid ID="dgMyChannels" runat="server" ShowHeader="False" BorderWidth="0px" Width="100%" BorderStyle="None" AutoGenerateColumns="False">
            <SelectedItemStyle BackColor="#FFFFC0"></SelectedItemStyle>
            <Columns>
                <asp:BoundColumn HeaderText="Channel Id" DataField="ChannelId" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Channel Name" DataField="ChannelName" ></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Channel Type" DataField="ChannelType"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblEditChannel" CommandName="View" CommandArgument='<%# "~/EditChannel.aspx?ChannelId=" + DataBinder.Eval(Container.DataItem, "ChannelId") %>'
                                OnCommand="Page_Command" Text='Edit' runat="server" />
                        <asp:LinkButton ID="lbViewChannel" CommandName="View" CommandArgument='<%# "~/ViewChannelInfo.aspx?ChannelId=" + DataBinder.Eval(Container.DataItem, "ChannelId") %>'
                                OnCommand="Page_Command" Text='View' runat="server" />
                        <span onclick="return confirm('Delete this channel?')">
                            <asp:LinkButton ID="lbDeleteChannel" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ChannelId") %>'
                                OnCommand="Page_Command" Text='Delete' runat="server" />
                        </span>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        
        <p runat="server" id="pError"></p>

    </asp:Panel>
</asp:Content>
