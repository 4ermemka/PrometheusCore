using System;
using System.Collections.Generic;
using System.Linq;

public class SPECIAL
{
    public int S;
    public int P;
    public int E;
    public int C;
    public int I;
    public int A;
    public int L;

    public SPECIAL()
    {
        S = 0;
        P = 0;
        E = 0;
        C = 0;
        I = 0;
        A = 0;
        L = 0;
    }

    public SPECIAL(int s = 0, int p = 0, int e = 0, int c = 0, int i = 0, int a = 0, int l = 0)
    {
        S = s;
        P = p;
        E = e;
        C = c;
        I = i;
        A = a;
        L = l;
    }

    public SPECIAL Clone()
    {
        return new SPECIAL(S, P, E, C, I, A, L);
    }

    public SPECIAL(string specialString)
    {
        List<int> parsed = specialString.Split(',').Select(x => Convert.ToInt32(x)).ToList();

        S = parsed[0];
        P = parsed[1];
        E = parsed[2];
        C = parsed[3];
        I = parsed[4];
        A = parsed[5];
        L = parsed[6];
    }

    public static SPECIAL operator +(SPECIAL a, SPECIAL b)
    {
        return new SPECIAL()
        {
            S = a.S + b.S,
            P = a.P + b.P,
            E = a.E + b.E,
            C = a.C + b.C,
            I = a.I + b.I,
            A = a.A + b.A,
            L = a.L + b.L
        };
    }

    public static SPECIAL operator -(SPECIAL a, SPECIAL b)
    {
        return new SPECIAL()
        {
            S = a.S - b.S,
            P = a.P - b.P,
            E = a.E - b.E,
            C = a.C - b.C,
            I = a.I - b.I,
            A = a.A - b.A,
            L = a.L - b.L
        };
    }

    public static bool operator ==(SPECIAL a, SPECIAL b)
    {
        return
        a.S == b.S &&
        a.P == b.P &&
        a.E == b.E &&
        a.C == b.C &&
        a.I == b.I &&
        a.A == b.A &&
        a.L == b.L;
    }

    public static bool operator !=(SPECIAL a, SPECIAL b)
    {
        return
        a.S != b.S ||
        a.P != b.P ||
        a.E != b.E ||
        a.C != b.C ||
        a.I != b.I ||
        a.A != b.A ||
        a.L != b.L;
    }

    public virtual string ToString()
    {
        string result =
            $"{S}," +
            $"{P}," +
            $"{E}," +
            $"{C}," +
            $"{I}," +
            $"{A}," +
            $"{L}";
        return result;
    }
}