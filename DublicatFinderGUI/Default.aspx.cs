using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DublicatFinderGUI
{
    public partial class Default : Page
    {
        private ToDb dublicatConnector = new ToDb();
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
               
            CreateDropDownLIst(46);
            
        }

        protected void CreateDropDownLIst(int count)
        {
            for(int i =0 ;i < count ; i++)
            {
                var dd = new DropDownList {ID = "ddlist"+i};
                p1enal.Controls.Add(dd);
                dd.EnableViewState = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    dublicatConnector.ExecStoredProcedure("Duplicates_DeleteTable");   
                }
                catch(Exception ex)
                {
                    //do nothing...
                }
                dublicatConnector.ExecStoredProcedure(@"Duplicates_BuildTable");

                FillGridApersonsWhithDuplicate(false);
                lbl1.Text += dublicatConnector.Errors;
                
                p1enal.EnableViewState = true;
                PrepareDDlistBoxes(0);
                DataBind();

                BuildChekBoxesPanel();
            }

            
        }
        
        protected void BuildChekBoxesPanel()
        {
            int count = gridPersons.Rows.Count;
            if(count>=1)
            {
                CheckBox1.Visible = true;
            }
            if(count>=2)
            {
                CheckBox2.Visible = true;
            }
            if(count>=3)
            {
                CheckBox3.Visible = true;
            }
            if(count>=4)
            {
                CheckBox4.Visible = true;
            }
            if(count>=5)
            {
                CheckBox5.Visible = true;
            }
            if (count >= 6)
            {
                CheckBox6.Visible = true;
            }
            if (count >= 7)
            {
                CheckBox7.Visible = true;
            }
            if (count >= 8)
            {
                CheckBox8.Visible = true;
            }
            if (count >= 9)
            {
                CheckBox9.Visible = true;
            }
            if (count >= 10)
            {
                CheckBox10.Visible = true;
            }

        }

        protected void HideChekBoxesPanel()
        {
            CheckBox1.Visible = false;
            CheckBox2.Visible = false;
            CheckBox3.Visible = false;
            CheckBox4.Visible = false;
            CheckBox5.Visible = false;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
        }
        
        protected void FillGridApersonsWhithDuplicate(bool byFIO)
        {
            SqlConnection sqlConnection = new SqlConnection(dublicatConnector.ConnectionString);
            
            try
            {
                sqlConnection.Open();
                SqlCommand command;
                if (byFIO)
                    command = new SqlCommand(dublicatConnector.SelectPersonsByFIO, sqlConnection);
                else
                    command = new SqlCommand(dublicatConnector.SelectDublicate, sqlConnection);

                command.CommandType = CommandType.StoredProcedure;
                if(byFIO)
                {
                    command.Parameters.AddWithValue("LastName", tbLastName.Text);
                    command.Parameters.AddWithValue("FirstName", tbFirsName.Text);
                    command.Parameters.AddWithValue("MiddleName", tbMiddleName.Text);
                }
                SqlDataAdapter adapter = new SqlDataAdapter {SelectCommand = command};
                DataSet dataSet = new DataSet();
                
                dataSet.Tables.Add("APersons");
//                dataSet.Tables["APersons"].Columns.Add("Merge", typeof (bool));
                adapter.Fill(dataSet, "APersons");
                gridPersons.DataSource = dataSet.Tables["APersons"];
                gridPersons.DataBind();

                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;

            }
            catch(Exception ex)
            {
                lbl1.Text += ex.Message;
            }
            finally {sqlConnection.Close();}

        }
        
        protected void BtnMerge_Click(object sender, EventArgs e)
        {
            MergePersonsRecords();
            var mergeList = BuildWhiteList();
            if (mergeList.Count >= 2)
            {
                btnOtherTables.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        protected void PrepareDDlistBoxes(int ddindex)
        {
            //todo programmm i - not hardcode 46
            for(int i = ddindex ; i < 46 ; i++)
            {
                DropDownList DropDownList1 = (DropDownList)this.p1enal.Controls[i];
                DropDownList1.Visible = false;
            }
        }

        protected List<string> BuildWhiteList()
        {
            var whiteList = new List<string>();
            for (int i = 0; i < panelChekboxes.Controls.Count; i++)
            {
                CheckBox ch = panelChekboxes.Controls[i] as CheckBox;
                if (ch != null && ch.Checked)
                {
                    whiteList.Add((int.Parse(ch.Text) - 1).ToString());
                }
            }
            return whiteList;
        }

        protected void MergePersonsRecords()
        {
            var whiteList = BuildWhiteList();

            var ddindex = 0;

            for (int i = 0; i < gridPersons.Rows[0].Cells.Count; i++)
            {
                var different = false;
                for (int j = 0; j < gridPersons.Rows.Count -1 ; j++)
                {
                    for (int k = 1; k < gridPersons.Rows.Count; k++)
                    {
                        if (whiteList.BinarySearch(k.ToString()) >=0 && whiteList.BinarySearch(j.ToString()) >=0)
                        if (gridPersons.Rows[j].Cells[i].Text != gridPersons.Rows[k].Cells[i].Text)
                        {
                            different = true;
                            break;
                        }
                    }
                    if(different)
                        break;
                }
               

                if (different)
                {
                    DropDownList DropDownList1 = (DropDownList)this.p1enal.Controls[ddindex];
                    ddindex++;
                    List<string> dataList = new List<string>();

                    for (int j = 0; j < gridPersons.Rows.Count; j++)
                    {

                        if (gridPersons.Rows[j].Cells[i].Text != @"&nbsp;")
                        {
                            if (whiteList.BinarySearch(j.ToString()) >= 0 && dataList.BinarySearch(gridPersons.Rows[j].Cells[i].Text)< 0)
                                dataList.Add(gridPersons.Rows[j].Cells[i].Text);
                            
                        }
                    }
                    
                    DropDownList1.DataSource = dataList;
                    DropDownList1.EnableViewState = true;
                    DropDownList1.Visible = true;
                    DropDownList1.ToolTip = gridPersons.HeaderRow.Cells[i].Text;
                    DropDownList1.DataBind();
                }
            }
            PrepareDDlistBoxes(ddindex);
        }
        
        protected void BtnNextDuplicate_Click(object sender, EventArgs e)
        {
            
            FillGridApersonsWhithDuplicate(false);
            PreparePageBeforeNextDuplicate();

        }

        protected void PreparePageBeforeNextDuplicate()
        {
            PrepareDDlistBoxes(0);
            btnSave.Enabled = false;
            btnOtherTables.Enabled = false;

            grid1.DataSource = null;
            grid2.DataSource = null;
            grid3.DataSource = null;
            grid4.DataSource = null;
            DataBind();

            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            HideChekBoxesPanel();
            BuildChekBoxesPanel();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DropDownList ddList = p1enal.Controls[0] as DropDownList;
            if (ddList != null && ddList.Items.Count >= 2)
            {
                int newID = int.Parse(ddList.SelectedValue);

                for (int i = 0; i < ddList.Items.Count; i++)
                    if (ddList.Items[i].Value != newID.ToString())
                    {
                        int oldID = int.Parse(ddList.Items[i].Value);
                        dublicatConnector.ExecUpdatePersonIdInOtherTables(oldID, newID);
                        string com = BuildUpdatePersonCommand(newID);
                        dublicatConnector.ExecStoredProcedure(com,false);
                        dublicatConnector.ExecStoredProcedure("Persons_Delete", oldID, "PersonID");
                        dublicatConnector.ExecStoredProcedure("DomainUsers_Delete", oldID, "PersonID");
                        lbl1.Text = string.Empty;
                    }


            }
            else lbl1.Text = "Please select two or more rows";
        }
        
        protected string BuildUpdatePersonCommand(int id)
        {
            StringBuilder updateString = new StringBuilder("UPDATE APersons SET ");
            bool first = true;

            for (int i = 0; i < p1enal.Controls.Count;i++ )
            {
                 DropDownList ddList = p1enal.Controls[i] as DropDownList;

                 if (ddList != null && ddList.ToolTip != "PersonID" && ddList.Items.Count>0)
                 {
                     if (!first) updateString.Append(@",");
                     
                     updateString.Append(ddList.ToolTip);
                     updateString.Append(@" = N'");
                     if (ddList.ToolTip == "Birthday" || ddList.ToolTip == "PassportIssueDate" || ddList.ToolTip == "CreationDate" || ddList.ToolTip == "ModificationDate")
                     {
                         updateString.Append(ParseDateToUniversalFormat(ddList.SelectedValue));
                     }
                     else
                     {
                         updateString.Append(ddList.SelectedValue);
                     }
                     updateString.Append(@"' ");
                     first = false;

                 }

            }

            updateString.Append("WHERE PersonID = ");
            updateString.Append(id);


                return updateString.ToString();
        }

        protected string ParseDateToUniversalFormat(string dateAndTime)
        {
            StringBuilder result = new StringBuilder();
            
            result.Append(dateAndTime.Substring(6, 4));
            result.Append( @"-");
            result.Append(dateAndTime.Substring(3, 2));
            result.Append(@"-");
            result.Append(dateAndTime.Substring(0, 2));
            dateAndTime.Substring(10);
            
            return result.ToString();
        }

        protected void btnOtherTables_Click(object sender, EventArgs e)
        {
            var ids = new List<string>();
            DropDownList ddList = p1enal.Controls[0] as DropDownList;
            if (ddList != null && ddList.Items.Count >= 2)
            {
                for (int i = 0; i < ddList.Items.Count; i++)
                {
                    ids.Add(ddList.Items[i].Value);
                }
            }

            FillOtherGridsWhithDuplicates(ids, "PersonContacts", true, ref grid1);
            label1.Text = "PersonContacts";
            FillOtherGridsWhithDuplicates(ids, "ExamAssignments", true, ref grid2);
            label2.Text = "ExamAssignments";
            FillOtherGridsWhithDuplicates(ids, "GroupRegistrations", true, ref grid3);
            label3.Text = "GroupRegistrations";
            FillOtherGridsWhithDuplicates(ids, "GradeBook", false, ref grid4);
            label4.Text = "GradeBook";

            Panel1.Visible = true;
            Panel2.Visible = true;
            Panel3.Visible = true;
            Panel4.Visible = true;

        }

        protected void FillOtherGridsWhithDuplicates(List<string> ids, string tableName, bool personId ,ref GridView gridView)
        {
            SqlConnection sqlConnection = new SqlConnection(dublicatConnector.ConnectionString);
            SqlCommand command = new SqlCommand(dublicatConnector.CreateSelectString(tableName, ids,personId), sqlConnection);

            try
            {
                sqlConnection.Open();
                var adapter = new SqlDataAdapter {SelectCommand = command};
                var dataSet = new DataSet();

                dataSet.Tables.Add(tableName);
//              не оправданный функционал
//                dataSet.Tables[tableName].Columns.Add("We need it", typeof (bool));
                adapter.Fill(dataSet, tableName);
                gridView.DataSource = dataSet.Tables[tableName];

                gridView.DataBind();

            }
            catch (Exception ex)
            {
                lbl1.Text += ex.Message;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        protected void btnFindDuplByFIO_Click(object sender, EventArgs e)
        {
            FillGridApersonsWhithDuplicate(true);
            PreparePageBeforeNextDuplicate();
        }
    }
}