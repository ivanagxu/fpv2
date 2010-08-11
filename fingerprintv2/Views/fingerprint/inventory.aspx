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
                }, { name: 'stored',
                    type: 'string'
                }, { name: 'productno',
                    type: 'string'
                }, { name: 'dimension',
                    type: 'string'
                }, { name: 'unit',
                    type: 'string'
                }, { name: 'unitcost',
                    type: 'string'
                }, { name: 'receivedby',
                    type: 'string'
                }, { name: 'deadline',
                    type: 'string'
                }, { name: 'person',
                    type: 'string'
                }, { name: 'tel',
                    type: 'string'
                }, { name: 'receiveddate',
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
                }, {
                    type: 'string',
                    dataIndex: 'stored'
}]
                });

                var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
                var createColModel = function(finish, start) {
                    var columns = [sm,
                   {
                       dataIndex: 'category',
                       header: 'Category.',
                       filterable: true
                   }, {
                       dataIndex: 'assetno',
                       header: 'Asset No.',
                       filterable: true,
                       renderer: adminidRenderer
                   }, {
                       dataIndex: 'productnameen',
                       header: 'Asset(eng).',
                       filterable: true
                   }, {
                       dataIndex: 'productnamecn',
                       header: 'Asset (中文)',
                       filterable: true
                   }, {
                       dataIndex: 'description',
                       header: 'Description.',
                       filterable: true
                   }, {
                       dataIndex: 'stored',
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
                        html: "<a href='#' class='leftstyle1'>Inventory</a> → <a href='#' class='leftstyle1'>Monitor</a>"
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

                        var sUrl = "/" + APP_NAME + "/inventory.aspx/deleteinventory";

                        var xParameter = { ids: rec.data.objectid, pwd: pwd };

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
                        fieldLabel: 'Inventory No',
                        name: 'objectid',
                        id: 'add_inventory_objectid',
                        anchor: '70%',
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
                        fieldLabel: 'Category',
                        name: 'category',
                        id: 'add_inventory_category',
                        anchor: '70%',
                        readOnly: false
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Product No',
                        name: 'productno',
                        id: 'add_inventory_productno',
                        anchor: '70%',
                        readOnly: false
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Name(Eng)',
                        name: 'productnameen',
                        id: 'add_inventory_productnameen',
                        anchor: '70%',
                        readOnly: false
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Name(中文)',
                        name: 'productnamecn',
                        id: 'add_inventory_productnamecn',
                        anchor: '70%',
                        readOnly: false
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Dimension',
                        name: 'dimension',
                        id: 'add_inventory_dimension',
                        anchor: '70%',
                        readOnly: false
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'combo',
                        fieldLabel: 'Unit:',
                        value: 'PCS',
                        id: 'add_inventory_unit',
                        mode: 'local',
                        store: [["PCS", "PCS"], ["mm x mm", "mm x mm"]],
                        editable: false,
                        forceSelection: true,
                        anchor: '70%',
                        displayField: 'name',
                        valueField: 'id',
                        triggerAction: 'all',
                        hiddenName: 'status',
                        anyMatch: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Unit Cost',
                        name: 'unitcost',
                        id: 'add_inventory_unitcost',
                        anchor: '70%',
                        readOnly: false
                    }
                },
               { xtype: 'container',
                   autoEl: {},
                   columnWidth: 0.5,
                   layout: 'form',
                   items: [{
                       xtype: 'combo', id: 'add_inventory_receivedby',
                       fieldLabel: 'Received By',
                       value: '',
                       mode: 'local',
                       anchor: '70%',
                       store: new Ext.data.JsonStore({
                           url: "/" + APP_NAME + "/order.aspx/getSalesComboList",
                           fields: ['id', 'name'],
                           root: 'tags',
                           autoLoad: true
                       }),
                       displayField: 'name',
                       valueField: 'id',
                       forceSelection: true,
                       triggerAction: 'all',
                       hiddenName: 'receivedby',
                       listeners: {
                           select: {
                               fn: function(combo, value) {

                               }
                           }
                       }
}]
                   },
                   {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'datefield',
                           fieldLabel: 'Order Deadline ',
                           name: 'deadline',
                           id: 'add_inventory_deadline',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 1,
                       layout: 'form',
                       items: {
                           xtype: 'datefield',
                           fieldLabel: 'Received Date ',
                           name: 'deadline',
                           id: 'add_inventory_receiveddate',
                           anchor: '35%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 1,
                       layout: 'form',
                       items: {
                           xtype: 'combo',
                           fieldLabel: 'Select Contact',
                           value: '',
                           id: 'add_inventory_code',
                           mode: 'remote ',
                           minChars: 0,
                           store: new Ext.data.JsonStore({
                               url: "/" + APP_NAME + "/delivery.aspx/getcustomersbyname",
                               fields: ['name', 'code', 'contact', 'tel'],
                               root: 'tags',
                               autoLoad: true
                           }),
                           editable: true,
                           forceSelection: true,
                           displayField: 'name',
                           valueField: 'code',
                           triggerAction: 'all',
                           hiddenName: 'contact',
                           anyMatch: true,
                           listeners: {
                               select: {
                                   fn: function(combo, value) {
                                       var tel = Ext.getCmp('add_inventory_tel');
                                       var person = Ext.getCmp('add_inventory_person');
                                       var rec = combo.getValue();
                                       for (var i = 0; i < combo.store.getCount(); i++) {
                                           var record = combo.store.getAt(i);
                                           if (record.get('code') == rec) {
                                               tel.setValue(record.get('tel'));
                                               person.setValue(record.get('contact'));
                                           }
                                       }
                                   }
                               },
                               expand: {
                                   fn: function(combo, value) {
                                       // createBasekeyStoreFilter(combo.store, 'code', Ext.getCmp('newadmin-customer-filter').getValue());
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
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Contact Person.',
                           name: 'contactperson',
                           id: 'add_inventory_person',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Tel.',
                           name: 'tel',
                           id: 'add_inventory_tel',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 1,
                       layout: 'form',
                       items: {
                           xtype: 'textarea',
                           fieldLabel: 'Description.',
                           name: 'remark',
                           id: 'add_inventory_description',
                           anchor: '80%',
                           readOnly: false
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
                           id: 'add_inventory_remark',
                           anchor: '80%',
                           readOnly: false
                       }
                   }
			],
                                    buttons: [
                   ]
                                });


                                ///////////////////////////////

                                var jobStore = new Ext.data.JsonStore({
                                    storeId: "jobstore",
                                    url: "/" + APP_NAME + "/inventory.aspx/getconsumptions",
                                    idProperty: 'conid',
                                    root: 'data',
                                    totalProperty: 'total',
                                    fields: [
               { name: 'conid', type: 'string' },
		       { name: 'total', type: 'string' },
               { name: 'totalunit', type: 'string' },
               { name: 'store', type: 'string' },
               { name: 'storeunit', type: 'string' },
		       { name: 'used', type: 'string' },
		       { name: 'usedunit', type: 'string' },
		      { name: 'asdate', type: 'string' },
            ]
                                });


                                ///test


                                var jobGrid = new Ext.grid.GridPanel({
                                    id: 'neworder-grid-newjob',
                                    store: jobStore,
                                    columns: [sm,
                { id: 'conid', header: 'Item', sortable: true, dataIndex: 'conid', hide: true },
                { header: 'Quantity (Total)', sortable: true, dataIndex: 'total' },
			    { header: 'Quantity (Store)', sortable: true, dataIndex: 'store' },
                { header: 'Quantity (Used)', sortable: true, dataIndex: 'used' },
                { header: 'As at', sortable: true, dataIndex: 'asdate' }
            ],
                                    stripeRows: true,
                                    anchor: "90%",
                                    autoHeight: true,
                                    stateful: true,
                                    selModel: sm,
                                    sm: new Ext.grid.RowSelectionModel({
                                        singleSelect: true
                                    }),
                                    stateId: 'jobGrid',

                                    tbar: [
            {
                xtype: 'buttongroup',
                items: [{
                    text: 'Add',
                    handler: function() {

                        addconsumption.show();
                        var totalunitpcs = Ext.getCmp('add_consumption_total_unit_pcs');
                        var totalpcs = Ext.getCmp('add_consumption_total_pcs');

                        var totalunitmm = Ext.getCmp('add_consumption_total_unit_mm');
                        var totalmm = Ext.getCmp('add_consumption_total_mm');

                        var storeunitpcs = Ext.getCmp('add_consumption_store_unit_pcs');
                        var storepcs = Ext.getCmp('add_consumption_store_pcs');

                        var storeunitmm = Ext.getCmp('add_consumption_store_unit_mm');
                        var storemm = Ext.getCmp('add_consumption_store_mm');

                        var usedunitpcs = Ext.getCmp('add_consumption_used_unit_pcs');
                        var usedpcs = Ext.getCmp('add_consumption_used_pcs');

                        var usedunitmm = Ext.getCmp('add_consumption_used_unit_mm');
                        var usedmm = Ext.getCmp('add_consumption_used_mm');


                        var objectid = Ext.getCmp('add_consumption_objectid');

                        var asdate = Ext.getCmp('add_consumption_asdate');

                        asdate.setValue(new Date().getDate());

                        objectid.setValue("0");

                        totalunitpcs.setValue(true);
                        totalpcs.setValue("");
                        totalpcs.show();

                        totalunitmm.setValue(false);
                        totalmm.setValue("--");
                        totalmm.hide();

                        storeunitpcs.setValue(true);
                        storepcs.setValue("");
                        storepcs.show();

                        storeunitmm.setValue(false);
                        storemm.setValue("--");
                        storemm.hide();

                        usedunitpcs.setValue(true);
                        usedpcs.setValue("");
                        usedpcs.show();

                        usedunitmm.setValue(false);
                        usedmm.setValue("--");
                        usedmm.hide();

                        location.href = "#add_consumption_used_unit_mm";

                    }
                }
            ]
            }, {
                xtype: 'buttongroup',
                items: [
                    {
                        text: 'Edit',
                        handler: function() {


                            var totalunitpcs = Ext.getCmp('add_consumption_total_unit_pcs');
                            var totalpcs = Ext.getCmp('add_consumption_total_pcs');

                            var totalunitmm = Ext.getCmp('add_consumption_total_unit_mm');
                            var totalmm = Ext.getCmp('add_consumption_total_mm');

                            var storeunitpcs = Ext.getCmp('add_consumption_store_unit_pcs');
                            var storepcs = Ext.getCmp('add_consumption_store_pcs');

                            var storeunitmm = Ext.getCmp('add_consumption_store_unit_mm');
                            var storemm = Ext.getCmp('add_consumption_store_mm');

                            var usedunitpcs = Ext.getCmp('add_consumption_used_unit_pcs');
                            var usedpcs = Ext.getCmp('add_consumption_used_pcs');

                            var usedunitmm = Ext.getCmp('add_consumption_used_unit_mm');
                            var usedmm = Ext.getCmp('add_consumption_used_mm');


                            var objectid = Ext.getCmp('add_consumption_objectid');

                            var asdate = Ext.getCmp('add_consumption_asdate');



                            // Ext.getCmp('newjob-jobsubmitmode').setValue('Edit');
                            var grid = Ext.getCmp('neworder-grid-newjob');
                            var selectModel = grid.getSelectionModel();
                            var rec = selectModel.getSelected();

                            if (rec == undefined || rec.length == 0) {
                                Ext.Msg.alert('Fingerprint', 'Please select a record');
                                return;
                            }


                            addconsumption.show();


                            var date = new Date(Date.parse(rec.data.asdate));

                            asdate.setValue(date);

                            objectid.setValue(rec.data.conid);

                            if (rec.data.totalunit == "PCS") {
                                totalunitpcs.setValue(true);
                                totalpcs.setValue(rec.data.total);
                                totalpcs.show();
                                totalmm.hide();
                            }
                            else {
                                totalunitmm.setValue(true);
                                totalmm.setValue(rec.data.total);
                                totalmm.show();
                                totalpcs.hide();
                            }

                            if (rec.data.storeunit == "PCS") {
                                storeunitpcs.setValue(true);
                                storepcs.setValue(rec.data.store);
                                storepcs.show();
                                storemm.hide();
                            }
                            else {
                                storeunitmm.setValue(true);
                                storemm.setValue(rec.data.store);
                                storepcs.hide();
                                storemm.show();
                            }

                            if (rec.data.usedunit == "PCS") {
                                usedunitpcs.setValue(true);
                                usedpcs.setValue(rec.data.used);
                                usedpcs.show();
                                usedmm.hide();

                            }
                            else {
                                usedunitmm.setValue(true);
                                usedmm.setValue(rec.data.used);
                                usedmm.show();
                                usedpcs.hide();
                            }
                            location.href = "#add_consumption_used_unit_mm";
                        }
                    }
                ]
            }, {
                xtype: 'buttongroup',
                items: [
                        {
                            text: 'Delete',
                            handler: function() {
                                var grid = jobGrid;
                                var selectModel = grid.getSelectionModel();
                                var rec = selectModel.getSelected();

                                if (rec == undefined || rec.data.length == 0) {
                                    Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                                    return;
                                }


                                if (!deleteJobWin) {
                                    var deleteJobPanel = new Ext.FormPanel({
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
                                                id: 'delete-job-password',
                                                fieldLabel: 'Please enter your password'
                                            }

                                        ],
                                        buttons: [
                                            {
                                                text: 'OK',
                                                handler: function() {
                                                    var pwd = Ext.getCmp('delete-job-password').getValue();
                                                    if (pwd == "")
                                                        return;
                                                    var grid = Ext.getCmp('neworder-grid-newjob');
                                                    var selectModel = grid.getSelectionModel();
                                                    var rec = selectModel.getSelected();

                                                    if (rec == undefined || rec.data.length == 0) {
                                                        Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                                                        return;
                                                    }

                                                    var sUrl = "/" + APP_NAME + "/inventory.aspx/deleteconsumption";

                                                    var xParameter = { ids: rec.data.conid, pwd: pwd };

                                                    LoadData(sUrl, xParameter, onDeleteAdminReceived);

                                                    function onDeleteAdminReceived(data) {

                                                        deleteJobWin.hide();
                                                        grid.getStore().reload();
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
                                                    deleteJobWin.hide();
                                                }
                                            }
                                        ]
                                    });


                                    deleteJobWin = new Ext.Window({
                                        title: 'Fingerprint',
                                        layout: 'fit',
                                        width: 400,
                                        height: 100,
                                        closeAction: 'hide',
                                        plain: true,
                                        items: deleteJobPanel
                                    });
                                }
                                Ext.getCmp('delete-job-password').setValue('');
                                deleteJobWin.show();
                            }
                        }
                ]
            }
            ]
                                });
                                var deleteJobWin;


                                var unitStore = new Ext.data.ArrayStore({
                                    fields: ['name', 'value'],
                                    data: [
		            ['MM', 'MM'],
		            ['PCS', 'PCS']
	            ]
                                });

                                var addconsumption = new Ext.FormPanel({
                                    defaultType: "textFiled",
                                    id: 'add_consumption_panel',
                                    layout: 'form',
                                    labelAlign: 'right',
                                    buttonAlign: 'center',
                                    anchor: '90%',
                                    border: false,
                                    items: [{
                                        xtype: 'container',
                                        layout: 'column',
                                        border: false,
                                        items: [{
                                            xtype: 'box',
                                            html: '<table width=10><tr><td></td></tr></table>'
                                        },
                                        {
                                            xtype: 'button',
                                            width: 60,
                                            text: "Save",
                                            handler: function() {

                                                var totalunitpcs = Ext.getCmp('add_consumption_total_unit_pcs');
                                                var totalpcs = Ext.getCmp('add_consumption_total_pcs');

                                                var totalunitmm = Ext.getCmp('add_consumption_total_unit_mm');
                                                var totalmm = Ext.getCmp('add_consumption_total_mm');

                                                var storeunitpcs = Ext.getCmp('add_consumption_store_unit_pcs');
                                                var storepcs = Ext.getCmp('add_consumption_store_pcs');

                                                var storeunitmm = Ext.getCmp('add_consumption_store_unit_mm');
                                                var storemm = Ext.getCmp('add_consumption_store_mm');

                                                var usedunitpcs = Ext.getCmp('add_consumption_used_unit_pcs');
                                                var usedpcs = Ext.getCmp('add_consumption_used_pcs');

                                                var usedunitmm = Ext.getCmp('add_consumption_used_unit_mm');
                                                var usedmm = Ext.getCmp('add_consumption_used_mm');

                                                var iid = Ext.getCmp('add_inventory_objectid');


                                                var objectid = Ext.getCmp('add_consumption_objectid');

                                                var asdate = Ext.getCmp('add_consumption_asdate');

                                                var mtu = "PCS";
                                                var mt = "";
                                                var msu = "PCS";
                                                var ms = "";
                                                var muu = "PCS";
                                                var mu = "";


                                                if (totalunitpcs.checked == true) {
                                                    mtu = "PCS";
                                                    mt = totalpcs.getValue();
                                                } else {
                                                    mtu = "mm x mm";
                                                    mt = totalmm.getValue();
                                                }

                                                if (storeunitpcs.checked == true) {
                                                    msu = "PCS";
                                                    ms = storepcs.getValue();
                                                } else {
                                                    msu = "mm x mm";
                                                    ms = storemm.getValue();

                                                }

                                                if (usedunitpcs.checked == true) {
                                                    muu = "PCS";
                                                    mu = usedpcs.getValue();

                                                }
                                                else {
                                                    muu = "mm x mm";
                                                    mu = usedmm.getValue();

                                                }

                                                var sUrl = "/" + APP_NAME + "/inventory.aspx/addconsumption";
                                                var xParameter = { objectid: objectid.getValue(), total: mt, totalunit: mtu, store: ms, storeunit: msu, used: mu, usedunit: muu, asdate: asdate.getValue(), inventoryid: iid.getValue()
                                                };

                                                LoadData(sUrl, xParameter, onAddconsumptionReceived);

                                                function onAddconsumptionReceived(data) {

                                                    if (data.success == false) {
                                                        Ext.Msg.show({
                                                            title: 'Fingerprint',
                                                            msg: data.result,
                                                            buttons: Ext.Msg.OK,
                                                            icon: Ext.Msg.INFO
                                                        });
                                                    } else {
                                                        Ext.getCmp('neworder-grid-newjob').getStore().reload();
                                                        Ext.getCmp('add_consumption_panel').hide();
                                                        Ext.Msg.show({
                                                            title: 'Fingerprint',
                                                            msg: data.result,
                                                            buttons: Ext.Msg.OK,
                                                            icon: Ext.Msg.INFO
                                                        });
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            xtype: 'box',
                                            html: '<table width=10><tr><td></td></tr></table>'
                                        },
                                        {
                                            xtype: 'button',
                                            text: "Cancel",
                                            width: 60,
                                            handler: function() {
                                                //  Ext.getCmp('new_consumption_panel').hide();
                                                addconsumption.hide();
                                            }
                                        }
                                    ]
                                    }, {
                                        xtype: 'box',
                                        html: '<br/>'
                                    },
                                    {
                                        xtype: 'container',
                                        layout: 'column',
                                        border: false,
                                        items: [{
                                            xtype: 'box',
                                            html: '<table width=10><tr><td></td></tr></table>'
                                        },
                            {
                                xtype: 'box',
                                html: '<table width=120><tr><td>Quantity (Total) : </td></tr></table>'
                            },
                            {
                                xtype: 'radio',
                                width: 60,
                                name: 'totalunit',
                                id: 'add_consumption_total_unit_pcs',
                                boxLabel: 'PCS',
                                handler: function() {
                                    if (this.checked == true) {
                                        var totalmm = Ext.getCmp('add_consumption_total_mm');
                                        var totalpcs = Ext.getCmp('add_consumption_total_pcs');
                                        totalpcs.show();
                                        totalmm.hide();
                                        totalpcs.setValue("");
                                        totalmm.setValue("--");
                                    }
                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_total_pcs',
                                id: 'add_consumption_total_pcs'
                            }
                        ]

                                    },
                  {
                      xtype: 'container',
                      layout: 'column',
                      border: false,
                      items: [
                            {
                                xtype: 'box',
                                html: '<table width=130><tr><td></td></tr></table>'
                            },
                            {

                                xtype: 'radio',
                                width: 60,
                                name: 'totalunit',
                                id: 'add_consumption_total_unit_mm',
                                boxLabel: 'MM',
                                handler: function() {
                                    if (this.checked == true) {
                                        var totalmm = Ext.getCmp('add_consumption_total_mm');
                                        var totalpcs = Ext.getCmp('add_consumption_total_pcs');
                                        totalpcs.hide();
                                        totalmm.show();
                                        totalpcs.setValue("--");
                                        totalmm.setValue("");
                                    }

                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_total_mm',
                                id: 'add_consumption_total_mm'
                            }
                            , {
                                xtype: 'box',
                                html: '<span  style=\"width:10px;\"></span>  (Format : mm x mm).'
                            }
                        ]

                  },
                  {
                      xtype: 'container',
                      layout: 'column',
                      border: false,
                      items: [{
                          xtype: 'box',
                          html: '<table width=10><tr><td></td></tr></table>'
                      },
                            {
                                xtype: 'box',
                                html: '<table width=120><tr><td>Quantity (Store) : </td></tr></table>'
                            },
                            {
                                xtype: 'radio',
                                width: 60,
                                name: 'storeunit',
                                id: 'add_consumption_store_unit_pcs',
                                boxLabel: 'PCS',
                                handler: function() {
                                    if (this.checked == true) {
                                        var storemm = Ext.getCmp('add_consumption_store_mm');
                                        var storepcs = Ext.getCmp('add_consumption_store_pcs');
                                        storepcs.show();
                                        storemm.hide();
                                        storepcs.setValue("");
                                        storemm.setValue("--");
                                    }
                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_store_pcs',
                                id: 'add_consumption_store_pcs'
                            }
                        ]

                  },
                  {
                      xtype: 'container',
                      layout: 'column',
                      border: false,
                      items: [
                            {
                                xtype: 'box',
                                html: '<table width=130><tr><td></td></tr></table>'
                            },
                            {

                                xtype: 'radio',
                                width: 60,
                                name: 'storeunit',
                                id: 'add_consumption_store_unit_mm',
                                boxLabel: 'MM',
                                handler: function() {
                                    if (this.checked == true) {
                                        var storemm = Ext.getCmp('add_consumption_store_mm');
                                        var storepcs = Ext.getCmp('add_consumption_store_pcs');
                                        storepcs.hide();
                                        storemm.show();
                                        storepcs.setValue("--");
                                        storemm.setValue("");
                                    }
                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_store_mm',
                                id: 'add_consumption_store_mm'
                            }
                            , {
                                xtype: 'box',
                                html: '<span  style=\"width:10px;\"></span>  (Format : mm x mm).'
                            }
                        ]

                  },
                  {
                      xtype: 'container',
                      layout: 'column',
                      border: false,
                      items: [{
                          xtype: 'box',
                          html: '<table width=10><tr><td></td></tr></table>'
                      },
                            {
                                xtype: 'box',
                                html: '<table width=120><tr><td>Quantity (Used) : </td></tr></table>'
                            },
                            {
                                xtype: 'radio',
                                width: 60,
                                name: 'usedunit',
                                id: 'add_consumption_used_unit_pcs',
                                boxLabel: 'PCS',
                                handler: function() {
                                    if (this.checked == true) {
                                        var usedmm = Ext.getCmp('add_consumption_used_mm');
                                        var usedpcs = Ext.getCmp('add_consumption_used_pcs');
                                        usedpcs.show();
                                        usedmm.hide();
                                        usedpcs.setValue("");
                                        usedmm.setValue("--");
                                    }
                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_used_pcs',
                                id: 'add_consumption_used_pcs'
                            }
                        ]

                  },
                  {
                      xtype: 'container',
                      layout: 'column',
                      border: false,
                      items: [
                            {
                                xtype: 'box',
                                html: '<table width=130><tr><td></td></tr></table>'
                            },
                            {

                                xtype: 'radio',
                                width: 60,
                                name: 'usedunit',
                                id: 'add_consumption_used_unit_mm',
                                boxLabel: 'MM',
                                handler: function() {
                                    if (this.checked == true) {
                                        var usedmm = Ext.getCmp('add_consumption_used_mm');
                                        var usedpcs = Ext.getCmp('add_consumption_used_pcs');
                                        usedpcs.hide();
                                        usedmm.show();
                                        usedpcs.setValue("--");
                                        usedmm.setValue("");
                                    }
                                }
                            }, {
                                xtype: 'textfield',
                                width: 160,
                                name: 'add_consumption_used_mm',
                                id: 'add_consumption_used_mm'
                            }
                            , {
                                xtype: 'box',
                                html: '<span  style=\"width:10px;\"></span>  (Format : mm x mm).'
                            }
                        ]

                  },
                   {
                       xtype: 'container',
                       layout: 'column',
                       border: false,
                       items: [{
                           xtype: 'box',
                           html: '<table width=10><tr><td></td></tr></table>'
                       },
                            {
                                xtype: 'box',
                                html: '<table width=120><tr><td>As Date:</td></tr></table>'
                            }, { xtype: 'box',
                                width: 60,
                                html: "<table width=130><tr><td></td></tr></table>"
                            },
                            {

                                xtype: 'datefield',
                                width: 160,
                                name: 'asdate',
                                id: 'add_consumption_asdate',
                                fieldLabel: 'As Date',
                                readOnly: false
                            }
                        ]

                   },
                {
                    xtype: 'hidden',
                    id: 'add_consumption_objectid',
                    name: 'add_consumption_objectid'
                }, {
                    xtype: 'container',
                    layout: 'column',
                    border: false,
                    items: {
                        xtype: 'box',
                        html: '<br/>'
                    }
                }
                                 ]
                                });

                                var addJobPanel = new Ext.FormPanel({
                                    id: 'neworder-addjob-panel',
                                    url: "/" + APP_NAME + "/job.aspx/newJob",
                                    defaultType: 'textfield',
                                    layout: 'form',
                                    labelAlign: 'right',
                                    buttonAlign: 'center',
                                    anchor: '90%',
                                    border: false,
                                    items: [
                {
                    xtype: 'container',
                    layout: 'column',
                    border: false,
                    items: {
                        xtype: 'box',
                        html: '<br/>'
                    }
                }, {

                    xtype: 'container',
                    layout: 'column',
                    border: false,
                    items: {
                        xtype: 'container',
                        layout: 'absolute',
                        height: 110,
                        width: 524,
                        fieldLabel: 'Items',
                        columnWidth: 1,
                        items: [
                            jobGrid
                        ]
                    }
                }
            ]
                                });


                                ///end test


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
	                            html: "<a href='#' class='leftstyle1'>Inventory</a> → <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Part I - Product Information',
	                        collapsible: true,
	                        collapsed: false,
	                        anchor: '90%',
	                        items: [
                           addAdminPanel
                        ]
	                    }
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    id: 'new_consumption_panel',
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Part II - Consumption History',
	                        collapsible: true,
	                        collapsed: false,
	                        anchor: '90%',
	                        items: [addJobPanel, addconsumption
                        ]
	                    }
	                }
	            ],
                                    buttons: [
                    {
                        text: 'Save',
                        handler: function() {


                            var cid = Ext.getCmp('add_inventory_objectid').getValue();
                            var category = Ext.getCmp('add_inventory_category').getValue();
                            var productno = Ext.getCmp('add_inventory_productno').getValue();
                            var productnameen = Ext.getCmp('add_inventory_productnameen').getValue();
                            var productnamecn = Ext.getCmp('add_inventory_productnamecn').getValue();
                            var dimension = Ext.getCmp('add_inventory_dimension').getValue();
                            var unit = Ext.getCmp('add_inventory_unit').getValue();
                            var unitcost = Ext.getCmp('add_inventory_unitcost').getValue();
                            var receivedby = Ext.getCmp('add_inventory_receivedby').getValue();
                            var deadline = Ext.getCmp('add_inventory_deadline').getValue();
                            var receiveddate = Ext.getCmp('add_inventory_receiveddate').getValue();
                            var person = Ext.getCmp('add_inventory_person').getValue();
                            var tel = Ext.getCmp('add_inventory_tel').getValue();
                            var remark = Ext.getCmp('add_inventory_remark').getValue();
                            var description = Ext.getCmp('add_inventory_description').getValue();


                            var sUrl = "/" + APP_NAME + "/inventory.aspx/add";
                            var xParameter = { cid: cid, category: category, productno: productno, productnameen: productnameen, productnamecn: productnamecn,
                                dimension: dimension, unit: unit, unitcost: unitcost, receivedby: receivedby, deadline: deadline, receiveddate: receiveddate,
                                person: person, tel: tel, remark: remark, description: description
                            };

                            LoadData(sUrl, xParameter, onAddInventoryReceived);

                            function onAddInventoryReceived(data) {

                                if (data.success == "false") {
                                    Ext.Msg.show({
                                        title: 'Fingerprint',
                                        msg: data.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                } else {

                                    if (cid != "--") {
                                        Ext.getCmp('newadmin-form-panel').collapse();
                                        Ext.getCmp('inventory-inventorygrid').getStore().reload();
                                    } else {
                                        Ext.getCmp('add_inventory_objectid').setValue(data.objectid);
                                        var consumption = Ext.getCmp('add_consumption_panel');
                                        consumption.hide();
                                        Ext.getCmp('new_consumption_panel').show();

                                        //show consumption items
                                        var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
                                        orderStore.load({
                                            params: {
                                                cid: data.objectid
                                            }
                                        });

                                    }

                                    Ext.Msg.show({
                                        title: 'Fingerprint',
                                        msg: data.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                }
                            }
                        }
                    }, { text: 'Cancel',
                        handler: function() {
                            Ext.getCmp('newadmin-form-panel').collapse();

                            Ext.getCmp('inventory-inventorygrid').getStore().reload();
                        }
                    }
                ]
                                });






                                function setYourLocation(val) {
                                    var a = Ext.getCmp('your-admin-location');
                                    var location = "<a href='#' class='leftstyle1'>Inventory</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
                                    try {
                                        a.el.dom.innerHTML = location;
                                    }
                                    catch (e)
            { }

                                    a = Ext.getCmp('your-admin-location2');
                                    location = "<a href='#' class='leftstyle1'>Inventory</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
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
        editInventory();
    }

    function newInventory() {

        var cid = Ext.getCmp('add_inventory_objectid');
        var category = Ext.getCmp('add_inventory_category');
        var productno = Ext.getCmp('add_inventory_productno');
        var productnameen = Ext.getCmp('add_inventory_productnameen');
        var productnamecn = Ext.getCmp('add_inventory_productnamecn');
        var dimension = Ext.getCmp('add_inventory_dimension');
        var unit = Ext.getCmp('add_inventory_unit');
        var unitcost = Ext.getCmp('add_inventory_unitcost');
        var receivedby = Ext.getCmp('add_inventory_receivedby');
        var deadline = Ext.getCmp('add_inventory_deadline');
        var receiveddate = Ext.getCmp('add_inventory_receiveddate');
        var person = Ext.getCmp('add_inventory_person');
        var tel = Ext.getCmp('add_inventory_tel');
        var remark = Ext.getCmp('add_inventory_remark');
        var description = Ext.getCmp('add_inventory_description');
        var date = new Date().getDate();
        cid.setValue("--");
        category.setValue("");
        productno.setValue("");
        productnameen.setValue("");
        productnamecn.setValue("");
        dimension.setValue("");
        unit.setValue("PCS");
        unitcost.setValue("");
        receivedby.setValue("");
        deadline.setValue(date);
        person.setValue("");
        tel.setValue("");
        remark.setValue("");
        receiveddate.setValue(date);
        remark.setValue("");
        description.setValue("");

        Ext.getCmp('newadmin-form-panel').expand();

        var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
        orderStore.load({
            params: {
                cid: 0
            }
        });
        
      // var consumption = Ext.getCmp('add_consumption_panel');
      //  consumption.hide();
      //  Ext.getCmp('new_consumption_panel').hide();
        
        
    }


    function editInventory() {
        var grid = Ext.getCmp('inventory-inventorygrid');
       
        var selectModel = grid.getSelectionModel();
        var rec = selectModel.getSelected();

        

        if (rec == undefined || rec.length == 0) {
            Ext.Msg.alert('Fingerprint', 'Please select a record');
            return;
        }


       // var consumption = Ext.getCmp('add_consumption_panel');
      //  consumption.hide();
      //  Ext.getCmp('new_consumption_panel').show();
        
       //show consumption items
        var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
        orderStore.load({
            params: {
                cid: rec.data.objectid
            }
        });

        var cid = Ext.getCmp('add_inventory_objectid');
        var category = Ext.getCmp('add_inventory_category');
        var productno = Ext.getCmp('add_inventory_productno');
        var productnameen = Ext.getCmp('add_inventory_productnameen');
        var productnamecn = Ext.getCmp('add_inventory_productnamecn');
        var dimension = Ext.getCmp('add_inventory_dimension');
        var unit = Ext.getCmp('add_inventory_unit');
        var unitcost = Ext.getCmp('add_inventory_unitcost');
        var receivedby = Ext.getCmp('add_inventory_receivedby');
        var deadline = Ext.getCmp('add_inventory_deadline');
        var receiveddate = Ext.getCmp('add_inventory_receiveddate');
        var person = Ext.getCmp('add_inventory_person');
        var tel = Ext.getCmp('add_inventory_tel');
        var remark = Ext.getCmp('add_inventory_remark');
        var description = Ext.getCmp('add_inventory_description');

        cid.setValue(rec.data.objectid);
        category.setValue(rec.data.category);
        productno.setValue(rec.data.productno);
        productnameen.setValue(rec.data.productnameen);
        productnamecn.setValue(rec.data.productnamecn);
        dimension.setValue(rec.data.dimension);
        unit.setValue(rec.data.unit);
        unitcost.setValue(rec.data.unitcost);
        receivedby.setValue(rec.data.receivedby);
        deadline.setValue(rec.data.deadline);
        person.setValue(rec.data.person);
        tel.setValue(rec.data.tel);
        remark.setValue(rec.data.remark);
        receiveddate.setValue(rec.data.receiveddate);
        remark.setValue(rec.data.remark);
        description.setValue(rec.data.description);
        
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
