<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Delivery
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    <div id="fingerprint-customer-body"></div>
    <div id="fingerprint-customer-left" class="x-hide-display">
      
    </div>
    
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="admin-customer-link" href ="delivery"  class="menubar_click"
      
      >Monitor</a></li>
    </ul>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="styleSheetContent" runat="server">
<link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-15.css") %>/>
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
            background-color: #cff9c3 ! important;
        }
        
        #newadmin-addAdmin-panel .x-panel-body {
            background-color: #cff9c3 ! important;
        }
        #newdelivery-form-panel .x-panel-body {
            background-color: #cff9c3 ! important;
        }
        
        #neworder-toolbar-panel .x-panel-body
        {
        	 background-color: #cff9c3 ! important;
        }
        
        #neworder-filter-panel .x-panel-body
        {
        	 background-color: #cff9c3 ! important;
        }
        
        #customer-centerPanel .x-panel-body {
            background-color: #cff9c3 ! important;
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

         deliveryStore = new Ext.data.JsonStore({
             // store configs
             autoDestroy: true,
             url: "/" + APP_NAME + "/delivery.aspx/delivery",
             remoteSort: true,
             sortInfo: {
                 field: 'objectid',
                 direction: 'desc'
             },
             storeId: 'deliveryStore',

             // reader configs
             idProperty: 'objectid',
             root: 'data',
             totalProperty: 'total',
             fields: [{
                 name: 'objectid',
                 type: 'String'
             }, {
                 name: 'customerid',
                 type: 'String'
             }, {
                 name: 'number',
                 type: 'string'
             }, {
                 name: 'company_code',
                 type: 'String'
             }, {
                 name: 'company_name',
                 type: 'String'
             }, {
                 name: 'district',
                 type: 'String'

             }, {
                 name: 'delivery_type',
                 type: 'String'
             }, {
                 name: 'date',
                 type: 'String'
             }, {
                 name: 'time',
                 type: 'String'
             }, {
                 name: 'handledby',
                 type: 'String'
             }, {
                 name: 'status',
                 type: 'String'
             }, {
                 name: 'partno',
                 type: 'String'
             }, {
                 name: 'nonorder',
                 type: 'String'
             }, {
                 name: 'length',
                 type: 'String'
             }, {
                 name: 'width',
                 type: 'String'
             }, {
                 name: 'height',
                 type: 'String'
             }, {
                 name: 'weight',
                 type: 'String'
             }, {
                 name: 'goods_type',
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
                 name: 'city',
                 type: 'String'
             }, {
                 name: 'tel',
                 type: 'String'
             }, {
                 name: 'mobile',
                 type: 'String'
             }, {
                 name: 'remark',
                 type: 'String'
             }, {
                 name: 'contact',
                 type: 'String'
             }, {
                 name: 'requestby',
                 type: 'String'
             }, {
                 name: 'handledbyid',
                 type: 'String'
             }, {
                 name: 'deadline',
                 type: 'String'
             }, {
                 name: 'notes',
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
                 dataIndex: 'number',
                 type: 'String'
             }, {
                 dataIndex: 'company_name',
                 type: 'String'
             }, {
                 dataIndex: 'district',
                 type: 'String'

             }, {
                 dataIndex: 'delivery_type',
                 type: 'String'
             }, {
                 dataIndex: 'date',
                 type: 'String'
             }, {
                 dataIndex: 'time',
                 type: 'String'
             }, {
                 dataIndex: 'handledby',
                 type: 'String'
             }, {
                 dataIndex: 'status',
                 type: 'String'
             }, {
                 dataIndex: 'partno',
                 type: 'String'
             }, {
                 dataIndex: 'nonorder',
                 type: 'String'
             }, {
                 dataIndex: 'length',
                 type: 'String'
             }, {
                 dataIndex: 'width',
                 type: 'String'
             }, {
                 dataIndex: 'height',
                 type: 'String'
             }, {
                 dataIndex: 'weight',
                 type: 'String'
             }, {
                 dataIndex: 'goods_type',
                 type: 'String'
             }
				]
         });

         var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
         var createColModel = function(finish, start) {
             var columns = [sm,
                {
                    dataIndex: 'objectid',
                    header: 'Delivery ID.',
                    filterable: true,
                    renderer: adminidRenderer
                },
                   {
                       dataIndex: 'number',
                       header: 'Order No.',
                       filterable: true,
                       filter: { type: 'string' }
                   }, {
                       dataIndex: 'company_name',
                       header: 'Customer Name.',
                       filterable: true
                   }, {
                       dataIndex: 'district',
                       header: 'District',
                       filterable: true
                   }, {
                       dataIndex: 'delivery_type',
                       header: 'Delivery Type',
                       filterable: true
                   }, {
                       dataIndex: 'date',
                       header: 'Date.',
                       filterable: true
                   }, {
                       dataIndex: 'time',
                       header: 'Time.',
                       filterable: true
                   }, {
                       dataIndex: 'handledby',
                       header: 'Handled by',
                       filterable: true
                   }, {
                       dataIndex: 'status',
                       header: 'Status.',
                       filterable: true
                   }
				]; return new Ext.grid.ColumnModel({
				    columns: columns.slice(start || 0, finish),
				    defaults: {
				        sortable: true
				    }
				});
         };



         var delivery_filter_panel = new Ext.FormPanel({
             id: 'neworder-filter-panel',
             title: '',
             labelAlign: 'right',
             defaultType: 'textfield',
             layout: 'absolute',
             height: 50,
             labelWidth: 60,
             items: [
                    {
                        xtype: 'box',
                        x: 10,
                        y: 4,
                        html: 'Delivery Type:'

                    },
                    {
                        xtype: 'combo', id: 'filter-itemtype',
                        fieldLabel: 'Job Type',
                        width: 100,
                        x: 80,
                        y: 2,
                        mode: 'local',
                        store: [["Send", "Send"], ["Receive", "Receive"]],
                        displayField: 'name',
                        valueField: 'id',
                        forceSelection: true,
                        triggerAction: 'all',
                        hiddenName: 'itemtype',
                        listeners: {
                            select: {
                                fn: function(combo, value) {

                                }
                            }
                        }

                    },


                    {
                        xtype: 'radiogroup',
                        fieldLabel: 'Order Status',
                        id: 'neworder-status-rg',
                        name: 'neworder-filter-status-all',
                        border: false,
                        x: 210,
                        y: 2,
                        width: 350,
                        items: [{
                            value: "0",
                            inputValue: "0",

                            name: 'neworder-filter-status',
                            boxLabel: 'Pending'
                        }, {
                            value: "1",
                            inputValue: "1",
                            name: 'neworder-filter-status',
                            boxLabel: 'Processing'
                        }, {
                            value: "2",
                            inputValue: "2",
                            name: 'neworder-filter-status',
                            boxLabel: 'Finish'
                        }, {
                            value: "3",
                            inputValue: "3",
                            name: 'neworder-filter-status',
                            checked: true,
                            boxLabel: 'All'
                        }
                    ]
                    },
                    {
                        xtype: 'radiogroup',
                        fieldLabel: '',
                        hideLabel: true,
                        id: 'neworder-filter-type-rg',
                        name: 'neworder-filter-type-all',
                        border: false,
                        x: 10,
                        y: 22,
                        width: 400,
                        items: [{
                            value: 0,
                            inputValue: 0,
                            checked: true,
                            name: 'neworder-filter-type',
                            boxLabel: 'Customer Code'
                        }, {
                            value: 1,
                            inputValue: 1,
                            name: 'neworder-filter-type',
                            boxLabel: 'Customer Name'
                        }, {
                            value: 2,
                            inputValue: 2,
                            name: 'neworder-filter-type',
                            boxLabel: 'Delivery ID.'
                        }, {
                            value: 3,
                            inputValue: 3,
                            name: 'neworder-filter-type',
                            boxLabel: 'Job No.'
                        }
                    ]
                    },

                    {
                        x: 420,
                        y: 22,
                        xtype: 'textfield',
                        id: 'neworder-filter-value',
                        name: 'neworder-filter-value',
                        hideLabel: true
                    }
                    ,

                    {
                        x: 560,
                        y: 17,
                        xtype: 'buttongroup',
                        items: [
                        {
                            text: 'Search',
                            handler: searchOrder
                        }
                        ]
                    }

                ]

         });


         var order_toolbar_panel = new Ext.Panel({
             id: 'neworder-toolbar-panel',
             title: '',
             layout: 'hBox',
             items: [
                   { margins: '5 5 0 5',
                       xtype: 'buttongroup',
                       hidden: false,
                       items: [{
                           text: 'New Delivery',
                           handler: newAdmin
}]
                       }, { margins: '5 5 0 5',
                           xtype: 'buttongroup',
                           items: [{
                               text: 'Edit',
                               handler: editAdmin
}]
                           }, { margins: '5 5 0 5',
                               xtype: 'buttongroup',
                               items: [{
                                   text: 'Delete',
                                   handler: deleteCustomer
}]
                               }, {
                                   xtype: 'combo',
                                   fieldLabel: 'Delivery Type:',
                                   value: 'change selected delivery type',
                                   id: 'select_delivery_type',
                                   mode: 'local',
                                   store: [["Send", "Send"], ["Receive", "Receive"]],
                                   editable: false,
                                   margins: '10 5 10 5',
                                   defaults: { margins: '10 0 0 0' },
                                   width: 200,
                                   forceSelection: true,
                                   displayField: 'name',
                                   valueField: 'id',
                                   triggerAction: 'all',
                                   hiddenName: 'delivery_type',
                                   anyMatch: true,
                                   listeners: {
                                       select: {
                                           fn: function(combo, value) {
                                               // var rec = combo.getValue();
                                               //  alert(rec);
                                               //  combo.setValue("Pending");

                                               var grid = Ext.getCmp('delivery-deliverygrid');
                                               var selectModel = grid.getSelectionModel();
                                               var rec = selectModel.getSelected();

                                               if (rec == undefined || rec.length == 0) {
                                                   Ext.Msg.alert('Fingerprint', 'Please select a record');

                                                   combo.reset();
                                                   return;
                                               }

                                               var sUrl = "/" + APP_NAME + "/delivery.aspx/updatedelivery";
                                               var xParameter = { id: rec.data.objectid, name: "type", value: combo.getValue() };
                                               LoadData(sUrl, xParameter, onDeleteAdminReceived);

                                               function onDeleteAdminReceived(data) {
                                                   grid.getStore().reload();

                                                   Ext.Msg.show({
                                                       title: 'Fingerprint',
                                                       msg: data.result,
                                                       buttons: Ext.Msg.OK,
                                                       icon: Ext.Msg.INFO
                                                   });
                                               }

                                               // grid.getStore().reload();

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

                               }, {
                                   xtype: 'combo',
                                   fieldLabel: 'Handledby:',
                                   value: 'change selected Handledby',
                                   id: 'select_delivery_handledby',
                                   margins: '10 5 10 5',
                                   defaults: { margins: '10 0 0 0' },
                                   mode: 'local',
                                   store: new Ext.data.JsonStore({
                                       url: "/" + APP_NAME + "/order.aspx/getSalesComboList",
                                       fields: ['id', 'name'],
                                       root: 'tags',
                                       autoLoad: true
                                   }),
                                   editable: false,
                                   width: 200,
                                   forceSelection: true,
                                   displayField: 'name',
                                   valueField: 'id',
                                   triggerAction: 'all',
                                   hiddenName: 'handledby',
                                   anyMatch: true,
                                   listeners: {
                                       select: {
                                           fn: function(combo, value) {
                                               // var rec = combo.getValue();
                                               //  alert(rec);
                                               //  combo.setValue("Pending");

                                               var grid = Ext.getCmp('delivery-deliverygrid');
                                               var selectModel = grid.getSelectionModel();
                                               var rec = selectModel.getSelected();

                                               if (rec == undefined || rec.length == 0) {
                                                   Ext.Msg.alert('Fingerprint', 'Please select a record');

                                                   combo.reset();
                                                   return;
                                               }

                                               var sUrl = "/" + APP_NAME + "/delivery.aspx/updatedelivery";
                                               var xParameter = { id: rec.data.objectid, name: "user", value: combo.getValue() };
                                               LoadData(sUrl, xParameter, onDeleteAdminReceived);

                                               function onDeleteAdminReceived(data) {
                                                   grid.getStore().reload();

                                                   Ext.Msg.show({
                                                       title: 'Fingerprint',
                                                       msg: data.result,
                                                       buttons: Ext.Msg.OK,
                                                       icon: Ext.Msg.INFO
                                                   });
                                               }

                                               // grid.getStore().reload();

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

                               }, {
                                   xtype: 'combo',
                                   fieldLabel: 'status:',
                                   value: 'change selected status',
                                   id: 'select_delivery_status',
                                   margins: '10 5 10 5',
                                   defaults: { margins: '10 0 10 0' },
                                   mode: 'local',
                                   store: [["Pending", "Pending"], ["Processing", "Processing"], ["Finish", "Finish"]],
                                   editable: false,
                                   forceSelection: true,
                                   width: 200,
                                   displayField: 'name',
                                   valueField: 'id',
                                   triggerAction: 'all',
                                   hiddenName: 'status',
                                   anyMatch: true,
                                   listeners: {
                                       select: {
                                           fn: function(combo, value) {
                                               var grid = Ext.getCmp('delivery-deliverygrid');
                                               var selectModel = grid.getSelectionModel();
                                               var rec = selectModel.getSelected();

                                               if (rec == undefined || rec.length == 0) {
                                                   Ext.Msg.alert('Fingerprint', 'Please select a record');

                                                   combo.reset();
                                                   return;
                                               }

                                               var sUrl = "/" + APP_NAME + "/delivery.aspx/updatedelivery";
                                               var xParameter = { id: rec.data.objectid, name: "status", value: combo.getValue() };
                                               LoadData(sUrl, xParameter, onDeleteAdminReceived);

                                               function onDeleteAdminReceived(data) {
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
                ]
         });


         //customer grid
         var deliveryGrid = new Ext.grid.GridPanel({
             id: 'delivery-deliverygrid',
             border: false,
             store: deliveryStore,
             height: '100%',
             colModel: createColModel(10),
             selModel: sm,
             loadMask: true,
             plugins: [filters],
             stripeRows: true,
             flex: 10,
             listeners: {
                 render: {
                     fn: function() {
                         deliveryStore.load({
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
                 rowclick: { fn: function() {
                     Ext.getCmp('select_delivery_type').setValue("change selected delivery type");
                     Ext.getCmp('select_delivery_handledby').setValue("change selected handledby");
                     Ext.getCmp('select_delivery_status').setValue("change selected status");
                 }
                 },
                 rowdblclick: onClick
             },
             bbar: new Ext.PagingToolbar({
                 store: deliveryStore,
                 pageSize: 20,
                 plugins: [filters],
                 displayInfo: true,
                 displayMsg: 'Displaying record {0} - {1} of {2}',
                 emptyMsg: "No record to display"
             }),
             tbar: {
                 layout: 'anchor',
                 items: [
order_toolbar_panel,

             delivery_filter_panel]
             }
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
                        html: "<a href='#' class='leftstyle1'>Delivery</a> -> <a href='#' class='leftstyle1'>Monitor</a>"
                    },
                    deliveryGrid
                ]
         });




         var deleteCustomerWin;
         function deleteCustomer() {
             var grid = Ext.getCmp('delivery-deliverygrid');
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

                        var grid = Ext.getCmp('delivery-deliverygrid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();

                        if (rec == undefined || rec.length == 0) {
                            Ext.Msg.alert('Fingerprint', 'Please select a record');
                            return;
                        }

                        var sUrl = "/" + APP_NAME + "/delivery.aspx/deletedelivery";
                        var xParameter = { ids: rec.data.objectid, pwd: pwd };
                        LoadData(sUrl, xParameter, onDeleteAdminReceived);

                        function onDeleteAdminReceived(data) {

                            deleteCustomerWin.hide();

                            Ext.getCmp('delivery-deliverygrid').getStore().reload();

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

         ///////////////////////////////

         var jobStore = new Ext.data.JsonStore({
             storeId: "jobstore",
             url: "/" + APP_NAME + "/delivery.aspx/getcontactbydeliveryid",
             idProperty: 'contact_objectid',
             root: 'data',
             totalProperty: 'total',
             fields: [
               { name: 'objectid', type: 'string' },
		       { name: 'company_code', type: 'string' },
               { name: 'company_name', type: 'string' },
               { name: 'contact_objectid', type: 'string' },
               { name: 'contact_person', type: 'string' },
		       { name: 'contact_tel', type: 'string' },
		       { name: 'deliveryid', type: 'string' },
		       { name: 'street1', type: 'string' },
		       { name: 'street2', type: 'string' },
		       { name: 'street3', type: 'string' },
		       { name: 'district', type: 'string' },
		       { name: 'city', type: 'string' },
		       { name: 'mobile', type: 'string' },
		      { name: 'contact_address', type: 'string' },
            ]
         });

         var jobGrid = new Ext.grid.GridPanel({
             id: 'neworder-grid-newjob',
             store: jobStore,
             columns: [sm,
                { header: 'Contact Objectid', sortable: true, dataIndex: 'contact_objectid' },
			    { header: 'Contact Person', sortable: true, dataIndex: 'contact_person' },
                { header: 'Contact Tel', sortable: true, dataIndex: 'contact_tel' },
                { header: 'Street1', sortable: true, dataIndex: 'street1' },
                { header: 'Street2', sortable: true, dataIndex: 'street2' },
                { header: 'Street3', sortable: true, dataIndex: 'street3' },
                { header: 'City', sortable: true, dataIndex: 'city' },
                 { header: 'Mobile', sortable: true, dataIndex: 'mobile' }
            ],
             stripeRows: true,
             anchor: "90%",
             //  autoHeight: true,
             height: 200,
             stateful: true,
             selModel: sm,
             sm: new Ext.grid.RowSelectionModel({
                 singleSelect: true
             }),
             stateId: 'jobGrid',
             listeners: {
                 contextMenu: {
                     fn: function(e) {
                         e.stopEvent();
                         //filingMenu.showAt(e.xy);
                     }
                 },
                 rowclick: function(grid, rowIndex, e) {
                     var grid = jobGrid;
                     var selectModel = grid.getSelectionModel();
                     var rec = selectModel.getSelected();

                     if (rec == undefined || rec.data.length == 0) {
                         Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                         return;
                     }

                     var name = Ext.getCmp('add_delivery_company_name');
                     var street1 = Ext.getCmp('add_delivery_street1');
                     var street2 = Ext.getCmp('add_delivery_street2');
                     var street3 = Ext.getCmp('add_delivery_street3');
                     var district = Ext.getCmp('add_delivery_district');
                     var city = Ext.getCmp('add_delivery_city');
                     var contact = Ext.getCmp('add_delivery_contact');
                     var tel = Ext.getCmp('add_delivery_tel');
                     var mobile = Ext.getCmp('add_delivery_mobile');
                     var remark = Ext.getCmp('add_delivery_remark');
                     var code = Ext.getCmp('add_customer_company_code');

                     street1.setValue(rec.data.street1);
                     street2.setValue(rec.data.street2);
                     street3.setValue(rec.data.street3);

                     district.setValue(rec.data.district);
                     city.setValue(rec.data.city);
                     contact.setValue(rec.data.contact_person);
                     tel.setValue(rec.data.contact_tel);
                     mobile.setValue(rec.data.mobile);

                 }
             },

             tbar: [
            {
                xtype: 'buttongroup',
                items: [{
                    text: 'Add',
                    handler: function() {

                        var sUrl = "/" + APP_NAME + "/delivery.aspx/addcustomer";

                        var objectid = Ext.getCmp('add_delivery_objectid').getValue();

                        var code = Ext.getCmp('add_customer_company_code').getValue();
                        var name = Ext.getCmp('add_delivery_company_name').getValue();
                        var street1 = Ext.getCmp('add_delivery_street1').getValue();
                        var street2 = Ext.getCmp('add_delivery_street2').getValue();
                        var street3 = Ext.getCmp('add_delivery_street3').getValue();
                        var district = Ext.getCmp('add_delivery_district').getValue();
                        var city = Ext.getCmp('add_delivery_city').getValue();
                        var contact = Ext.getCmp('add_delivery_contact').getValue();
                        var tel = Ext.getCmp('add_delivery_tel').getValue();
                        var mobile = Ext.getCmp('add_delivery_mobile').getValue();
                        var customerid = Ext.getCmp('add_customer_company_code').getValue();


                        var xParameter = { objectid: objectid, name: name, street1: street1, street2: street2, street3: street3, district: district, city: city, contact: contact, tel: tel, mobile: mobile,
                            customerid: customerid
                        };
                        LoadData(sUrl, xParameter, onDeleteAdminReceived);

                        function onDeleteAdminReceived(data) {

                            if (data.success == false) {
                                Ext.Msg.show({
                                    title: 'Fingerprint',
                                    msg: data.result,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO
                                });
                            } else {


                                // Ext.getCmp('neworder-grid-newjob').getStore().reload();
                                var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
                                orderStore.load({
                                    params: {
                                        cid: code,
                                        did: objectid
                                    }
                                });

                                Ext.Msg.show({
                                    title: 'Fingerprint',
                                    msg: data.result,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO
                                });
                            }
                        }


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

                                var grid = Ext.getCmp('neworder-grid-newjob');
                                var selectModel = grid.getSelectionModel();
                                var rec = selectModel.getSelected();
                                if (rec == undefined || rec.data.length == 0) {
                                    Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                                    return;
                                }
                                var sUrl = "/" + APP_NAME + "/delivery.aspx/deletecustomercontact";
                                var xParameter = { pid: rec.data.contact_objectid };
                                LoadData(sUrl, xParameter, onDeleteAdminReceived);
                                function onDeleteAdminReceived(data) {
                                    grid.getStore().reload();
                                    Ext.Msg.show({
                                        title: 'Fingerprint',
                                        msg: data.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                }


//                                if (!deleteJobWin) {
//                                    var deleteJobPanel = new Ext.FormPanel({
//                                        layout: 'form',
//                                        buttonAlign: 'center',
//                                        id: 'deleteJobForm',
//                                        labelWidth: 200,
//                                        baseCls: 'x-plain',
//                                        items: [
//                                            {
//                                                xtype: 'textfield',
//                                                name: 'password',
//                                                inputType: 'password',
//                                                id: 'delete-job-password',
//                                                fieldLabel: 'Please enter your password'
//                                            }

//                                        ],
//                                        buttons: [
//                                            {
//                                                text: 'OK',
//                                                handler: function() {
//                                                    var pwd = Ext.getCmp('delete-job-password').getValue();
//                                                    if (pwd == "")
//                                                        return;
//                                                    var grid = Ext.getCmp('neworder-grid-newjob');
//                                                    var selectModel = grid.getSelectionModel();
//                                                    var rec = selectModel.getSelected();

//                                                    if (rec == undefined || rec.data.length == 0) {
//                                                        Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
//                                                        return;
//                                                    }

//                                                    var sUrl = "/" + APP_NAME + "/delivery.aspx/deletecustomercontact";

//                                                    var xParameter = { pid: rec.data.contact_objectid, pwd: pwd };

//                                                    LoadData(sUrl, xParameter, onDeleteAdminReceived);

//                                                    function onDeleteAdminReceived(data) {

//                                                        deleteJobWin.hide();

//                                                        grid.getStore().reload();
//                                                        Ext.Msg.show({
//                                                            title: 'Fingerprint',
//                                                            msg: data.result,
//                                                            buttons: Ext.Msg.OK,
//                                                            icon: Ext.Msg.INFO
//                                                        });
//                                                    }
//                                                }
//                                            },
//                                            {
//                                                text: 'Cancel',
//                                                handler: function() {
//                                                    deleteJobWin.hide();
//                                                }
//                                            }
//                                        ]
//                                    });


//                                    deleteJobWin = new Ext.Window({
//                                        title: 'Fingerprint',
//                                        layout: 'fit',
//                                        width: 400,
//                                        height: 100,
//                                        closeAction: 'hide',
//                                        plain: true,
//                                        items: deleteJobPanel
//                                    });
//                                }
//                                Ext.getCmp('delete-job-password').setValue('');
//                                deleteJobWin.show();
                            }
                        }
                ]
            }
            ]
         });
         var deleteJobWin;




         var addDeliveryPanel = new Ext.FormPanel({
             id: 'newadmin-addadmin-panel',
             url: "/" + APP_NAME + "/delivery.aspx/add",
             defaultType: 'textfield',
             layout: 'column',
             containerScroll: true,
             autoScroll: true,
             labelAlign: 'right',
             buttonAlign: 'left',
             anchor: '90%',
             items: [{
                 xtype: 'container',
                 autoEL: {},
                 columnWidth: 1,
                 anchor: '100%',
                 items: [
	                        {
	                            id: 'your-customer-location2',
	                            xtype: 'box',
	                            anchor: '100%',
	                            html: "<a href='#' class='leftstyle1'>Delivery</a> -> <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
             }, {
                 xtype: 'container',
                 autoEl: {},
                 id: 'part1',
                 columnWidth: 1,
                 anchor: '90%',
                 items: {
                     title: 'Part I - Item Information',
                     collapsible: true,
                     collapsed: false,
                     layout: 'column',
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
                        fieldLabel: 'Delivery ID',
                        name: 'objectid',
                        id: 'add_delivery_objectid',
                        anchor: '70%',
                        value: '--',
                        readOnly: true
                    }
                },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'radio',
                        name: 'delivery_order_number',
                        id: 'add_check_order',
                        boxLabel: 'order no',
                        handler: function() {
                            if (this.checked == true) {
                                var number = Ext.getCmp('add_delivery_number');
                                var nonorder = Ext.getCmp('add_delivery_nonorder');
                                number.show();
                                nonorder.hide();
                                nonorder.setValue("");
                            }
                        }
                    }
                }, {

                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'radio',
                        name: 'delivery_order_number',
                        id: 'add_check_order1',
                        boxLabel: 'non order',
                        handler: function() {
                            if (this.checked == true) {
                                var number = Ext.getCmp('add_delivery_number');
                                var nonorder = Ext.getCmp('add_delivery_nonorder');
                                number.hide();
                                nonorder.show();
                                number.setValue("");
                            }
                        }
                    }
                },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        xtype: 'combo',
                        fieldLabel: 'Order No',
                        value: '',
                        id: 'add_delivery_number',
                        mode: 'remote ',
                        minChars: 0,
                        store: new Ext.data.JsonStore({
                            url: "/" + APP_NAME + "/delivery.aspx/GetOrderItemsDetails",
                            fields: ['pid', 'item_details', 'code', 'name', 'person', 'address', 'city', 'district', 'email', 'fax', 'mobile', 'street1', 'street2', 'street3', 'tel'],
                            root: 'tags',
                            autoLoad: true
                        }),
                        editable: true,
                        forceSelection: true,
                        displayField: 'pid',
                        valueField: 'pid',
                        triggerAction: 'all',
                        hiddenName: 'pid',
                        anyMatch: true,
                        listeners: {
                            select: {
                                fn: function(combo, value) {
                                    var remark = Ext.getCmp('add_delivery_remark');
                                    var name = Ext.getCmp('add_delivery_company_name');
                                    var street1 = Ext.getCmp('add_delivery_street1');
                                    var street2 = Ext.getCmp('add_delivery_street2');
                                    var street3 = Ext.getCmp('add_delivery_street3');
                                    var district = Ext.getCmp('add_delivery_district');
                                    var city = Ext.getCmp('add_delivery_city');
                                    var contact = Ext.getCmp('add_delivery_contact');
                                    var tel = Ext.getCmp('add_delivery_tel');
                                    var mobile = Ext.getCmp('add_delivery_mobile');
                                    var remark = Ext.getCmp('add_delivery_remark');
                                    var code = Ext.getCmp('add_customer_company_code');
                                    var rec = combo.getValue();
                                    var deliveryid = Ext.getCmp('add_delivery_objectid');
                                    for (var i = 0; i < combo.store.getCount(); i++) {
                                        var record = combo.store.getAt(i);
                                        if (record.get('pid') == rec) {
                                            remark.setValue(record.get('item_details'));

                                            if (deliveryid.getValue() == '--') {

                                                name.setValue(record.get('name'));
                                                if (record.get('street1') == "") {
                                                    street1.setValue(record.get('address'));
                                                } else {
                                                    street1.setValue(record.get('street1'));
                                                }
                                                street2.setValue(record.get('street2'));
                                                street3.setValue(record.get('street3'));
                                                district.setValue(record.get('district'));
                                                city.setValue(record.get('city'));
                                                contact.setValue(record.get('contact'));
                                                tel.setValue(record.get('tel'));
                                                mobile.setValue(record.get('mobile'));
                                                code.setValue(record.get('code'));
                                                //  fax.setValue(record.get('fax'));
                                                //  alert(record.get('name'));
                                            }
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
                },
               {
                   xtype: 'container',
                   autoEl: {},
                   columnWidth: 0.5,
                   layout: 'form',
                       hidden:true ,
                   items: {
                       xtype: 'textfield',
                       fieldLabel: 'Part No',
                       name: 'partno',
                       id: 'add_delivery_partno',
                       anchor: '70%',
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
                           fieldLabel: 'Non-Order',
                           name: 'nonorder',
                           id: 'add_delivery_nonorder',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       hidden: true,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Length(cm)',
                           name: 'length',
                           id: 'add_delivery_length',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       hidden: true,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Width(cm)',
                           name: 'width',
                           id: 'add_delivery_width',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       hidden: true,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Height(cm)',
                           name: 'height',
                           id: 'add_delivery_height',
                           anchor: '70%',
                           readOnly: false
                       }
                   }, {
                       xtype: 'container',
                       autoEl: {},
                       hidden: true,
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'textfield',
                           fieldLabel: 'Weight(lbs)',
                           name: 'weight',
                           id: 'add_delivery_weight',
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
                           fieldLabel: 'Type',
                           value: 'File',
                           id: 'add_delivery_goods_type',
                           mode: 'local',
                           store: [["File", "File"], ["Box", "Box"]],
                           editable: false,
                           forceSelection: true,
                           displayField: 'name',
                           valueField: 'id',
                           triggerAction: 'all',
                           hiddenName: 'goods_type',
                           anyMatch: true,
                           listeners: {
                               select: {
                                   fn: function(combo, value) {

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
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'combo',
                           fieldLabel: 'Delivery Type',
                           value: 'Send',
                           id: 'add_delivery_type',
                           mode: 'local',
                           store: [["Send", "Send"], ["Receive", "Receive"]],
                           editable: false,
                           forceSelection: true,
                           displayField: 'name',
                           valueField: 'id',
                           triggerAction: 'all',
                           hiddenName: 'delivery_type',
                           anyMatch: true,
                           listeners: {
                               select: {
                                   fn: function(combo, value) {

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

                   }, { xtype: 'container',
                       autoEl: {},
                       columnWidth: 0.5,
                       layout: 'form',
                       items: {
                           xtype: 'combo',
                           fieldLabel: 'Status',
                           value: 'Processing',
                           id: 'add_delivery_status',
                           margins: '10 5 10 5',
                           defaults: { margins: '10 0 10 0' },
                           mode: 'local',
                           store: [["Pending", "Pending"], ["Processing", "Processing"], ["Finish", "Finish"]],
                           editable: false,
                           forceSelection: true,
                           width: 200,
                           displayField: 'name',
                           valueField: 'id',
                           triggerAction: 'all',
                           hiddenName: 'status',
                           anyMatch: true,
                           listeners: {
                               select: {
                                   fn: function(combo, value) {

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

                   }
                        ]
                 }
             }, {
                 xtype: 'container',
                 autoEl: {}
	                ,
                 columnWidth: 1,
                 anchor: '90%',
                 id: 'part2',
                 items: { title: 'Part II - Shipping Address',
                     collapsible: true,
                     layout: 'column',
                     collapsed: false,
                     anchor: '90%',
                     items: [{
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'box',
                             html: '<br/>'
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'combo',
                             fieldLabel: 'Company Code',
                             value: '',
                             id: 'add_customer_company_code',
                             mode: 'remote ',
                             minChars: 0,
                             store: new Ext.data.JsonStore({
                                 url: "/" + APP_NAME + "/delivery.aspx/GetCustomers",
                                 fields: ['objectid', 'code', 'name', 'street1', 'street2', 'street3', 'district', 'city', 'contact', 'tel', 'mobile', 'remark'],
                                 root: 'tags',
                                 autoLoad: true
                             }),
                             editable: true,
                             forceSelection: true,
                             displayField: 'code',
                             valueField: 'code',
                             triggerAction: 'all',
                             hiddenName: 'code',
                             anyMatch: true,
                             listeners: {
                                 select: {
                                     fn: function(combo, value) {

                                         var rec = combo.getValue();
                                         var name = Ext.getCmp('add_delivery_company_name');
                                         var street1 = Ext.getCmp('add_delivery_street1');
                                         var street2 = Ext.getCmp('add_delivery_street2');
                                         var street3 = Ext.getCmp('add_delivery_street3');
                                         var district = Ext.getCmp('add_delivery_district');
                                         var city = Ext.getCmp('add_delivery_city');
                                         var contact = Ext.getCmp('add_delivery_contact');
                                         var tel = Ext.getCmp('add_delivery_tel');
                                         var mobile = Ext.getCmp('add_delivery_mobile');
                                         //   var remark = Ext.getCmp('add_delivery_remark');

                                         for (var i = 0; i < combo.store.getCount(); i++) {
                                             var record = combo.store.getAt(i);
                                             if (record.get('code') == rec) {
                                                 name.setValue(record.get('name'));
                                                 street1.setValue(record.get('street1'));
                                                 street2.setValue(record.get('street2'));
                                                 street3.setValue(record.get('street3'));
                                                 contact.setValue(record.get('contact'));
                                                 tel.setValue(record.get('tel'));
                                                 city.setValue(record.get('city'));
                                                 mobile.setValue(record.get('mobile'));
                                                 //     remark.setValue(record.get('remark'));
                                                 district.setValue(record.get('district'));

                                             }
                                         }
                                         //    alert(rec);

                                         Ext.getCmp('neworder-grid-newjob').getStore().load({
                                             params: {
                                                 cid: Ext.getCmp('add_customer_company_code').getValue(),
                                                 did: Ext.getCmp('add_delivery_objectid').getValue()
                                             }
                                         });

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
                         columnWidth: 1,
                         layout: 'form',
                         items:
                           { xtype: 'container',
                               layout: 'absolute',
                               height: 200,
                               fieldLabel: 'Addresses',
                               items: [
                            jobGrid
                        ]
                           }

                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 0.5,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: 'Company Name',
                             name: 'companyname',
                             id: 'add_delivery_company_name',
                             anchor: '70%',
                             readOnly: false
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: 'Street',
                             name: 'street1',
                             id: 'add_delivery_street1',
                             anchor: '40%',
                             readOnly: false
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: '',
                             name: 'street2',
                             id: 'add_delivery_street2',
                             anchor: '40%',
                             readOnly: false
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: '',
                             name: 'street3',
                             id: 'add_delivery_street3',
                             anchor: '40%',
                             readOnly: false
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 1,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: 'District',
                             name: 'district',
                             id: 'add_delivery_district',
                             anchor: '40%',
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
                             id: 'add_delivery_city',
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
                             fieldLabel: 'Contact',
                             name: 'contact',
                             id: 'add_delivery_contact',
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
                             fieldLabel: 'Tel',
                             name: 'tel',
                             id: 'add_delivery_tel',
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
                             fieldLabel: 'Mobile',
                             name: 'mobile',
                             id: 'add_delivery_mobile',
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
                             fieldLabel: 'Remarks',
                             name: 'remark',
                             id: 'add_delivery_remark',
                             anchor: '85%',
                             readOnly: false
                         }
}]
                     }
                 }, {
                     xtype: 'container',
                     autoEl: {}
	                ,
                     columnWidth: 1,
                     anchor: '90%',
                     items: { title: 'Part III - Assignment',
                         collapsible: true,
                         collapsed: false,

                         layout: 'column',
                         anchor: '90%',
                         items: [{
                             xtype: 'container',
                             autoEl: {},
                             columnWidth: 1,
                             layout: 'form',
                             items: {
                                 xtype: 'box',
                                 html: '<br/>'
                             }
                         }, {
                             xtype: 'container',
                             autoEl: {},
                             columnWidth: 1,
                             layout: 'form',
                             items: {
                                 xtype: 'textfield',
                                 fieldLabel: 'Input By',
                                 name: 'inputby',
                                 id: 'add_delivery_inputby',
                                 anchor: '35%',
                                 value: '<%= Session["userName"]  %>',
                                 readOnly: true
                             }
                         },
                             {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items: [{
                                     xtype: 'combo', id: 'add_delivery_requestby',
                                     fieldLabel: 'Request By',
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
                                     hiddenName: 'requestby',
                                     listeners: {
                                         select: {
                                             fn: function(combo, value) {

                                             }
                                         }
                                     }
}]

                                 }, {

                                     xtype: 'container',
                                     autoEl: {},
                                     columnWidth: 0.5,
                                     layout: 'form',
                                     items: {
                                         xtype: 'datefield',
                                         format: 'Y-m-d',
                                         fieldLabel: 'Deadline',
                                         name: 'deadline',
                                         id: 'add_delivery_deadline',
                                         value: '',
                                         anchor: '70%'
                                     }
                                 },
                             {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items: [{
                                     xtype: 'combo', id: 'add_delivery_handledby',
                                     fieldLabel: 'Handled By',
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
                                     hiddenName: 'handleby',
                                     listeners: {
                                         select: {
                                             fn: function(combo, value) {

                                             }
                                         }
                                     }
}]
                                 }, {
                                     xtype: 'container',
                                     autoEl: {},
                                     columnWidth: 1,
                                     layout: 'form',
                                     items: {
                                         xtype: 'textarea',
                                         fieldLabel: 'Notes',
                                         name: 'notes',
                                         id: 'add_delivery_notes',
                                         value: '',
                                         anchor: '85%'
                                     }
}]
                     }
                 }

			],
                 buttons: [
                   ]
             });

             var newDeliveryPanel = new Ext.Panel({
                 id: 'newdelivery-form-panel',
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


                           addDeliveryPanel


	            ],
                 buttons: [
                    {
                        text: 'Save',
                        handler: function() {

                            var sUrl = "/" + APP_NAME + "/delivery.aspx/add";


                            var objectid = Ext.getCmp('add_delivery_objectid').getValue();
                            var number = Ext.getCmp('add_delivery_number').getValue();
                            var partno = Ext.getCmp('add_delivery_partno').getValue();
                            var nonorder = Ext.getCmp('add_delivery_nonorder').getValue();
                            var length = Ext.getCmp('add_delivery_length').getValue();
                            var width = Ext.getCmp('add_delivery_width').getValue();
                            var height = Ext.getCmp('add_delivery_height').getValue();
                            var weight = Ext.getCmp('add_delivery_weight').getValue();
                            var type = Ext.getCmp('add_delivery_type').getValue();
                            var status = Ext.getCmp('add_delivery_status').getValue();
                            var goods_type = Ext.getCmp('add_delivery_goods_type').getValue();

                            var name = Ext.getCmp('add_delivery_company_name').getValue();
                            var street1 = Ext.getCmp('add_delivery_street1').getValue();
                            var street2 = Ext.getCmp('add_delivery_street2').getValue();
                            var street3 = Ext.getCmp('add_delivery_street3').getValue();
                            var district = Ext.getCmp('add_delivery_district').getValue();
                            var city = Ext.getCmp('add_delivery_city').getValue();
                            var contact = Ext.getCmp('add_delivery_contact').getValue();
                            var tel = Ext.getCmp('add_delivery_tel').getValue();
                            var mobile = Ext.getCmp('add_delivery_mobile').getValue();
                            var remark = Ext.getCmp('add_delivery_remark').getValue();
                            var code = Ext.getCmp('add_customer_company_code').getValue();

                            var requestby = Ext.getCmp('add_delivery_requestby').getValue();
                            var handledby = Ext.getCmp('add_delivery_handledby').getValue();
                            var deadline = Ext.getCmp('add_delivery_deadline').getValue();
                            var notes = Ext.getCmp('add_delivery_notes').getValue();

                            var xParameter = { objectid: objectid, number: number, partno: partno, nonorder: nonorder, length: length, width: width, height: height,goods_type:goods_type,
                                weight: weight, name: name, street1: street1, street2: street2, street3: street3, city: city, contact: contact, tel: tel, mobile: mobile,
                                remarks: remark, code: code, requestby: requestby, handleby: handledby, deadline: deadline, notes: notes, district: district, delivery_type: type, status: status

                            };
                            LoadData(sUrl, xParameter, onDeleteAdminReceived);

                            function onDeleteAdminReceived(data) {

                                if (data.success == false) {
                                    Ext.Msg.show({
                                        title: 'Fingerprint',
                                        msg: data.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                } else {
                                    if (objectid != "--") {
                                        Ext.getCmp('newdelivery-form-panel').collapse();
                                        Ext.getCmp('delivery-deliverygrid').getStore().reload();
                                    } else {
                                        // alert(data.objectid);
                                        Ext.getCmp('add_delivery_objectid').setValue(data.objectid);
                                        Ext.getCmp('delivery-deliverygrid').getStore().reload();

                                        var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
                                        orderStore.load({
                                            params: {
                                                cid: code,
                                                did: objectid
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

                            //                            addDeliveryPanel.getForm().submit({
                            //                                url: "/" + APP_NAME + "/Delivery.aspx/add",
                            //                                waitMsg: 'Please wait...',
                            //                                success: function(form, o) {
                            //                                    Ext.Msg.show({
                            //                                        title: 'Result',
                            //                                        msg: o.result.result,
                            //                                        buttons: Ext.Msg.OK,
                            //                                        icon: Ext.Msg.INFO
                            //                                    });
                            //                                    Ext.getCmp('newdelivery-form-panel').collapse();
                            //                                    Ext.getCmp('delivery-deliverygrid').getStore().reload();
                            //                                },
                            //                                failure: function(form, o) {
                            //                                    Ext.Msg.show({
                            //                                        title: 'Result',
                            //                                        msg: o.result.result,
                            //                                        buttons: Ext.Msg.OK,
                            //                                        icon: Ext.Msg.ERROR
                            //                                    });
                            //                                }
                            //                            });

                        }
                    }, { text: 'Cancel',
                        handler: function() {
                            Ext.getCmp('newdelivery-form-panel').collapse();
                        }
                    }
                ]
             })


             function setYourLocation(val) {
                 var a = Ext.getCmp('your-customer-location');
                 var location = "<a href='#' class='leftstyle1'>Delivery</a> -> <a href='#' class='leftstyle1'>" + val + "</a>"
                 try {
                     a.el.dom.innerHTML = location;
                 }
                 catch (e)
            { }

                 a = Ext.getCmp('your-customer-location2');
                 location = "<a href='#' class='leftstyle1'>Delivery</a> -> <a href='#' class='leftstyle1'>" + val + "</a>"
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
                 items: [leftPanel, centerPanel, newDeliveryPanel]
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
             Ext.getCmp('newdelivery-form-panel').collapse();
             fn_click(document.getElementById('delivery'));
         })

                     function adminidRenderer(val) {
                         return "<a href='#' onclick =editAdmin()>" + val + "</a>";
                     }
                     function onClick() {
                         alert("NonImplemented");
                     }




                     Ext.EventManager.onWindowResize(function(w, h) {
                         Ext.getCmp('delivery-deliverygrid').getStore().reload();
                         var newadd = Ext.getCmp('newdelivery-form-panel');

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

                         var part1 = Ext.getCmp('part1');
                         part1.setWidth(w * 0.9);
                         part1.doLayout();

                         var part2 = Ext.getCmp('part2');
                         part2.setWidth(w * 0.9);
                         part2.doLayout();

                         if (result == true)
                             newadd.collapse();

                     }, this, true);


                     

                     function newAdmin() {
                         Ext.getCmp('add_delivery_objectid').setValue("--");

                         Ext.getCmp('add_delivery_number').setValue("");
                         Ext.getCmp('add_delivery_partno').setValue("");
                         
                         Ext.getCmp('add_delivery_nonorder').setValue("");
                         Ext.getCmp('add_delivery_length').setValue("");
                         Ext.getCmp('add_delivery_width').setValue("");

                         Ext.getCmp('add_delivery_height').setValue("");
                         Ext.getCmp('add_delivery_weight').setValue("");
                         Ext.getCmp('add_delivery_goods_type').setValue("File");
                         var type = Ext.getCmp('add_delivery_type').setValue("Send");
                         var status = Ext.getCmp('add_delivery_status').setValue("Processing");


                         var name = Ext.getCmp('add_delivery_company_name');
                         var street1 = Ext.getCmp('add_delivery_street1');
                         var street2 = Ext.getCmp('add_delivery_street2');
                         var street3 = Ext.getCmp('add_delivery_street3');
                         var district = Ext.getCmp('add_delivery_district');
                         var city = Ext.getCmp('add_delivery_city');
                         var contact = Ext.getCmp('add_delivery_contact');
                         var tel = Ext.getCmp('add_delivery_tel');
                         var mobile = Ext.getCmp('add_delivery_mobile');
                         var remark = Ext.getCmp('add_delivery_remark');
                         var code = Ext.getCmp('add_customer_company_code');
                         code.setValue('');
                         
                         
                        

                         name.setValue('');
                         street1.setValue('');
                         street2.setValue('');
                         street3.setValue('');
                         city.setValue('');
                         district.setValue('');
                         contact.setValue('');
                         tel.setValue('');
                         mobile.setValue('');
                         remark.setValue('');

                         var requestby = Ext.getCmp('add_delivery_requestby');
                         var handledby = Ext.getCmp('add_delivery_handledby');
                         var deadline = Ext.getCmp('add_delivery_deadline');
                         var notes = Ext.getCmp('add_delivery_notes');

                         requestby.setValue('');
                         handledby.setValue('');
                         deadline.setValue('');
                         notes.setValue('');



                       
                             var number = Ext.getCmp('add_delivery_number');
                             var nonorder = Ext.getCmp('add_delivery_nonorder');
                             Ext.getCmp('add_check_order1').setValue(true);
                             if (Ext.getCmp('add_check_order').checked == true) {
                                 number.show();
                                 nonorder.hide();
                             } else if (Ext.getCmp('add_check_order1').checked == true) {
                                 number.hide();
                                 nonorder.show();
                             } else {
                                 number.hide();
                                 nonorder.hide();
                             }
                            

                             var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
                             orderStore.load({
                                 params: {
                                     cid: '',
                                     did: '0'
                                 }
                             });

                           //  orderStore.reload();
                         Ext.getCmp('newdelivery-form-panel').expand();
                         
                     }


                     function editAdmin() {
                         var grid = Ext.getCmp('delivery-deliverygrid');
                         var selectModel = grid.getSelectionModel();
                         var rec = selectModel.getSelected();

                         if (rec == undefined || rec.length == 0) {
                             Ext.Msg.alert('Fingerprint', 'Please select a record');
                             return;
                         }
                         
                         Ext.getCmp('add_delivery_objectid').setValue(rec.data.objectid);
                         Ext.getCmp('add_delivery_number').setValue(rec.data.number);
                         Ext.getCmp('add_delivery_partno').setValue(rec.data.partno);
                         Ext.getCmp('add_delivery_nonorder').setValue(rec.data.nonorder);
                         Ext.getCmp('add_delivery_length').setValue(rec.data.length);
                         Ext.getCmp('add_delivery_width').setValue(rec.data.width);
                         Ext.getCmp('add_delivery_height').setValue(rec.data.height);
                         Ext.getCmp('add_delivery_weight').setValue(rec.data.weight);
                         Ext.getCmp('add_delivery_type').setValue(rec.data.delivery_type);
                         Ext.getCmp('add_delivery_status').setValue(rec.data.status);
                         Ext.getCmp('add_delivery_goods_type').setValue(rec.data.goods_type);


                         var name = Ext.getCmp('add_delivery_company_name');
                         var street1 = Ext.getCmp('add_delivery_street1');
                         var street2 = Ext.getCmp('add_delivery_street2');
                         var street3 = Ext.getCmp('add_delivery_street3');
                         var district = Ext.getCmp('add_delivery_district');
                         var city = Ext.getCmp('add_delivery_city');
                         var contact = Ext.getCmp('add_delivery_contact');
                         var tel = Ext.getCmp('add_delivery_tel');
                         var mobile = Ext.getCmp('add_delivery_mobile');
                         var remark = Ext.getCmp('add_delivery_remark');
                         var code = Ext.getCmp('add_customer_company_code');
                         code.setValue(rec.data.company_code);

                         name.setValue(rec.data.company_name);
                         street1.setValue(rec.data.street1);
                         street2.setValue(rec.data.street2);
                         street3.setValue(rec.data.street3);
                         district.setValue(rec.data.district);
                         city.setValue(rec.data.city);
                         contact.setValue(rec.data.contact);
                         tel.setValue(rec.data.tel);
                         mobile.setValue(rec.data.mobile);
                         remark.setValue(rec.data.remark);

                         var requestby = Ext.getCmp('add_delivery_requestby');
                         var handledby = Ext.getCmp('add_delivery_handledby');
                         var deadline = Ext.getCmp('add_delivery_deadline');
                         var notes = Ext.getCmp('add_delivery_notes');

                         requestby.setValue(rec.data.requestby);
                         handledby.setValue(rec.data.handledbyid);
                         deadline.setValue(rec.data.deadline);
                         notes.setValue(rec.data.notes);

                         var orderStore = Ext.getCmp('neworder-grid-newjob').getStore();
                         orderStore.load({
                             params: {
                             cid: rec.data.company_code,
                             did: rec.data.objectid
                             }
                         });

                       
                             var number = Ext.getCmp('add_delivery_number');
                             var nonorder = Ext.getCmp('add_delivery_nonorder');

                             if (number.getValue() != "") {
                                 Ext.getCmp('add_check_order').setValue(true);
                                 Ext.getCmp('add_check_order1').setValue(false);
                                 nonorder.setValue('');
                             } 
                             
                             if (nonorder.getValue() != "") {
                                 Ext.getCmp('add_check_order1').setValue(true);
                                 Ext.getCmp('add_check_order').setValue(false);
                                 number.setValue('');
                             }
                         
                         Ext.getCmp('newdelivery-form-panel').expand();
                     }

                     function searchOrder() {
                         //    alert(Ext.getCmp('filter-itemtype').getValue());
                         //    alert(Ext.getCmp('neworder-status-rg').getValue().value);
                         //    alert(Ext.getCmp('neworder-filter-type-rg').getValue().value);
                         //    alert(Ext.getCmp('neworder-filter-value').getValue());

                         var jt = Ext.getCmp('filter-itemtype').getValue();
                         var js = Ext.getCmp('neworder-status-rg').getValue().value;
                         var ft = Ext.getCmp('neworder-filter-type-rg').getValue().value;
                         var fv = Ext.getCmp('neworder-filter-value').getValue();

                         if (js == "0")
                             js = "Pending";
                         else if (js == "1")
                             js = "Processing";
                         else if (js == "2")
                             js = "Finish";
                         else
                             js = "";

                         if (ft == 0)
                             ft = "customer_code";
                         else if (ft == 1)
                             ft = "customer_name"
                         else if (ft == 2)
                             ft = "deliveryID";
                         else if (ft == 3)
                             ft = "jobno";

                         Ext.getCmp('delivery-deliverygrid').getStore().load(
        {
            params: {
                jt: jt,
                js: js,
                ft: ft,
                fv: fv
            }
        }
    );
                     }
      
    </script>
</asp:Content>
