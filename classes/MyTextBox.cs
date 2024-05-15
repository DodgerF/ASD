using System.Windows.Forms;
using System.Drawing;
using System;

public class MyTextBox : TextBox
{
    public string watermark = "";
    public MyTextBox() : base()
    {
        ForeColor = SystemColors.GrayText;
        Text = watermark;
        Leave += new EventHandler(OnLeave);
        Enter += new EventHandler(OnEnter);
    }

    public void SetWatermark(string watermark)
    {
        this.watermark = watermark;
        Text = watermark;
        ForeColor = SystemColors.GrayText;
    }
    private void OnEnter(object sender, EventArgs e)
    {
        if (Text == watermark)
        {
            Text = "";
            ForeColor = SystemColors.WindowText;
        }
    }
    private void OnLeave(object sender, EventArgs e)
    {
        if (Text.Length == 0)
        {
            SetWatermark(watermark);
        }
    }


}