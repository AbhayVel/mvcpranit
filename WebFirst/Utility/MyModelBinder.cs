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
        public static void Bind<T>(T obj, HttpRequest httpRequest, string[] include, string[] exclude,string prefix="")
        {

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
           var objType= obj.GetType();
            foreach (var key in httpRequest.Query)
            {
                if (keyValuePairs.ContainsKey(key.Key))
                {
                    keyValuePairs[key.Key] = httpRequest.Query[key.Key].First().ToString();
                } else
                {
                    keyValuePairs.Add(key.Key,httpRequest.Query[key.Key].First().ToString());
                }
            }

            foreach (var key in httpRequest.Form.Keys)
            {
                if (keyValuePairs.ContainsKey(key))
                {
                    keyValuePairs[key] = httpRequest.Form[key].First().ToString();
                }
                else
                {
                    keyValuePairs.Add(key, httpRequest.Form[key].First().ToString());
                }
            }

            Bind(obj, keyValuePairs,include, exclude,prefix );

            



        }

        public static void Bind<T>(T obj, Dictionary<string, string> keyValuePairs, string[] include, string[] exclude, string prefix = "")
        {
            var objType = obj.GetType();
            if (!string.IsNullOrEmpty(prefix))
            {
                Dictionary<string, string> keyValues = new Dictionary<string, string>();

                foreach (var key2 in keyValuePairs.Keys)
                {
                    if (key2.StartsWith(prefix))
                    {
                        keyValues.Add(key2.Replace(prefix + ".", ""), keyValuePairs[key2]);
                    }
                }

                Bind(obj, keyValues, include, exclude, "");
                //var  pPrpoerty = objType.GetProperty(prefix);
                //  if (pPrpoerty != null)
                //  {

                //      if (pPrpoerty.GetValue(obj) == null)
                //      {
                //          var objInst = Activator.CreateInstance(pPrpoerty.PropertyType);
                //          pPrpoerty.SetValue(obj, objInst);
                //          Bind(objInst, keyValues, include, exclude, "");
                //      }
                //      else
                //      {
                //          Bind(pPrpoerty.GetValue(obj), keyValues, include, exclude, "");
                //      }
                //  }
                return;
            }
            
           
            foreach (var key in keyValuePairs.Keys)
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
                   var starString= key.Split(new char[] { '.'}).ToArray()[0];
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();

                    foreach (var key2 in keyValuePairs.Keys)
                    {
                        if (key2== key)
                        {
                            keyValues.Add(key2.Replace(starString + ".", ""), keyValuePairs[key]);
                        }
                    }

                    pPrpoerty = objType.GetProperty(starString);
                    if (pPrpoerty != null)
                    {
                         
                        if (pPrpoerty.GetValue(obj) == null)
                        {
                           var objInst = Activator.CreateInstance(pPrpoerty.PropertyType);
                            pPrpoerty.SetValue(obj, objInst);
                            Bind(objInst, keyValues, include, exclude, "");
                        } else
                        {
                            Bind(pPrpoerty.GetValue(obj), keyValues, include, exclude, "");
                        }

                       // pPrpoerty.




                        continue;
                    } else
                    {
                        continue;
                    }
                
                }

                if (pPrpoerty == null)
                {
                    continue;
                }

                var value = Converts(keyValuePairs[key].ToString(), Type.GetType(pPrpoerty.PropertyType.ToString()));
                var listAttribute = pPrpoerty.GetCustomAttributes(true);
                if (listAttribute != null)
                {
                    var isValid = true;
                    for (var i = 0; i < listAttribute.Length; i++)
                    {
                        if (listAttribute[i] is ValidationAttribute)
                        {
                            ValidationAttribute va = listAttribute[i] as ValidationAttribute;
                            isValid = isValid && va.IsValid(value);
                        }
                    }

                    if (isValid)
                    {
                        pPrpoerty.SetValue(obj, value);
                    }
                    else
                    {
                        // pPrpoerty.SetValue(obj, value);
                    }

                }
                else
                {
                    try
                    {
                        pPrpoerty.SetValue(obj, value);
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }

            }

        }



        //public static void SetPropertyy(string compoundProperty, object target, object value)
        //{
        //    string[] properties = compoundProperty.Split('.');

        //    for (int i = 0; i <= properties.Length - 2; i++)
        //    {
        //        PropertyInfo propertyToGet = target.GetType().GetProperty(properties[i]);
        //        var property_value = propertyToGet.GetValue(target, null);
        //        if (property_value == null)
        //        {
        //            if (propertyToGet.PropertyType.IsClass)
        //            {
        //                property_value = Activator.CreateInstance(propertyToGet.PropertyType);
        //                propertyToGet.SetValue(target, property_value);
        //            }
        //        }
        //        target = property_value;
        //    }
        //    PropertyInfo propertyToSet = target.GetType().GetProperty(properties.Last());
        //    var originalValue = Converts(value.ToString(), Type.GetType(propertyToSet.PropertyType.ToString()));
        //    propertyToSet.SetValue(target, originalValue);
        //}


        public static dynamic Converts(string value ,Type type)
        {
            return Convert.ChangeType(value, type);
        }
    }
}
