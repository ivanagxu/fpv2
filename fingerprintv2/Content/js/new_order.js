var newOrderWin;
var addJobPanel;
var jobDetailsItems;

function fillCustomerInfoByNo() {
    var sUrl = "/" + APP_NAME + "/order.aspx/getCustomerInfoByNo";
    var number = Ext.getCmp('neworder-customer_no').getValue();
    var xParameter = { customer_no: number, mode: Ext.getCmp('newjob-jobsubmitmode').getValue() };
    LoadData(sUrl, xParameter, fillCustomerInfo);
}
function fillCustomerInfo(data) {
    if (data) {
        if (data.id) {
            
            Ext.getCmp('neworder-customer-combo').setValue(data.id);
            Ext.getCmp('neworder-customer_tel').setValue(data.tel);
            Ext.getCmp('neworder-customer_contact_person').setValue(data.contact);
        }
        else {
            Ext.Msg.alert('Fingerprint', 'Customer is not found');
        }
    }else
    {
        Ext.Msg.alert('Fingerprint', 'Customer is not found');
    }
}

function editOrder()
{
    var grid = Ext.getCmp('fp-order-grid');
    var selectModel = grid.getSelectionModel();
    var rec = selectModel.getSelected();
    
    if(rec == undefined || rec.length == 0)
    {
        Ext.Msg.alert('Fingerprint','Please select a record');
        return;
    }
    newOrder("edit", rec.data.pid);
}

function editOrder2(pid)
{
    Ext.getCmp('newjob-jobsubmitmode').setValue('Edit');
    newOrder("edit", pid);
}

var deleteOrderWin;
function deleteOrder()
{
    var grid = Ext.getCmp('fp-order-grid');
    var selectModel = grid.getSelectionModel();
    var rec = selectModel.getSelected();
    
    if(rec == undefined || rec.length == 0)
    {
        Ext.Msg.alert('Fingerprint','Please select a record');
        return;
    }
    
    
    if(!deleteOrderWin)
    {
        var deleteOrderPanel = new Ext.FormPanel({
            layout:'form',
            buttonAlign:'center',
            id:'deleteJobForm',
            labelWidth:200,
            baseCls: 'x-plain',
            items:[
                {
                    xtype:'textfield',
                    name:'password',
                    inputType: 'password',
                    id:'delete-order-password',
                    fieldLabel:'Please enter your password'
                }
                
            ],
            buttons:[
                {
                    text:'OK',
                    handler:function(){
                        var pwd = Ext.getCmp('delete-order-password').getValue();
                        if(pwd == "")
                            return;
                        
                        var grid = Ext.getCmp('fp-order-grid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();
                        
                        if(rec == undefined || rec.length == 0)
                        {
                            Ext.Msg.alert('Fingerprint','Please select a record');
                            return;
                        }
                        
                        var sUrl = "/" + APP_NAME + "/order.aspx/deleteOrder";
                        var xParameter = {pid:rec.data.pid,pwd:pwd};
                        LoadData(sUrl, xParameter, onDeleteOrderReceived);
                        
                        function onDeleteOrderReceived(data)
                        {
                        
                            deleteOrderWin.hide();
                            
                            Ext.getCmp('fp-order-grid').getStore().reload();
                            
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
                    text:'Cancel',
                    handler:function(){
                        deleteOrderWin.hide();
                    }
                }
            ]
        });
        
        
        deleteOrderWin= new Ext.Window({
			title:'Fingerprint',
			layout:'fit',
			width:400,
			height:100,
			closeAction:'hide',
			plain: true,
			items: deleteOrderPanel
		});
    }
    Ext.getCmp('delete-order-password').setValue('');
    deleteOrderWin.show();
}
function fillOrder(data)
{
    if(!data)
    {
        Ext.Msg.alert('Fingerprint','Load order failed');
        return;
    }
    Ext.getCmp('neworder-pid').setValue(data.pid);
    setYourLocation(data.pid);
    Ext.getCmp('neworder-hidden-pid').setValue(data.pid);
    Ext.getCmp('neworder-customer-combo').setValue(data.cid);
    Ext.getCmp('neworder-customer_no').setValue(data.cid);
    Ext.getCmp('neworder-received_date').setValue(data.received_date);
    Ext.getCmp('neworder-customer_tel').setValue(data.customer_tel);
    Ext.getCmp('neworder-received_by').setValue(data.received_by);
    Ext.getCmp('neworder-customer_contact_person').setValue(data.customer_contact_person);
    Ext.getCmp('neworder-order_deadline').setValue(data.order_deadline);
    Ext.getCmp('neworder-remarks').setValue(data.remarks);
    Ext.getCmp('neworder-modified_date').setValue(data.modified_date);
    Ext.getCmp('neworder-modified_by').setValue(data.modified_by);
    Ext.getCmp('neworder-audit-info').el.dom.innerHTML = "<img width=20 height=20 title='Update By "+ data.update_by +" on " + data.update_date + "' src='" + "/" + APP_NAME + "/Content/images/InfoIcon.gif'/>";
   
    
    var sUrl = "/" + APP_NAME + "/job.aspx/getItemsByOrder";
    var xParameter = {pid:data.pid};
    LoadData(sUrl, xParameter, fillJobList);
}
function fillJobList(data)
{
    if(!data)
    {
        Ext.Msg.alert('Fingerprint','Load items failed');
        return;
    }
    
    Ext.getCmp('neworder-grid-newjob').getStore().loadData(data);
}

var fillJobDetails;
function fillJobDetail(data)
{
    if(data.length > 0)
    {
        Ext.getCmp('neworder-combo-newjobtype').setValue(data[0].job_type);
        Ext.getCmp('neworder-combo-newjobtype').disable();
        new_order_add_job(data);
    }
    else
    {
        Ext.Msg.alert('Fingerprint','Load item detail failed');
    }
    //Ext.getCmp('neworder-combo-newjobtype').setValue('');
}
function newOrder(mode,pid)
{
        //Reset form
        var editMode = Ext.getCmp('newjob-jobsubmitmode').getValue();
        
        Ext.getCmp('neworder-addjob-panel').getForm().reset();
        Ext.getCmp('neworder-addorder-panel').getForm().reset();
        if(jobDetailsItems) 
        {
            addJob('');
            Ext.getCmp('neworder-combo-newjobtype').setValue('');
        }
        Ext.getCmp('newjob-request-container').hide();
        Ext.getCmp('newjob-filename-container').hide();
        Ext.getCmp('newjob-notes-container').hide();
        Ext.getCmp('neworder-received_date').setValue(new Date());
        Ext.getCmp('neworder-hidden-jobtype').setValue("");
        try
        {
            Ext.getCmp('neworder-audit-info').el.dom.innerHTML = "<img width=20 height=20 title='Update By "+ USER_NAME +" on " + new Date().format('Y-m-d') + "' src='" + "/" + APP_NAME + "/Content/images/InfoIcon.gif'/>";
        }catch(e){}
        Ext.getCmp('neworder-customer-combo').store.load({});
        Ext.getCmp('newjob-jobsubmitmode').setValue(editMode);
        //End reset




        Ext.getCmp('neworder-form-panel').expand();

        //alert(Ext.getCmp('neworder-main-panel').getWidth());

        var width = Ext.getCmp('neworder-main-panel').getWidth() - Ext.getCmp('neworder-left-panel').getWidth() - 10;
        Ext.getCmp('neworder-form-panel').setWidth(width);
        Ext.getCmp('neworder-form-panel').syncSize();
        Ext.getCmp('neworder-form-panel').doLayout();
        Ext.getCmp('neworder-form-panel').collapse();
        Ext.getCmp('neworder-form-panel').expand();

        var jobData = [];
        Ext.getCmp('neworder-grid-newjob').getStore().loadData(jobData);
        
        
       
        
        //Is edit mode
        if(mode == "edit")
        {
            //--Ext.getCmp('neworder-butt-saveorder').show();
            //--Ext.getCmp('neworder-butt-createorder').hide();

            
            var sUrl = "/" + APP_NAME + "/order.aspx/getOrderByPid";
            var xParameter = {pid:pid};
            LoadData(sUrl, xParameter, fillOrder);
            
            document.getElementById('order-neworder-link').className='menubar_click';
            document.getElementById('order-monitor-link').className='menubar';
            setYourLocation(pid);
            
        }
        else
        {
             document.getElementById('order-neworder-link').className='menubar_click';
             document.getElementById('order-monitor-link').className='menubar';
             setYourLocation('New Order');
             
             Ext.getCmp('neworder-pid').setValue('--');
             Ext.getCmp('neworder-customer-combo').setValue('');
             Ext.getCmp('neworder-customer_tel').setValue('');
             Ext.getCmp('neworder-received_by').setValue('');
             Ext.getCmp('neworder-customer_contact_person').setValue('');
             Ext.getCmp('neworder-order_deadline').setValue('');
             Ext.getCmp('neworder-remarks').setValue('');
             
             var getUserUrl = "/" + APP_NAME + "/Fingerprint.aspx/getUser";
             LoadData(getUserUrl, {}, OnGetUserReceived);
             function OnGetUserReceived(data) {
                 if (data) {
                     Ext.getCmp('neworder-received_by').setRawValue(data.eng_name);
                     //Ext.getCmp('neworder-received_by').setValue(data.ObjectId);
                    
                 }
             }
        }
        //End check
        
    }

    function new_order_add_job(data)
    {
        var job_type = Ext.getCmp('neworder-combo-newjobtype').getValue();
        if (!job_type || job_type == "")
        {
            Ext.Msg.alert('FP', 'please select a job type');
            return;
        }
        
        if(!jobDetailsItems)
        {
            jobDetailsItems = [
                 { //Group1
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group0',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other1_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other1_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }
                     ]
                 }, { //Group2
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group1',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other2_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other2_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group3
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group2',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other3_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other3_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group4
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group3',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other4_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other4_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group5

                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group4',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other5_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other5_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group6
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group5',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other6_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other6_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group7
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group6',
                         anchor: '100%',
                         xtype: 'checkboxgroup',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other7_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other7_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group8
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group7',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other8_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other8_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group9
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group8',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other9_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other9_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group10
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group9',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other10_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other10_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group11
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group10',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other11_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other11_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group12
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group11',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other12_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other12_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group13
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group12',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other13_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other13_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group14
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group13',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other14_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other14_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }, { //Group15
                     xtype: 'container',
                     autoEl: {},
                     columnWidth: 1,
                     anchor: '100%',
                     layout: 'form',
                     items: [{
                         id: 'newjob-detail-group14',
                         xtype: 'checkboxgroup',
                         anchor: '100%',
                         fieldLabel: ' ',
                         columns: 4,
                         items: [
                            { xtype: 'checkbox', boxLabel: ' ', name: 'newjob-options-none', hidden: false }
				        ]
                     },{
                         xtype: 'container',
                         layout: 'column',
                         fieldLabel:'',
                         hideLabel:true,
                         items:[
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other15_0',
                                        anchor:'100%'
                                    }
                                 ]
                            },
                            {
                                 xtype: 'container',
                                 autoEl: {},
                                 columnWidth: 0.5,
                                 layout: 'form',
                                 items:[
                                    {
                                        fieldLabel:'',
                                        xtype:'textfield',
                                        hidden:true,
                                        id:'newjob-detail-other15_1',
                                        anchor:'100%'
                                    }
                                 ]
                            }
                         ]
                     }]
                 }
            ];
            for(var i = 0 ; i < jobDetailsItems.length; i++)
            {
                addJobPanel.add(jobDetailsItems[i]);
            }
            addJobPanel.doLayout();
        }
        addJob(job_type,data);
    }

    function addJob(job_type,data)
    {
        
        var conn = new Ext.data.Connection();
        conn.request({
            url: "/" + APP_NAME + "/order.aspx/getJobDetailByCategory?categoryId=" + job_type,
            method: 'POST',
            success: function(responseObject)
            {
                showJobDetailOptions(eval(responseObject.responseText),data);
            },
            failure: function()
            {
                Ext.Msg.alert('Status', 'Unable to load job details');
            }
        });

        function showJobDetailOptions(options,dataArr)
        {
            if (!options)
            {
                Ext.Msg.alert('Status', 'Unable to load job details');
                return;
            }
            
            //hide all other fields
            
            for(var i = 0; i < 15; i++)
            {
                var sID = 'newjob-detail-other' + (i + 1) + '_0';
                var otherField = Ext.getCmp(sID);
                otherField.hide();
                otherField.setValue('');
                otherField.setFieldLabel('');
                
                sID = 'newjob-detail-other' + (i + 1) + '_1';
                otherField = Ext.getCmp(sID);
                otherField.hide();
                otherField.setValue('');
                otherField.setFieldLabel('');
                
                
            }

            //Create check box
            var group;
            var items;
            var checkboxgroup;
            var columns;
            var column;
            var checkbox;
            //eg. options = [{type:'1',name:'Scanning',items:[{xtype: 'checkbox',boxLabel:'CMYK',name:'CMYK'},{boxLabel:'RGB',name:'RGB'}]}]
            //Create check box grouo by options
            for (var i = 0; i < options.length; i++)
            {
                //shows other field
                if(options[i].others)
                {
                    for(var j = 0; j < options[i].others.length; j++)
                    {
                        otherField = Ext.getCmp(options[i].others[j].id);
                        otherField.show();
                        otherField.setFieldLabel(options[i].others[j].label);
                        otherField.el.dom.name = options[i].others[j].name;
                    }
                }
            
                group = options[i];
                checkboxgroup = Ext.getCmp("newjob-detail-group" + i);
                
                
                
                //set bgcolor

                if(i % 2 == 0)
                    set_checkboxgroup_bgcolor(checkboxgroup,"#E9F1E7");
                else
                    set_checkboxgroup_bgcolor(checkboxgroup,"#FCFEFC");
                    
//                set_textfield_bocolor(Ext.getCmp('newjob-file_name'),"#E9F1E7");
//                set_textfield_bocolor(Ext.getCmp('newjob-notes'),"#E9F1E7");
//                set_checkboxgroup_bgcolor(Ext.getCmp('newjob-request-request'),"#E9F1E7");
//                set_textfield_bocolor(Ext.getCmp('neworder-grid-newjob'),"#E9F1E7");
                
                var cb;

                checkboxgroup.setLabel(group.name);
                items = checkboxgroup.items;
                columns = checkboxgroup.panel.items;

                for (var j = 0; j < group.items.length; j++)
                {
                    if (group.items[j].boxLabel == '')
                        group.items[j].boxLabel = ' ';
                    cb = checkboxgroup.items.items[j];
                    if (cb)
                    {
                        cb.setBoxLabel(group.items[j].boxLabel);
                        cb.el.dom.name = group.items[j].name;
                        cb.setValue(false);
                        cb.show();
                        if (group.items[j].boxLabel == ' ')
                            cb.disable();
                        continue;
                    }

                    column = columns.itemAt(items.getCount() % columns.getCount());
                    checkbox = column.add(group.items[j]);

                    items.add(checkbox);

                    if (group.items[j].boxLabel == ' ')
                        checkbox.disable();
                }
                //Hide no use checkbox
                for (j = group.items.length; j < checkboxgroup.items.items.length; j++)
                {
                    cb = checkboxgroup.items.items[j];
                    cb.hide();
                }
                checkboxgroup.doLayout();
                checkboxgroup.panel.doLayout();
                checkboxgroup.show();
                
                if(i % 2 == 0)
                    set_checkboxgroup_bgcolor(checkboxgroup,"#E9F1E7");
                else
                    set_checkboxgroup_bgcolor(checkboxgroup,"#FCFEFC");
            }

            //Hide no used checkboxgroup 
            for (i = options.length; i < 15; i++)
            {
                checkboxgroup = Ext.getCmp("newjob-detail-group" + i);
                checkboxgroup.setLabel('');
                checkboxgroup.hide();
                

                set_checkboxgroup_bgcolor(checkboxgroup,"#AAE6A2");

            }
            
            Ext.getCmp('neworder-addjob-panel').getForm().reset();
            
            Ext.getCmp('neworder-hidden-jobtype').setValue(job_type);
            Ext.getCmp('neworder-hidden-pid').setValue(Ext.getCmp('neworder-pid').getValue());
            
            if(dataArr)
            {
                
                var data = dataArr[0];
                
                Ext.getCmp('neworder-hidden-jobid').setValue(data.jobid);
                Ext.getCmp('newjob-request-mac').setValue(data.mac);
                Ext.getCmp('newjob-request-pc').setValue(data.pc);
                Ext.getCmp('newjob-request-newjob').setValue(data.newjob);
                Ext.getCmp('newjob-request-em').setValue(data.em);
                Ext.getCmp('newjob-request-ftp').setValue(data.ftp);
                Ext.getCmp('newjob-request-cddvd').setValue(data.cddvd);
                Ext.getCmp('newjob-request-test').setValue(data.test);
                
                Ext.getCmp('newjob-file_name').setValue(data.file_name);
                Ext.getCmp('newjob-notes').setValue(data.notes);

//                Ext.getCmp('newjob-quantity').setValue(data.qty);
//                Ext.getCmp('newjob-size').setValue(data.size);
//                Ext.getCmp('newjob-id-unit').setValue(data.unit);
                var tQ = data.qty.split('　');
                var tS = data.size.split('　');
                var tU = data.unit.split('　');
                var tStore = Ext.getCmp('neworder_job_quantity_list').store;
                tStore.removeAll();
                for (var i = 0; i < tQ.length; i++) {
                    var valStr = tQ[i] + '　' + tS[i] + '　' + tU[i];
                    tStore.add(
                        new tStore.recordType(
                            {
                                NAME: valStr,
                                VALUE: valStr
                            }
                        )
                    );
                }

                var checkItems = data.print_job.split('/');
                for(var i = 0; i < checkItems.length; i++)
                {
                    try
                    {
                        var tmpCb = Ext.query("*[name=item" + checkItems[i] + "]")[0];
                        tmpCb.checked = true;
                    }catch(e)
                    {
                        //alert(e.message);
                    }
                }
                
                var tmptxt = Ext.query("*[name=Fdelivery_address]")[0];
                if(tmptxt)
                    tmptxt.value = data.Fdelivery_address;
                
                tmptxt = Ext.query("*[name=Fdelivery_date]")[0];
                if(tmptxt)
                    tmptxt.value = data.Fdelivery_date;
                    
                tmptxt = Ext.query("*[name=Fpaper]")[0];
                if(tmptxt)
                    tmptxt.value = data.Fpaper;
                
                tmptxt = Ext.query("*[name=Fcolor]")[0];
                if(tmptxt)
                    tmptxt.value = data.Fcolor;
                
                
            }
            else
            {

            }

            
    
        Ext.getCmp('neworder-combo-newjobtype').enable();
        Ext.getCmp('neworder-addjob-panel').doLayout();
    }
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
        js = "Finished";
    else
        js = "";

    if (ft == 0)
        ft = "customer_number";
    else if (ft == 1)
        ft = "customer_name"
    else if (ft == 3)
        ft = "invoice_no";
    else if (ft == 2)
        ft = "order_no";
    else if (ft == 4)
        ft = "sales"
        
    Ext.getCmp('fp-order-grid').getStore().load(
        {
            params: {
                jt:jt,
                js:js,
                ft:ft,
                fv:fv
            }
        }
    );
}