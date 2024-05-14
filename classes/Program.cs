using System;
using System.Linq.Expressions;
using System.Windows.Forms;

public class Program
{
    private static Window _window = new Window();
    [STAThread]
    static void Main(string[] args)
    {
        int key = -1, len = -1;
        Console.Write("Create a list. Enter the desired number of items: ");
        len = Convert.ToInt32(Console.ReadLine());
        while (len < 0)
        {
            Console.WriteLine("Invalid number of items");
            len = Convert.ToInt32(Console.ReadLine());
        }
        MyList<int> list = new MyList<int>(len);
        bool isWorking = true;
        while(isWorking)
        {
            Menu();
            Console.Write("Enter operation number: ");
            key = Convert.ToInt32(Console.ReadLine());
            switch(key)
            {
                case(1):
                    Console.WriteLine($"Size of list: {list.Lenght}");
                    break;
                case(2):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                    {
                        list.Clear();
                        Console.WriteLine("List is cleared");
                    }
                    break;
                case(3):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                        Console.WriteLine("List isn't empty");
                    break;
                case(4):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                    {
                        Console.Write("Enter value: ");
                        int value = Convert.ToInt32(Console.ReadLine());
                        if (list.IsExist(value))
                            Console.WriteLine("Exist");
                        else
                            Console.WriteLine("Element not found");
                    }
                    break;
                case(5):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                    {
                        Console.WriteLine($"Enter index from 0 to {list.Count - 1}");
                        int index = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine($"Value at index {list.Get(index).value}");
                    }
                    break;
                case(6):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                    {
                        Console.WriteLine($"Enter index from 0 to {list.Count - 1}");
                        int index = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter value: ");
                        int value = Convert.ToInt32(Console.ReadLine());
                        list.ChangeValue(index, value);
                        Console.WriteLine($"Value at index {index} changed to {value}");
                    }
                    break;
                case(7):
                    if (list.IsEmpty)
                        Console.WriteLine("List is empty");
                    else
                    {
                        Console.Write("Enter value: ");
                        int value = Convert.ToInt32(Console.ReadLine());
                        int index = list.GetIndexByValue(value);
                        Console.WriteLine($"Element position: {index}");
                    }
                    break;
                case(8):
                    InsertMenu(list, len);
                    break;
                case(9):
                    DeleteMenu(list);
                    break;
                case(10):
                    Console.WriteLine($"Laboriousness: {list.statistics}");
                    break;
                case(11):
                    list.Print();
                    break;
                case(12):
                    IteratorMenu(list);
                    break;
                case(0):
                    isWorking = false;
                    break;
                default:
                    break;
            }
        }
        //Application.Run(_window);
    }

    static void Menu()
    {
        Console.WriteLine(
            "*********** Operation menu ***********\n" + 
            "1 - Request list size\n" +
            "2 - Clear the list\n" +
            "3 - Check list for empty\n" +
            "4 - Request the presence of an element\n" +
            "5 - Read value by number in list\n" +
            "6 - Change value by number in list\n" +
            "7 - Get element position by value\n" +
            "8 - Insert value\n" +
            "9 - Delete value\n" +
            "10 - Request statistics\n" +
            "11 - Show all values\n" +
            "12 - Iterator operation\n" +
            "0 - Exit"
            );
    }
    static void InsertMenu(MyList<int> list, int len)
    {
        int choice = -1;
        while(choice != 0)
        {
            Console.WriteLine("_____Choose what you want to do:\n" +
                              "1 - Fill the list with random values\n" +
                              "2 - Insert new value\n" +
                              "3 - Insert new value by number\n" +
                              "4 - Print\n" +
                              "0 - Go back");
            Console.Write("Enter number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case(1):
                    var random = new Random();
                    for (int i = 0; i < len; i++)
                        list.Push(random.Next());
                    break;
                case(2):
                    Console.Write("Enter value: ");
                    int value = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(list.Push(value));
                    break;
                case(3):
                    Console.Write("Enter value: ");
                    int val = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Enter index from 0 to {list.Count - 1}");
                    int index = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(list.Push(val, index)); 
                    break;
                case(4):
                    list.Print();
                    break;
                default:
                    break;
            }
        }
    }

    static void DeleteMenu(MyList<int> list)
    {
        int choice = -1;
        while(choice != 0)
        {
            Console.WriteLine("_____Choose what you want to do:\n" +
                              "1 - Delete value\n" +
                              "2 - Delete value by number\n" +
                              "0 - Go back");
            Console.Write("Enter number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case(1):
                    Console.Write("Enter value: ");
                    int value = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(list.Pop(value));
                    break;
                case(2):
                    Console.Write($"Enter index from 0 to {list.Count - 1}");
                    int index = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(list.PopByIndex(index));
                    break;
                default:
                    break;
            }
        } 
    }

    static void IteratorMenu(MyList<int> list)
    {
        MyList<int>.Iterator iterator = new MyList<int>.Iterator(null, -2);
        int choice = -1;
        while(choice != 0)
        {
            Console.WriteLine("_____Choose what you want to do:\n" +
                              "1 - Set iterator\n" +
                              "2 - Set iterator by number\n" +
                              "3 - Get value by iterator\n" +
                              "4 - Move to next value\n" +
                              "5 - Show all values\n" +
                              "0 - Go back");
            Console.Write("Enter number: ");
            choice = Convert.ToInt32(Console.ReadLine());
            switch(choice)
            {
                case(1):
                    iterator = list.Begin();
                    break;
                case(2):
                    Console.Write($"Enter index from 0 to {list.Count - 1}");
                    int index = Convert.ToInt32(Console.ReadLine());
                    iterator = list.Begin();
                    for (int i = 0; i < index; i++)
                    {
                        iterator.Next();
                    }
                    break;
                case(3):
                    if (iterator.GetIndex == -2)
                    {
                        Console.WriteLine("Iterator is not set");
                    }
                    Console.WriteLine($"Value: {list.Get(iterator.GetIndex).value}"); 
                    break;
                case(4):
                    if (iterator.GetIndex == -2 || iterator.Next().GetIndex == -1)
                    {
                        Console.WriteLine("Iterator is not set");
                    }
                    Console.WriteLine($"Value: {list.Get(iterator.GetIndex).value}");
                    break;
                case(5):
                    list.Print();
                    break;
                default:
                    break;
            }
        } 
    }
}