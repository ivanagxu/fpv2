<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Customer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    <div id="fingerprint-customer-body"></div>
    <div id="fingerprint-customer-left" class="x-hide-display">
      
    </div>
    
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="admin-customer-link" href ="admin"  class="menubar" 

      >admin</a></li>
     <li><a id="admin-group-link" href ="group"  class="menubar" 
      
      >group</a></li>
      <li><a id="admin-customer-link" href ="customer"  class="menubar_click"
      
      >customer</a></li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="styleSheetContent" runat="server">
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

            Ext.state.Manager.setProvider(new Ext.state.CookieProvider());
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

            customerStore = new Ext.data.JsonStore({
                // store configs
                autoDestroy: true,
                url: "/" + APP_NAME + "/customer.aspx/customer",
                remoteSort: true,
                sortInfo: {
                    field: 'objectid',
                    direction: 'desc'
                },
                storeId: 'customerStore',

                // reader configs
                idProperty: 'objectid',
                root: 'data',
                totalProperty: 'total',
                fields: [{
                    name: 'objectid',
                    type: 'String'
                }, {
                    name: 'company_code',
                    type: 'string'
                }, {
                    name: 'company_name',
                    type: 'String'
                }, {
                    name: 'contact_objectid',
                    type: 'String'

                }, {
                    name: 'contact_person',
                    type: 'String'
                }, {
                    name: 'contact_tel',
                    type: 'String'
                }, {
                    name: 'street1',
                    type: 'String'
                }, {
                    name: 'street2',
                    type: 'String'
                }, {
                    name: 'street3',
                    type: 'String'
                }, {
                    name: 'email',
                    type: 'String'
                }, {
                    name: 'city',
                    type: 'String'
                }, {
                    name: 'mobile',
                    type: 'String'
                }, {
                    name: 'district',
                    type: 'String'
                }, {
                    name: 'fax',
                    type: 'String'
                }, {
                    name: 'remark',
                    type: 'String'
                }
				]
            });

            var filters = new Ext.ux.grid.GridFilters({
                encode: false,
                local: true,
                filters: [{
                    dataIndex: 'objectid',
                    type: 'String'
                }, {
                    dataIndex: 'company_code',
                    type: 'String'
                }, {
                    dataIndex: 'company_name',
                    type: 'String'
                }, {
                    dataIndex: 'contact_objectid',
                    type: 'String'

                }, {
                    dataIndex: 'contact_person',
                    type: 'String'
                }, {
                    dataIndex: 'contact_tel',
                    type: 'String'
                }, {
                    dataIndex: 'street1',
                    type: 'String'
                }
				]
            });

            var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
            var createColModel = function(finish, start) {
                var columns = [sm,
                {
                    dataIndex: 'objectid',
                    header: 'Company No',
                    filterable: true
                },
                   {
                       dataIndex: 'company_code',
                       header: 'Company Code',
                       filterable: true,
                       filter: { type: 'string' },
                       renderer: adminidRenderer
                   }, {
                       dataIndex: 'company_name',
                       header: 'Company Name',
                       filterable: true
                   }, {
                       dataIndex: 'contact_person',
                       header: 'Contact Person',
                       filterable: true
                   }, {
                       dataIndex: 'contact_tel',
                       header: 'Contact Tel',
                       filterable: true
                   }, {
                       dataIndex: 'street1',
                       header: 'Address',
                       filterable: true
                   }
				]; return new Ext.grid.ColumnModel({
				    columns: columns.slice(start || 0, finish),
				    defaults: {
				        sortable: true
				    }
				});
            };
            //customer grid
            var customerGrid = new Ext.grid.GridPanel({
                id: 'customer-customerrgrid',
                border: false,
                store: customerStore,
                height: '100%',
                colModel: createColModel(8),
                selModel: sm,
                loadMask: true,
                plugins: [filters],
                stripeRows: true,
                flex: 8,
                listeners: {
                    render: {
                        fn: function() {
                            customerStore.load({
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
                    store: customerStore,
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
                        text: 'New Customer',
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
                            items: [{
                                text: 'Delete',
                                handler: deleteCustomer
}]
}]
                            });

                            var centerPanel = new Ext.Panel({
                                id: 'customer-center-panel',
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
                        id: 'your-customer-location',
                        xtype: 'box',
                        anchor: '100%',
                        html: "<a href='#' class='leftstyle1'>Customer</a> -> <a href='#' class='leftstyle1'>Monitor</a>"
                    },
                    customerGrid
                ]
                            });




                            var deleteCustomerWin;
                            function deleteCustomer() {
                                var grid = Ext.getCmp('customer-customerrgrid');
                                var selectModel = grid.getSelectionModel();
                                var rec = selectModel.getSelected();

                                if (rec == undefined || rec.length == 0) {
                                    Ext.Msg.alert('Fingerprint', 'Please select a record');
                                    return;
                                }

                                if (!deleteCustomerWin) {
                                    var deleteCustomerPanel = new Ext.FormPanel({
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
                    id: 'delete-customer-password',
                    fieldLabel: 'Please enter your password'
                }

            ],
                                        buttons: [
                {
                    text: 'OK',
                    handler: function() {
                        var pwd = Ext.getCmp('delete-customer-password').getValue();
                        if (pwd == "") {
                            Ext.Msg.alert('Fingerprint', 'Please input correct password !');
                            return;
                        }

                        var grid = Ext.getCmp('customer-customerrgrid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();

                        if (rec == undefined || rec.length == 0) {
                            Ext.Msg.alert('Fingerprint', 'Please select a record');
                            return;
                        }

                        var sUrl = "/" + APP_NAME + "/customer.aspx/DeleteCustomer";
                        var xParameter = { pid: rec.data.objectid, pwd: pwd };
                        LoadData(sUrl, xParameter, onDeleteAdminReceived);

                        function onDeleteAdminReceived(data) {

                            deleteCustomerWin.hide();

                            Ext.getCmp('customer-customerrgrid').getStore().reload();

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
                        deleteCustomerWin.hide();
                    }
                }
            ]
                                    });


                                    deleteCustomerWin = new Ext.Window({
                                        title: 'Fingerprint',
                                        layout: 'fit',
                                        width: 400,
                                        height: 100,
                                        closeAction: 'hide',
                                        plain: true,
                                        items: deleteCustomerPanel
                                    });
                                }
                                Ext.getCmp('delete-customer-password').setValue('');
                                deleteCustomerWin.show();
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
                        fieldLabel: 'Company No',
                        name: 'cid',
                        id: 'add_customer_objectid',
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
                        fieldLabel: 'Company Code',
                        name: 'code',
                        id: 'add_customer_code',
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
                       fieldLabel: 'Company Name',
                       name: 'name',
                       id: 'add_customer_name',
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
                           fieldLabel: 'Person',
                           name: 'person',
                           id: 'add_customer_person',
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
                           fieldLabel: 'Tel',
                           name: 'tel',
                           id: 'add_customer_tel',
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
                           fieldLabel: 'Address',
                           name: 'address',
                           id: 'add_customer_address',
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
                           fieldLabel: 'Street2',
                           name: 'street2',
                           id: 'add_customer_street2',
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
                           fieldLabel: 'Street3',
                           name: 'street3',
                           id: 'add_customer_street3',
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
                           id: 'add_customer_email',
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
                           fieldLabel: 'Fax',
                           name: 'fax',
                           id: 'add_customer_fax',
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
                           fieldLabel: 'City',
                           name: 'city',
                           id: 'add_customer_city',
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
                           fieldLabel: 'Mobile',
                           name: 'mobile',
                           id: 'add_customer_mobile',
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
                           fieldLabel: 'District',
                           name: 'district',
                           id: 'add_customer_district',
                           anchor: '60%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 1,
                       layout: 'form',
                       items: {
                           xtype: 'textarea',
                           fieldLabel: 'Remarks',
                           name: 'remark',
                           id: 'add_customer_remark',
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
                                            Ext.getCmp('customer-center-panel').doLayout();
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
	                            id: 'your-customer-location2',
	                            xtype: 'box',
	                            anchor: '100%',
	                            html: "<a href='#' class='leftstyle1'>Customer</a> -> <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Customer Item',
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
                                url: "/" + APP_NAME + "/customer.aspx/addcustomer",
                                waitMsg: 'Please wait...',
                                success: function(form, o) {
                                    Ext.Msg.show({
                                        title: 'Result',
                                        msg: o.result.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                    Ext.getCmp('newadmin-form-panel').collapse();
                                    Ext.getCmp('customer-customerrgrid').getStore().reload();
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
                                var a = Ext.getCmp('your-customer-location');
                                var location = "<a href='#' class='leftstyle1'>Customer</a> -> <a href='#' class='leftstyle1'>" + val + "</a>"
                                try {
                                    a.el.dom.innerHTML = location;
                                }
                                catch (e)
            { }

                                a = Ext.getCmp('your-customer-location2');
                                location = "<a href='#' class='leftstyle1'>Customer</a> -> <a href='#' class='leftstyle1'>" + val + "</a>"
                                try {
                                    a.el.dom.innerHTML = location;
                                }
                                catch (e)
            { }
                            }




                            var mainPanel = new Ext.Panel({
                                contentEl: 'fingerprint-customer-body',
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

    function adminidRenderer(val) {
        return "<a href='#' onclick =editAdmin()>" + val + "</a>";
    }
    function onClick() {
        alert("NonImplemented");
    }



    Ext.EventManager.onWindowResize(function(w, h) {
        Ext.getCmp('customer-customerrgrid').getStore().reload();
        var newadd = Ext.getCmp('newadmin-form-panel');

        var result = false;
        if (newadd.collapsed) {
            newadd.expand();
            result = true;
        }
        newadd.setWidth(w * 0.89);

        var inventoyrgrid = Ext.getCmp('customer-center-panel')
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



    function newAdmin() {
        Ext.getCmp('add_customer_objectid').setValue("--");
        Ext.getCmp('add_customer_code').setValue("");
        Ext.getCmp('add_customer_name').setValue("");
        Ext.getCmp('add_customer_person').setValue("");
        Ext.getCmp('add_customer_tel').setValue("");
        Ext.getCmp('add_customer_address').setValue("");

        Ext.getCmp('add_customer_street2').setValue("") ;
        Ext.getCmp('add_customer_street3').setValue("");
        
        Ext.getCmp('add_customer_email').setValue("");
        Ext.getCmp('add_customer_fax').setValue("");
        Ext.getCmp('add_customer_city').setValue("");
        Ext.getCmp('add_customer_mobile').setValue("");
        Ext.getCmp('add_customer_district').setValue("");
        Ext.getCmp('add_customer_remark').setValue("");
        
        Ext.getCmp('newadmin-form-panel').expand(); 
    }


    function editAdmin() {
        var grid = Ext.getCmp('customer-customerrgrid');
        var selectModel = grid.getSelectionModel();
        var rec = selectModel.getSelected();

        if (rec == undefined || rec.length == 0) {
            Ext.Msg.alert('Fingerprint', 'Please select a record');
            return;
        }
        Ext.getCmp('add_customer_objectid').setValue(rec.data.objectid);
        Ext.getCmp('add_customer_code').setValue(rec.data.company_code);
        Ext.getCmp('add_customer_name').setValue(rec.data.company_name);
        Ext.getCmp('add_customer_person').setValue(rec.data.contact_person);
        Ext.getCmp('add_customer_tel').setValue(rec.data.contact_tel);
        Ext.getCmp('add_customer_address').setValue(rec.data.street1);
        Ext.getCmp('add_customer_street2').setValue(rec.data.street2);
        Ext.getCmp('add_customer_street3').setValue(rec.data.street3);

        Ext.getCmp('add_customer_email').setValue(rec.data.email);
        Ext.getCmp('add_customer_fax').setValue(rec.data.fax);
        Ext.getCmp('add_customer_city').setValue(rec.data.city);
        Ext.getCmp('add_customer_mobile').setValue(rec.data.mobile);
        Ext.getCmp('add_customer_district').setValue(rec.data.district);
        Ext.getCmp('add_customer_remark').setValue(rec.data.remark);
        
        Ext.getCmp('newadmin-form-panel').expand();
    }
      
    </script>
</asp:Content>
