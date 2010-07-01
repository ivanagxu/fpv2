<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Fingerprint
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID=styleSheetContent runat="server">
    <link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-coffee.css") %>/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">
    
    
    <script type="text/javascript">
        Ext.onReady(function() {
            Ext.state.Manager.setProvider(new Ext.state.CookieProvider());
            
                var memberStore = new Ext.data.ArrayStore({
                fields: [
                       { name: 'ID', type: 'string' },
		               { name: 'chinese_name', type: 'string' },
		               { name: 'eng_name', type: 'string' },
                       { name: 'login_name', type: 'string' },
                       { name: 'dept', type: 'string' },
		               { name: 'role', type: 'string' }
                    ]
                });

                // create the Grid
                var sm = new Ext.grid.CheckboxSelectionModel({singleSelect: true});
                var memberGrid = new Ext.grid.GridPanel({
                    border:false,
                    id: 'admin-membergrid',
                    store: memberStore,
                    columns: [sm,
                        { id: 'ID', header: 'ID' , sortable: true, dataIndex: 'ID' },
                        { header: 'Chinese Name', sortable: true, dataIndex: 'chinese_name' },
                        { header: 'English Nmae.', sortable: true, dataIndex: 'eng_name' },
			            { header: 'Login Name', sortable: true, dataIndex: 'login_name' },
                        { header: 'Department', sortable: true, dataIndex: 'dept' },
                        { header: 'Role', sortable: true, dataIndex: 'role' }
                    ],
                    sm:sm
                });


                //Create view
                var MainView = new Ext.Viewport({
                    layout: 'anchor',
                    items: [
                    {
                        contentEl: 'topdiv',
                        border: false
                    },
                    new Ext.TabPanel({
					    region: 'center', // a center region is ALWAYS required for border layout
					    deferredRender: false,
					    activeTab: 0,     // first tab initially active
					    items: [{
						    title: 'Member',
						    closable: false,
						    autoScroll: true,
						    plain:true,
						    layout: 'fit',
						    items: [
						        memberGrid
						    ]
					    },{
						    title: 'Customer',
						    closable: false,
						    autoScroll: true,
						    plain:true,
						    layout: 'vbox',
						    items: [
						        
						    ]
					    }]
				    })]
                });
            })
	</script>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
    
</asp:Content>
