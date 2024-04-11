using System;


public class MyList<T>
{
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
    public bool IsEmpty => _beginIndex == -1;

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
            _array[i] = new Node<T>(i + 1);
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
        Console.WriteLine($"_array[{_freeIndex}].value = {val}");
        _array[_freeIndex].next = _beginIndex;
        Console.WriteLine($"_array[{_freeIndex}].next = {_beginIndex}");
        Console.WriteLine("--------------------------------------------------------");
        _beginIndex = _freeIndex;
        _freeIndex = tmp;
        _count++;
        return true;
    }
     
    public bool Push(T val, int index)
    {
        if (index < 0 || index >= _count || _lenght == 0) return false;

        statistics = 0;

        if (index == 0)
        {
            statistics++;
            Push(val);
            return true;
        }

        if(_count == _lenght - 1) Resize();

        var iteratorCurrent = Begin();
        iteratorCurrent.Next();
        var iteratorPast = Begin();

        var endIterator = End();
        for (int i = 1; iteratorCurrent != endIterator; iteratorCurrent.Next(), iteratorPast.Next(), i++)
        {
            statistics++;
            if (i == index)
            {
                int pastIndex = iteratorPast.GetIndex();
                int tmpIndex = _array[_freeIndex].next;     
                _array[_freeIndex].value = val;
                Console.WriteLine($"_array[{_freeIndex}].value = {val}");
                _array[_freeIndex].next = iteratorCurrent.GetIndex();
                Console.WriteLine($"_array[{_freeIndex}].next = {iteratorCurrent.GetIndex()}");
                _array[pastIndex].next = _freeIndex;
                Console.WriteLine($"_array[{pastIndex}].next = {_freeIndex}");
                Console.WriteLine("--------------------------------------------------------");
                _freeIndex = tmpIndex;
                _count++;
                return true;
            }
        }
        return false;
    }
    public bool Pop(T val)
    {
        int currentIndex = _beginIndex;
        if (_array[currentIndex].value.Equals(val))
        {
            _beginIndex = _array[_beginIndex].next;
            _array[currentIndex].next = _freeIndex;
            _freeIndex = currentIndex;
            _count--;
            return true;
        }

        var iteratorCurrent = Begin();
        iteratorCurrent.Next();
        var itEnd = End();
        for (var iteratorPast = Begin(); iteratorCurrent != itEnd; iteratorCurrent.Next(), iteratorPast.Next()) 
        {
            currentIndex = iteratorCurrent.GetIndex();
            if (_array[currentIndex].value.Equals(val))
            {
                int pastIndex = iteratorPast.GetIndex();
                _array[pastIndex].next = _array[currentIndex].next;
                _array[currentIndex].next = _freeIndex;
                _freeIndex = currentIndex;
                _count--;
                return true;
            }
        }

        return false;
    }

    public bool PopByIndex(int index)
    {
        if (index < 0 || index >= _count || _lenght == 0) return false;

        int currentIndex = _beginIndex;
        statistics = 0;

        if (index == 0)
        {
            _beginIndex = _array[currentIndex].next;
            _array[currentIndex].next = _freeIndex;
            _freeIndex = currentIndex;
            _count--;
            statistics++;
            return true;
        }
        
        var iteratorCurrent = Begin();
        iteratorCurrent.Next();
        var iteratorPast = Begin();
        var itEnd = End();
        for (int i = 1; iteratorCurrent != itEnd; iteratorCurrent.Next(), iteratorPast.Next(), i++)
        {
            statistics++;
            if (i == index)
            {
                int pastIndex = iteratorPast.GetIndex();
                currentIndex = iteratorCurrent.GetIndex();
                _array[pastIndex].next = _array[currentIndex].next;
                _array[currentIndex].next = _freeIndex;
                _freeIndex = currentIndex;
                _count--;
                return true;
            }
        }
       return false;
    }

    public T GetValue(int index)
    {
        if (index >= _count || index < 0) return default;

        var iterator = Begin();
        var end = End();
        for (int i = 0; iterator != end; iterator.Next(), i++)
        {
            if (i == index)
            {
                return iterator.Current;
            }
        }

        return default;
    }

    public bool ChangeValue(int index, T val)
    {
        if (index >= _count || index < 0) return false;

        var iterator = Begin();
        var end = End();
        for (int i = 0; iterator != end; iterator.Next(), i++)
        {
            if (i == index)
            {
                _array[iterator.GetIndex()].value = val;
                return true;
            }
        }
        return false;
    }
    public int GetIndexByValue(T value)
    {
        int i = 0;
        var end = End();
        for (var iterator = Begin(); iterator != end; iterator.Next(), i++)
        {
            if  (_array[iterator.GetIndex()].value.Equals(value))
            {
                return i;
            }
        }
        return -1;
    }
    public void Print()
    {
        var end = End();
        for(var iterator = Begin(); iterator != end; iterator.Next())
        {
            Console.WriteLine(_array[iterator.GetIndex()].value);
        }
    }

    public bool IsExist(T value)
    {
        statistics = 0;
        var end = End();
        for (var iterator = Begin(); iterator != end; iterator.Next())
        {
            statistics++;
            if (_array[iterator.GetIndex()].value.Equals(value))
            {
                return true;
            }
        }
        return false;
    }

    private void Resize()
    {
        Console.WriteLine("Resizing");
        Array.Resize(ref _array, _lenght * 2);
        for (int i = _lenght - 1; i < _array.Length; i++)
        {
            _array[i] = new Node<T>(i + 1);
        }
        _array[_array.Length - 1].next = -1;
        _lenght *= 2;
    }
    private Iterator Begin() => new Iterator(_array, _beginIndex);
    private Iterator End() => new Iterator(_array, -1);
    
}


