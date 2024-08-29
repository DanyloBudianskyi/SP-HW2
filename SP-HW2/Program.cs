using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP_HW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////1. Для класса String вывести все методы, поля, свойства типа и список поддерживаемых интерфейсов.
            //Type type = typeof(string);
            //MethodInfo[] methodInfo = type.GetMethods();
            //Console.WriteLine("Methods:");
            //Console.Write("\t");
            //foreach (MethodInfo method in methodInfo)
            //{
            //    Console.Write($"{method.Name}, ");
            //}

            //FieldInfo[] fieldsInfo = type.GetFields();
            //Console.WriteLine("\n\nFields:");
            //Console.Write("\t");
            //foreach (FieldInfo field in fieldsInfo)
            //{
            //    Console.Write($"{field.Name} ");
            //}

            //PropertyInfo[] propertyInfo  = type.GetProperties();
            //Console.WriteLine("\n\nProperties:");
            //Console.Write("\t");
            //foreach (PropertyInfo property in propertyInfo)
            //{
            //    Console.Write($"{property.Name}, ");
            //}

            //Type[] interfacesInfo = type.GetInterfaces();
            //Console.WriteLine("\n\ninterfaces:");
            //Console.Write("\t");
            //foreach (Type interfaceInfo in interfacesInfo)
            //{
            //    Console.Write($"{interfaceInfo.Name}, ");
            //}

            //Console.ReadKey();

            //2. Для собственного типа вывести список всех методов, запросить у пользователя имя метода, который он хочет выполнить, запросить параметры, необходимые для выполнения вызова, выполнить вызов и вывести результат.
            try
            {
                Assembly assembly = Assembly.LoadFrom("StudentLibrary.dll");

                Type type = assembly.GetType("StudentLibrary.Student");
                object obj = Activator.CreateInstance(type, new object[] { "Pupkin", "Vasya", 2000 });
                Console.WriteLine("All methods:");
                foreach (var method in type.GetMethods())
                {
                    Console.Write($"\t{method.ReturnType.Name}: {method.Name}(");
                    if (method.GetParameters().Length == 0)
                    {
                        Console.WriteLine(")");
                    }
                    else
                    {
                        foreach (var item in method.GetParameters())
                        {
                            Console.Write($"{item.ParameterType.Name}");
                            Console.Write($"{(item == method.GetParameters().Last() ? ")" : ", ")}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                string choice;
                double grant = 0;
                while (true)
                {
                    Console.Write("What type of method you need(set, get, show): ");
                    choice = Console.ReadLine();
                    MethodInfo methodInfo;
                    if (choice == "show")
                    {
                        methodInfo = type.GetMethod("Show");
                        Console.WriteLine(methodInfo.Invoke(obj, null));
                    }
                    else if (choice == "get")
                    {
                        Console.Write("Get: name, lastName or grant?: ");
                        choice = Console.ReadLine();
                        if (choice == "name")
                        {
                            methodInfo = type.GetMethod("GetFirstName");
                            Console.WriteLine(methodInfo.Invoke(obj, null));
                        }
                        else if (choice == "lastName")
                        {
                            methodInfo = type.GetMethod("GetLastName");
                            Console.WriteLine(methodInfo.Invoke(obj, null));
                        }
                        else if (choice == "grant")
                        {
                            methodInfo = type.GetMethod("GetGrant");
                            Console.WriteLine(methodInfo.Invoke(obj, null));
                        }
                    }
                    else if (choice == "set")
                    {
                        Console.WriteLine("Set: name, lastName or grant?:");
                        choice = Console.ReadLine();
                        if (choice == "name")
                        {
                            Console.Write("Input new name: ");
                            choice = Console.ReadLine();
                            methodInfo = type.GetMethod("SetFirstName");
                            methodInfo.Invoke(obj, new object[] { choice });
                        }
                        else if (choice == "lastName")
                        {
                            Console.Write("Input new last name: ");
                            choice = Console.ReadLine();
                            methodInfo = type.GetMethod("SetLastName");
                            methodInfo.Invoke(obj, new object[] { choice });
                        }
                        else if (choice == "grant")
                        {
                            Console.Write("Input new grant: ");
                            grant = double.Parse(Console.ReadLine());
                            methodInfo = type.GetMethod("SetGrant");
                            methodInfo.Invoke(obj, new object[] { grant });
                        }

                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
