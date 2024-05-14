using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;

public class Window : Form
{
    #region Filds

    #region Button
    private Button addValueButton = new Button();
    private Button clearButton = new Button();
    private Button checkByValueButton = new Button();
    private Button viewValueButton = new Button();
    private Button viewIndexButton = new Button();
    private Button iteratorViewButton = new Button();
    private Button changeValueByNumber = new Button();
    private List<Button> buttonsByValue;
    private List<Button> buttonsByIndex;
    private List<Button> buttonsForIterator;
    private List<Button> buttonsBasic;
    private const int BUTTON_HEIGHT = 30;
    private const int BUTTON_WIDTH = 120;
    private const int BUTTON_OFFSET = 10;
    #endregion

    #region TextBox
    private TextBox inputText = new TextBox();
    private TextBox sizeText = new TextBox();
    private List<TextBox> textBoxes;

    private const int TEXTBOX_HEIGHT = 50;
    private const int TEXTBOX_WIDTH = 70;
    private const int TEXTBOX_OFFSET = 10;
    #endregion
    
    private CheckBox isEmptyBox = new CheckBox();

    private DataGridView dataGridView = new DataGridView();
    private const float FONT_SCALE = 10;

    #endregion
    public Window()
    {
        Text = "ASD lab1";
        Size = new Size(1200, 800);
        StartPosition = FormStartPosition.CenterScreen;

        InitTextBoxes();
        InitButtons();

        isEmptyBox.Location = new Point(10, 150);
        isEmptyBox.Text = "Is Empty";
        isEmptyBox.Enabled = false;
        isEmptyBox.Font = new Font(dataGridView.Font.FontFamily, FONT_SCALE);
        Controls.Add(isEmptyBox);
        
        dataGridView.RowTemplate.Height = 30;
        dataGridView.Size = new Size(Width, 50);
        dataGridView.Font = new Font(dataGridView.Font.FontFamily, FONT_SCALE);
        Controls.Add(dataGridView);
    }
    private void Draw(IEnumerable<Control> controls, Point startPosition, int width, int height, int offset)
    {
        int offsetX = offset;
        int offsetY = 0;
        foreach (var control in controls)
        {
            control.Location = new Point(startPosition.X + offsetX, startPosition.Y + offsetY);
            control.Size = new Size(width, height);
            control.Font = new Font(control.Font.FontFamily, FONT_SCALE);

            offsetX += width + offset;
            if (offsetX + width >= Width)
            {
                offsetX = offset;
                offsetY += height + offset;
            }
            Controls.Add(control);
        }
    }

    private void InitTextBoxes()
    {
        sizeText.ReadOnly = true;
        textBoxes  = new List<TextBox>()
        {
            inputText,
            sizeText,
        };
        

        Draw(textBoxes, new Point(0, 60), TEXTBOX_WIDTH, TEXTBOX_HEIGHT, TEXTBOX_OFFSET);
    }
    private void InitButtons()
    {
        addValueButton.Text = "AddValue";
        addValueButton.Click += new EventHandler(OnAddValue);

        checkByValueButton.Text = "CheckByValue";
        checkByValueButton.Click += new EventHandler(OnSelectByValue);
        buttonsByValue = new List<Button>()
        {
            addValueButton,
            checkByValueButton
        };

        clearButton.Text = "Clear";
        clearButton.Click += new EventHandler(OnClear);

        viewValueButton.Text = "ByValue";
        viewValueButton.Click += new EventHandler(OnViewValue);

        viewIndexButton.Text = "ByIndex";
        viewIndexButton.Click += new EventHandler(OnViewIndex);

        iteratorViewButton.Text = "Iterator";
        iteratorViewButton.Click += new EventHandler(OnViewIterator);
        buttonsBasic = new List<Button>()
        {
            viewValueButton,
            viewIndexButton,
            iteratorViewButton,
            clearButton,
        };

        changeValueByNumber.Text = "ChangeValueByNumber";
        changeValueByNumber.Click += new EventHandler(OnChangeValueByNumber);
        buttonsByIndex = new List<Button>() 
        {
            changeValueByNumber
        };
        buttonsForIterator = new List<Button>();

        Draw(buttonsByValue, new Point(0, 100), BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_OFFSET);
        Draw(buttonsBasic, new Point(650, 700), BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_OFFSET);

    }

    private void OnChangeValueByNumber(object sender, EventArgs e)
    {
        // string text = inputText.Text;
        // int index;
        
        // if (!int.TryParse(text, out index)) return;
        // int val = Program.Get(index);
    }

    private void OnViewIterator(object sender, EventArgs e)
    {
        foreach (Button button in buttonsByValue)
        {
            button.Visible = false;
        }
        foreach (Button button in buttonsByIndex)
        {
            button.Visible = false;
        }
        foreach (Button button in buttonsForIterator)
        {
            button.Visible = true;
        }
    }

    private void OnViewIndex(object sender, EventArgs e)
    {
        foreach (Button button in buttonsByValue)
        {
            button.Visible = false;
        }
        foreach (Button button in buttonsByIndex)
        {
            button.Visible = true;
        }
        foreach (Button button in buttonsForIterator)
        {
            button.Visible = false;
        }
    }

    private void OnViewValue(object sender, EventArgs e)
    {
        foreach (Button button in buttonsByValue)
        {
            button.Visible = true;
        }
        foreach (Button button in buttonsByIndex)
        {
            button.Visible = false;
        }
        foreach (Button button in buttonsForIterator)
        {
            button.Visible = false;
        }
    }

    private void OnSelectByValue(object sender, EventArgs e)
    {
        Unselect();
        
        string text = inputText.Text;
        int val;
        
        if (!int.TryParse(text, out val)) return;
        int index = Program.GetIndexByValue(val);

        if (index == -1) return;
        Console.WriteLine(index);
        dataGridView.Columns[index].DefaultCellStyle.Font = new Font(dataGridView.DefaultCellStyle.Font, FontStyle.Bold);
    }
    private void Unselect()
    {
        for(int i = 0; i < dataGridView.Columns.Count; i++)
        {
            dataGridView.Columns[i].DefaultCellStyle.Font = new Font(dataGridView.DefaultCellStyle.Font, FontStyle.Regular);
        }
    }



    private void OnAddValue(object sender, EventArgs e)
    {
        string text = inputText.Text;
        if (!Program.TryAddValue(text)) return;

        while (dataGridView.ColumnCount < Program.GetFreeIndex)
        {
            AddColumn();
        }
        
        for (int i = dataGridView.ColumnCount - 1; i > 0; i--)
        {
            dataGridView[i, 0].Value = dataGridView[i - 1, 0].Value;
        }
        dataGridView[0, 0].Value = text;
        CheckListForEmpty();
    }
    private void OnClear(object sender, EventArgs e)
    {
        Program.ClearList();
        Clear();
        UpdateSizeText();
        CheckListForEmpty();
    }
    private void CheckListForEmpty()
    {
        isEmptyBox.Checked = Program.IsListEmpty;
    }
    private void UpdateSizeText()
    {
        sizeText.Text = "Size: " + dataGridView.Columns.Count;
    }
    private void AddColumn()
    {
        int columnCount = dataGridView.ColumnCount;
        dataGridView.Columns.Add("Column" + columnCount + 1, "Element" + columnCount);
        UpdateSizeText();
    }
    private void Clear()
    {
        for (int i = 0; i < dataGridView.ColumnCount; i++)
        {
            dataGridView[i, 0].Value = "";
        }

    }

    public void CreateDataGrid(int numberOfColumn)
    {
        Clear();
        for (int i = 0; i < numberOfColumn; i++)
        {
            AddColumn();
        }
        CheckListForEmpty();
    }
}