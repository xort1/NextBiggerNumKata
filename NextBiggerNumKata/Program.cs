using System;
using System.Collections.Immutable;

public class Kata
{
    public static void Main(string[] args)
    {
        long inputNumber = 1234567890;
        Console.WriteLine(NextBiggerNumber(inputNumber));
    }

    public static long NextBiggerNumber(long n)
    {
        Console.WriteLine(n);

        int amountOfElements = (int)Math.Log10(n) + 1;

        int[] array = new int[amountOfElements];

        for (int i = 0; i < amountOfElements; i++)
            array[i] = (int)GetDigit(n, i + 1);

        for (int i = amountOfElements; i > 1; i--)
        {
            if ((int)GetDigit(n, i) > (int)GetDigit(n, i - 1))
            {
                array[i - 1] = (int)GetDigit(n, i - 1);
                array[i - 2] = (int)GetDigit(n, i);

                array = Sorting(_array: array, _amountOfElements: amountOfElements, index: i);

                break;
            }
        }

        string transform = String.Concat<int>(array);

        long outputNumber = long.Parse(transform);

        if (outputNumber == n)
            return -1;

        return outputNumber;
    }

    public static long GetDigit(long _inputNumber, int digitNumber)
    {
        if (digitNumber < 0)
            throw new ArgumentOutOfRangeException("digitNumber");

        int digitCount = (int)Math.Log10(_inputNumber) + 1;
        if (digitNumber > digitCount)
            return 0;

        var power = (long)Math.Pow(10, digitCount - digitNumber);
        return (_inputNumber / power) % 10;
    }

    public static int[] Sorting(int[] _array, int _amountOfElements, int index)
    {
        List<int> numbers = new List<int>();
        List<int> indices = new List<int>();

        for (int j = index; j < _amountOfElements; j++)
        {
            if (_array[j] > _array[index - 1] && _array[j] < _array[index - 2])
            {
                numbers.Add(_array[j]);
                indices.Add(j);
            }
        }

        int positionOfMin = -1;

        for (int k = 0, min = 10; k < numbers.Count; k++)
        {
            if (numbers[k] < min)
            {
                min = numbers[k];
                positionOfMin = indices[k];
            }
        }

        if (positionOfMin != -1)
        {
            int temp = _array[index - 2];
            _array[index - 2] = _array[positionOfMin];
            _array[positionOfMin] = temp;
        }

        int[] tempArray = new int[_amountOfElements - index + 1];

        for (int l = _amountOfElements - 1, x = tempArray.Length - 1; x > -1; l--)
        {
            tempArray[x] = _array[l];
            x--;
        }

        Array.Sort(tempArray);

        for (int l = _amountOfElements - 1, x = tempArray.Length - 1; x > -1; l--)
        {
            _array[l] = tempArray[x];
            x--;
        }

        return _array;
    }
}