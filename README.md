Lacking specifics, here is a general example of adding a row to a second `DataGridView` when selected in the first. I hope this helps get you started working with DGV. 

[![screenshot][1]][1]

***
It's usually easier to work with DGV if you make a `class` to represent a row of data.

    class Lesson 
    {
        public bool Selected { get; set; }
        public string Description { get; set; }
    }

***
Next you declare lists that contain instances of your `Lesson` class.

    BindingList<Lesson> All { get; } = new BindingList<Lesson>();
    BindingList<Lesson> Selected { get; } = new BindingList<Lesson>();

***
The binding lists are assigned to the respective DGVs when the main form loads. This method will also attach an event for when cell content (e.g. a checkbox) is clicked.

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
        .
        .
        .
    }

How to populate the first DGV:

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

***
Now all you have to do is handle the content click and either add the lesson to (or remove the item from) the second bound collection for the lesson that has changed.

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

  [1]: https://i.stack.imgur.com/r7HFk.png