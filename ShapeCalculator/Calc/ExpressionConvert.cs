using System;
using System.Collections;
using System.Collections.Generic;
namespace Calc
{
    public class ExpressionConvert
    {
        public ExpressionConvert()
        {
        }

        private static List<string> split(string s){
            List<string> res = new List<string>();
            string[] tmp = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string i in tmp)
            {
                res.Add(i);
            }
            return res;
        }

        private static List<string> infixToPostFix(string s)
        {
            List<string> inp = split(s);
            Stack<string> tmp = new Stack<string>();
            List<string> res = new List<string>();
            foreach (string i in inp)
            {
                int check = PriorityExpression.getInstance().getPriority(i);
                switch (check)
                {
                    case 0:
                        res.Add(i);
                        break;
                    case -1:
                        tmp.Push(i);
                        break;
                    case 5:
                        while (!tmp.Peek().Equals("("))
                        {
                            res.Add(tmp.Peek());
                            tmp.Pop();
                        }
                        tmp.Pop();
                        break;
                    default:
                        if (tmp.Count == 0) tmp.Push(i);
                        else
                        {
                            while (check <= PriorityExpression.getInstance().getPriority(tmp.Peek()))
                            {
                                res.Add(tmp.Peek());
                                tmp.Pop();
                                if (tmp.Count == 0) break;
                            }
                            tmp.Push(i);
                        }
                        break;
                }
            }

            while (tmp.Count != 0)
            {
                res.Add(tmp.Peek());
                tmp.Pop();
            }

            return res;
        }

        public static Expression stringToExpression(string s)
        {
            List<string> tmp = infixToPostFix(s);
            Stack<Expression> res = new Stack<Expression>();
            ExpressionFactory expressionFactory = new ExpressionFactory();
            foreach(string i in tmp){
                if (PriorityExpression.getInstance().getPriority(i) == 0)
                {
                    try
                    {
                        res.Push(new ConstExpression(double.Parse(i)));
                    }
                    catch(FormatException e)
                    {
                        res.Push(new VarExpression(i));
                    }
                }
                else 
                {
                    if (PriorityExpression.getInstance().getPriority(i) == 4)
                    {
                        Expression p = res.Pop();
                        res.Push(((TrigonometryExPression)(expressionFactory.GetExpression(i))).setExp(p));
                    }
                    else
                    {
                        Expression p1 = res.Pop();
                        Expression p2 = res.Pop();
                        res.Push(((BinaryExpression)(expressionFactory.GetExpression(i))).setExp(p2, p1));
                    }
                }
            }

            return res.Pop();
        }
    }
}