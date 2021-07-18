using System;
using System.Collections.Generic;
using System.Linq;

namespace FIrst
{
    public delegate R MyFunc<out R>();
    public delegate R MyFunc<in T, out R>(T t);
    public delegate bool MyPredicate<in R>(R r);
    public delegate int MyCompare<in T>(T x,T y, int a, string b);
    public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
    public delegate R MyFunc<in T1,in T2, out R>(T1 t1, T2 t2);
    public delegate void MyAction<in R>(R r);
    public delegate void MyAction();


    class Program
    {

     static   MyFunc<bool> myt =new MyFunc<bool>(() => {
         Console.WriteLine("I hav run myt");
         return true; });
        static void Main(string[] args)
        {
            MyAction ma = new MyAction(() =>{ Console.WriteLine("I hav run"); });

        //    IAsyncResult a = myt.BeginInvoke(null, null);


     //   var data2=    myt.EndInvoke(a);



           // Console.WriteLine("Hello World!");
            List<string> strList = new List<string>(12);
            strList.Add("abhay");
            strList.Add("Pranit");
            strList.Add("Pradeep");
            strList.Add("RItesh");
            strList.Add("sandeep");
            strList.Add("Manish");

        var arr=    strList.ToArray();
           // var ddir = strList.ToDictionary(x => x.Substring(0,1), y => y);
            var dd=(Lookup<string,string>)strList.ToLookup(x => x.Substring(0, 1), y => y);

            //foreach (var keyLookup in ddir)
            //{
            //    Console.WriteLine(keyLookup.Key + keyLookup.Value);
            //}
            foreach (var keyLookup in dd)
            {
                Console.WriteLine(keyLookup.Key);
            }
            int t = strList.CountMy();

          

            //var lst = MyLinq.Contains(strList, "P");
            //foreach (var l in lst)
            //{

            //        Console.WriteLine(l);


            //}

            var lst = strList.WhereMy<string>((e,i)=>e.Contains("p", StringComparison.OrdinalIgnoreCase) && i > 2).ToListMy();
            var er = lst.GetEnumerator();
              er.MoveNext();
            var data = er.Current;

            //foreach (var l in lst)
            //{

            //    Console.WriteLine(l);


            //}

            var lst1 = strList.WhereMy<string>((e) => e.Contains("a", StringComparison.OrdinalIgnoreCase));
            //foreach (var l in lst)
            //{

            //    Console.WriteLine(l);


            //}

            Console.Read();


            List<int> intList = new List<int>();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            intList.Add(4);
            intList.Add(5);
            intList.Add(6);

           var ilst = intList.WhereMy<int>((e) => e> 2);
            //foreach (var l in ilst)
            //{

            //    Console.WriteLine(l);


            //}

            
            Console.Read();

        }
    }
}
