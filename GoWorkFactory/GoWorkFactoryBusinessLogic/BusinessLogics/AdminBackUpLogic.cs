using GoWorkFactoryBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    public static class AdminBackUpLogic
    {
        public static void CreateBackupToJSON(Assembly assembly, string folder)
        {
            Type typeDataBaseContext = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object dataBaseContext = Activator.CreateInstance(typeDataBaseContext);
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<AdminBackupAttribute>() != null)
                {
                    string content = "[";

                    object listObj = typeDataBaseContext.GetMethod("Set").MakeGenericMethod(type).Invoke(dataBaseContext, null);

                    List<string> objValues = new List<string>();
                    foreach (var obj in listObj as IEnumerable<object>)
                    {
                        List<string> values = new List<string>();
                        foreach (var property in type.GetProperties())
                        {
                            if (property.PropertyType.IsPrimitive) {
                                values.Add($"\"{property.Name}\":{property.GetValue(obj)}");
                            }
                            else if (property.PropertyType == typeof(string))
                            {
                                values.Add($"\"{property.Name}\":\"{property.GetValue(obj)}\"");
                            }
                        }

                        objValues.Add($" {{ {string.Join(",", values)} }}");
                    }

                    content += String.Join(",", objValues);

                    content += "]";

                    using (StreamWriter sw = new StreamWriter($"{folder}/{type.Name}.json"))
                    {
                        sw.WriteLine(content);
                    }
                }
            }
        }
        public static void CreateBackupToXML(Assembly assembly, string folder)
        {
            Type typeDataBaseContext = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object dataBaseContext = Activator.CreateInstance(typeDataBaseContext);
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetCustomAttribute<AdminBackupAttribute>() != null)
                {
                    string content = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> \n";
                    content += $"<{type.Name}s> \n";

                    object listObj = typeDataBaseContext.GetMethod("Set").MakeGenericMethod(type).Invoke(dataBaseContext, null);

                    foreach (var obj in listObj as IEnumerable<object>)
                    {
                        content += $"\t<{type.Name}> \n";
                        foreach (var property in type.GetProperties())
                        {
                            if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                            {
                                content += $"\t\t<{property.Name}>{property.GetValue(obj)}</{property.Name}> \n";
                            }
                        }
                        content += $"\t</{type.Name}> \n";
                    }

                    content += $"</{type.Name}s> \n";

                    using (StreamWriter sw = new StreamWriter($"{folder}/{type.Name}.xml"))
                    {
                        sw.WriteLine(content);
                    }
                }
            }
        }
    }
}
