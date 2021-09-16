<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Funds.aspx.cs" Inherits="Payroll.Views.Funds" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="col-lg-12 container-padding">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>      
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Funds</h3>
            </div>

            <div class="panel-body">

                <div id="DivSearch" runat="server" class="form-group  col-lg-12">
                  <%--<div id="DivSearch" runat="server" class="form-group  col-lg-12 col-md-12 col-sm-12">--%>
                    <table class="table  table-bordered table-hover table-responsive">
                        <tbody>
                            <tr>
                                <td style="width: 20%; vertical-align:middle;">Search by Name or Code</td>
                                <td style="width: 51%">
                                    
                                        <asp:TextBox ID="txtSearch" Width="100%" CssClass=" form-control" runat="server" placeholder="Search by Code or Name"></asp:TextBox>
                                    
                                </td>
                                <td style="width: 7%">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Search Record" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                </td>
                                <td style="width: 10%">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" ToolTip="Adding New Record" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                                </td>
                                <td style="width: 6%">
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" ToolTip="Printing All Record" CssClass="btn btn-primary" OnClick="btnPrint_Click" />
                                </td>
                                <td style="width: 6%">
                                    <asp:Button ID="btncloseMain" runat="server" Text="Close" ToolTip="Close" CssClass="btn btn-primary" OnClick="btncloseMain_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <asp:Panel ID="panelError" runat="server" BorderStyle="None" Height="50%" Visible="true" Width="100%" BackColor="White">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                
                <div class="row">
                    <div class="col-lg-12 ">
                         <%------------------------------------------------- Detaisls Grid View -------------------------------------------------%>
                                <div class="table-responsive rounded-corners"> <%--PageSize="12"--%>
                                    <asp:Panel ID="panleGridView" runat="server" BorderStyle="None" Height="360px" Visible="true" Width="100%"  BackColor="White" >
                                   
                                    <asp:GridView ID="gvView" runat="server"  AutoGenerateColumns="false" Width="100%" DataKeyNames="FN_FUND_CODE" CssClass="table  table-hover table-striped table-bordered table-responsive "
                                        BorderColor="Black" OnRowCommand="gvView_RowCommand" OnPageIndexChanging="gvView_PageIndexChanging" AllowPaging="false" Height="20px" EmptyDataRowStyle-BackColor="LightCyan" EmptyDataText="Sorry No Data Found!!!." EmptyDataRowStyle-Font-Bold="true"
                                         EmptyDataRowStyle-Font-Size="Medium" EmptyDataRowStyle-ForeColor="Red" EmptyDataRowStyle-HorizontalAlign="Center" ShowHeader="true" OnSelectedIndexChanged="gvView_SelectedIndexChanged" >
                                       <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnView" CommandName="detail" ToolTip="View Details" CommandArgument='<%#Eval("FN_FUND_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/View.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemStyle Width="3%" Height="10px" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnEdit" CommandName="editRecord" ToolTip="Edit" CommandArgument='<%#Eval("FN_FUND_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/Edit.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Del">
                                                <ItemStyle Width="3%" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgBtnDelete" CommandName="deleteRecord" ToolTip="Delete" CommandArgument='<%#Eval("FN_FUND_CODE") %>' ImageUrl="~/Attachment/DefaultPhoto/delete.ico" runat="server" Width="25" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                           <asp:BoundField DataField="FN_FUND_CODE" ItemStyle-Width="20%" HeaderText="CODE" ItemStyle-Height="8px"  />
                                            <asp:BoundField DataField="FN_FUND_NAME" ItemStyle-Width="20%" ItemStyle-Height="8px" HeaderText="NAME" />
                                           
                                       </Columns>
                                    </asp:GridView>
                                        </asp:Panel>
                                         </div>
                                </div>
                    </div>
                </div>
                



                  <%--------------------------------------------------------- Add Record Modal START------------------------------------------------------------------------------%>
   <div class="row">
        <div class="col-lg-2 "></div>
                    <div class="col-lg-8 ">
            <asp:Panel ID="panelAddEdit" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White" >
                <table class="table table-bordered table-hover table-responsive">
                    <tr>
                        <td> FUND TYPE</td>
                        <td>
                            
                                <asp:TextBox ID="txtFundType" class="form-control" runat="server" Width="80%"></asp:TextBox>
                            
                        </td>
                    </tr>

                    <tr>
                        <td> CODE</td>
                        <td>
                            
                                <asp:TextBox ID="txtCode" class="form-control" runat="server" Width="80%"></asp:TextBox>
                            
                        </td>
                    </tr>

                    <tr>
                        <td> NAME</td>
                        <td>
                            
                                <asp:TextBox ID="txtName" Width="80%" class="form-control" runat="server" Height="40px"></asp:TextBox>
                            
                            <asp:RequiredFieldValidator ID="rfvtxtName" runat="server" ValidationGroup="vgAdd" ControlToValidate="txtName" Display="None" SetFocusOnError="true" ErrorMessage="Enter Fund Name"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vcetxtName" PopupPosition="BottomRight" runat="server" TargetControlID="rfvtxtName"></ajaxToolkit:ValidatorCalloutExtender>
                            
                        </td>
                    </tr>
                    
                  
                   
                </table>
                 <div style="float: right">
                    
                     <%--<asp:Label ID="lblsl" Visible="true" runat="server"></asp:Label>--%>
                    <asp:Button ID="btnAddRecord" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnAddRecord_Click"
                        ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                     <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnUpdate_Click"
                        ToolTip="enter the all fileds then click Add New link!" ValidationGroup="vgAdd" />
                   <%-- <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>--%>
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                     <asp:HiddenField ID="hfSl" runat="server" />   
                </div>

            </asp:Panel>

            <%--//------------------ View Recod -------------------//--%>
            <asp:Panel ID="panelVIEW" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
                <div class="row">

                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <asp:DetailsView ID="dvLookup" runat="server" CssClass="table table-bordered table-hover" BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False">
                            <Fields>

                                <asp:BoundField DataField="FN_FUND_TYPE" HeaderText="FUND TYPE" />
                                <asp:BoundField DataField="FN_FUND_CODE" HeaderText="CODE" />
                                 <asp:BoundField DataField="FN_FUND_NAME" HeaderText="NAME" />
                               <%-- <asp:BoundField DataField="TRUST_COMM" HeaderText="Trust Comm" />
                                <asp:BoundField DataField="ADDR1" HeaderText="ADDRESS 1" />
                                <asp:BoundField DataField="ADDR2" HeaderText="ADDRESS 2" />
                                <asp:BoundField DataField="ADDR3" HeaderText="ADDRESS 3" />
                                 <asp:BoundField DataField="ADDRPIN" HeaderText="ADDRESS PIN" />
                                 <asp:BoundField DataField="TELE" HeaderText="TELEPHONE NO" />
                                 <asp:BoundField DataField="FAX" HeaderText="FAX" />
                                 <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />
                                 <asp:BoundField DataField="WEBSITE" HeaderText="WEBSITE" />
                                 <asp:BoundField DataField="BNK_REF_CODE" HeaderText="BANK REFERENCE CODE" />--%>


                            </Fields>
                        </asp:DetailsView>
                         <div style="float: right">
            <asp:Button ID="Button3" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
             </div>

                    </div>
                </div>
            </asp:Panel>
</div>
        <div class="col-lg-2 "></div>
</div><%-- between row ende here--%>
<%-------------------- Panel Delete ---------------------------%>
 <asp:Panel ID="panelDelete" runat="server" BorderStyle="None" Height="100%" Visible="false" Width="100%" BackColor="White">
        <table class="table table-bordered table-hover table-responsive">
            <tr>
                <td style="width: 70%">Do you want to Delete this Record..?
                    <br />
                    <br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click" />
                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-info" OnClick="btnClose_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>



            </div>      

        </div>
    




</asp:Content>
