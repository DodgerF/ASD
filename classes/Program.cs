using System;
using System.Windows.Forms;

public class Program
{
    private static Window _window = new Window();
    [STAThread]
    static void Main(string[] args)
    {
        Application.Run(_window);
    }
}