using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GoWorkFactoryBusinessLogic.BusinessLogics
{
    public static class ClientBackupLogic
    {
        private static List<string> typeNames = new List<string>
        {
            "GoWorkFactoryDataBase.Models.Order",
            "GoWorkFactoryDataBase.Models.Product",
            "GoWorkFactoryDataBase.Models.ProductOrder",
            "GoWorkFactoryDataBase.Models.User"
        };

        public static void BackupJson(Assembly assembly, string path)
        {
            Type contextType = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object context = Activator.CreateInstance(contextType);

            foreach (var typeName in typeNames)
            {
                Type type = assembly.GetType(typeName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string body = "[\n";
                IEnumerable set = (IEnumerable)contextType.GetMethod("Set").MakeGenericMethod(type).Invoke(context, null);
                List<string> objs = new List<string>();
                foreach (var item in set)
                {
                    string s = "\t{\n";
                    s += string.Join(",\n", type.GetProperties().Select(prop => {
                        object val = prop.GetValue(item);
                        if (prop.PropertyType.IsPrimitive)
                        {
                            return $"\t\t\"{prop.Name}\": {val.ToString().ToLower()}";
                        }
                        else if (prop.PropertyType == typeof(string) || prop.PropertyType.IsEnum)
                        {
                            return $"\t\t\"{prop.Name}\": \"{val}\"";
                        }
                        return null;
                    }).Where(x => x != null));
                    s += "\n\t}";
                    objs.Add(s);
                }
                body += string.Join(",\n", objs);
                body += "\n]";

                File.WriteAllText(path + "/" + type.Name + ".json", body);
            }

            contextType.GetMethod("Dispose").Invoke(context, null);
        }

        public static void BackupXml(Assembly assembly, string path)
        {
            Type contextType = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object context = Activator.CreateInstance(contextType);

            foreach (var typeName in typeNames)
            {
                Type type = assembly.GetType(typeName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string body = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n";
                body += $"<{type.Name}-list>\n";
                IEnumerable set = (IEnumerable)contextType.GetMethod("Set").MakeGenericMethod(type).Invoke(context, null);
                foreach (var item in set)
                {
                    body += $"\t<{type.Name}>\n";
                    foreach (var prop in type.GetProperties())
                    {
                        object val = prop.GetValue(item);
                        if (prop.PropertyType.IsPrimitive || prop.PropertyType.IsEnum || prop.PropertyType == typeof(string))
                        {
                            body += $"\t\t<{prop.Name}>{val}</{prop.Name}>\n";
                        }
                    }
                    body += $"\t</{type.Name}>\n";
                }
                body += $"</{type.Name}-list>";

                File.WriteAllText(path + "/" + type.Name + ".xml", body);
            }

            contextType.GetMethod("Dispose").Invoke(context, null);
        }
    }
}
