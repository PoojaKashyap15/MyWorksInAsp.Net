using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using prototype.App_Code;

namespace Payroll.Views
{
    public partial class AccMinor : System.Web.UI.Page
    {
        string sql;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string viewyn = "0";
                    string addyn = "0";
                    string edityn = "0";
                    string deleteyn = "0";
                    string printyn = "0";
                    string qs_check = "0";
                    string typ = "";
                    string menu_name = "";

                    if (Request.QueryString["qsc"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }

                    if (Session["CoSl"] == null)
                    {
                        Response.Redirect("../Views/login.aspx");
                    }

                    mGlobal.qs_check(Session["LoginSl"].ToString(), Session["CoSl"].ToString(), Request.QueryString["qsc"].ToString().ToUpper(),
                      out viewyn, out addyn, out edityn, out deleteyn, out printyn, out qs_check, out typ, out menu_name);

                    if (qs_check == 0.ToString())
                    {
                        Response.Redirect("../Views/login.aspx");
                    }
                    else
                    {
                        Session["AddYN"] = addyn;
                        Session["EditYN"] = edityn;
                        Session["DeleteYN"] = deleteyn;
                        Session["ViewYN"] = viewyn;
                        Session["PrintYN"] = printyn;
                        Session["TYP"] = typ;
                        Session["MenuName"] = menu_name;
                    }

                     
                   //lblHeading.Text = Session["MenuName"].ToString();
                    Session["Mode"] = "";
                    Session["TYP"] = "Print";
                    Bind();
                    Session["Mode"] = "Add";
                    panleGridView.Visible = true;
                    panelAddEdit.Visible = false;
                    panelDelete.Visible = false;
                    panelVIEW.Visible = false;
                }
                catch (Exception ex)
                {
                    ShowMsgBox.Show(ex.Message);
                }
            }
        }
        private void Bind()
        {
            try
            {

                sql = "select MIN_CODE,MIN_DESC from acc_minor where col_sl='" + Session["CoSl"] + "' order by MIN_DESC";
               //// sql = " SELECT CODE,NAME,TRUST_COMM,ADDR1,ADDR2,ADDR3,ADDRPIN,TELE,FAX,EMAIL,WEBSITE,BNK_REF_CODE FROM colleges ORDER BY NAME";
                mGlobal.bindataGrid(gvView, sql);

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                mGlobal.conDatabase.Close();
            }

        }


        protected void Binddropdown()
        {
            try
            {
                sql = "SELECT ACC_MAJ_CODE,ACC_MAJ_DESC FROM acc_major WHERE col_sl = '" + Session["CoSl"] + "' ORDER BY ACC_MAJ_DESC";

                mGlobal.binddropdownlist(sql, ddlMinCode);
                ddlMinCode.Items.Insert(0, new ListItem("-- Major Description --", "0"));

                //ddlDept_SelectedIndexChanged();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {

            }
        }
        protected void gvView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MySqlDataReader myReader = null;
            int index = 0;
            lblMsg.Text = "";

            if (e.CommandName.Equals("detail"))
            {

                try
                {
                    if (Session["ViewYN"].ToString() == "0")
                    {
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append(@"<script>");
                        sb2.Append("alert('Sorry..you dont have View permission!...Please contact system admin');");
                        sb2.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                        return;
                    }
                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                    sql = " select MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL from acc_minor where  col_sl='" + Session["CoSl"] + "' AND MIN_CODE=" + index+""; /*col_sl = '" + Session["CoSl"] + "' and*/
                    //sql = " select  MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL from acc_minor  where col_sl = '" + Session["CoSl"] + "' and  MIN_CODE= " + index + "";
                    mGlobal.bindDetailsView(dvLookup, sql);

                    panleGridView.Visible = false;
                    panelAddEdit.Visible = false;
                    panelDelete.Visible = false;
                    panelVIEW.Visible = true;
                    this.DivSearch.Visible = false;

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    mGlobal.conDatabase.Close();
                }

            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                try
                {
                    if (Session["DeleteYN"].ToString() == "0")
                    {
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append(@"<script>");
                        sb2.Append("alert('Sorry..you dont have DELETE permission!...Please contact system admin');");
                        sb2.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                        return;
                    }

                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                    hfSl.Value = code.ToString();

                    panleGridView.Visible = false;
                    panelAddEdit.Visible = false;
                    panelVIEW.Visible = false;
                    panelDelete.Visible = true;
                    this.DivSearch.Visible = false;

                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }

            else if (e.CommandName.Equals("editRecord"))
            {

                try
                {
                    if (Session["EditYN"].ToString() == "0")
                    {
                        System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                        sb2.Append(@"<script>");
                        sb2.Append("alert('Sorry..you dont have Edit permission!...Please contact system admin');");
                        sb2.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb2.ToString(), false);
                        return;
                    }
                    index = Convert.ToInt32(e.CommandArgument);
                    int code = index;
                   
                    //sql = "select FN_FUND_TYPE,FN_FUND_CODE,FN_FUND_NAME from funds where FN_FUND_CODE = " + code + " ";//and col_sl = '" + Session["CoSl"] + "'";
                    sql = "SELECT MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL FROM acc_minor WHERE col_sl = '" + Session["CoSl"]+ "' AND MIN_CODE = " + code + "   ";
                    if (mGlobal.conDatabase.State == ConnectionState.Open)
                    {
                        mGlobal.conDatabase.Close();
                    }

                    mGlobal.conDatabase.Open();
                    MySqlCommand msc = new MySqlCommand(sql, mGlobal.conDatabase);

                    myReader = msc.ExecuteReader();
                    while (myReader.Read())
                    {
                        //txtFundType.Text = myReader["FN_FUND_TYPE "].ToString();
                        string groupCode = myReader["MIN_MAJCODE"].ToString();
                        ddlMinCode.SelectedValue = groupCode;

                        txtCode.Text = myReader["MIN_CODE"].ToString();
                        txtDesc.Text = myReader["MIN_DESC"].ToString();
                        txtUf.Text = myReader["MIN_UF"].ToString();
                        txtPf.Text = myReader["MIN_PF"].ToString();
                        txtFf.Text = myReader["MIN_FF"].ToString();
                        txtAg.Text = myReader["MIN_AG"].ToString();
                        txtAgfl.Text = myReader["MIN_AGFL"].ToString();

                        //lblsl.Text =  myReader["COLLEGE_SL"].ToString();


                        ////rbtnActive.SelectedValue = myReader["active_yn"].ToString();
                    }

                    myReader.Close();

                    btnAddRecord.Visible = false;
                    panleGridView.Visible = false;
                    panelDelete.Visible = false;
                    panelAddEdit.Visible = true;
                    btnUpdate.Visible = true;
                    this.DivSearch.Visible = false;
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                finally
                {
                    mGlobal.conDatabase.Close();
                    if (myReader != null)
                    {
                        myReader.Close();
                    }
                }

            }


        }

        protected void gvView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvView.PageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["AddYN"].ToString() == "0")
                {
                    System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
                    sb2.Append(@"<script>");
                    sb2.Append("alert('Sorry..you dont have ADD permission!...Please contact system admin');");
                    sb2.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb2.ToString(), false);
                    return;
                }
                Binddropdown();

                if (Session["Mode"].ToString() != "Add")
                {
                    lblMsg.Text = "";
                   txtSearch.Text = "";
                    Binddropdown();
                    txtCode.Text = "";
                    txtDesc.Text = "";
                    
                }

                panleGridView.Visible = false;
                panelAddEdit.Visible = true;
                btnAddRecord.Visible = true;
                btnUpdate.Visible = false;
                panelDelete.Visible = false;
                panelVIEW.Visible = false;
                this.DivSearch.Visible = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                if (txtSearch.Text == "")
                {
                    Bind();
                    return;
                }

                sql = "select MIN_CODE,MIN_DESC from acc_minor where  col_sl='" + Session["CoSl"] + "' AND (upper(MIN_CODE) like '%" + txtSearch.Text.ToUpper()+"%') or (upper(MIN_DESC) like '%"+ txtSearch.Text.ToUpper()+ "%')"; /*col_sl = '" + Session["CoSl"] + "' and*/
                mGlobal.bindataGrid(gvView, sql);

                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
 

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            finally
            {
                mGlobal.conDatabase.Close();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PrintYN"].ToString() == "0")
                {
                    ShowMsgBox.Show("Sorry..you don't have PRINT permission!...Please contact system admin");
                    return;
                }

                sql = " select MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL from acc_minor where  col_sl='" + Session["CoSl"] + "'  order by MIN_DESC "; /*where col_sl = '" + Session["CoSl"] + "'*/
                VSF.ExportToExcel(mGlobal.conDatabase, Session["TYP"].ToString().ToUpper(), sql);


            }
            catch (Exception ex)
            {
                ShowMsgBox.Show(ex.Message);
                ShowMsgBox.Show("Error in Report!.. Please Contact Support Desk");
            }
            finally
            {

            }

        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            bool IsAdded = false;
            try
            {
                int active_yn = 0;
                Session["Mode"] = "Add";


                if (txtDesc.Text == "")
                {
                    lblMsg.Text = "Please enter Description";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                //-----------------checking duplicate added by patel on 6-june-2016 ---------------------------------//

                //--------------------------------------- End --------------------------------------------------------//



                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }


                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "Insert into acc_minor(MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL,COL_SL,INSERT_BY,INSERT_ON ) " +
                    " values('" + ddlMinCode.SelectedValue + "','" + mGlobal.doQuotes(txtCode.Text) + "','" + mGlobal.doQuotes(txtDesc.Text) + "','" + mGlobal.doQuotes(txtUf.Text) + "','" + mGlobal.doQuotes(txtPf.Text) + "','" + mGlobal.doQuotes(txtFf.Text) + "','" + mGlobal.doQuotes(txtAg.Text) + "','" + mGlobal.doQuotes(txtAgfl.Text) + "','" + Session["CoSl"] + "','" + Session["LoginName"] + "',sysdate())";


                //cmd.CommandText = "Insert into acc_minor(MIN_MAJCODE,MIN_CODE,MIN_DESC,MIN_UF,MIN_PF,MIN_FF,MIN_AG,MIN_AGFL,INSERT_BY, INSERT_ON )"+
                   // " values('" + ddlMinCode.SelectedValue + "','" + mGlobal.doQuotes(txtCode.Text) + "','" + mGlobal.doQuotes(txtDesc.Text) + "','" + mGlobal.doQuotes(txtUf.Text) + "','" + mGlobal.doQuotes(txtPf.Text) + "','" + mGlobal.doQuotes(txtFf.Text) + "','" + mGlobal.doQuotes(txtAg.Text) + "','" + mGlobal.doQuotes(txtAgfl.Text) + "', '" + Session["LogonName"] + "', sysdate())";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();

                IsAdded = cmd.ExecuteNonQuery() > 0;

                if (IsAdded)
                {

                    lblMsg.Text = "Record Saved successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    Session["Mode"] = "0";
                    Bind();
                }
                else
                {
                    lblMsg.Text = "Error while adding  record";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                Bind();
                mGlobal.conDatabase.Close();
                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
                DivSearch.Visible = true;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            bool IsAdded = false;
            int active_yn = 0;
            try
            {
                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {
                    mGlobal.conDatabase.Close();
                }
                // MySqlCommand cmd = new MySqlCommand();
                sql = "update acc_minor set MIN_MAJCODE='" + ddlMinCode.SelectedValue + "',MIN_DESC='" + mGlobal.doQuotes(txtDesc.Text) + "',MIN_UF='" + mGlobal.doQuotes(txtUf.Text) + "',MIN_PF='" + mGlobal.doQuotes(txtPf.Text) + "',MIN_FF='" + mGlobal.doQuotes(txtFf.Text) + "',MIN_AG='" + mGlobal.doQuotes(txtAg.Text) + "',MIN_AGFL='" + mGlobal.doQuotes(txtAgfl.Text) + "', update_by = '" + Session["LogonName"] + "'"+
                 " where MIN_CODE ='" + mGlobal.doQuotes(txtCode.Text) + "' and col_sl = '" + Session["CoSl"] + "'";

               // sql = "update acc_minor set  ACC_MAJ_TYPE='" + mGlobal.doQuotes(txtMajType.Text) + "',  ACC_MAJ_DESC='" + mGlobal.doQuotes(txtDesc.Text) + "' where ACC_MAJ_CODE = '" + mGlobal.doQuotes(txtCode.Text) + "' and col_sl ='" + Session["CoSl"] + "'";




                //and  college_sl = '" + lblsl.Text + "'";
                mGlobal.ExecuteQuery(sql);
                ShowMsgBox.Show("updated sucessfully");

                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Connection = mGlobal.conDatabase;
                //mGlobal.conDatabase.Open();

                //IsAdded = cmd.ExecuteNonQuery() > 0;

                //if (IsAdded)
                //{
                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //    sb.Append(@"<script >");
                //    sb.Append("$('#editModal').modal('hide');");
                //    sb.Append(@"</script>");
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);

                //    lblMsg.Text = " Record Updated successfully!";
                //    lblMsg.ForeColor = System.Drawing.Color.Green;
                //    Session["Mode"] = "0";
                //}
                //else
                //{
                //    lblMsg.Text = "Error while updating details";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }
            finally
            {
                Bind();
                mGlobal.conDatabase.Close();
                panleGridView.Visible = true;
                panelAddEdit.Visible = false;
                this.DivSearch.Visible = true;
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                bool IsDeleted = false;
                string code = hfSl.Value;

                if (mGlobal.conDatabase.State == ConnectionState.Open)
                {

                    mGlobal.conDatabase.Close();
                }

                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "delete from acc_minor where MIN_CODE = " + code + " ";//and col_sl='"+Session["CoSl"]+"'";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = mGlobal.conDatabase;
                mGlobal.conDatabase.Open();
                IsDeleted = cmd.ExecuteNonQuery() > 0;

                if (IsDeleted)
                {
                    lblMsg.Text = "Record  has been deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;

                    panleGridView.Visible = true;
                    panelDelete.Visible = false;
                }
                else
                {
                    lblMsg.Text = "Error while deleting Record ";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            } //try

            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                Bind();
                mGlobal.conDatabase.Close();
                this.DivSearch.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            panleGridView.Visible = true;
            panelAddEdit.Visible = false;
            panelVIEW.Visible = false;
            panelError.Visible = true;
            panelDelete.Visible = false;
            this.DivSearch.Visible = true;
            lblMsg.Text = "";
            ddlMinCode.SelectedValue = "0".ToString();
            txtCode.Text = "";
            txtDesc.Text = "";
            



            Bind();
        }


        protected void btncloseMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Views/home.aspx");
        }

        protected void dvLookup_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {

        }

        protected void gvView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}