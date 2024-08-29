using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public enum Digit
{
    A = 1, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
}


public struct BigInteger
{
    public List<int> numberList;// = new List<int>();
    public int factor;// = 0;
    private int element;// = 0;
    public void Init(int n)
    {
        numberList = new List<int>();
        while (n >= 1000)
        {
            numberList.Add(n % 1000);
            n /= 1000;
        }

        numberList.Add(n % 1000);
        factor = numberList.Count;
    }


    public BigInteger(int n)
    {
        numberList = new List<int>();
        while (n >= 1000)
        {
            numberList.Add(n % 1000);
            n /= 1000;
        }
        numberList.Add(n % 1000);
        factor = numberList.Count;
        element = 0; // element �ʵ� �ʱ�ȭ
    }

    public BigInteger(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            numberList = null;
            element = 0;
            factor = 0;
            return;
        }
        numberList = new List<int>();

        int length = s.Length;
        int startIndex = s.Length - 3;
        if (length < 3)
        {
            startIndex = 0;
        }
        int count = (int)Math.Ceiling((double)s.Length / 3);

        for (int i = 0; i < count; i++)
        {
            int substringLength = Math.Min(3, length);
            if (!int.TryParse(s.Substring(startIndex, substringLength), out element))
            {
                //numberList = null;
                element = 0;
                factor = 0;
                Clear();
                //Debug.Log("Fail Int Parsing!!");
                return;
            }

            numberList.Add(element);
            length -= substringLength;
            startIndex -= 3;
            if (startIndex < 0)
            {
                startIndex = 0;
            }

        }
        factor = numberList.Count;
        element = 0; // element �ʵ� �ʱ�ȭ

    }
    public BigInteger(BigInteger original)
    {
        if (original.numberList == null)
        {
            numberList = new List<int> { 0 };

        }
        else
        {
            numberList = new List<int>(original.numberList);
        }
        factor = original.factor;
        element = original.element;
    }


    public static BigInteger operator +(BigInteger left, BigInteger right)
    {
        left.Plus(right);
        return left;
    }
    public static BigInteger operator +(BigInteger left, int right)
    {
        BigInteger newRight = new BigInteger(right);
        left.Plus(newRight);
        return left;
    }

    public static BigInteger operator -(BigInteger left, BigInteger right)
    {
        left.Minus(right);
        return left;
    }

    public static BigInteger operator -(BigInteger left, int right)
    {
        BigInteger newRight = new BigInteger(right);
        left.Minus(newRight);
        return left;
    }

    public static BigInteger operator *(BigInteger left, float right)
    {
        left.Multiple(right);
        return left;
    }

    public static bool operator >(BigInteger left, BigInteger right)
    {
        if (left.factor > right.factor)
        {
            return true;
        }
        else if(left.factor < right.factor)
        {
            return false;
        }

        int i = left.factor - 1;
        while (i >= 0)
        {
            if (left.numberList[i] > right.numberList[i])
            {
                return true;
            }
            if (left.numberList[i] < right.numberList[i])
            {
                return false;
            }

            i--;
        }
        return false;
    }

    public static bool operator >=(BigInteger left, BigInteger right)
    {
        if (left.factor > right.factor)
        {
            return true;
        }
        else if (left.factor < right.factor)
        {
            return false;
        }

        int i = left.factor - 1;
        while (i >= 0)
        {
            if (left.numberList[i] > right.numberList[i])
            {
                return true;
            }
            else if (left.numberList[i] < right.numberList[i])
            {
                return false;
            }
            i--;
        }
        return true;
    }

    public static bool operator <(BigInteger left, BigInteger right)
    {
        if (left.factor < right.factor)
        {
            return true;
        }
        else if (left.factor > right.factor)
        {
            return false;
        }

        int i = left.factor - 1;
        while (i >= 0)
        {
            if (left.numberList[i] < right.numberList[i])
            {
                return true;
            }
            if (left.numberList[i] > right.numberList[i])
            {
                return false;
            }

            i--;
        }
        return false;
    }

    public static bool operator <=(BigInteger left, BigInteger right)
    {
        if (left.factor < right.factor)
        {
            return true;
        }
        else if (left.factor > right.factor)
        {
            return false;
        }

        int i = left.factor - 1;
        while (i >= 0)
        {
            if (left.numberList[i] < right.numberList[i])
            {
                return true;
            }
            else if (left.numberList[i] > right.numberList[i])
            {
                return false;
            }
            i--;
        }
        return true;
    }


    public void Init(string s)
    {
        numberList = new List<int>();

        if (string.IsNullOrEmpty(s))
        {
            Clear();
            return;
        }

        int length = s.Length;
        int startIndex = s.Length - 3;
        if (length < 3)
        {
            startIndex = 0;
        }
        int count = (int)Math.Ceiling((double)s.Length / 3);

        for (int i = 0; i < count; i++)
        {
            int substringLength = Math.Min(3, length);
            if (!int.TryParse(s.Substring(startIndex, substringLength), out element))
            {
                Clear();
                //Debug.Log("Fail Int Parsing!!");
                return;
            }

            numberList.Add(element);
            length -= substringLength;
            startIndex -= 3;
            if (startIndex < 0)
            {
                startIndex = 0;
            }

        }
        factor = numberList.Count;

    }

    public void Plus(BigInteger b)
    {
        int carry = 0;
        int i = 0;
        var maxfactor = Mathf.Max(factor, b.factor);
        var minfactor = Mathf.Min(factor, b.factor);

        for (i = 0; i < minfactor; i++)
        {
            int sum = numberList[i] + b.numberList[i] + carry;
            carry = sum / 1000; // 1000 �̻��̸� �ڸ� �ø� �߻�
            numberList[i] = sum % 1000; // �ڸ� �ø� �� ���� ���� ����
        }

        while (i < maxfactor)
        {
            int sum = carry;

            if (i < numberList.Count)
            {
                sum += numberList[i];
            }

            if (i < b.numberList.Count)
            {
                sum += b.numberList[i];
            }

            carry = sum / 1000; // �ڸ� �ø� �߻� ����
            sum %= 1000; // �ڸ� �ø� �� ���� ����

            if (i < numberList.Count)
            {
                numberList[i] = sum;
            }
            else
            {
                numberList.Add(sum); // ����Ʈ�� ���ο� �ڸ� �߰�
            }

            i++;
        }
        if (carry > 0)
        {
            numberList.Add(carry);
        }

        // ����Ʈ�� ����(�ڸ���) ������Ʈ
        factor = numberList.Count;
    }

    public void Minus(BigInteger b)
    {
        int i = 0;
        if (b.factor > factor)
        {
            Clear();
            return;
        }

        if (b.factor == factor && numberList[factor - 1] < b.numberList[b.factor - 1])
        {
            Clear();
            return;
        }

        for (i = 0; i < b.factor; i++)
        {
            int sub = numberList[i] - b.numberList[i];
            if (sub < 0)
            {
                if (i + 1 == factor)
                {
                    Clear();
                    return;
                }

                numberList[i + 1]--;
                numberList[i] = sub + 1000;
            }
            else
            {
                numberList[i] = sub;
            }
        }

        for (i = i++; i < factor; i++)
        {
            if (numberList[i] < 0)
            {
                if (i + 1 == factor)
                {
                    Clear();
                    return;
                }

                numberList[i + 1]--;
                numberList[i] += 1000;
            }
        }


        while (numberList[factor-1] == 0)
        {
            numberList.Remove(numberList[factor - 1]);
            factor -= 1;
            if (numberList.Count == 0)
            {
                Clear();
                return;
            }
        }
        factor = numberList.Count;
    }
    public void Multiple(float f)
    {
        if(numberList == null)
        {
            return;
        }
        int lastIndex = numberList.Count - 1;
        
        var newFirstFloat = numberList[lastIndex] * f;

        if (f >= 1)
        {
            numberList[lastIndex] = (int)newFirstFloat;
            newFirstFloat -= numberList[lastIndex];

            if (numberList.Count > 1)
            {
                newFirstFloat *= 1000;
                int secondIndex = numberList.Count - 2;
                var newSecondFloat = numberList[secondIndex] * f;
                newSecondFloat += newFirstFloat;
                numberList[secondIndex] = (int)MathF.Ceiling(newSecondFloat);
                if (numberList[secondIndex] > 1000)
                {
                    int carry = numberList[secondIndex] / 1000;
                    numberList[lastIndex] += carry;
                    numberList[secondIndex] = numberList[secondIndex] % 1000;
                }
            }
            else
            {
                newFirstFloat = Mathf.RoundToInt(newFirstFloat);
                numberList[lastIndex] += (int)newFirstFloat;
            }

        }
        else
        {
            if (numberList.Count > 1)
            {
                newFirstFloat *= 1000;
                float newSecondFloat = numberList[lastIndex - 1] * f;
                newSecondFloat += newFirstFloat;
                numberList[lastIndex - 1] = (int)newSecondFloat;
                if (numberList[lastIndex - 1] / 1000 > 0)
                {
                    numberList[lastIndex] = numberList[lastIndex - 1] / 1000;
                    numberList[lastIndex - 1] = numberList[lastIndex - 1] % 1000;
                }
                else
                {
                    numberList[lastIndex - 1] = numberList[lastIndex - 1] % 1000;
                    numberList.Remove(numberList[lastIndex]);
                    factor = numberList.Count;
                }

            }
            else
            {
                numberList[lastIndex] = (int)newFirstFloat;
                if (numberList[lastIndex] == 0)
                {
                    Clear();
                }
            }
            return;
        }

        Promotion();

        factor = numberList.Count;
    }

    private void Promotion()
    {
        int lastIndex = numberList.Count - 1;
        int current = numberList[lastIndex];

        while (current >= 1000)
        {
            int carry = current / 1000;
            current = current % 1000;

            numberList[lastIndex] = current;

            numberList.Add(carry);

            lastIndex++;
            current = numberList[lastIndex];
        }
    }


    public void Clear()
    {
        numberList.Clear();
        numberList.Add(0);
        factor = 1;
        return;  // b�� �� ŭ
    }


    public override string ToString()
    {
        int i = 0;
        StringBuilder sb = new StringBuilder();
        foreach (var item in numberList)
        {
            if (i != numberList.Count - 1)
            {
                string paddedNumber = item.ToString().PadLeft(3, '0');
                sb.Insert(0, paddedNumber);
            }
            else
            {
                sb.Insert(0, item.ToString());
            }
            i++;
        }
        return sb.ToString();
    }

    public string ToStringShort()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(numberList[numberList.Count - 1]);

        if (factor >= 2)
        {
            string paddedNumber = (numberList[numberList.Count - 2] / 100).ToString();
            if (paddedNumber != "0")
            {
                sb.Append(".");
                sb.Append(paddedNumber);
            }
            //string paddedNumber = numberList[numberList.Count - 2].ToString().PadLeft(3, '0');
            //sb.Append(paddedNumber);  .001 ó�� �Ҽ��� ���ڸ����� ǥ��
        }

        StringBuilder digit = new StringBuilder();
        int d = factor - 1;
        if (d > 0)
        {
            while (d >= 0)
            {
                int remainder = d % 26; // �������� ��� (0-25 ����)
                if (remainder == 0)
                {
                    if (d != 0)
                    {
                        digit.Insert(0, Digit.Z); // Z�� ����
                    }
                    d = (d / 26) - 1; // ���� �ڸ����� �̵� (-1�� 0 ��� �ε����� ����� ����)
                }
                else
                {
                    digit.Insert(0, (Digit)remainder); // �������� ���ĺ����� ��ȯ�Ͽ� �տ� �߰�
                    d = d / 26; // ���� �ڸ����� �̵�
                }
            }
            if (digit.Length > 0)
            {
                sb.Append(digit);
            }
        }
        return sb.ToString();

    }
}
