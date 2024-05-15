using System.Windows.Forms;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class Window : Form
{
    #region Filds

    #region Button
    private Button addValueButton = new Button();
    private Button clearButton = new Button();
    private Button checkByValueButton = new Button();
    private Button changeValueByNumberButton = new Button();
    private Button deleteValueButton = new Button();
    private Button deleteValueByNumberButton = new Button();
    private Button addByNumberButton = new Button();
    private List<Button> buttons;
    private const int BUTTON_HEIGHT = 30;
    private const int BUTTON_WIDTH = 150;
    private const int BUTTON_OFFSET = 10;
    #endregion

    #region TextBox
    private MyTextBox inputValueText = new MyTextBox();
    private MyTextBox inputIndexText = new MyTextBox();
    private MyTextBox sizeText = new MyTextBox();
    private List<MyTextBox> textBoxes;

    private const int TEXTBOX_HEIGHT = 50;
    private const int TEXTBOX_WIDTH = 70;
    private const int TEXTBOX_OFFSET = 10;
    #endregion
    
    private CheckBox isEmptyBox = new CheckBox();

    private DataGridView dataGridView = new DataGridView();
    private const float FONT_SCALE = 10;
    private MyList<int> List => Program.list;

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
        
        inputIndexText.SetWatermark("For Index");
        inputValueText.SetWatermark("For Value");
        textBoxes  = new List<MyTextBox>()
        {
            inputValueText,
            inputIndexText,
            sizeText,
        };
        

        Draw(textBoxes, new Point(0, 60), TEXTBOX_WIDTH, TEXTBOX_HEIGHT, TEXTBOX_OFFSET);
    }

    private void InitButtons()
    {
        addValueButton.Text = "AddValue";
        addValueButton.Click += new EventHandler(OnAddValue);

        addByNumberButton.Text = "AddByNumber";
        addByNumberButton.Click += new EventHandler(OnAddByNumber);

        checkByValueButton.Text = "CheckByValue";
        checkByValueButton.Click += new EventHandler(OnSelectByValue);

        clearButton.Text = "Clear";
        clearButton.Click += new EventHandler(OnClear);

        changeValueByNumberButton.Text = "ChangeValByNumber";
        changeValueByNumberButton.Click += new EventHandler(OnChangeValueByNumber);

        deleteValueButton.Text = "DeleteValue";
        deleteValueButton.Click += new EventHandler(OnDeleteValue);

        deleteValueByNumberButton.Text = "DeleteByNumber";
        deleteValueByNumberButton.Click += new EventHandler(OnDeleteValueByNumber);
        buttons = new List<Button>()
        {
            addValueButton,
            addByNumberButton,
            checkByValueButton,
            changeValueByNumberButton,
            deleteValueButton,
            deleteValueByNumberButton,
            clearButton
        };


        Draw(buttons, new Point(0, 100), BUTTON_WIDTH, BUTTON_HEIGHT, BUTTON_OFFSET);

    }

    private void OnAddByNumber(object sender, EventArgs e)
    {
        int value = -1;
        if (!TryParse(inputValueText, ref value)) return;

        int index = -1;
        if (!TryParse(inputIndexText, ref index)) return;

        if(!List.Push(value, index)) return;

        UpdateGrid();
    }

    private void OnDeleteValueByNumber(object sender, EventArgs e)
    {
        int index = -1;
        if (!TryParse(inputIndexText, ref index)) return;

        if (!List.PopByIndex(index)) return;

        UpdateGrid();
    }

    private void OnDeleteValue(object sender, EventArgs e)
    {
        int value = -1;
        if (!TryParse(inputValueText, ref value)) return;

        if (!List.Pop(value)) return;

        UpdateGrid();        
    }

    private void OnChangeValueByNumber(object sender, EventArgs e)
    {
        int value = -1;
        if (!TryParse(inputValueText, ref value)) return;

        int index = -1;
        if (!TryParse(inputIndexText, ref index)) return;

        if(!List.ChangeValue(index, value)) return;

        UpdateGrid();
    }

    private void OnSelectByValue(object sender, EventArgs e)
    {
        Unselect();
        
        int value = -1;
        
        if (!TryParse(inputValueText, ref value)) return;
        int index = List.GetIndexByValue(value);

        if (index == -1) return;

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
        string text = inputValueText.Text;
        int val;
        if (!int.TryParse(text, out val)) return;
        if (!Program.list.Push(val)) return;
        
        UpdateGrid();
    }
    private void OnClear(object sender, EventArgs e)
    {
        List.Clear();
        UpdateGrid();
    }
    private void CheckListForEmpty()
    {
        isEmptyBox.Checked = Program.list.IsEmpty;
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
    private void UpdateGrid()
    {
        while (dataGridView.ColumnCount < List.Lenght)
        {
            AddColumn();
        }

        var end = List.End();
        int i = 0;
        for(var iterator = List.Begin(); iterator != end; iterator.Next())
        {
            dataGridView[i, 0].Value = List.Get(iterator.GetIndex).value;
            i++;
        }
        for(; i<dataGridView.ColumnCount; i++)
        {
            dataGridView[i, 0].Value = "";
        }
        CheckListForEmpty();
        inputIndexText.SetWatermark(inputIndexText.watermark);
        inputValueText.SetWatermark(inputValueText.watermark);

    }
    private bool TryParse(MyTextBox textBox, ref int val)
    {
        string text = textBox.Text;
        return int.TryParse(text, out val);
    }

    

    public void CreateDataGrid()
    {
        UpdateGrid();
    }
}