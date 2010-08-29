<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Orders
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID=styleSheetContent runat="server">
    <link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-gray.css") %>/>
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
        
        
        #neworder-addorder-panel .x-panel-body {
            background-color: #AAE6A2 ! important;
        }
        
        #neworder-addjob-panel .x-panel-body {
            background-color: #AAE6A2 ! important;
        }
        #neworder-form-panel .x-panel-body {
            background-color: #AAE6A2 ! important;
        }
        
        #order-centerPanel .x-panel-body {
            background-color: #AAE6A2 ! important;
        }
        
        #neworder-toolbar-panel .x-panel-body {
            background-color: #F0F0F0 ! important;
        }
         #neworder-filter-panel .x-panel-body {
            background-color: #F0F0F0 ! important;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">

    <script type="text/javascript" src=<%=Html.link("Content/js/new_job.js") %>></script>
    <script type="text/javascript" src=<%=Html.link("Content/js/new_order.js") %>></script>
    <script type="text/javascript" src=<%=Html.link("Content/js/edit_order.js") %>></script>
    <script type="text/javascript">
        var firstLoad = true;
        Ext.onReady(function() {
            Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

            var leftPanel = new Ext.Panel({
                id: 'neworder-left-panel',
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

            var order_toolbar_panel = new Ext.Panel({
                id: 'neworder-toolbar-panel',
                title: '',
                layout: 'hBox',
                items: [
                    {
                        xtype: 'buttongroup',
                        items: [
                            {
                                text: 'New Order',
                                handler: newOrder
                            }
                            ]
                    }, {
                        xtype: 'buttongroup',
                        hidden: true,
                        items: [
                            {
                                text: 'Approve',

                                handler: onClick
                            }
                            ]
                    }, {
                        xtype: 'buttongroup',
                        items: [
                            {
                                text: 'Edit',
                                handler: editOrder
                            }
                            ]
                    }, {
                        xtype: 'buttongroup',
                        items: [
                            {
                                text: 'Delete',
                                handler: deleteOrder
                            }
                            ]
                    }, {
                        xtype: 'buttongroup',
                        items: [
                            {
                                text: 'Print',
                                handler: onClick
                            }
                            ]
                    }
                ]
            });

            var order_filter_panel = new Ext.FormPanel({
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
                        html: 'Job Type:'

                    },
                    {
                        xtype: 'combo', id: 'filter-itemtype',
                        fieldLabel: 'Job Type',
                        width: 100,
                        x: 80,
                        y: 2,
                        mode: 'local',
                        store: new Ext.data.JsonStore({
                            url: "/" + APP_NAME + "/order.aspx/getJobType",
                            fields: ['id', 'name'],
                            root: 'tags',
                            autoLoad: true
                        }),
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
                        width: 200,
                        items: [{
                            value: 0,
                            inputValue: 0,
                            checked: true,
                            name: 'neworder-filter-status',
                            boxLabel: 'Pending'
                        }, {
                            value: 1,
                            inputValue: 1,
                            name: 'neworder-filter-status',
                            boxLabel: 'Finished'
                        }, {
                            value: 2,
                            inputValue: 2,
                            name: 'neworder-filter-status',
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
                            boxLabel: 'Order No.'
                        }, {
                            value: 3,
                            inputValue: 3,
                            name: 'neworder-filter-type',
                            boxLabel: 'Invoice No.'
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

            orderStore = new Ext.data.JsonStore({
                // store configs
                autoDestroy: true,
                url: "/" + APP_NAME + "/order.aspx/getOrder",
                remoteSort: true,
                sortInfo: {
                    field: 'pid',
                    direction: 'desc'
                },
                storeId: 'orderStore',

                // reader configs
                idProperty: 'pid',
                root: 'data',
                totalProperty: 'total',
                fields: [{
                    name: 'pid',
                    type: 'string'
                }, {
                    name: 'received_date',
                    type: 'string'
                }, {
                    name: 'modified_date',
                    type: 'string'
                }, {
                    name: 'order_deadline',
                    type: 'String'

                }, {
                    name: 'invoice_no',
                    type: 'String'
                }, {
                    name: 'cid',
                    type: 'string'
                }, {
                    name: 'received_by',
                    type: 'String'
                }, {
                    name: 'sales_person',
                    type: 'String'
                }, {
                    name: 'remarks',
                    type: 'String'
                }, {
                    name: 'customer_name',
                    type: 'String'
                }, {
                    name: 'status',
                    type: 'String'
                }, {
                    name: 'customer_tel',
                    type: 'String'
                }, {
                    name: 'customer_contact_person',
                    type: 'String'
                }
				]
            });

            var filters = new Ext.ux.grid.GridFilters({
                encode: false,
                local: true,
                filters: [{
                    type: 'string',
                    dataIndex: 'pid'
                }, {
                    type: 'string',
                    dataIndex: 'received_date'
                }, {
                    type: 'string',
                    dataIndex: 'modified_date'
                }, {
                    type: 'string',
                    dataIndex: 'order_deadline'
                }, {
                    type: 'string',
                    dataIndex: 'invoice_no'
                }, {
                    type: 'string',
                    dataIndex: 'cid'
                }, {
                    type: 'string',
                    dataIndex: 'received_by'
                }, {
                    type: 'string',
                    dataIndex: 'sales_person'
                }, {
                    type: 'string',
                    dataIndex: 'remarks'
                }, {
                    type: 'string',
                    dataIndex: 'customer_name'
                }, {
                    type: 'string',
                    dataIndex: 'status'
                }, {
                    type: 'string',
                    dataIndex: 'customer_tel'
                }, {
                    type: 'string',
                    dataIndex: 'customer_contact_person'
}]
                });

                var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });
                var createColModel = function(finish, start) {
                    
                    var columns = [sm,
                    {
                        dataIndex: 'pid',
                        header: 'Order No.',
                        filterable: false
                    }, {
                        dataIndex: 'invoice_no',
                        header: 'Invoice No.',
                        filterable: false
                    }, {
                        dataIndex: 'received_date',
                        header: 'Received Date',
                        filterable: false
                    }, {
                        dataIndex: 'customer_name',
                        header: 'Customer',
                        filterable: false,
                        sortable: false
                    }, {
                        dataIndex: 'order_deadline',
                        header: 'Order Deadline',
                        filterable: false
                    }, {
                        dataIndex: 'received_by',
                        header: 'Received by',
                        filterable: false
                    }, {
                        dataIndex: 'status',
                        header: 'Status',
                        renderer: renderOrderStatus,
                        filterable: false,
                        tooltip: 'New \nPendding \nIn Progress \nFinish'
                    }
				];

                    return new Ext.grid.ColumnModel({
                        columns: columns.slice(start || 0, finish),
                        defaults: {
                            sortable: true,
                            menuDisabled: true

                        }
                    });
                };


                //Order grid
                var orderGrid = new Ext.grid.GridPanel({
                    id: 'fp-order-grid',
                    border: false,
                    store: orderStore,
                    height: '100%',
                    colModel: createColModel(8),
                    selMode: sm,
                    loadMask: true,
                    plugins: [filters],
                    stripeRows: true,
                    flex: 5,
                    listeners: {
                        render: {
                            fn: function() {
                                orderStore.load({
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
                        rowdblclick: editOrder
                    },
                    bbar: new Ext.PagingToolbar({
                        store: orderStore,
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
                            order_filter_panel
                        ]
                    }
                });


                ///////////////////////////////

                var jobStore = new Ext.data.ArrayStore({
                    fields: [
               { name: 'jobid', type: 'string' },
		       { name: 'job_type', type: 'string' },
               { name: 'file_name', type: 'string' },
               { name: 'request', type: 'string' },
               { name: 'detail', type: 'string' },
		       { name: 'notes', type: 'string' }
            ]
                });

                //jobStore.loadData(jobData);

                // create the Grid
                var jobGrid = new Ext.grid.GridPanel({
                    id: 'neworder-grid-newjob',
                    store: jobStore,
                    columns: [sm,
                { id: 'jobid', header: 'Item', sortable: true, dataIndex: 'jobid', hide: true },
                { header: 'Item Type', sortable: true, dataIndex: 'job_type' },
			    { header: 'File Name', sortable: true, dataIndex: 'file_name' },
                { header: 'Request', sortable: true, dataIndex: 'request' },
                { header: 'Details', sortable: true, dataIndex: 'detail' },
                { header: 'Notes', sortable: true, dataIndex: 'notes' }
            ],
                    stripeRows: true,
                    autoHeight: true,
                    stateful: true,
                    selModel: sm,
                    sm: new Ext.grid.RowSelectionModel({
                        singleSelect: true
                    }),
                    stateId: 'jobGrid',

                    tbar: [
            {
                xtype: 'combo', id: 'neworder-combo-newjobtype',
                fieldLabel: 'Job Type',
                value: '',
                mode: 'local',
                store: new Ext.data.JsonStore({
                    url: "/" + APP_NAME + "/order.aspx/getJobType",
                    fields: ['id', 'name'],
                    root: 'tags',
                    autoLoad: true
                }),
                displayField: 'name',
                valueField: 'id',
                forceSelection: true,
                triggerAction: 'all',
                hiddenName: 'newjobtype',
                listeners: {
                    select: {
                        fn: function(combo, value) {
                            Ext.getCmp('newjob-request-container').show();
                            Ext.getCmp('newjob-filename-container').show();
                            Ext.getCmp('newjob-notes-container').show();
                            Ext.getCmp('newjob-jobsubmitmode').setValue('Add');
                            var type = Ext.getCmp('neworder-hidden-jobtype').getValue();

                            if (Ext.getCmp('neworder-combo-newjobtype').getValue() != type) {
                                Ext.getCmp('neworder-combo-newjobtype').disable();
                                new_order_add_job();
                            }
                        }
                    }
                }
            }, {
                xtype: 'buttongroup',
                items: [{
                    text: 'Add',
                    handler: function() {
                        if (Ext.getCmp('neworder-hidden-jobtype').getValue() + "" != "") {
                            if (Ext.getCmp('newjob-jobsubmitmode').getValue() == 'Add') {
                                addJobPanel.getForm().submit({
                                    url: "/" + APP_NAME + "/job.aspx/addNewJob",
                                    waitMsg: 'Please wait...',
                                    success: function(form, o) {
                                        //                                        Ext.Msg.show({
                                        //                                            title: 'Result',
                                        //                                            msg: o.result.result,
                                        //                                            buttons: Ext.Msg.OK,
                                        //                                            icon: Ext.Msg.INFO
                                        //                                        });

                                        if (Ext.getCmp('neworder-pid').getValue() == '--') {

                                            var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
                                            var xParameter = {}
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);
                                            }
                                        }
                                        else {
                                            var sUrl = "/" + APP_NAME + "/job.aspx/getItemsByOrder";
                                            var xParameter = { pid: Ext.getCmp('neworder-pid').getValue() }
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);
                                            }
                                        }
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
                            else if (Ext.getCmp('newjob-jobsubmitmode').getValue() == 'Edit') {
                                addJobPanel.getForm().submit({
                                    url: "/" + APP_NAME + "/job.aspx/saveJob",
                                    waitMsg: 'Please wait...',
                                    success: function(form, o) {
                                        //                                        Ext.Msg.show({
                                        //                                            title: 'Result',
                                        //                                            msg: o.result.result,
                                        //                                            buttons: Ext.Msg.OK,
                                        //                                            icon: Ext.Msg.INFO
                                        //                                        });

                                        if (Ext.getCmp('neworder-pid').getValue() == '--') {

                                            var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
                                            var xParameter = {}
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);
                                                submitOrder();
                                            }
                                        }
                                        else {
                                            var sUrl = "/" + APP_NAME + "/job.aspx/getItemsByOrder";
                                            var xParameter = { pid: Ext.getCmp('neworder-pid').getValue() }
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);

                                                submitOrder();
                                            }
                                        }
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
                        }
                        //                        Ext.getCmp('newjob-request-container').show();
                        //                        Ext.getCmp('newjob-filename-container').show();
                        //                        Ext.getCmp('newjob-notes-container').show();
                        //                        Ext.getCmp('newjob-jobsubmitmode').setValue('Add');
                        //                        var type = Ext.getCmp('neworder-hidden-jobtype').getValue();

                        //                        if (Ext.getCmp('neworder-combo-newjobtype').getValue() != type) {
                        //                            Ext.getCmp('neworder-combo-newjobtype').disable();
                        //                            new_order_add_job();
                        //                        }
                    }
                }
            ]
            }, {
                xtype: 'buttongroup',
                items: [
                    {
                        text: 'Edit',
                        handler: function() {
                            Ext.getCmp('newjob-request-container').show();
                            Ext.getCmp('newjob-filename-container').show();
                            Ext.getCmp('newjob-notes-container').show();
                            Ext.getCmp('newjob-jobsubmitmode').setValue('Edit');
                            var grid = Ext.getCmp('neworder-grid-newjob');
                            var selectModel = grid.getSelectionModel();
                            var rec = selectModel.getSelected();

                            if (rec == undefined || rec.length == 0) {
                                Ext.Msg.alert('Fingerprint', 'Please select a record');
                                return;
                            }

                            var sUrl = "/" + APP_NAME + "/job.aspx/getJobDetailByID";
                            var pid = Ext.getCmp('neworder-hidden-pid').getValue();
                            var xParameter = { jobid: rec.data.jobid, pid: pid };
                            LoadData(sUrl, xParameter, fillJobDetail);
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

                                var sUrl = "/" + APP_NAME + "/job.aspx/deleteJob";

                                var grid = Ext.getCmp('neworder-grid-newjob');
                                var selectModel = grid.getSelectionModel();
                                var rec = selectModel.getSelected();

                                if (rec == undefined || rec.data.length == 0) {
                                    Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                                    return;
                                }
                                var xParameter = { jobid: rec.data.jobid, pid: Ext.getCmp('neworder-pid').getValue() };
                                LoadData(sUrl, xParameter, onDeleteJobReceived);

                                function onDeleteJobReceived(data) {

                                    var sUrl = "/" + APP_NAME + "/job.aspx/getItemsByOrder";
                                    var xParameter = { pid: Ext.getCmp('neworder-pid').getValue() };
                                    LoadData(sUrl, xParameter, fillJobList);
                                }

                            }
                        }
                ]
            }
            ]
                });

                addJobPanel = new Ext.FormPanel({
                    id: 'neworder-addjob-panel',
                    url: "/" + APP_NAME + "/job.aspx/newJob",
                    defaultType: 'textfield',
                    layout: 'column',
                    labelAlign: 'right',
                    buttonAlign: 'center',
                    bodyStyle: 'order_bg',
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
                }, {

                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
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
                },
                 {
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     id: 'newjob-filename-container',
                     layout: 'form',
                     items: {
                         xtype: 'textfield',
                         fieldLabel: 'File Name',
                         name: 'newjob-filename',
                         id: 'newjob-file_name',
                         value: '',
                         anchor: '60%'
                     }
                 }, {
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     id: 'newjob-request-container',
                     layout: 'form',
                     items: {
                         anchor: '60%',
                         id: 'newjob-request-request',
                         xtype: 'checkboxgroup',
                         fieldLabel: 'Request',
                         columns: 4,
                         layout: 'form',
                         //NewJob EM FTP CD/DVD Mac PC Test
                         items: [
					    { xtype: 'checkbox', boxLabel: 'New Job', name: 'newjob-newjob', id: 'newjob-request-newjob' },
					    { xtype: 'checkbox', boxLabel: 'EM', name: 'newjob-em', id: 'newjob-request-em' },
					    { xtype: 'checkbox', boxLabel: 'FTP', name: 'newjob-ftp', id: 'newjob-request-ftp' },
					    { xtype: 'checkbox', boxLabel: 'CD/DVD', name: 'newjob-cddvd', id: 'newjob-request-cddvd' },
					    { xtype: 'checkbox', boxLabel: 'Mac', name: 'newjob-mac', id: 'newjob-request-mac' },
					    { xtype: 'checkbox', boxLabel: 'Pc', name: 'newjob-pc', id: 'newjob-request-pc' },
					    { xtype: 'checkbox', boxLabel: 'Test', name: 'newjob-test', id: 'newjob-request-test' }
				    ]
                     }
                 },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    id: 'newjob-notes-container',
                    layout: 'form',
                    items: {
                        xtype: 'textarea',
                        fieldLabel: 'Notes',
                        name: 'newjob-notes',
                        id: 'newjob-notes',
                        value: '',
                        anchor: '80%'
                    }
                },
                {
                    xtype: 'hidden',
                    id: 'neworder-hidden-jobtype',
                    name: 'newjob-jobtype'
                },
                {
                    xtype: 'hidden',
                    id: 'neworder-hidden-jobid',
                    name: 'newjob-jobid'
                },
                {
                    xtype: 'hidden',
                    id: 'neworder-hidden-pid',
                    name: 'newjob-pid'
                }
            ]
                });


                var addOrderPanel = new Ext.FormPanel({
                    id: 'neworder-addorder-panel',
                    url: "/" + APP_NAME + "/order.aspx/addNewOrder",
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
                        fieldLabel: 'Order No.',
                        name: 'orderNo',
                        id: 'neworder-pid',
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
                        xtype: 'combo',
                        fieldLabel: 'Company Code',
                        value: '',
                        id: 'neworder-customer_no',
                        mode: 'remote ',
                        minChars: 0,
                        store: new Ext.data.JsonStore({
                            url: "/" + APP_NAME + "/delivery.aspx/GetCustomers",
                            fields: ['code', 'name', 'street1', 'street2', 'street3', 'district', 'city', 'contact', 'tel', 'mobile', 'remark'],
                            root: 'tags',
                            autoLoad: true
                        }),
                        editable: true,
                        forceSelection: true,
                        displayField: 'code',
                        valueField: 'code',
                        triggerAction: 'all',
                        hiddenName: 'customer_no',
                        anyMatch: true,
                        listeners: {
                            select: {
                                fn: function(combo, value) {

                                    fillCustomerInfoByNo();
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
                    items: [{
                        xtype: 'combo', id: 'neworder-received_by',
                        fieldLabel: 'Received By',
                        value: '',
                        mode: 'local',
                        anchor: '80%',
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
                        hiddenName: 'received_by',
                        listeners: {
                            select: {
                                fn: function(combo, value) {

                                }
                            }
                        }
                    }
                ]

                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    layout: 'form',
                    items: {
                        anchor: '90%',
                        xtype: 'combo', id: 'shoutoutsTags',
                        fieldLabel: 'Customer',
                        value: '',
                        id: 'neworder-customer-combo',
                        mode: 'local',
                        store: new Ext.data.JsonStore({
                            url: "/" + APP_NAME + "/order.aspx/getCustomer",
                            fields: ['id', 'name'],
                            root: 'tags',
                            autoLoad: true
                        }),
                        displayField: 'name',
                        valueField: 'id',
                        forceSelection: false,
                        triggerAction: 'all',
                        hiddenName: 'customer',
                        //anyMatch: true,
                        listeners: {
                            select: {
                                fn: function(combo, value) {
                                    var rec = combo.getValue();
                                }
                            },
                            expand: {
                                fn: function(combo, value) {
                                    //createBasekeyStoreFilter(combo.store,'name',Ext.getCmp('neworder-customer-filter').getValue());
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
                     items: [{
                         xtype: 'datefield',
                         format: 'Y-m-d',
                         fieldLabel: 'Received Date',
                         hideLabel: true,
                         name: 'received_date',
                         id: 'neworder-received_date',
                         value: '',
                         anchor: '80%',
                         hidden: true
                     }, {
                         xtype: 'box'
}]
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 0.5,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: 'Tel',
                             name: 'customer_tel',
                             id: 'neworder-customer_tel',
                             anchor: '85%',
                             value: ''
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 0.5,
                         layout: 'form',
                         items: {
                             xtype: 'datefield',
                             format: 'Y-m-d',
                             fieldLabel: 'Order Deadline',
                             name: 'order_deadline',
                             id: 'neworder-order_deadline',
                             value: '',
                             anchor: '80%'
                         }
                     }, {
                         xtype: 'container',
                         autoEl: {},
                         columnWidth: 0.5,
                         layout: 'form',
                         items: {
                             xtype: 'textfield',
                             fieldLabel: 'Contact Person',
                             name: 'customer_contact_person',
                             id: 'neworder-customer_contact_person',
                             anchor: '85%',
                             value: ''
                         }
                     },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'textarea',
                        fieldLabel: 'Remarks',
                        name: 'remark',
                        id: 'neworder-remarks',
                        value: '',
                        anchor: '80%'
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    hidden: true,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Updated By',
                        name: 'update_by',
                        id: 'neworder-modified_by',
                        anchor: '80%',
                        value: USER_NAME,
                        readOnly: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 0.5,
                    hidden: true,
                    layout: 'form',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Updated Date',
                        name: 'updateDate',
                        id: 'neworder-modified_date',
                        anchor: '80%',
                        value: new Date().format('Y-m-d'),
                        readOnly: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'box',
                        id: 'neworder-audit-info',
                        html: "<img width=20 height=20 title='Update By " + USER_NAME + " on " + new Date().format('Y-m-d') + "' src='" + "/" + APP_NAME + "/Content/images/InfoIcon.gif'/>"
                    }
                },
                {
                    xtype: 'hidden',
                    id: 'newjob-jobsubmitmode',
                    name: 'newjob-jobsubmitmode'
                }
			],
                    buttons: []
                });


                var newOrderPanel = new Ext.Panel({
                    id: 'neworder-form-panel',
                    layout: 'anchor',
                    containerScroll: true,
                    autoScroll: true,
                    region: 'east',
                    minSize: '89%',
                    width: '89%',
                    margins: '3 0 3 3',
                    cmargins: '3 3 3 3',
                    defaults: { margins: '0 0 5 0' },
                    collapsible: true,
                    collapsed: false,
                    animCollapse: false,
                    hideCollapseTool: true,
                    buttonAlign: 'center',
                    listeners: {
                        collapse: {
                            fn: function(panel) {
                                Ext.getCmp('order-centerPanel').doLayout();
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
	                            id: 'your-order-location2',
	                            xtype: 'box',
	                            anchor: '100%',
	                            html: "<a href='#' class='leftstyle1'>Order</a> → <a href='#' class='leftstyle1'>Monitor</a>"
	                        }
	                    ]
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Part I - Order Summary',
	                        collapsible: true,
	                        collapsed: false,
	                        anchor: '90%',
	                        items: [
                            addOrderPanel
                        ]
	                    }
	                },
	                {
	                    xtype: 'container',
	                    autoEl: {},
	                    columnWidth: 1,
	                    anchor: '90%',
	                    items: {
	                        title: 'Part II - Item Summary',
	                        collapsible: true,
	                        collapsed: false,
	                        anchor: '90%',
	                        items: [
                                addJobPanel
                            ]
	                    }
	                }
	            ],
                    buttons: [
                    {
                        text: 'Save',
                        handler: function() {

                            if (Ext.getCmp('newjob-jobsubmitmode').getValue() == 'Edit' && Ext.getCmp('neworder-hidden-jobtype').getValue() != "") {
                                addJobPanel.getForm().submit({
                                    url: "/" + APP_NAME + "/job.aspx/saveJob",
                                    waitMsg: 'Please wait...',
                                    success: function(form, o) {
                                        //                                        Ext.Msg.show({
                                        //                                            title: 'Result',
                                        //                                            msg: o.result.result,
                                        //                                            buttons: Ext.Msg.OK,
                                        //                                            icon: Ext.Msg.INFO
                                        //                                        });

                                        if (Ext.getCmp('neworder-pid').getValue() == '--') {

                                            var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
                                            var xParameter = {}
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);
                                                submitOrder();
                                            }
                                        }
                                        else {
                                            var sUrl = "/" + APP_NAME + "/job.aspx/getItemsByOrder";
                                            var xParameter = { pid: Ext.getCmp('neworder-pid').getValue() }
                                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

                                            function getNewJob_OnReceived(data) {
                                                Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);

                                                submitOrder();
                                            }
                                        }
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
                            else {
                                submitOrder();
                            }
                            function submitOrder() {
                                addOrderPanel.getForm().submit({
                                    url: "/" + APP_NAME + "/order.aspx/addNewOrder",
                                    waitMsg: 'Please wait...',
                                    success: function(form, o) {
                                        Ext.Msg.show({
                                            title: 'Result',
                                            msg: o.result.result,
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.Msg.INFO
                                        });
                                        Ext.getCmp('neworder-form-panel').collapse();
                                        Ext.getCmp('fp-order-grid').getStore().reload();
                                    },
                                    failure: function(form, o) {
                                        Ext.Msg.show({
                                            title: 'Result',
                                            msg: o.result.result,
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.Msg.ERROR
                                        });

                                        if (Ext.getCmp('newjob-jobsubmitmode').getValue() == 'Add')
                                            Ext.getCmp('neworder-grid-newjob').getStore().loadData([]);
                                    }
                                });
                            }
                        }
                    }
                ]
                })
                //////////////////////////////////

                var centerPanel = new Ext.Panel({
                    id: 'order-centerPanel',
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
                    items: [
                                    {
                                        id: 'your-order-location',
                                        xtype: 'box',
                                        anchor: '100%',
                                        html: "<a href='#' class='leftstyle1'>Order</a> → <a href='#' class='leftstyle1'>Monitor</a>"
                                    },
                                    orderGrid
                                     ]
                });

                var mainPanel = new Ext.Panel({
                    id: 'neworder-main-panel',
                    contentEl: 'fingerprint-order-body',
                    closable: false,
                    autoScroll: true,
                    plain: true,
                    layout: 'border',
                    anchor: '-1, -100',
                    items: [leftPanel, centerPanel, newOrderPanel]
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
                Ext.getCmp('neworder-form-panel').collapse();
                fn_click(document.getElementById('order'));
            });

        function onClick()
        {
            alert("NonImplemented");
        }
        function renderOrderStatus(val)
        {
            if (val == "1")
                return "Finish";
            else
                return "Pending";
        }
        function setYourLocation(val)
        {
            var a = Ext.getCmp('your-order-location');
            var location = "<a href='#' class='leftstyle1'>Order</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
            try{
                a.el.dom.innerHTML = location;
            }
            catch(e)
            {}
            
            a = Ext.getCmp('your-order-location2');
            location = "<a href='#' class='leftstyle1'>Order</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
            try{
                a.el.dom.innerHTML = location;
            }
            catch(e)
            {}
        }
        function pidRenderer(val)
        {
            return "<a href='#' onclick =editOrder2('" + val + "')>" + val + "</a>";
        }

        window.onresize = function() {
            setTimeout("resizePanel()", 200);
        }
        function resizePanel() {

            var collapsed = Ext.getCmp('neworder-form-panel').collapsed;
            
            var width = Ext.getCmp('neworder-main-panel').getWidth() - Ext.getCmp('neworder-left-panel').getWidth() - 10;
            Ext.getCmp('neworder-form-panel').setWidth(width);
            Ext.getCmp('neworder-form-panel').syncSize();
            Ext.getCmp('neworder-form-panel').doLayout();
            Ext.getCmp('neworder-form-panel').collapse();
            
            if (collapsed)
            {
                var width = Ext.getCmp('neworder-main-panel').getWidth() - Ext.getCmp('neworder-left-panel').getWidth() - 10;
                Ext.getCmp('order-centerPanel').setWidth(width);
                Ext.getCmp('order-centerPanel').syncSize();
                Ext.getCmp('order-centerPanel').doLayout();
            }

            if (!collapsed)
                Ext.getCmp('neworder-form-panel').expand();

                
        }
    </script>
    
    

</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="fingerprint-order-body"></div>
    <div id="fingerprint-order-left" class="x-hide-display">
        <!--<table width="100%">
            <tr>
                <td align="center"><span>
                <a id="order-monitor-link" 
                    class="leftstyle1_1" 
                    href="#" 
                    onclick="this.className='leftstyle1_1';document.getElementById('order-neworder-link').className='leftstyle1';setYourLocation('Monitor');Ext.getCmp('neworder-form-panel').collapse();" 
                    onmouseover="set_underline(this)" 
                    onmouseout="set_underline(this)">
                    Monitor
                    </a>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center"><span>
                <a id="order-neworder-link" 
                    class="leftstyle1" href="#" 
                    onclick="newOrder();this.className='leftstyle1_1';document.getElementById('order-monitor-link').className='leftstyle1';setYourLocation('New Order')" 
                    onmouseover="set_underline(this)" 
                    onmouseout="set_underline(this)">
                    New Order
                    </a>
                    </span>
                </td>
            </tr>
        </table>-->
    </div>
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="order-monitor-link"  class="menubar_click" onclick="document.getElementById('order-monitor-link').className='menubar_click';document.getElementById('order-neworder-link').className='menubar'; Ext.getCmp('neworder-form-panel').collapse();"

      >Monitor</a></li>
      <li><a id="order-neworder-link"  class="menubar" style="display:none" onclick="document.getElementById('order-neworder-link').className='menubar_click';document.getElementById('order-monitor-link').className='menubar';newOrder();"
      
      >New Order</a></li>
    </ul>
</asp:Content>
