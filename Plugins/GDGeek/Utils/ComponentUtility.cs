using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{

    public static class ComponentUtility
    {

        public const int Path = 1;
        public const int Type = 2;
        public const int Order = 4;
     

        public static string longName<T>(this T component,
            int elements = (Order | Type | Path)) where T : Component
        {
            
            string name = "";
            if ((elements & Order) != 0)
            {
                T[] ts = component.gameObject.GetComponents<T>();
                if (ts.Length != 0) {
                    int index = new List<T>(ts).IndexOf(component);
                    name = index.ToString();
                }
            }
            if ((elements & Type) != 0)
            {
                name += "#" + component.typeName();
            }
            
       
            if ((elements & Path) != 0)
            {
                name += "@" + component.gameObject.longName();
            }

            return name;
        }

        public static string typeName<T>(this T component) where T : Component
        {
            return component.GetType().ToString();
        }
/*
        public static string fullName<T>(this T component) where T : Component
        {
            string name = component.typeName() + "@" + component.gameObject.longName();

            return name;
        }*/
    }
}