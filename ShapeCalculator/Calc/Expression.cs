using System;
namespace Calc
{
    public interface Expression
    {
        double calculate();
        string toString(Notation ntt);
        Expression clone();
    }
}
