using System;
using System.Windows.Forms;

public class Program
{
    private static Window _window = new Window();
    public static MyList<int> list;
    [STAThread]
    static void Main(string[] args)
    {
        int startNumber = 3;
        list = new MyList<int>(startNumber);
        _window.CreateDataGrid();       
        Application.Run(_window);
    }
}