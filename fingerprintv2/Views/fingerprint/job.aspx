<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Jobs
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID=styleSheetContent runat="server">
    <link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-blue.css") %>/>
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
         background: #EBF3FD repeat-x scroll left bottom;
        }
        
        .menubar_click {
         background: #DFE8F6;
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
         background: #EBF3FD repeat-x scroll left bottom;
        }


 #neworder-addorder-panel .x-panel-body {
            background-color: #AAE6A2 ! important;
        }
        
        
        #newjob-vieworder-panel .x-panel-body {
            background-color: #FFFBE8 ! important;
        }
        #newjob-form-panel .x-panel-body {
            background-color: #FFFBE8 ! important;
        }
        
        #job-center-panel .x-panel-body {
            background-color: #FFFBE8 ! important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">

    <script type="text/javascript" src=<%=Html.link("Content/js/new_order.js") %>></script>
    <script type="text/javascript" src=<%=Html.link("Content/js/new_job.js") %>></script>
    
    <script type="text/javascript">
        Ext.onReady(function()
        {
            Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

            var leftPanel = new Ext.Panel({
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

            jobStore = new Ext.data.JsonStore({
                // store configs
                autoDestroy: true,
                url: "/" + APP_NAME + "/job.aspx/getJob",
                remoteSort: true,
                sortInfo: {
                    field: 'jobid',
                    direction: 'DESC'
                },
                storeId: 'jobStore',

                // reader configs
                idProperty: 'jobid',
                root: 'data',
                totalProperty: 'total',
                fields: [{
                    name: 'jobid',
                    type: 'string'
                }, {
                    name: 'job_type',
                    type: 'string'
                }, {
                    name: 'handled_by',
                    type: 'string'
                }, {
                    name: 'custumer_name',
                    type: 'String'

                }, {
                    name: 'job_deadline',
                    type: 'String'
                }, {
                    name: 'notes',
                    type: 'string'
                }, {
                    name: 'job_status',
                    type: 'String'
                }
				]
            });

            var filters = new Ext.ux.grid.GridFilters({
                encode: false,
                local: true,
                filters: [{
                    type: 'string',
                    dataIndex: 'jobid'
                }, {
                    type: 'string',
                    dataIndex: 'job_type'
                }, {
                    type: 'string',
                    dataIndex: 'handled_by'
                }, {
                    type: 'string',
                    dataIndex: 'customer_name'
                }, {
                    type: 'string',
                    dataIndex: 'job_deadline'
                }, {
                    type: 'string',
                    dataIndex: 'notes'
                }, {
                    type: 'string',
                    dataIndex: 'job_status'
                }]
                });

                var sm = new Ext.grid.CheckboxSelectionModel({singleSelect: true});
                var createColModel = function(finish, start)
                {

                    var columns = [sm,
                    {
                        dataIndex: 'jobid',
                        header: 'Job No.',
                        filterable: true,
                        filter: {
                            type: 'string'
                        },
                        renderer:jobidRenderer
                    }, {
                        dataIndex: 'job_type',
                        header: 'Job Type',
                        filterable: true
                    }, {
                        dataIndex: 'handled_by',
                        header: 'Handled by',
                        filterable: true
                    }, {
                        dataIndex: 'customer_name',
                        header: 'Customer',
                        filterable: true
                    }, {
                        dataIndex: 'job_deadline',
                        header: 'Job Deadline',
                        filterable: true
                    }, {
                        dataIndex: 'notes',
                        header: 'Notes',
                        filterable: true
                    }, {
                        dataIndex: 'job_status',
                        header: 'Status',
                        renderer : renderJobStatus,
                        filterable: true
                    }
				];

                    return new Ext.grid.ColumnModel({
                        columns: columns.slice(start || 0, finish),
                        defaults: {
                            sortable: true
                        }
                    });
                };

                //Order grid
                var jobGrid = new Ext.grid.GridPanel({
                    id:'newjob-jobgrid',
                    border: false,
                    store: jobStore,
                    height: '100%',
                    colModel: createColModel(7),
                    selModel:sm,
                    loadMask: true,
                    plugins: [filters],
                    stripeRows: true,
                    flex: 5,
                    listeners: {
                        render: {
                            fn: function()
                            {
                                jobStore.load({
                                    params: {
                                        start: 0,
                                        limit: 50
                                    }
                                });
                            }
                        },
                        contextMenu: {
                            fn: function(e)
                            {
                                e.stopEvent();
                                //filingMenu.showAt(e.xy);
                            }
                        },
                        rowdblclick: onClick
                    },
                    bbar: new Ext.PagingToolbar({
                        store: jobStore,
                        pageSize: 25,
                        plugins: [filters],
                        displayInfo: true,
                        displayMsg: 'Displaying record {0} - {1} of {2}',
                        emptyMsg: "No record to display"
                    }),
                    tbar: [{
                        xtype: 'buttongroup',
                        hidden:true,
                        items: [{
                            text: 'New Job',
                            handler: newJob
}]
                        }, {
                            xtype: 'buttongroup',
                            hidden:true,
                            items: [{
                                text: 'Approve',
                                
                                handler: onClick
}]
                            }, {
                                xtype: 'buttongroup',
                                items: [{
                                    text: 'Edit',
                                    handler: newJob
}]
                                }, {
                                    xtype: 'buttongroup',
                                    items: [{
                                        text: 'Delete',
                                        handler: deleteJob
}]
}]
                });
                
            var orderPanel = new Ext.FormPanel(
            {
                id:'newjob-vieworder-panel',
                defaultType: 'textfield',
                layout: 'column',
                containerScroll: true,
                autoScroll: true,
                labelAlign: 'right',
                buttonAlign: 'left',
                anchor: '89%',
                items: [
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'box',
                        html:'<br/>'
                    }
                },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Order No.',
                        name: 'orderNo',
                        id:'vieworder-pid',
                        anchor: '40%',
                        value: '--',
                        readOnly: true
                    }
                },  {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'datefield',
                        format: 'Y-m-d',
                        fieldLabel: 'Received Date',
                        name: 'received_date',
                        id:'vieworder-received_date',
                        value: '',
                        anchor: '40%',
                        readOnly: true
                    }
                },  {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Received By',
                        name: 'received_by',
                        id:'vieworder-received_by',
                        anchor: '40%',
                        value: '',
                        readOnly: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth:1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'datefield',
                        format: 'Y-m-d',
                        fieldLabel: 'Order Deadline',
                        name: 'order_deadline',
                        id:'vieworder-order_deadline',
                        value: '',
                        anchor: '40%',
                        readOnly: true
                    }
                },{
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Customer Name',
                        name: 'customer_name',
                        id:'vieworder-customer_name',
                        anchor: '40%',
                        value: '',
                        readOnly: true
                    }
                },{
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Customer Tel',
                        name: 'customer_tel',
                        id:'vieworder-customer_tel',
                        anchor: '40%',
                        value: '',
                        readOnly: true
                    }
                }, {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth:1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textfield',
                        fieldLabel: 'Contact',
                        name: 'customer_contact_person',
                        id:'vieworder-customer_contact_person',
                        anchor: '40%',
                        value: '',
                        readOnly: true
                    }
                },
                {
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    labelAlign: 'right',
                    items: {
                        xtype: 'textarea',
                        fieldLabel: 'Remarks',
                        name: 'remark',
                        id:'vieworder-remarks',
                        value: '',
                        anchor: '60%',
                        readOnly: true
                    }
                }, {
                    xtype: 'hidden',
                    id: 'vieworder-jobsubmitmode',
                    name: 'vieworder-jobsubmitmode'
                }
			],
            buttons: []
            }
            );
            
          
            var addjobPanel = new Ext.FormPanel({
                labelAlign:'right',
                items:[
                {
                    xtype: 'textfield',
                    fieldLabel: 'Section',
                    name: 'newjob-section',
                    id:'newjob-section',
                    value: '',
                    anchor: '40%',
                    readOnly: true
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'Job No.',
                    name: 'newjob-jobid',
                    id:'newjob-jobid',
                    value: '',
                    anchor: '40%',
                    readOnly: true
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'File Name',
                    name: 'newjob-filename',
                    id:'newjob-filename',
                    value: '',
                    anchor: '40%',
                    readOnly: true
                }, {
                    xtype: 'textfield',
                    fieldLabel: 'Request',
                    name: 'newjob-request',
                    id:'newjob-request',
                    value: '',
                    anchor: '40%',
                    readOnly: true
                },{
                    xtype: 'textfield',
                    fieldLabel: 'Notes',
                    name: 'newjob-notes',
                    id:'newjob-notes',
                    value: '',
                    anchor: '60%',
                    readOnly: true
                },{
                    xtype: 'combo', id: 'newjob-handledby',
                    fieldLabel: 'Handled By',
                    value: '',
                    mode: 'local',
                    store: new Ext.data.JsonStore({
                        url: "/" + APP_NAME + "/job.aspx/getJobHandlerComboList",
                        fields: ['id', 'name'],
                        root: 'tags',
                        autoLoad: true
                    }),
                    displayField: 'name',
                    valueField: 'id',
                    forceSelection: true,
                    triggerAction: 'all',
                    hiddenName: 'newjob-hidden-handledby',
                    listeners: {
                        select: {
                            fn: function(combo, value)
                            {
                                
                            }
                        }
                    }
                },{
                    xtype: 'container',
                    autoEl: {},
                    columnWidth: 1,
                    layout: 'form',
                    items: {
                        xtype: 'box',
                        html:'<br/>'
                    }
                },{
                    xtype: 'textarea',
                    fieldLabel: 'Details',
                    name: 'newjob-itemdetails',
                    id:'newjob-itemdetails',
                    value: '',
                    anchor: '60%',
                    readOnly: true
                }]
            })
            
            
        	
        	var newJobStore = new Ext.data.ArrayStore({
            fields: [
                   { name: 'jobindex', type: 'string' },
		           { name: 'purpose', type: 'string' },
		           { name: 'qty', type: 'string' },
                   { name: 'size', type: 'string' },
                   { name: 'cost', type: 'string' },
		           { name: 'datetime', type: 'string' }
                ]
            });

            // create the Grid
            var newJobGrid = new Ext.grid.GridPanel({
                id: 'newjob-newjobJobGrid',
                store: newJobStore,
                columns: [
                    { id: 'jobindex', header: '',hidden:true , sortable: true, dataIndex: 'jobindex' },
                    { header: 'Purpose', sortable: true, dataIndex: 'purpose' },
                    { header: 'Qty.', sortable: true, dataIndex: 'qty' },
			        { header: 'Size(cm x cm)', sortable: true, dataIndex: 'size' },
                    { header: 'Cost', sortable: true, dataIndex: 'cost' },
                    { header: 'Date & Time', sortable: true, dataIndex: 'datetime' }
                ],
                sm: new Ext.grid.RowSelectionModel({
                    singleSelect: true
                }),
                height:100,
                stateId: 'newjob-newjobJobGrid'
                
            });
	        var unitStore = new Ext.data.ArrayStore({
                fields: ['name', 'value'],
                data : [
		            ['MM', 'MM'],
		            ['PCS', 'PCS']
	            ]
            });
            
            var jobStatusPanel = new Ext.FormPanel({
                defaultType: 'textfield',
                layout: 'form',
                labelAlign: 'right',
                buttonAlign: 'center',
                anchor: '90%',
                border:false,
                items:[
                {
                    xtype:'hidden',
                    id:'add-jobdetail-pid',
                    name:'pid'
                },
                {
                    xtype:'hidden',
                    id:'add-jobdetail-jobid',
                    name:'jobid'
                },
                {
                      xtype: 'radiogroup',
                      fieldLabel: 'Job Status',
                      id:'newjob-statusgroup',
                      name: 'newjob-status',
                      border:false,
                      anchor:'55%',
                      items:[{
                        value:2,
                        inputValue:2,
                        checked:true,
                        name:'newjob-status',
                        boxLabel:'New'
                      }, {
                        value:3,
                        inputValue:3,
                        name:'newjob-status',
                        boxLabel:'WIP'
                      }, {
                        value:0,
                        inputValue:0,
                        name:'newjob-status',
                        boxLabel:'Pending'
                      }, {
                        value:1,
                        inputValue:1,
                        name:'newjob-status',
                        boxLabel:'Finish'
                      }],
                      listeners:{
                        change:function(){
                            var value = Ext.getCmp('newjob-statusgroup').getValue();
                        }
                      }
                    },{
                      xtype: 'radiogroup',
                      fieldLabel: 'Purpose',
                      name: 'newjob-purpose',
                      anchor:'42%',
                      items:[{
                        value:0,
                        inputValue:0,
                        checked:true,
                        name:'newjob-purpose',
                        boxLabel:'Test'
                      }, {
                        value:1,
                        inputValue:1,
                        name:'newjob-purpose',
                        boxLabel:'Redo'
                      }, {
                        value:2,
                        inputValue:2,
                        name:'newjob-purpose',
                        boxLabel:'Final'
                      }]
                    },
                    {
                        xtype:'container',
                        layout:'column',
                        border:false,
                        items:[
                            {
                                xtype :'box',
                                html:'<table width=100><tr><td></td></tr></table>'
                            },
                            {
                                xtype :'textfield',
                                name:'new-detail-x',
                                id:'new-detail-x'
                            },{
                                xtype :'box',
                                html:'X'
                            },{
                                xtype :'textfield',
                                name:'new-detail-y',
                                id:'new-detail-y'
                            },{
                                xtype: 'combo', id: 'newjob-unitcombo',
                                fieldLabel: '',
                                mode: 'local',
                                store: unitStore,
                                displayField: 'name',
                                valueField: 'value',
                                hiddenName:'new-detail-unit',
                                forceSelection: true,
                                triggerAction: 'all',
                                width:100,
                                listeners: {
                                    select: {
                                        fn: function(combo, value)
                                        {
                                            
                                        }
                                    }
                                }
                            }
                        ]
                        
                    },
                    {
                        xtype:'container',
                        layout:'column',
                        border:false,
                        items:[
                        {
                            xtype :'box',
                            html:'<table width=10><tr><td></td></tr></table>'
                        },
                        {
                            xtype :'button',
                            text:'Add',
                            handler:function(){
                                
                                jobStatusPanel.getForm().submit({
                                    url: "/" + APP_NAME + "/job.aspx/addJobDetail",
                                    waitMsg: 'Please wait...',
                                    success: function(form, o)
                                    {
                                        newJob("edit",Ext.getCmp('add-jobdetail-jobid').getValue());
                                    },
                                    failure: function(form, o)
                                    {
                                        Ext.Msg.show({
                                            title: 'Result',
                                            msg: o.result.result,
                                            buttons: Ext.Msg.OK,
                                            icon: Ext.Msg.ERROR
                                        });
                                    }
                                });
                            }
                        },
                        {
                            xtype :'box',
                            html:'<table width=10><tr><td></td></tr></table>'
                        },
                        {
                            xtype :'button',
                            text:'Delete',
                            handler:function(){
                                
                                var grid = Ext.getCmp('newjob-newjobJobGrid');
                                var selectModel = grid.getSelectionModel();
                                var rec = selectModel.getSelected();
                                
                                if(rec == undefined || rec.length == 0)
                                {
                                    Ext.Msg.alert('Fingerprint','Please select a record');
                                    return;
                                }
                                var sUrl = "/" + APP_NAME + "/job.aspx/deleteJobDetail";
                                var jobid = Ext.getCmp('add-jobdetail-jobid').getValue();
                                
                                //alert(rec.data.jobindex);
                                
                                var xParameter = {jobid:jobid,objectId:rec.data.jobindex, createDate:rec.data.datetime};
                                LoadData(sUrl, xParameter, OnDeleteJobDetail_Received);
                                
                                function OnDeleteJobDetail_Received(data)
                                {
                                    newJob("edit",Ext.getCmp('add-jobdetail-jobid').getValue());
                                    Ext.Msg.show({
                                        title: 'Fingerprint',
                                        msg: data.result,
                                        buttons: Ext.Msg.OK,
                                        icon: Ext.Msg.INFO
                                    });
                                    
                                    var jobid = Ext.getCmp('add-jobdetail-jobid').getValue();
                                    var sUrl = "/" + APP_NAME + "/job.aspx/getJobByItem";
                                    var xParameter = {jobid:jobid};
                                    LoadData(sUrl, xParameter, fillJobs);
                                }
                            }
                        }
                        ]
                    }
                    ,
                    {

                        xtype: 'container',
                        autoEl: {},
                        columnWidth: 1,
                        items: {
                            xtype: 'container',
                            layout: 'absolute',
                            x:50,
                            y:10,
                            height: 110,
                            width: 524,
                            fieldLabel: '',
                            columnWidth: 1,
                            items: [
                                newJobGrid
                            ]
                        }
                    }
                    ,
                    {
                        xtype :'box',
                        html:'<br/><br/><br/><br/>'
                    }

                ]
            })

            var newjobPanel = new Ext.Panel({
                id:'newjob-form-panel',
                layout: 'Column',
                containerScroll: true,
                autoScroll: true,
                region:'east',
                width:'89%',
                margins: '3 0 3 3',
                cmargins: '3 3 3 3',
                defaults: { margins: '0 0 5 0' },
                collapsible: true,
                collapsed:false,
                animCollapse:false,
                hideCollapseTool:true,
                labelAlign: 'right',
                buttonAlign:'center',
                listeners: {
                    collapse: {
                        fn: function(panel)
                        {
                            Ext.getCmp('job-center-panel').doLayout();
                            setYourLocation("Monitor");
                        }
                    }
                },
	            items: [
	                {
	                    xtype:'container',
	                    autoEL:{},
	                    columnWidth:1,
	                    anchor:'100%',
	                    items:[
	                        {
                                id:'your-order-location2',
                                xtype : 'box',
                                anchor: '100%',
                                html: "<a href='#' class='leftstyle1'>Job</a> → <a href='#' class='leftstyle1'>Monitor</a>"
                            }
	                    ]
	                },{
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
                                orderPanel
                        ]
	                    }
	                },
	                {
                        xtype: 'container',
                        autoEl: {},
                        columnWidth:1,
                        anchor: '90%',
                        items: {
                            title: 'Part II - Job Summary',
                            collapsible: true,
                            collapsed: false,
                            anchor: '90%',
                            layout:'form',
                            items: [
                                addjobPanel
                            ]
                        }
                    },
                    {
                        xtype: 'container',
                        autoEl: {},
                        columnWidth:1,
                        anchor: '90%',
                        items: {
                            title: 'Part III - Job Status',
                            collapsible: true,
                            collapsed: false,
                            anchor: '90%',
                            layout:'form',
                            items: [
                                jobStatusPanel
                            ]
                        }
                    }
	            ],
                buttons: [
                    {
                        text: 'Complete and save',
                        handler: function()
                        {
                            var jobid = Ext.getCmp('add-jobdetail-jobid').getValue();
                            var status = 0;
                             
                            if(Ext.getCmp('newjob-statusgroup').items.items[3].getValue())
                                status = 1;
                            if(Ext.getCmp('newjob-statusgroup').items.items[2].getValue())
                                status = 0;
                            if(Ext.getCmp('newjob-statusgroup').items.items[0].getValue())
                                status = 2
                            if(Ext.getCmp('newjob-statusgroup').items.items[1].getValue())
                                status = 3
                            var handledBy = Ext.getCmp('newjob-handledby').getValue();
                            
                            var sUrl = "/" + APP_NAME + "/job.aspx/updateItem";
                            
                            var xParameter = {jobid:jobid, status:status, handledBy:handledBy};
                            LoadData(sUrl, xParameter, OnUpdateItem_Received);
                            
                            function OnUpdateItem_Received(data)
                            {
                                newJob("edit",Ext.getCmp('add-jobdetail-jobid').getValue());
                                Ext.Msg.show({
                                    title: 'Fingerprint',
                                    msg: data.result,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.Msg.INFO
                                });
                                
                                Ext.getCmp('newjob-form-panel').collapse();
                            }
                        }
                    }
                ]
            }) 
                var centerPanel = new Ext.Panel({
                    id:'job-center-panel',
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
                        id: 'your-job-location',
                        xtype: 'box',
                        anchor: '100%',
                        html: "<a href='#' class='leftstyle1'>Job</a> → <a href='#' class='leftstyle1'>Monitor</a>"
                    },
                    jobGrid
                ]
                });

                var mainPanel = new Ext.Panel({
                    contentEl: 'fingerprint-job-body',
                    closable: false,
                    autoScroll: true,
                    plain: true,
                    layout: 'border',
                    anchor: '-1, -100',
                    items: [leftPanel, centerPanel ,newjobPanel]
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
                    Ext.getCmp('newjob-form-panel').collapse();
                    fn_click(document.getElementById('job'));
                })

        function onClick()
        {
            alert("NonImplemented");
        }
        function renderJobStatus(val)
        {
            if (val == "1")
                return "Finish";
            else
                return "Pending";
        }
        function setYourLocation(val)
        {
            var a = Ext.getCmp('your-order-location');
            var location = "<a href='#' class='leftstyle1'>Job</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
            try{
                a.el.dom.innerHTML = location;
            }
            catch(e)
            {}
            
            a = Ext.getCmp('your-order-location2');
            location = "<a href='#' class='leftstyle1'>Job</a> → <a href='#' class='leftstyle1'>" + val + "</a>"
            try{
                a.el.dom.innerHTML = location;
            }
            catch(e)
            {}
        }
        function jobidRenderer(val)
        {
            return "<a href='#' onclick =editJob('" + val + "')>" + val + "</a>";
        }
    </script>

</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="fingerprint-job-body"></div>
    <div id="fingerprint-job-left" class="x-hide-display">
        <!--<table width="100%">
            <tr>
                <td align="center"><span>
                <a id="order-monitor-link" 
                    class="leftstyle1_1" 
                    href="#" 
                    onclick="this.className='leftstyle1_1';document.getElementById('order-newjob-link').className='leftstyle1';setYourLocation('Monitor')" 
                    onmouseover="set_underline(this)" 
                    onmouseout="set_underline(this)">
                    Monitor
                    </a>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center"><span>
                <a id="order-newjob-link" 
                    class="leftstyle1" href="#" 
                    onclick="newJob();this.className='leftstyle1_1';document.getElementById('order-monitor-link').className='leftstyle1';setYourLocation('New Job')" 
                    onmouseover="set_underline(this)" 
                    onmouseout="set_underline(this)">
                    New Job
                    </a>
                    </span>
                </td>
            </tr>
        </table>-->
    </div>
    
    <ul id="leftmenu" class="x-hidden mymenu">
      <li><a id="job-monitor-link"  class="menubar_click" onclick="document.getElementById('job-monitor-link').className='menubar_click';document.getElementById('job-newjob-link').className='menubar';setYourLocation('Monitor');Ext.getCmp('newjob-form-panel').collapse();"

      >Monitor</a></li>
      <li><a id="job-newjob-link"  class="menubar" style="display:none" onclick="document.getElementById('job-newjob-link').className='menubar_click';document.getElementById('job-monitor-link').className='menubar';setYourLocation('New Job');newJob();"
      
      >New Job</a></li>
    </ul>
</asp:Content>
