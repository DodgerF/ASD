using System;


public class MyList<T>
{
    //TODO: figure out if an array is needed here?
    private class Iterator
    {
        private Node<T>[] _array;
        private int _index;

        public Iterator(Node<T>[] array, int index)
        {
            _array = array;
            _index = index;
        }
        public T Current => _array[_index].value;
        public int GetIndex() => _index;
        
        public Iterator Next()
        {
            _index = _array[_index].next;
            return this;
        }
        public static bool operator ==(Iterator first, Iterator second)
        {
            return first._index == second._index;
        }
        public static bool operator !=(Iterator first, Iterator second)
        {
            return first._index != second._index;
        }

    }
    private int _lenght;
    private int _count;
    private Node<T>[] _array;
    private int _beginIndex;
    private int _freeIndex;
    public int statistics;

    public int Lenght => _lenght;
    public int Count => _count;
    public int BeginIndex => _beginIndex;
    public int FreeIndex => _freeIndex;
    public Node<T> Get(int index) => _array[index];

    public MyList(int lenght)
    {
        _lenght = lenght;
        _array = new Node<T>[lenght];
        Clear();
    }

    public MyList(MyList<T> list)
    {
        _lenght = list.Lenght;
        _beginIndex = list.BeginIndex;
        _freeIndex = list.FreeIndex;
        _count = list.Count;
        _array = new Node<T>[_lenght];
        for (int i = 0; i < _lenght; i++)
        {
            _array[i] = list.Get(i);
        }
    }

    public void Clear()
    {
        _beginIndex = -1;
        _freeIndex = 0;
        _count = 0;
        for (int i = 0; i < _lenght; i++)
        {
            _array[i].next = i++;
        }
        _array[_lenght - 1].next = -1;
    }

    public bool Push(T val)
    {
        if (_lenght == 0) return false;

        if (_count == _lenght - 1)
        {
            Resize();
        }
        
        int tmp = _array[_freeIndex].next;

        _array[_freeIndex].value = val;
        _array[_freeIndex].next = _beginIndex;
        _beginIndex = _freeIndex;
        _freeIndex = tmp;
        _count++;
        return true;
    }
    //TODO: do this method
    public bool Push(T val, int index)
    {
        return false;
    }
    //TODO: do this method
    public bool Pop(T val)
    {
        return false;
    }
    private void Resize()
    {
        Array.Resize(ref _array, _lenght * 2);
        for (int i = _lenght - 1; i < _array.Length - 1; i++)
        {
            _array[i].next = i + 1;
        }
        _array[_array.Length - 1].next = -1;
        _lenght *= 2;
    }
    private Iterator Begin() => new Iterator(_array, _beginIndex);
    
}


