<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Error
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div align="center"><br /><br /><h2><%="" + Session["errorMsg"] %></h2></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="styleSheetContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
