using System;
using System.Reflection;
using UnityEngine;

namespace GDGeek{
    
        // 示例类
        public class MyClass
        {
            public void PublicMethod(int number)
            {
               Debug.LogError($"Public Method Called with number: {number}");
            }

            private string PrivateMethod(string text)
            {
                return $"Private Method Called with text: {text}";
            }
        }
        // 定义接口
    
    
    public interface IMethodInvoker
    {
        object InvokeMethod(object target, string methodName, params object[] parameters);
    }

    public class MethodInvoker : IMethodInvoker
    {
        public object InvokeMethod(object target, string methodName, params object[] parameters)
        {
            if (target == null || string.IsNullOrEmpty(methodName))
            {
                return null;
            }

            // 获取目标对象的类型
            Type targetType = target.GetType();

            // 查找方法，忽略大小写并匹配参数类型
            MethodInfo method = targetType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
            {
                Console.WriteLine($"Method '{methodName}' not found.");
                return null;
            }

            try
            {
                // 调用方法并返回结果
                object result = method.Invoke(target, parameters);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to invoke method: {ex.Message}");
                return null;
            }
        }
    }

}