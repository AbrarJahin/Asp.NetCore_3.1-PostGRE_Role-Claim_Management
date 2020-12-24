﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace StartupProject_Asp.NetCore_PostGRE.Data.Enums
{
    public static class EnumerationExtension
    {
        public static string Description(this Enum value)
        {
            // get attributes  
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                //displayAttribute = attributes.ElementAt(0);
                displayAttribute = attributes.Where(t => t is DescriptionAttribute).FirstOrDefault();
            }

            // return description
            return displayAttribute?.Description ?? "Description Not Found";
        }

        public static string DesplayName(this Enum value)
        {
            // get attributes  
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                //displayAttribute = attributes.Where(t => t is DisplayNameAttribute).FirstOrDefault();
                try
                {
                    displayAttribute = GetPropValue(attributes[0], "Name");
                }
                catch {
                    displayAttribute = null;
                }
            }
            return displayAttribute;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
