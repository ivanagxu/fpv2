<%@ Page Title="" Language="C#" MasterPageFile="~/Views/shared/fingerprint.master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="server">
	Fingerprint
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID=styleSheetContent runat="server">
    <link rel="stylesheet" type="text/css" href=<%=Html.link("Content/css/xtheme-coffee.css") %>/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scriptContent" runat="server">
    
    
    <script type="text/javascript">
        Ext.onReady(function() {
            Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

            //Create view
            var MainView = new Ext.Viewport({
                layout: 'anchor',
                items: [
                    {
                        contentEl: 'topdiv',
                        border: false
                    }
                ]
            });

            var loginUrl = "/" + APP_NAME + "/fingerPrint.aspx/login";
            var loginFormPanel = new Ext.FormPanel({
                id: 'fingerprint_login_panel',
                layout: 'form',
                labelAlign: 'right',
                baseCls: 'x-plain',
                border: false,
                url: loginUrl,
                title: '',
                keys: [
                        { key: [Ext.EventObject.ENTER], handler: function() {
                            loginFormPanel.getForm().submit({
                                url: loginUrl,
                                waitMsg: 'Submiting form...',
                                success: function(form, o) {
                                    location.href = "/" + APP_NAME + "/fingerPrint.aspx/index";
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
                    ],

                buttons: [{
                    text: 'Login',
                    id: 'fingerprint-login-button',
                    handler: function() {
                        loginFormPanel.getForm().submit({
                            url: loginUrl,
                            waitMsg: 'Submiting form...',
                            success: function(form, o) {
                                location.href = "/" + APP_NAME + "/fingerPrint.aspx/" + o.result.data;
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
}],
                    items: [
                    {
                        xtype: 'panel',
                        title: 'System login',
                        baseCls: 'x-plain',
                        border: false,
                        anchor: '100%'
                    },
                    {
                        xtype: 'textfield',
                        fieldLabel: 'User name',
                        name: 'loginName',
                        anchor: '90%'
                    }, {
                        xtype: 'textfield',
                        fieldLabel: 'Password',
                        inputType: 'password',
                        name: 'loginPassword',
                        anchor: '90%'
                    }
                    ]
                });

                loginFormPanel.render(document.body);
                var loginFormWin = new Ext.Window({
                    title: 'Fingerprint Ltd.',
                    layout: 'fit',
                    width: 260,
                    height: 160,
                    closeAction: 'show',
                    plain: true,
                    modal: true,
                    items: loginFormPanel
                });
                loginFormWin.show();
            })
	</script>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="bodyContent" runat="server">
    
</asp:Content>
