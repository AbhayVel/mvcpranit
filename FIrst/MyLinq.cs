using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FIrst
{
    public static class MyLinq
    {
         public static List<string> Contains(List<string> lst , string comp)
        {
            List<string> lstStr = new List<string>();

            foreach (var l in lst)
            {
                if (l.Contains("p", StringComparison.OrdinalIgnoreCase))
                {
                    lstStr.Add(l);
                }

            }

            return lstStr;
        }

        public static List<int> GreateThen(List<int> lst, int comp)
        {
            List<int> lstStr = new List<int>();

            foreach (var l in lst)
            {
                if (l> comp)
                {
                    lstStr.Add(l);
                }

            }

            return lstStr;
        }


        public static int CountMy<T>(this IEnumerable<T> lst, MyFunc<T, bool> condition)
        {
            int count = 0;
            foreach (var l in lst)
            {
                if (condition(l))
                {
                    count++;
                }
            }

                return count;
        }

        public static int CountMy<T>(this IEnumerable<T> lst)
        {
            int count = 0;
            foreach (var l in lst)
            {
               
                    count++;
                 
            }

            return count;
        }


        public static IEnumerable WhereMy<T>(this IEnumerable<T> lst, Func<T,int, bool> condition)
        {
            int index = -1;
            foreach (var l in lst)
            {
                index++;
                if (condition(l, index))
                {
                   
                    yield return l;
                }
                
            }


        }


        public static IEnumerable WhereMy<T>(this IEnumerable<T> lst, Predicate<T> condition)
        {
           foreach (var l in lst)
            {
                if (condition(l))
                {
                    yield return l;
                }

            }

            
        }

        public static IEnumerable Distinct<T>(this IEnumerable<T> lst, Func<T, bool> condition)
        {
            
            foreach (var l in lst)
            {
                if (condition(l))
                {
                    yield return l;
                }

            }


        }


        public static System.Collections.ArrayList ToListMy(this IEnumerable lst)
        {
            ArrayList lstin = new ArrayList();
            foreach (var l in lst)
            {
                 
                    lstin.Add(l);
                

            }
            return lstin;

        }


        public static List<T> ToListMy<T>(this IEnumerable<T> lst)
        {
            List<T> lstin = new List<T>();
            foreach (var l in lst)
            {

                lstin.Add(l);


            }
            return lstin;

        }



    }
}
