using GoWorkFactoryBusinessLogic.Enums;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        private static Stream CreateArchive(Assembly assembly, int userId, FileType fileType)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo("backup" + userId);
                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                else
                {
                    dirInfo.Create();
                }
                string fileName = $"backup{userId}.zip";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                IEnumerable<(string fileName, Stream stream)> files = new List<(string fileName, Stream stream)>();
                if (fileType == FileType.Json)
                {
                    files = BackupJson(assembly);
                }
                else if (fileType == FileType.Xml)
                {
                    files = BackupXml(assembly);
                }

                int i = 0;
                foreach (var file in files)
                {
                    using (FileStream fileStream = File.OpenWrite("backup" + userId + "/" + file.fileName))
                    {
                        byte[] data = new byte[file.stream.Length];
                        file.stream.Read(data, 0, data.Length);
                        fileStream.Write(data, 0, data.Length);
                    }
                    i++;
                }

                // архивируем
                ZipFile.CreateFromDirectory("backup" + userId, fileName);
                // удаляем папку
                dirInfo.Delete(true);

                return File.OpenRead(fileName);
            }
            catch (Exception)
            {
                // делаем проброс
                throw;
            }
        }

        public static Stream CreateArchiveJson(Assembly assembly, int userId)
        {
            return CreateArchive(assembly, userId, FileType.Json);
        }

        public static Stream CreateArchiveXml(Assembly assembly, int userId)
        {
            return CreateArchive(assembly, userId, FileType.Xml);
        }

        private static IEnumerable<(string fileName, Stream stream)> BackupJson(Assembly assembly)
        {
            Type contextType = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object context = Activator.CreateInstance(contextType);

            foreach (var typeName in typeNames)
            {
                Type type = assembly.GetType(typeName);

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

                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(body));
                memoryStream.Position = 0;
                yield return (type.Name + ".json", memoryStream);
            }

            contextType.GetMethod("Dispose").Invoke(context, null);
        }

        private static IEnumerable<(string fileName, Stream stream)> BackupXml(Assembly assembly)
        {
            Type contextType = assembly.GetType("GoWorkFactoryDataBase.GoWorkFactoryDataBaseContext");
            object context = Activator.CreateInstance(contextType);

            foreach (var typeName in typeNames)
            {
                Type type = assembly.GetType(typeName);

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

                MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(body));
                memoryStream.Position = 0;
                yield return (type.Name + ".xml", memoryStream);
            }

            contextType.GetMethod("Dispose").Invoke(context, null);
        }
    }
}
