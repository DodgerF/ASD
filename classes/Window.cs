using System.Windows.Forms;
using System.Drawing;
using System;

public class Window : Form
{
    private Button _button = new Button();
    public Window()
    {
        Text = "Text";
        Size = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;
        _button.Text = "Button";
        _button.Size = new Size(100, 30);
        _button.Location = new Point(100, 100);
        Controls.Add(_button);
    }
}