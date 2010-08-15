<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	group
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

  <div id="fingerprint-group-body"></div>
    <div id="fingerprint-group-left" class="x-hide-display">
       
    </div>
    
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="admin-admin-link" href ="admin"  class="menubar"

      >admin</a></li>
     <li><a id="admin-group-link" href ="group"   class="menubar_click" 
      
      >group</a></li>
      <li><a id="admin-customer-link" href ="customer"  class="menubar" 
      
      >customer</a></li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="styleSheetContent" runat="server">

 
<link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-blue.css") %>/>
    <link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-red.css") %>/>
     <style type="text/css">
    
        .menubar {
         color: #222222;
         cursor: pointer;
         display: block;
         height: 20px;
         width: 100%;
         line-height: 12px;
         outline-color: -moz-use-text-color;
         outline-style: none;
         outline-width: 0;
         padding: 3px;
         text-align: center;
         text-decoration: none;
         white-space: nowrap;
          font-family: "Century"; font-size: x-small; 
        }

        .menubar:hover {
         background: #F0F0F0 repeat-x scroll left bottom;
        }
        
        .menubar_click {
         background: #EBEBEB;
         cursor: pointer;
         display: block;
         height: 20px;
         width: 100%;
         line-height: 12px;
         outline-color: -moz-use-text-color;
         outline-style: none;
         outline-width: 0;
         padding: 3px;
         text-align: center;
         text-decoration:underline;
         white-space: nowrap;
         font-weight:bold;
          font-family: "Century"; font-size: x-small; 
        }
        
        .menubar_click:hover {
         background: #F0F0F0 repeat-x scroll left bottom;
        }
        
        
        #newadmin-addadmin-panel .x-panel-body {
            background-color: #ffdfdf ! important;
        }
        
        #newadmin-addAdmin-panel .x-panel-body {
            background-color: #ffdfdf ! important;
        }
        #newadmin-form-panel .x-panel-body {
            background-color:#ffdfdf ! important;
        }
        
        #admin-centerPanel .x-panel-body {
            background-color: #ffdfdf ! important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">
 <script type="text/javascript">
     var firstLoad = true;
     Ext.onReady(function() {
         var leftPanel = new Ext.Panel({
             id: 'newadmin-left-panel',
             title: '',
             region: 'west',
             split: true,
             width: '10%',
             height: '100%',
             collapsible: false,
             margins: '3 0 3 3',
             cmargins: '3 3 3 3',
             layout: 'form',
             contentEl: 'leftmenu'
         });
         Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

         groupStore = new Ext.data.JsonStore({
             // store configs
             autoDestroy: true,
             url: "/" + APP_NAME + "/group.aspx/group",
             remoteSort: true,
             sortInfo: {
                 field: 'objectid',
                 direction: 'DESC'
             },
             storeId: 'groupStore',

             // reader configs
             idProperty: 'objectid',
             root: 'data',
             totalProperty: 'total',
             fields: [{
                 name: 'objectid',
                 type: 'string'
             }, {
                 name: 'name',
                 type: 'string'
             }, {
                 name: 'user_ids',
                 type: 'string'
             }, {
                 name: 'user_names',
                 type: 'String'

}]
             });

             var filters = new Ext.ux.grid.GridFilters({
                 encode: false,
                 local: true,
                 filters: [{
                     type: 'string',
                     dataIndex: 'objectid'
                 }, {
                     type: 'string',
                     dataIndex: 'name'
                 }, {
                     type: 'string',
                     dataIndex: 'user_ids'
                 }, {
                     type: 'string',
                     dataIndex: 'user_names'
}]
                 });

                 var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
                 var createColModel = function(finish, start) {
                     var columns = [sm,
                    {
                        dataIndex: 'objectid',
                        header: 'Group No.',
                        filterable: true,
                        filter: {
                            type: 'string'
                        },
                        renderer: adminidRenderer
                    }, {
                        dataIndex: 'name',
                        header: 'Group Name',
                        filterable: true
                    }, {
                        dataIndex: 'user_names',
                        header: 'Users',
                        filterable: true
                    }
				]; return new Ext.grid.ColumnModel({
				    columns: columns.slice(start || 0, finish),
				    defaults: {
				        sortable: true
				    }
				});
                 };
                 //group grid
                 var memberGrid = new Ext.grid.GridPanel({
                     id: 'group-membergrid',
                     badmin: false,
                     store: groupStore,
                     height: '100%',
                     colModel: createColModel(4),
                     selModel: sm,
                     loadMask: true,
                     plugins: [filters],
                     stripeRows: true,
                     flex: 4,
                     listeners: {
                         render: {
                             fn: function() {
                                 groupStore.load({
                                     params: {
                                         start: 0,
                                         limit: 10000
                                     }
                                 });
                             }
                         },
                         contextMenu: {
                             fn: function(e) {
                                 e.stopEvent();
                                 //filingMenu.showAt(e.xy);
                             }
                         },
                         rowdblclick: onClick
                     },
                     bbar: new Ext.PagingToolbar({
                         store: groupStore,
                         pageSize: 10000,
                         plugins: [filters],
                         displayInfo: true,
                         displayMsg: 'Displaying record {0} - {1} of {2}',
                         emptyMsg: "No record to display"
                     }),
                     tbar: [{
                         xtype: 'buttongroup',
                         hidden: true,
                         items: [{
                             text: 'New Group',
                             handler: newAdmin
}]
                         }, {
                             xtype: 'buttongroup',
                             items: [{
                                 text: 'Edit',
                                 handler: editAdmin
}]
                             }, {
                                 xtype: 'buttongroup',
                                 hidden: true,
                                 items: [{
                                     text: 'Delete',
                                     handler: deleteAdmin
}]
}]
                                 });

                                 var centerPanel = new Ext.Panel({
                                     id: 'group-center-panel',
                                     title: '',
                                     region: 'center',
                                     split: true,
                                     width: '90%',
                                     height: '100%',
                                     collapsible: false,
                                     margins: '3 0 3 3',
                                     cmargins: '3 3 3 3',
                                     defaults: { margins: '0 0 5 0' },
                                     layout: 'vbox',
                                     labelAlign: 'right',

                                     items: [
                    {
                        id: 'your-group-location',
                        xtype: 'box',
                        anchor: '100%',
                        html: "<a href='#' class='leftstyle1'>Group</a> → <a href='#' class='leftstyle1'>Monitor</a>"
                    },
                    memberGrid
                ]
                                 });




                                 var deleteAdminWin;
                                 function deleteAdmin() {
                                     var grid = Ext.getCmp('group-membergrid');
                                     var selectModel = grid.getSelectionModel();
                                     var rec = selectModel.getSelected();

                                     if (rec == undefined || rec.length == 0) {
                                         Ext.Msg.alert('Fingerprint', 'Please select a record');
                                         return;
                                     }

                                     if (!deleteAdminWin) {
                                         var deleteAdminPanel = new Ext.FormPanel({
                                             layout: 'form',
                                             buttonAlign: 'center',
                                             id: 'deleteJobForm',
                                             labelWidth: 200,
                                             baseCls: 'x-plain',
                                             items: [
                {
                    xtype: 'textfield',
                    name: 'password',
                    inputType: 'password',
                    id: 'delete-group-password',
                    fieldLabel: 'Please enter your password'
                }

            ],
                                             buttons: [
                {
                    text: 'OK',
                    handler: function() {
                        var pwd = Ext.getCmp('delete-group-password').getValue();
                        if (pwd == "") {
                            Ext.Msg.alert('Fingerprint', 'Please input correct password !');
                            return;
                        }

                        var grid = Ext.getCmp('group-membergrid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();

                        if (rec == undefined || rec.length == 0) {
                            Ext.Msg.alert('Fingerprint', 'Please select a record');
                            return;
                        }

                        var sUrl = "/" + APP_NAME + "/group.aspx/DeleteAdmin";
                        var xParameter = { pid: rec.data.objectid, pwd: pwd };
                        LoadData(sUrl, xParameter, onDeleteAdminReceived);

                        function onDeleteAdminReceived(data) {

                            deleteAdminWin.hide();

                            Ext.getCmp('group-membergrid').getStore().reload();

                            Ext.Msg.show({
                                title: 'Fingerprint',
                                msg: data.result,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.INFO
                            });
                        }
                    }
                },
                {
                    text: 'Cancel',
                    handler: function() {
                        deleteAdminWin.hide();
                    }
                }
            ]
                                         });


                                         deleteAdminWin = new Ext.Window({
                                             title: 'Fingerprint',
                                             layout: 'fit',
                                             width: 400,
                                             height: 100,
                                             closeAction: 'hide',
                                             plain: true,
                                             items: deleteAdminPanel
                                         });
                                     }
                                     Ext.getCmp('delete-group-password').setValue('');
                                     deleteAdminWin.show();
                                 }



                                 var addAdminPanel = new Ext.FormPanel({
                                     id: 'newadmin-addadmin-panel',
                                     defaultType: 'textfield',
                                     layout: 'column',
                                     containerScroll: true,
                                     autoScroll: true,
                                     labelAlign: 'right',
                                     buttonAlign: 'left',
                                     anchor: '90%',
                                     items: [
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'box',
                        html: '<br/>'
                    }
                },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'group No.',
                        name: 'roleID',
                        id: 'add_admin_adminID',
                        anchor: '60%',
                        value: '--',
                        readOnly: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Group Name',
                        name: 'name',
                        id: 'add_admin_username',
                        anchor: '60%',
                        readOnly: true
                    }
                },
             itemSelector
			],
                                     buttons: [
                   ]
                                 });


                                 var newAdminPanel = new Ext.Panel({
                                     id: 'newadmin-form-panel',
                                     layout: 'Column',
                                     containerScroll: true,
                                     autoScroll: true,
                                     region: 'east',
                                     width: '89%',
                                     margins: '3 0 3 3',
                                     cmargins: '3 3 3 3',
                                     defaults: { margins: '0 0 5 0' },
                                     collapsible: true,
                                     collapsed: false,
                                     animCollapse: false,
                                     hideCollapseTool: false,
                                     buttonAlign: 'center',
                                     listeners: {
                                         collapse: {
                                             fn: function(panel) {
                                                 Ext.getCmp('group-center-panel').doLayout();
                                                 setYourLocation("Monitor");
                                             }
                                         }
                                     },
                                     items: [
	                {
	                    xtype: 'container',
	                    autoEL: {},
	                    columnWidth: 1,
	                    anchor: '100%',
	                    items: [
	                        {
	                            id: 'your-group-location2',
	                            xtype: 'box',
	                            anchor: '100%',
	                            html: "<a href='#' class='leftstyle1'>group</a> → <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'group item',
	                        collapsible: true,
	                        collapsed: false,
	                        anchor: '90%',
	                        items: [
                           addAdminPanel
                        ]
	                    }
	                }
	            ],
                                     buttons: [
                    {
                        text: 'Save',
                        handler: function() {


                            addAdminPanel.getForm().submit({
                                url: "/" + APP_NAME + "/group.aspx/addgroup",
                                waitMsg: 'Please wait...',
                                success: function(form, o) {
                                    Ext.Msg.show({
                                        title: 'Result',
                                        msg: o.result.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                    Ext.getCmp('newadmin-form-panel').collapse();
                                    Ext.getCmp('group-membergrid').getStore().reload();
                                },
                                failure: function(form, o) {
                                    Ext.Msg.show({
                                        title: 'Result',
                                        msg: o.result.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.ERROR
                                    });
                                }
                            });

                        }
                    }, { text: 'Cancel',
                        handler: function() {
                            addAdminPanel.getForm().findField('itemselector').reset();
                            Ext.getCmp('newadmin-form-panel').collapse();
                        }
                    }
                ]
                                 })


                                 function setYourLocation(val) {
                                     var a = Ext.getCmp('your-group-location');
                                     var location = "<a href='#' class='leftstyle1'>Group</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
                                     try {
                                         a.el.dom.innerHTML = location;
                                     }
                                     catch (e)
            { }

                                     a = Ext.getCmp('your-group-location2');
                                     location = "<a href='#' class='leftstyle1'>Group</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
                                     try {
                                         a.el.dom.innerHTML = location;
                                     }
                                     catch (e)
            { }
                                 }




                                 var mainPanel = new Ext.Panel({
                                     contentEl: 'fingerprint-group-body',
                                     closable: false,
                                     autoScroll: true,
                                     plain: true,
                                     layout: 'border',
                                     anchor: '-1, -100',
                                     items: [leftPanel, centerPanel, newAdminPanel]
                                 });


                                 //Create view
                                 var MainView = new Ext.Viewport({
                                     layout: 'anchor',
                                     items: [
                    {
                        region: 'north',
                        contentEl: 'topdiv',
                        border: false
                    },
                    mainPanel
                ]
                                 });
                                 Ext.getCmp('newadmin-form-panel').collapse();
                                 fn_click(document.getElementById('admin'));
                             })


                             Ext.EventManager.onWindowResize(function(w, h) {
                                 Ext.getCmp('group-membergrid').getStore().reload();
                                 var newadd = Ext.getCmp('newadmin-form-panel');

                                 var result = false;
                                 if (newadd.collapsed) {
                                     newadd.expand();
                                     result = true;
                                 }
                                 newadd.setWidth(w * 0.89);

                                 var inventoyrgrid = Ext.getCmp('group-center-panel')
                                 inventoyrgrid.setWidth(w * 0.89);


                                 var left = Ext.getCmp('newadmin-left-panel');
                                 left.setWidth(w * 0.1);

                                 newadd.doLayout();
                                 inventoyrgrid.doLayout();
                                 left.doLayout();

                                 var addadmin = Ext.getCmp('newadmin-addadmin-panel');
                                 addadmin.setWidth(w * 0.9);
                                 addadmin.doLayout();

                                 if (result == true)
                                     newadd.collapse();

                             }, this, true);


                         function adminidRenderer(val) {
                             return "<a href='#' onclick =editAdmin()>" + val + "</a>";
                         }
                         function onClick() {
                             alert("NonImplemented");
                         }

                         function newAdmin()
                         { Ext.getCmp('newadmin-form-panel').expand(); }


                         function editAdmin() {
                             var grid = Ext.getCmp('group-membergrid');
                             var selectModel = grid.getSelectionModel();
                             var rec = selectModel.getSelected();

                             if (rec == undefined || rec.length == 0) {
                                 Ext.Msg.alert('Fingerprint', 'Please select a record');
                                 return;
                             }
                             Ext.getCmp('add_admin_adminID').setValue(rec.data.objectid);
                             Ext.getCmp('add_admin_username').setValue(rec.data.name);
                             Ext.getCmp('newadmin-form-panel').expand();
                             var addpanel = Ext.getCmp('newadmin-addadmin-panel');


                             ds.load({
                                 params: {
                                     objectid: rec.data.objectid
                                 }
                             });

                             ds2.load({
                             params: {
                                 objectid:rec.data.objectid
                             }
                         });

                     }


                         var ds = new Ext.data.JsonStore({
                             url: "/" + APP_NAME + "/group.aspx/getLeftSales",
                             fields: ['objectid', 'name'],
                             root: 'data',
                             autoLoad: true
                         });

                             var ds2 = new Ext.data.JsonStore({
                             url: "/" + APP_NAME + "/group.aspx/getContactSales",
                             fields: ['objectid', 'name'],
                             root: 'data',
                             autoLoad: true
                                
                             });
                         // 分配资源form
                         var itemSelector = new Ext.ux.ItemSelector({
                             xtype: 'itemselector',
                             columnWidth: 1,
                             bodyStyle:"text-align:center; margin-top:20px;",
                             name: 'itemselector',
                             fieldLabel: 'ItemSelector',
                             imagePath: '../content/js/ux/images/',
                             multiselects: [{
                                 width: 250,
                                 height: 200,
                                 store: ds,
                                 displayField: 'name',
                                 valueField: 'objectid'
                             }, {
                                 width: 250,
                                 height: 200,
                                  displayField: 'name',
                                 valueField: 'objectid',
                                 store:ds2,
                                 tbar: [{
                                     text: 'clear',
                                     handler: function() {
                                         var addpanel = Ext.getCmp('newadmin-addadmin-panel');
                                         addpanel.getForm().findField('itemselector').reset();
                                     }
}]
}]
                                 }); 
                                 

    </script>
</asp:Content>
