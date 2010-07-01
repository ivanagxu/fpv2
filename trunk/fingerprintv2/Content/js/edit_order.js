//var editOrderWin;
//var editJobPanel;
//var jobDetailsItems;
//function editOrder()
//{
//    var jobData = [];
//    if (!editOrderWin)
//    {
//        var jobStore = new Ext.data.ArrayStore({
//            fields: [
//               { name: 'jobid', type: 'string' },
//		       { name: 'job_type', type: 'string' },
//               { name: 'file_name', type: 'string' },
//               { name: 'request', type: 'string' },
//               { name: 'detail', type: 'string' },
//		       { name: 'notes', type: 'string' }
//            ]
//        });

//        //jobStore.loadData(jobData);

//        // create the Grid
//        var jobGrid = new Ext.grid.GridPanel({
//            id: 'editorder-grid-editjob',
//            store: jobStore,
//            columns: [
//                { id: 'jobid', header: 'Item', sortable: true, dataIndex: 'jobid', hide: true },
//                { header: 'Item Type', sortable: true, dataIndex: 'job_type' },
//			    { header: 'File Name', sortable: true, dataIndex: 'file_name' },
//                { header: 'Request', sortable: true, dataIndex: 'request' },
//                { header: 'Details', sortable: true, dataIndex: 'detail' },
//                { header: 'Notes', sortable: true, dataIndex: 'notes' }
//            ],
//            stripeRows: true,
//            autoHeight: true,
//            stateful: true,
//            sm: new Ext.grid.RowSelectionModel({
//                singleSelect: true
//            }),
//            stateId: 'jobGrid',

//            tbar: [
//            {
//                xtype: 'combo', id: 'editorder-combo-editjobtype',
//                fieldLabel: 'Job Type',
//                value: '',
//                mode: 'local',
//                store: new Ext.data.JsonStore({
//                    url: "/" + APP_NAME + "/order.aspx/getJobType",
//                    fields: ['id', 'name'],
//                    root: 'tags',
//                    autoLoad: true
//                }),
//                displayField: 'name',
//                valueField: 'id',
//                forceSelection: true,
//                triggerAction: 'all',
//                hiddenName: 'editjobtype',
//                listeners: {
//                    select: {
//                        fn: function(combo, value)
//                        {
//                            var type = Ext.getCmp('editorder-hidden-editjobtype').getValue();
//                            combo.disable();
//                            if (combo.getValue() != type)
//                                edit_order_add_job();
//                        }
//                    }
//                }
//            }, {
//                xtype: 'buttongroup',
//                items: [{
//                    text: 'Add',
//                    handler: function()
//                    {
//                        editJobPanel.getForm().submit({
//                            url: "/" + APP_NAME + "/job.aspx/addNewJob",
//                            waitMsg: 'Please wait...',
//                            success: function(form, o)
//                            {
//                                Ext.Msg.show({
//                                    title: 'Result',
//                                    msg: o.result.result,
//                                    buttons: Ext.Msg.OK,
//                                    icon: Ext.Msg.INFO
//                                });

//                                var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
//                                var xParameter = {}
//                                LoadData(sUrl, xParameter, getNewJob_OnReceived);

//                                function getNewJob_OnReceived(data)
//                                {
//                                    Ext.getCmp('editorder-grid-editjob').getStore().loadData(data);
//                                }
//                            },
//                            failure: function(form, o)
//                            {
//                                Ext.Msg.show({
//                                    title: 'Result',
//                                    msg: o.result.result,
//                                    buttons: Ext.Msg.OK,
//                                    icon: Ext.Msg.ERROR
//                                });
//                            }
//                        });
//                    }
//                }
//            ]
//            }, {
//                xtype: 'buttongroup',
//                items: [
//                    {
//                        text: 'Edit',
//                        handler: function() { alert('Nonimplement'); }
//                    }
//                ]
//            }, {
//                xtype: 'buttongroup',
//                items: [
//                        {
//                            text: 'Delete',
//                            handler: function()
//                            {
//                                var grid = jobGrid;
//                                var selectModel = grid.getSelectionModel();
//                                var rec = selectModel.getSelected();

//                                if (rec == undefined || rec.data.length == 0)
//                                {
//                                    Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
//                                    return;
//                                }

//                                var deleteJobPanel = new Ext.FormPanel({
//                                    items: [
//                                        {
//                                            xtype: 'hidden',
//                                            name: 'deleteJobId',
//                                            value: rec.data.jobid
//                                        }
//                                    ]
//                                });

//                                deleteJobPanel.render(document.body);

//                                deleteJobPanel.getForm().submit({
//                                    url: "/" + APP_NAME + "/job.aspx/deleteNewJob",
//                                    waitMsg: 'Please wait...',
//                                    success: function(form, o)
//                                    {
//                                        Ext.Msg.show({
//                                            title: 'Result',
//                                            msg: o.result.result,
//                                            buttons: Ext.Msg.OK,
//                                            icon: Ext.Msg.INFO
//                                        });

//                                        var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
//                                        var xParameter = {}
//                                        LoadData(sUrl, xParameter, getNewJob_OnReceived);

//                                        function getNewJob_OnReceived(data)
//                                        {
//                                            Ext.getCmp('editorder-grid-editjob').getStore().loadData(data);
//                                        }
//                                    },
//                                    failure: function(form, o)
//                                    {
//                                        Ext.Msg.show({
//                                            title: 'Result',
//                                            msg: o.result.result,
//                                            buttons: Ext.Msg.OK,
//                                            icon: Ext.Msg.ERROR
//                                        });
//                                    }
//                                });
//                            }
//                        }
//                ]
//            }
//            ]
//        });

//        editJobPanel = new Ext.FormPanel({
//            id: 'editorder-editjob-panel',
//            url: "/" + APP_NAME + "/job.aspx/newJob",
//            defaultType: 'textfield',
//            layout: 'column',
//            labelAlign: 'right',
//            buttonAlign: 'left',
//            bodyStyle:'order_bg',
//            anchor: '90%',
//            items: [
//                {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 1,
//                    layout: 'form',
//                    items: {
//                        xtype: 'box',
//                        html:'<br/>'
//                    }
//                },
//                 {
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     layout: 'form',
//                     items: {
//                         xtype: 'textfield',
//                         fieldLabel: 'File Name',
//                         name: 'editjob-filename',
//                         value: '',
//                         anchor:'60%'
//                     }
//                 }, {
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 0.6,
//                     layout: 'form',
//                     items: {
//                         anchor: '100%',
//                         xtype: 'checkboxgroup',
//                         fieldLabel: 'Request',
//                         columns: 4,
//                         layout: 'form',
//                         //NewJob EM FTP CD/DVD Mac PC Test
//                         items: [
//					    { xtype: 'checkbox', boxLabel: 'New Job', name: 'editjob-newjob' },
//					    { xtype: 'checkbox', boxLabel: 'EM', name: 'editjob-em' },
//					    { xtype: 'checkbox', boxLabel: 'FTP', name: 'editjob-ftp' },
//					    { xtype: 'checkbox', boxLabel: 'CD/DVD', name: 'editjob-cddvd' },
//					    { xtype: 'checkbox', boxLabel: 'Mac', name: 'editjob-mac' },
//					    { xtype: 'checkbox', boxLabel: 'Pc', name: 'editjob-pc' },
//					    { xtype: 'checkbox', boxLabel: 'Test', name: 'editjob-test' }
//				    ]
//                     }
//                 },
//                {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 1,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textarea',
//                        fieldLabel: 'Notes',
//                        name: 'editjob-notes',
//                        value: '',
//                        anchor: '80%'
//                    }
//                },
//                {
//                    xtype: 'hidden',
//                    id: 'editorder-hidden-editjobtype',
//                    name: 'editjob-jobtype'
//                }
//            ],
//            buttons: [{
//                text: 'Add to job list',
//                handler: function()
//                {
//                    editJobPanel.getForm().submit({
//                        url: "/" + APP_NAME + "/job.aspx/addNewJob",
//                        waitMsg: 'Please wait...',
//                        success: function(form, o)
//                        {
//                            Ext.Msg.show({
//                                title: 'Result',
//                                msg: o.result.result,
//                                buttons: Ext.Msg.OK,
//                                icon: Ext.Msg.INFO
//                            });

//                            var sUrl = "/" + APP_NAME + "/job.aspx/getNewJobs";
//                            var xParameter = {}
//                            LoadData(sUrl, xParameter, getNewJob_OnReceived);

//                            function getNewJob_OnReceived(data)
//                            {
//                                Ext.getCmp('editorder-grid-newjob').getStore().loadData(data);
//                            }
//                        },
//                        failure: function(form, o)
//                        {
//                            Ext.Msg.show({
//                                title: 'Result',
//                                msg: o.result.result,
//                                buttons: Ext.Msg.OK,
//                                icon: Ext.Msg.ERROR
//                            });
//                        }
//                    });
//                }
//}]
//            });


//            var editOrderPanel = new Ext.FormPanel({
//                id:'editorder-addorder-panel',
//                url: "/" + APP_NAME + "/order.aspx/addNewOrder",
//                defaultType: 'textfield',
//                layout: 'column',
//                containerScroll: true,
//                autoScroll: true,
//                labelAlign: 'right',
//                buttonAlign: 'left',
//                anchor: '90%',
//                items: [
//                {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 1,
//                    layout: 'form',
//                    items: {
//                        xtype: 'box',
//                        html:'<br/>'
//                    }
//                },
//                {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.3,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Order No.',
//                        name: 'orderNo',
//                        anchor: '80%',
//                        value: '--',
//                        readOnly: true
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.7,
//                    layout: 'form',
//                    id: 'editorder-customer-combo',
//                    items: {
//                        anchor: '90%',
//                        xtype: 'combo', id: 'shoutoutsTags',
//                        fieldLabel: 'Customer',
//                        value: '',
//                        mode: 'local',
//                        store: new Ext.data.JsonStore({
//                            url: "/" + APP_NAME + "/order.aspx/getCustomer",
//                            fields: ['id', 'name'],
//                            root: 'tags',
//                            autoLoad: true
//                        }),
//                        displayField: 'name',
//                        valueField: 'id',
//                        forceSelection: true,
//                        triggerAction: 'all',
//                        hiddenName: 'customer_edit',
//                        listeners: {
//                            select: {
//                                fn: function(combo, value)
//                                {
//                                    
//                                    var comboCustomer = Ext.getCmp('editorder-customer-combo');
//                                    var rec = value;
//                                }
//                            }
//                        }
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'datefield',
//                        format: 'Y-m-d',
//                        fieldLabel: 'Received Date',
//                        name: 'received_date',
//                        value: '',
//                        anchor: '80%'
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Tel',
//                        name: 'customer_tel',
//                        anchor: '85%',
//                        value: ''
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Received By',
//                        name: 'received_by',
//                        anchor: '80%',
//                        value: USER_NAME,
//                        readOnly: true
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Contact Person',
//                        name: 'customer_contact_person',
//                        anchor: '85%',
//                        value: ''
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'datefield',
//                        format: 'Y-m-d',
//                        fieldLabel: 'Order Deadline',
//                        name: 'order_deadline',
//                        value: '',
//                        anchor: '80%'
//                    }
//                },
//                {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 1,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textarea',
//                        fieldLabel: 'Remarks',
//                        name: 'remark',
//                        value: '',
//                        anchor: '80%'
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Updated By',
//                        name: 'update_by',
//                        anchor: '80%',
//                        value: USER_NAME,
//                        readOnly: true
//                    }
//                }, {
//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 0.5,
//                    layout: 'form',
//                    items: {
//                        xtype: 'textfield',
//                        fieldLabel: 'Updated Date',
//                        name: 'updateDate',
//                        anchor: '80%',
//                        value: new Date().format('Y-m-d'),
//                        readOnly: true
//                    }
//                },{

//                    xtype: 'container',
//                    autoEl: {},
//                    columnWidth: 1,
//                    layout: 'form',
//                    items: {
//                        xtype: 'container',
//                        layout: 'absolute',
//                        height: 110,
//                        width: 524,
//                        fieldLabel: 'Items',
//                        columnWidth: 1,
//                        items: [
//                            jobGrid
//                        ]
//                    }
//                }
//			],
//                buttons: [{
//                    text: 'Create order',
//                    handler: function()
//                    {
//                        editOrderPanel.getForm().submit({
//                            url: "/" + APP_NAME + "/order.aspx/addNewOrder",
//                            waitMsg: 'Please wait...',
//                            success: function(form, o)
//                            {
//                                Ext.Msg.show({
//                                    title: 'Result',
//                                    msg: o.result.result,
//                                    buttons: Ext.Msg.OK,
//                                    icon: Ext.Msg.INFO
//                                });
//                                editOrderWin.hide();
//                                Ext.getCmp('fp-order-grid').getStore().reload();
//                            },
//                            failure: function(form, o)
//                            {
//                                Ext.Msg.show({
//                                    title: 'Result',
//                                    msg: o.result.result,
//                                    buttons: Ext.Msg.OK,
//                                    icon: Ext.Msg.ERROR
//                                });
//                            }
//                        });
//                    }
//                }, {
//                    text: 'Cancel',
//                    handler: function()
//                    {
//                        editOrderWin.hide();
//                    }
//                }
//                ]
//            });

//            var editorderPanel = new Ext.Panel({
//                layout: 'Column',
//                containerScroll: true,
//                autoScroll: true,
//                anchor:'90%',
//	            items: [
//	                {
//	                    xtype: 'container',
//	                    autoEl: {},
//	                    columnWidth: 1,
//	                    anchor: '90%',
//	                    items: {
//	                        title: 'Part I - Order Summary',
//	                        collapsible: true,
//	                        collapsed: false,
//	                        anchor: '90%',
//	                        items: [
//                            editOrderPanel
//                        ]
//	                    }
//	                },
//	                {
//                        xtype: 'container',
//                        autoEl: {},
//                        columnWidth:1,
//                        anchor: '90%',
//                        items: {
//                            title: 'Part II - Item Summary',
//                            collapsible: true,
//                            collapsed: false,
//                            anchor: '90%',
//                            items: [
//                                editJobPanel
//                            ]
//                        }
//                    }
//	            ]
//            }) 

//            editorderPanel.render(document.body);
//            
//            editOrderWin = new Ext.Window({
//                title: 'Order Information',
//                layout: 'fit',
//                width: '100%',
//                height: 600,
//                closeAction: 'hide',
//                plain: true,
//                maximized:true,
//                items: editorderPanel
//            });
//        }
//        Ext.getCmp('editorder-grid-editjob').getStore().loadData(jobData);
//        editOrderWin.show();
//    }

//    function edit_order_add_job()
//    {
//        var job_type = Ext.getCmp('editorder-combo-editjobtype').getValue();
//        if (!job_type || job_type == "")
//        {
//            Ext.Msg.alert('FP', 'please select a job type');
//            return;
//        }
//        
//        if(!jobDetailsItems)
//        {
//            jobDetailsItems = [
//                 { //Group1
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group0',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other1_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other1_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }
//                     ]
//                 }, { //Group2
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group1',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other2_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other2_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group3
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group2',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other3_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other3_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group4
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group3',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other4_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other4_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group5

//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group4',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other5_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other5_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group6
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group5',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other6_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other6_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group7
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group6',
//                         anchor: '100%',
//                         xtype: 'checkboxgroup',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other7_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other7_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group8
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group7',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other8_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other8_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group9
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group8',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other9_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other9_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group10
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group9',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other10_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other10_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group11
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group10',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other11_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other11_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group12
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group11',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other12_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other12_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group13
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group12',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other13_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other13_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group14
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group13',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other14_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other14_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }, { //Group15
//                     xtype: 'container',
//                     autoEl: {},
//                     columnWidth: 1,
//                     anchor: '100%',
//                     layout: 'form',
//                     items: [{
//                         id: 'newjob-detail-group14',
//                         xtype: 'checkboxgroup',
//                         anchor: '100%',
//                         fieldLabel: ' ',
//                         columns: 4,
//                         items: [
//                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
//				        ]
//                     },{
//                         xtype: 'container',
//                         layout: 'column',
//                         fieldLabel:'',
//                         hideLabel:true,
//                         items:[
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other15_0',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            },
//                            {
//                                 xtype: 'container',
//                                 autoEl: {},
//                                 columnWidth: 0.5,
//                                 layout: 'form',
//                                 items:[
//                                    {
//                                        fieldLabel:'',
//                                        xtype:'textfield',
//                                        hidden:true,
//                                        id:'newjob-detail-other15_1',
//                                        anchor:'100%'
//                                    }
//                                 ]
//                            }
//                         ]
//                     }]
//                 }
//            ];
//            for(var i = 0 ; i < jobDetailsItems.length; i++)
//            {
//                editJobPanel.add(jobDetailsItems[i]);
//            }
//            editJobPanel.doLayout();
//        }
//        editJob(job_type);
//    }

//    function edit_order_edit_job()
//    {
//        editJob();
//    }

//    function edit_order_delete_job()
//    {
//        editJob();
//    }

//    function editJob(job_type)
//    {
//        var conn = new Ext.data.Connection();
//        conn.request({
//            url: "/" + APP_NAME + "/order.aspx/getJobDetailByCategory?categoryId=" + job_type,
//            method: 'POST',
//            success: function(responseObject)
//            {
//                showJobDetailOptions(eval(responseObject.responseText));
//            },
//            failure: function()
//            {
//                Ext.Msg.alert('Status', 'Unable to load job details');
//            }
//        });

//        function showJobDetailOptions(options)
//        {
//            if (!options)
//            {
//                Ext.Msg.alert('Status', 'Unable to load job details');
//                return;
//            }

//            Ext.getCmp('editorder-hidden-editjobtype').setValue(job_type);
//            
//            //hide all other fields
//            
//            for(var i = 0; i < 15; i++)
//            {
//                var sID = 'editjob-detail-other' + (i + 1) + '_0';
//                var otherField = Ext.getCmp(sID);
//                otherField.hide();
//                otherField.setValue('');
//                otherField.setFieldLabel('');
//                
//                sID = 'editjob-detail-other' + (i + 1) + '_1';
//                otherField = Ext.getCmp(sID);
//                otherField.hide();
//                otherField.setValue('');
//                otherField.setFieldLabel('');
//                
//                
//            }

//            //Create check box
//            var group;
//            var items;
//            var checkboxgroup;
//            var columns;
//            var column;
//            var checkbox;
//            //eg. options = [{type:'1',name:'Scanning',items:[{xtype: 'checkbox',boxLabel:'CMYK',name:'CMYK'},{boxLabel:'RGB',name:'RGB'}]}]
//            //Create check box grouo by options
//            for (var i = 0; i < options.length; i++)
//            {
//                //shows other field
//                if(options[i].others)
//                {
//                    for(var j = 0; j < options[i].others.length; j++)
//                    {
//                        otherField = Ext.getCmp(options[i].others[j].id);
//                        otherField.show();
//                        otherField.setFieldLabel(options[i].others[j].label);
//                        otherField.el.dom.name = options[i].others[j].name;
//                    }
//                }
//            
//                group = options[i];
//                checkboxgroup = Ext.getCmp("editjob-detail-group" + i);
//                var cb;

//                checkboxgroup.setLabel(group.name);
//                items = checkboxgroup.items;
//                columns = checkboxgroup.panel.items;

//                for (var j = 0; j < group.items.length; j++)
//                {
//                    cb = checkboxgroup.items.items[j];
//                    if (cb)
//                    {
//                        cb.setBoxLabel(group.items[j].boxLabel);
//                        cb.el.dom.name = group.items[j].name;
//                        cb.setValue(false);
//                        cb.show();
//                        continue;
//                    }

//                    column = columns.itemAt(items.getCount() % columns.getCount());
//                    checkbox = column.add(group.items[j]);

//                    items.add(checkbox);
//                }
//                //Hide no use checkbox
//                for (j = group.items.length; j < checkboxgroup.items.items.length; j++)
//                {
//                    cb = checkboxgroup.items.items[j];
//                    cb.hide();
//                }
//                checkboxgroup.doLayout();
//                checkboxgroup.panel.doLayout();
//                checkboxgroup.show();
//            }

//            //Hide no used checkboxgroup 
//            for (i = options.length; i < 15; i++)
//            {
//                checkboxgroup = Ext.getCmp("editjob-detail-group" + i);
//                checkboxgroup.setLabel('');
//                checkboxgroup.hide();
//            }
//            Ext.getCmp('editorder-combo-editjobtype').enable();
//            Ext.getCmp('editorder-editjob-panel').doLayout();
//            //editJobWin.show();
//        }
//    }

//    