using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFirst.Utility
{
    public static class MyModelBinder
    {

        public static void Bind<T>(T obj, HttpRequest httpRequest, string[] include)
        {

            Bind(obj, httpRequest, include, new string[] { });
        }

        
        public static void Bind<T>(T obj, HttpRequest httpRequest)
        {

            Bind(obj, httpRequest, new string[] { });
        }
        public static void Bind<T>(T obj, HttpRequest httpRequest, string[] include, string[] exclude)
        {

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
           var objType= obj.GetType();
            foreach (var key in httpRequest.Query)
            {
                if (include !=null && include.Length  > 0 && !include.Contains(key.Key))
                {
                    continue;
                }

                if (exclude != null && exclude.Length > 0 && exclude.Contains(key.Key))
                {
                    continue;
                }
                var pPrpoerty = objType.GetProperty(key.Key);
                if (pPrpoerty == null)
                {
                    continue;
                }

                var value = Converts(httpRequest.Query[key.Key].First().ToString(), Type.GetType(pPrpoerty.PropertyType.ToString()));
                pPrpoerty.SetValue(obj, value);
                //if (pPrpoerty.PropertyType.Name.Equals("Int32"))
                //{
                //    var i = 0;
                //    if (Int32.TryParse(httpRequest.Query[key.Key].First().ToString(), out i))
                //    {
                //        pPrpoerty.SetValue(obj, i);
                //    }

                //}
                //else
                //{
                //    pPrpoerty.SetValue(obj, httpRequest.Query[key.Key].First().ToString());
                //}

            }

            foreach (var key in httpRequest.Form.Keys)
            {
                if (include != null && include.Length > 0 && !include.Contains(key))
                {
                    continue;
                }

                if (exclude != null && exclude.Length > 0 && exclude.Contains(key))
                {
                    continue;
                }
                var pPrpoerty = objType.GetProperty(key);
                if (key.Contains("."))
                {

                }
                
                if (pPrpoerty == null)
                {
                    continue;
                }

                var value = Converts(httpRequest.Form[key].First().ToString(), Type.GetType(pPrpoerty.PropertyType.ToString()));
                var listAttribute = pPrpoerty.GetCustomAttributes(true);
                if (listAttribute != null)                
                {
                    var isValid = true;
                    for(var i = 0; i < listAttribute.Length; i++)
                    {
                        if(listAttribute[i] is ValidationAttribute)
                        {
                            ValidationAttribute va = listAttribute[i] as ValidationAttribute;
                            isValid = isValid && va.IsValid(value);
                        }
                    }

                    if (isValid)
                    {
                        pPrpoerty.SetValue(obj, value);
                    }else
                    {
                       // pPrpoerty.SetValue(obj, value);
                    }
                    
                }
                else
                {
                    pPrpoerty.SetValue(obj, value);
                }
                
                


                //if (pPrpoerty.PropertyType.Name.Equals("Int32"))
                //{
                //    var i = 0;
                //    if (Int32.TryParse(httpRequest.Form[key].First().ToString(), out i))
                //    {
                //        pPrpoerty.SetValue(obj, i);
                //    }

                //}
                //else
                //{
                //    pPrpoerty.SetValue(obj, httpRequest.Form[key].First().ToString());
                //}

            }



        }


        public static dynamic Converts(string value ,Type type)
        {
            return Convert.ChangeType(value, type);
        }
    }
}
