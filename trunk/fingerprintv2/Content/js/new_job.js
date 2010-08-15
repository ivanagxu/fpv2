function fillJob(data)
{
     Ext.getCmp('vieworder-pid').setValue(data.pid);
     Ext.getCmp('vieworder-received_date').setValue(data.received_date);
     Ext.getCmp('vieworder-received_by').setValue(data.received_by);
     Ext.getCmp('vieworder-order_deadline').setValue(data.order_deadline);
     Ext.getCmp('vieworder-customer_name').setValue(data.customer_name);
     Ext.getCmp('vieworder-customer_tel').setValue(data.customer_tel);
     Ext.getCmp('vieworder-customer_contact_person').setValue(data.customer_contact_person);
     Ext.getCmp('vieworder-remarks').setValue(data.remarks);
     Ext.getCmp('vieworder-jobsubmitmode').setValue('edit');

     Ext.getCmp('newjob-section').setValue(data.section);
     Ext.getCmp('newjob-jobid').setValue(data.jobid);
     Ext.getCmp('newjob-filename').setValue(data.filename);
     Ext.getCmp('newjob-request').setValue(data.request);
     Ext.getCmp('newjob-notes').setValue(data.notes);
     Ext.getCmp('newjob-handledby').setValue(data.handledby);
     
     Ext.getCmp('add-jobdetail-pid').setValue(data.pid);
     Ext.getCmp('add-jobdetail-jobid').setValue(data.jobid);
     
     Ext.getCmp('newjob-itemdetails').setValue(data.item_details);
     
     Ext.getCmp('newjob-statusgroup').setValue(data.job_status);
     
     var t = Ext.getCmp('newjob-statusgroup');
     if(data.job_status == 1)
        Ext.getCmp('newjob-statusgroup').items.items[3].setValue(true);
     else if(data.job_status == 0)
        Ext.getCmp('newjob-statusgroup').items.items[2].setValue(true);
     else if(data.job_status == 2)
        Ext.getCmp('newjob-statusgroup').items.items[0].setValue(true);
    else if(data.job_status == 3)
        Ext.getCmp('newjob-statusgroup').items.items[1].setValue(true);

     
     setYourLocation(data.jobid);
     
     var sUrl = "/" + APP_NAME + "/job.aspx/getJobByItem";
     var xParameter = {jobid:data.jobid};
     LoadData(sUrl, xParameter, fillJobs);
}

function fillJobs(data)
{
    if(!data)
    {
        Ext.Msg.alert('Fingerprint','Load Job failed');
        return;
    }
    
    Ext.getCmp('newjob-newjobJobGrid').getStore().loadData(data);
}

function editJob(jobid)
{
    newJob("edit",jobid);
}
function newJob(mode,jobid)
{
     //Reset from
     Ext.getCmp('vieworder-pid').setValue('--');
     Ext.getCmp('vieworder-received_date').setValue('');
     Ext.getCmp('vieworder-received_by').setValue('');
     Ext.getCmp('vieworder-order_deadline').setValue('');
     Ext.getCmp('vieworder-customer_name').setValue('');
     Ext.getCmp('vieworder-customer_tel').setValue('');
     Ext.getCmp('vieworder-customer_contact_person').setValue('');
     Ext.getCmp('vieworder-remarks').setValue('');
     Ext.getCmp('vieworder-jobsubmitmode').setValue('');

     Ext.getCmp('newjob-section').setValue('');
     Ext.getCmp('newjob-jobid').setValue('');
     Ext.getCmp('newjob-filename').setValue('');
     Ext.getCmp('newjob-request').setValue('');
     Ext.getCmp('newjob-notes').setValue('');
     Ext.getCmp('newjob-handledby').setValue('');
     Ext.getCmp('newjob-itemdetails').setValue('');
     
     
     Ext.getCmp('newjob-unitcombo').setValue('');
     Ext.getCmp('new-detail-x').setValue('');
     Ext.getCmp('new-detail-y').setValue('');
     //End reset
     
     if(mode == "edit" && jobid)
     {
     
            var sUrl = "/" + APP_NAME + "/job.aspx/getJobById";
            var xParameter = {jobid:jobid};
            LoadData(sUrl, xParameter, fillJob);
     }
     else
     {
        var grid = Ext.getCmp('newjob-jobgrid');
        var selectModel = grid.getSelectionModel();
        var rec = selectModel.getSelected();
        
        if(rec == undefined || rec.length == 0)
        {
            Ext.Msg.alert('Fingerprint','Please select a record');
            return;
        }
        
        return newJob("edit",rec.data.jobid);
    }

    var width = Ext.getCmp('newjob-main-panel').getWidth() - Ext.getCmp('newjob-left-panel').getWidth() - 10;
    Ext.getCmp('newjob-form-panel').setWidth(width);
    Ext.getCmp('newjob-form-panel').syncSize();
    Ext.getCmp('newjob-form-panel').doLayout();
    Ext.getCmp('newjob-form-panel').collapse();
     
    Ext.getCmp('newjob-form-panel').show();
    Ext.getCmp('newjob-form-panel').expand();
    document.getElementById('job-newjob-link').className='menubar_click';document.getElementById('job-monitor-link').className='menubar';setYourLocation('New Job');
    
}


var deleteJobWin;
function deleteJob()
{
    var grid = Ext.getCmp('newjob-jobgrid');
    var selectModel = grid.getSelectionModel();
    var rec = selectModel.getSelected();
    
    if(rec == undefined || rec.length == 0)
    {
        Ext.Msg.alert('Fingerprint','Please select a record');
        return;
    }


    if(!deleteJobWin)
    {
        var deleteJobPanel = new Ext.FormPanel({
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
                    id:'delete-job-password',
                    fieldLabel:'Please enter your password'
                }
                
            ],
            buttons:[
                {
                    text:'OK',
                    handler:function(){
                        var pwd = Ext.getCmp('delete-job-password').getValue();
                        if(pwd == "")
                            return;
                        
                        var sUrl = "/" + APP_NAME + "/job.aspx/deleteJob";
                        var grid = Ext.getCmp('newjob-jobgrid');
                        var selectModel = grid.getSelectionModel();
                        var rec = selectModel.getSelected();

                        if (rec == undefined || rec.data.length == 0)
                        {
                            Ext.Msg.alert('Fingerprint', 'Pelase select a record to delete');
                            return;
                        }
                        var xParameter = {jobid:rec.data.jobid, pwd:pwd};
                        LoadData(sUrl, xParameter, onDeleteJobReceived);
                        
                        function onDeleteJobReceived(data)
                        {
                        
                            deleteJobWin.hide();
                            
                            Ext.getCmp('newjob-jobgrid').getStore().reload();
                            
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
                        deleteJobWin.hide();
                    }
                }
            ]
        });
        
        
        deleteJobWin= new Ext.Window({
			title:'Fingerprint',
			layout:'fit',
			width:400,
			height:100,
			closeAction:'hide',
			plain: true,
			items: deleteJobPanel
		});
    }
    Ext.getCmp('delete-job-password').setValue('');
    deleteJobWin.show();
}

function searchJob() {
    //    alert(Ext.getCmp('filter-itemtype').getValue());
    //    alert(Ext.getCmp('neworder-status-rg').getValue().value);
    //    alert(Ext.getCmp('neworder-filter-type-rg').getValue().value);
    //    alert(Ext.getCmp('neworder-filter-value').getValue());

    var jt = Ext.getCmp('filter-itemtype').getValue();
    var js = Ext.getCmp('neworder-status-rg').getValue().value;
    var ft = Ext.getCmp('neworder-filter-type-rg').getValue().value;
    var fv = Ext.getCmp('neworder-filter-value').getValue();



    if (js == "0")
        js = "New";
    else if (js == "1")
        js = "In Progress";
    else if (js == "2")
        js = "Pending";
    else if (js == "3")
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

    Ext.getCmp('newjob-jobgrid').getStore().load(
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