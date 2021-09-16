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
    public partial class AccMajor : System.Web.UI.Page
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

                sql = "select  ACC_MAJ_CODE,ACC_MAJ_DESC from acc_major where col_sl='" + Session["CoSl"] + "' order by ACC_MAJ_DESC";
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
                    sql = " select ACC_MAJ_TYPE,ACC_MAJ_CODE,ACC_MAJ_DESC from acc_major where  col_sl='" + Session["CoSl"] + "' AND ACC_MAJ_CODE=" + index+""; /*col_sl = '" + Session["CoSl"] + "' and*/
                   // sql = " select NAME,CODE from colleges where code="+index+"";
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
                    sql = "SELECT ACC_MAJ_TYPE,ACC_MAJ_CODE,ACC_MAJ_DESC FROM acc_major WHERE col_sl = '" + Session["CoSl"].ToString() + "' AND ACC_MAJ_CODE = " + code + "";
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
                        txtMajType.Text = myReader["ACC_MAJ_TYPE"].ToString();
                        txtCode.Text = myReader["ACC_MAJ_CODE"].ToString();
                        txtDesc.Text = myReader["ACC_MAJ_DESC"].ToString();
                        //txtTrustComm.Text = myReader["TRUST_COMM"].ToString();
                        //txtAddr1.Text = myReader["ADDR1"].ToString();
                        //txtAddr2.Text = myReader["ADDR2"].ToString();
                        //txtAddr3.Text = myReader["ADDR3"].ToString();
                        //txtAddrPin.Text = myReader["ADDRPIN"].ToString();
                        //txtTele.Text = myReader["TELE"].ToString();
                        //txtFax.Text = myReader["FAX"].ToString();
                        //txtEmail.Text = myReader["EMAIL"].ToString();
                        //txtWebsite.Text = myReader["WEBSITE"].ToString();
                        //txtBnkRefCode.Text = myReader["BNK_REF_CODE"].ToString();
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

                if (Session["Mode"].ToString() != "Add")
                {
                    lblMsg.Text = "";
                   txtSearch.Text = "";
                    txtMajType.Text = "";
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

                sql = "select ACC_MAJ_CODE,ACC_MAJ_DESC from acc_major where  col_sl='" + Session["CoSl"] + "' AND (upper(ACC_MAJ_CODE) like '%" + txtSearch.Text.ToUpper()+"%') or (upper(ACC_MAJ_DESC) like '%"+ txtSearch.Text.ToUpper()+ "%')"; /*col_sl = '" + Session["CoSl"] + "' and*/
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

                sql = " select ACC_MAJ_TYPE,ACC_MAJ_CODE,ACC_MAJ_DESC from acc_major where  col_sl='" + Session["CoSl"] + "'  order by ACC_MAJ_CODE "; /*where col_sl = '" + Session["CoSl"] + "'*/
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
                    lblMsg.Text = "Please enter Group Description";
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

                //cmd.CommandText = "Insert into psys_locn(LCN_DESC,LCN_CTCD,LCN_CTCDHRA,LCN_CTCDCCA,LCN_INSBY,LCN_INSON,co_sl,active_yn) " +
                //    " values('" + mGlobal.doQuotes(txtdesc.Text) + "'," + "'" + mGlobal.doQuotes(txtctcd.Text) + "'," + "'" + mGlobal.doQuotes(txtctcdhra.Text) + "'," + "'" + mGlobal.doQuotes(txtctcdcca.Text) + "','" + Session["LoginName"] + "',sysdate(),'" + Session["CoSl"] + "','" + rbtnActive.SelectedValue + "')";


                cmd.CommandText = "Insert into acc_major(ACC_MAJ_DESC,COL_SL,INSERT_BY, INSERT_ON ) " +
                    " values('" + mGlobal.doQuotes(txtDesc.Text) + "','" + Session["CoSl"] + "', '" + Session["LogonName"] + "', sysdate())";

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
        //{
        //    lblMsg.Text = "";
        //    bool IsAdded = false;
        //    int active_yn = 0;
        //    try
        //    {
        //        if (mGlobal.conDatabase.State == ConnectionState.Open)
        //        {
        //            mGlobal.conDatabase.Close();
        //        }
        //        MySqlCommand cmd = new MySqlCommand();
        //        cmd.CommandText = "update acc_major set  ACC_MAJ_TYPE='" + mGlobal.doQuotes(txtMajType.Text) + "',  ACC_MAJ_CODE = '" + mGlobal.doQuotes(txtCode.Text) + "',ACC_MAJ_DESC='" + mGlobal.doQuotes(txtDesc.Text) + "' where col_sl ='" + Session["CoSl"] + "'";

        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Connection = mGlobal.conDatabase;
        //        mGlobal.conDatabase.Open();

        //        IsAdded = cmd.ExecuteNonQuery() > 0;

        //        if (IsAdded)
        //        {
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append(@"<script >");
        //            sb.Append("$('#editModal').modal('hide');");
        //            sb.Append(@"</script>");
        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);

        //            lblMsg.Text = " Record Updated successfully!";
        //            lblMsg.ForeColor = System.Drawing.Color.Green;
        //            Session["Mode"] = "0";
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Error while updating details";
        //            lblMsg.ForeColor = System.Drawing.Color.Red;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //        lblMsg.ForeColor = System.Drawing.Color.Red;

        //    }
        //    finally
        //    {
        //        Bind();
        //        mGlobal.conDatabase.Close();
        //        panleGridView.Visible = true;
        //        panelAddEdit.Visible = false;
        //        this.DivSearch.Visible = true;
        //    }

        //}




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
                sql = "update acc_major set  ACC_MAJ_TYPE='" + mGlobal.doQuotes(txtMajType.Text) + "',  ACC_MAJ_DESC='" + mGlobal.doQuotes(txtDesc.Text) + "' where ACC_MAJ_CODE = '" + mGlobal.doQuotes(txtCode.Text) + "' and col_sl ='" + Session["CoSl"] + "'";

                mGlobal.ExecuteQuery(sql);
                ShowMsgBox.Show("updated sucessfully");
           
           

        //cmd.CommandType = System.Data.CommandType.Text;
        //cmd.Connection = mGlobal.conDatabase;
        //mGlobal.conDatabase.Open();

        ////////        //IsAdded = cmd.ExecuteNonQuery() > 0;

        ////////        //if (IsAdded)
        ////////        //{
        ////////        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        ////////        //    sb.Append(@"<script >");
        ////////        //    sb.Append("$('#editModal').modal('hide');");
        ////////        //    sb.Append(@"</script>");
        ////////        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);

        ////////        //    lblMsg.Text = " Record Updated successfully!";
        ////////        //    lblMsg.ForeColor = System.Drawing.Color.Green;
        ////////        //    Session["Mode"] = "0";
        ////////        //}
        ////////        //else
        ////////        //{
        ////////        //    lblMsg.Text = "Error while updating details";
        ////////        //    lblMsg.ForeColor = System.Drawing.Color.Red;
        ////////        //}


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

                cmd.CommandText = "delete from acc_major where ACC_MAJ_CODE = " + code + " ";//and col_sl = '" + Session["CoSl"] + "'";
                                                                                         // cmd.CommandText = "delete from acc_major where ACC_MAJ_CODE = " + code + " ";
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
            txtMajType.Text = "";
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