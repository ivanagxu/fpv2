<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
    Fingerprint
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="styleSheetContent" runat="server">
<link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-20.css") %>/>
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
            background-color: #c3eaf9 ! important;
        }
        
        #newadmin-addAdmin-panel .x-panel-body {
            background-color: #c3eaf9 ! important;
        }
        #newadmin-form-panel .x-panel-body {
            background-color: #c3eaf9 ! important;
        }
        
        #admin-centerPanel .x-panel-body {
            background-color: #c3eaf9 ! important;
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

            inventoryStore = new Ext.data.JsonStore({
                // store configs
                autoDestroy: true,
                url: "/" + APP_NAME + "/inventory.aspx/inventory",
                remoteSort: true,
                sortInfo: {
                    field: 'objectid',
                    direction: 'desc'
                },
                storeId: 'inventoryStore',

                // reader configs
                idProperty: 'objectid',
                root: 'data',
                totalProperty: 'total',
                fields: [{
                    name: 'objectid',
                    type: 'string'
                }, {
                    name: 'category',
                    type: 'string'
                }, {
                    name: 'assetno',
                    type: 'string'
                }, {
                    name: 'productnameen',
                    type: 'String'

                }, {
                    name: 'productnamecn',
                    type: 'String'
                }, {
                    name: 'description',
                    type: 'string'
                }, {
                    name: 'quantity',
                    type: 'String'
                }, { name: 'asat',
                    type: 'string'
                }, { name: 'remark',
                    type: 'string'
                }
				]
            });

            var filters = new Ext.ux.grid.GridFilters({
                encode: false,
                local: true,
                filters: [{
                    type: 'string',
                    dataIndex: 'objectid'
                }, {
                    type: 'string',
                    dataIndex: 'category'
                }, {
                    type: 'string',
                    dataIndex: 'assetno'
                }, {
                    type: 'string',
                    dataIndex: 'productnameen'
                }, {
                    type: 'string',
                    dataIndex: 'productnamecn'
                }, {
                    type: 'string',
                    dataIndex: 'description'
                }, {
                    type: 'string',
                    dataIndex: 'quantity'
                }, {
                    type: 'string',
                    dataIndex: 'asat'
}]
                });

                var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
                var createColModel = function(finish, start) {
                    var columns = [sm,
                   {
                        dataIndex: 'category',
                        header: 'Category.',
                        filterable: true,
                        renderer: adminidRenderer
                    }, {
                    dataIndex: 'assetno',
                        header: 'Asset No.',
                        filterable: true
                    }, {
                    dataIndex: 'productnameen.',
                        header: 'Asset(eng)',
                        filterable: true
                    }, {
                        dataIndex: 'productnamecn.',
                        header: 'Asset (ÖÐÎÄ)',
                        filterable: true
                    }, {
                    dataIndex: 'description',
                        header: 'Description.',
                        filterable: true
                    }, {
                        dataIndex: 'quantity',
                        header: 'Quantity (Stored).',
                        filterable: true
                    }, {
                        dataIndex: 'asat',
                        header: 'As at.',
                        filterable: true
                    }, {
                        dataIndex: 'remark',
                        header: 'Remark.',
                        filterable: true
                    }
				]; return new Ext.grid.ColumnModel({
				    columns: columns.slice(start || 0, finish),
				    defaults: {
				        sortable: true
				    }
				});
                };
                //admin grid
                var inventoryGrid = new Ext.grid.GridPanel({
                    id: 'inventory-inventorygrid',
                    border: false,
                    store: inventoryStore,
                    height: '100%',
                    colModel: createColModel(9),
                    selModel: sm,
                    loadMask: true,
                    plugins: [filters],
                    stripeRows: true,
                    flex: 9,
                    listeners: {
                        render: {
                            fn: function() {
                                inventoryStore.load({
                                    params: {
                                        start: 0,
                                        limit: 20
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
                        store: inventoryStore,
                        pageSize: 20,
                        plugins: [filters],
                        displayInfo: true,
                        displayMsg: 'Displaying record {0} - {1} of {2}',
                        emptyMsg: "No record to display"
                    }),
                    tbar: [{
                        xtype: 'buttongroup',
                        hidden: false,
                        items: [{
                            text: 'New Inventory',
                            handler: newInventory
}]
                        }, {
                            xtype: 'buttongroup',
                            items: [{
                                text: 'Edit',
                                handler: editInventory
}]
                            }, {
                                xtype: 'buttongroup',
                                items: [{
                                    text: 'Delete',
                                    handler: deleteAdmin
}]
}]
                                });

                                var centerPanel = new Ext.Panel({
                                    id: 'admin-center-panel',
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
                        id: 'your-admin-location',
                        xtype: 'box',
                        anchor: '100%',
                        html: "<a href='#' class='leftstyle1'>Inventory</a> ¡ú <a href='#' class='leftstyle1'>Monitor</a>"
                    },
                    inventoryGrid
                ]
                                });




                                var deleteAdminWin;
                                function deleteAdmin() {
                                    var grid = Ext.getCmp('inventory-inventorygrid');
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
                    id: 'delete-admin-password',
                    fieldLabel: 'Please enter your password'
                }

            ],
                                            buttons: [
                {
                    text: 'OK',
                    handler: function() {
                        var pwd = Ext.getCmp('delete-admin-password').getValue();
                        if (pwd == "") {
                            Ext.Msg.alert('Fingerprint', 'Please input correct password !');
                            return;
                        }

                        var grid = Ext.getCmp('inventory-inventorygrid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();

                        if (rec == undefined || rec.length == 0) {
                            Ext.Msg.alert('Fingerprint', 'Please select a record');
                            return;
                        }

                        var sUrl = "/" + APP_NAME + "/admin.aspx/DeleteAdmin";
                        var xParameter = { pid: rec.data.objectid, pwd: pwd };
                        LoadData(sUrl, xParameter, onDeleteAdminReceived);

                        function onDeleteAdminReceived(data) {

                            deleteAdminWin.hide();

                            Ext.getCmp('inventory-inventorygrid').getStore().reload();

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
                                    Ext.getCmp('delete-admin-password').setValue('');
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
                        fieldLabel: 'Inventory No.',
                        name: 'objectid',
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
                        fieldLabel: 'User Name.',
                        name: 'username',
                        id: 'add_admin_username',
                        anchor: '60%',
                        readOnly: false
                    }
                },
               {
                   xtype: 'container',
                   autoEl: {},
                   columnWidth: 0.5,
                   layout: 'form',
                   items: {
                       xtype: 'textfield',
                       fieldLabel: 'English Name.',
                       name: 'nameen',
                       id: 'add_admin_nameen',
                       anchor: '60%',
                       readOnly: false
                   }
               },
                   {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Chinese Name.',
                           name: 'namecn',
                           id: 'add_admin_namecn',
                           anchor: '60%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Password',
                           name: 'pwd',
                           id: 'add_admin_password',
                           anchor: '60%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Post.',
                           name: 'post',
                           id: 'add_admin_post',
                           anchor: '60%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Email',
                           name: 'email',
                           id: 'add_admin_email',
                           anchor: '60%',
                           readOnly: false
                       }
                   },
                    {
                        xtype: 'container',
                        autoEl: {},
                        columnWidth: 0.5,
                        layout: 'form',
                        items: {
                            anchor: '60%',
                            xtype: 'combo',
                            fieldLabel: 'Status',
                            value: '',
                            id: 'add_admin_status',
                            mode: 'local',
                            editable: false,
                            store: [["Active", "Active"], ["Inactive", "Inactive"]],
                            displayField: 'name',
                            valueField: 'id',
                            forceSelection: false,
                            triggerAction: 'all',
                            hiddenName: 'status',
                            anyMatch: true,
                            listeners: {
                                select: {
                                    fn: function(combo, value) {
                                        var rec = combo.getValue();
                                    }
                                },
                                expand: {
                                    fn: function(combo, value) {
                                        //createBasekeyStoreFilter(combo.store,'name',Ext.getCmp('newadmin-customer-filter').getValue());
                                    }
                                },
                                collapse: {
                                    fn: function(combo, value) {

                                        //clearBasekeyStoreFilter(combo.store);
                                    }
                                }
                            }
                        }
                    }, {
                        xtype: 'container',
                        autoEl: {},
                        columnWidth: 1,
                        layout: 'form',
                        items: {
                            xtype: 'textarea',
                            fieldLabel: 'Remark.',
                            name: 'remark',
                            id: 'add_admin_remark',
                            anchor: '80%',
                            readOnly: false
                        }
                    }
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
                                                Ext.getCmp('admin-center-panel').doLayout();
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
	                            id: 'your-admin-location2',
	                            xtype: 'box',
	                            anchor: '100%',
	                            html: "<a href='#' class='leftstyle1'>Inventory</a> ¡ú <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Inventory Item',
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
                                url: "/" + APP_NAME + "/admin.aspx/AddAdmin",
                                waitMsg: 'Please wait...',
                                success: function(form, o) {
                                    Ext.Msg.show({
                                        title: 'Result',
                                        msg: o.result.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                    Ext.getCmp('newadmin-form-panel').collapse();
                                    Ext.getCmp('inventory-inventorygrid').getStore().reload();
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
                            Ext.getCmp('newadmin-form-panel').collapse();
                        }
                    }
                ]
                                })


                                function setYourLocation(val) {
                                    var a = Ext.getCmp('your-admin-location');
                                    var location = "<a href='#' class='leftstyle1'>Inventory</a> ¡ú <a href='#' class='leftstyle1'>" + val + "</a>"
                                    try {
                                        a.el.dom.innerHTML = location;
                                    }
                                    catch (e)
            { }

                                    a = Ext.getCmp('your-admin-location2');
                                    location = "<a href='#' class='leftstyle1'>Inventory</a> ¡ú <a href='#' class='leftstyle1'>" + val + "</a>"
                                    try {
                                        a.el.dom.innerHTML = location;
                                    }
                                    catch (e)
            { }
                                }




                                var mainPanel = new Ext.Panel({
                                    contentEl: 'fingerprint-admin-body',
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
                                fn_click(document.getElementById('inventory'));
                            })

    function adminidRenderer(val) {
        return "<a href='#' onclick =editInventory()>" + val + "</a>";
    }
    function onClick() {
        alert("NonImplemented");
    }

    function newInventory() {
        Ext.getCmp('newadmin-form-panel').expand(); 
    }


    function editInventory() {
        var grid = Ext.getCmp('inventory-inventorygrid');
        var selectModel = grid.getSelectionModel();
        var rec = selectModel.getSelected();

        if (rec == undefined || rec.length == 0) {
            Ext.Msg.alert('Fingerprint', 'Please select a record');
            return;
        }
        Ext.getCmp('add_admin_adminID').setValue(rec.data.objectid);
        Ext.getCmp('add_admin_username').setValue(rec.data.user_name);
        Ext.getCmp('add_admin_nameen').setValue(rec.data.eng_name);
        Ext.getCmp('add_admin_namecn').setValue(rec.data.chi_name);
        Ext.getCmp('add_admin_password').setValue(rec.data.pwd);
        Ext.getCmp('add_admin_post').setValue(rec.data.post);
        Ext.getCmp('add_admin_email').setValue(rec.data.email);
        Ext.getCmp('add_admin_remark').setValue(rec.data.remark);
        Ext.getCmp('add_admin_status').setValue(rec.data.status);
        Ext.getCmp('newadmin-form-panel').expand();
    }
      
    </script>

</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
 <div id="fingerprint-admin-body"></div>
    <div id="fingerprint-admin-left" class="x-hide-display">
        
    </div>
    
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="admin-admin-link" href ="inventory"  class="menubar_click" 

      >Monitor</a></li>     
    </ul>
</asp:Content>
