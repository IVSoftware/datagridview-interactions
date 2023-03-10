using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datagridview_interactions
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dataGridViewAll.AllowUserToAddRows= false;
            dataGridViewAll.RowHeadersVisible= false;
            dataGridViewAll.DataSource = All;
            dataGridViewSelected.DataSource = Selected;
            dataGridViewSelected.AllowUserToAddRows= false;
            dataGridViewSelected.RowHeadersVisible = false;

            #region A U T O G E N E R A T E    C O L U M N S
            All.Add(new Lesson());
            Selected.Add(new Lesson());
            #endregion A U T O G E N E R A T E    C O L U M N S

            #region F O R M A T    C O L U M N S
            dataGridViewAll.Columns[nameof(Lesson.Selected)].Width = 40;
            dataGridViewAll.Columns[nameof(Lesson.Selected)].HeaderText = string.Empty;
            dataGridViewAll.Columns[nameof(Lesson.Description)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            All.Clear();

            dataGridViewSelected.Columns[nameof(Lesson.Selected)].Visible = false;
            dataGridViewSelected.Columns[nameof(Lesson.Selected)].HeaderText = string.Empty;
            dataGridViewSelected.Columns[nameof(Lesson.Description)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Selected.Clear();
            #endregion F O R M A T    C O L U M N S

            dataGridViewAll.CellContentClick += onCellContentClick;
            dataGridViewSelected.SelectionChanged += (sender, e) =>dataGridViewSelected.ClearSelection();
            addTestData();
        }

        private void addTestData()
        {
            // Add a few classes for testing purposes
            All.Add(new Lesson
            {
                Description = "Learn C# with Windows Forms"
            });
            All.Add(new Lesson
            {
                Description = "C# Database Driven WinForm Apps"
            });
            All.Add(new Lesson
            {
                Description = "Learn C# With SQL Server"
            });
            dataGridViewAll.ClearSelection();
        }

        private void onCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridViewAll.Columns[e.ColumnIndex].Name)
            {
                case nameof(Lesson.Selected):
                    if(e.RowIndex != -1) // Don't execute is this is the header cell
                    {
                        dataGridViewAll.EndEdit();
                        if (All[e.RowIndex].Selected)
                        {
                            Selected.Add(All[e.RowIndex]);
                        }
                        else
                        {
                            Selected.Remove(All[e.RowIndex]);
                        }
                        dataGridViewAll.ClearSelection();
                    }
                    break;
            }
        }

        BindingList<Lesson> All { get; } = new BindingList<Lesson>();
        BindingList<Lesson> Selected { get; } = new BindingList<Lesson>();
    }
    class Lesson 
    {
        public bool Selected { get; set; }
        public string Description { get; set; }
    }
}
