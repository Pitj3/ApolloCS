using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Apollo.Core
{
    using Path = String;

    public class FileSystem
    {
        public static bool Exists(Path path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }

        public static bool IsFile(Path path)
        {
            return File.Exists(path);
        }

        public static bool IsDirectory(Path path)
        {
            return Directory.Exists(path);
        }

        public static Path[] GetFilePaths(Path directory, bool recursive)
        {
            return GetFilePaths(directory, recursive, "*.*");
        }

        public static Path[] GetFilePaths(Path directory, bool recursive, string searchPattern)
        {
            if (IsDirectory(directory))
                return Directory.GetFiles(directory, searchPattern,
                    recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            Log.Error("Path does not exist or is not a directory.");
            return null;

        }

        public static StreamReader LoadFileToStream(Path file)
        {
            if (IsFile(file))
            {
                try
                {
                    return new StreamReader(file);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }

            Log.Error(file + " does not exist or is not a valid file");
            return null;
        }

        public static string LoadFileToString(Path file)
        {
            if(IsFile(file))
            {
                try
                {
                    return File.ReadAllText(file);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }

            Log.Error(file + " does not exist or is not a valid file");
            return "";
        }

        public static byte[] LoadFileToBytes(Path file)
        {
            if (IsFile(file))
            {
                try
                {
                    return File.ReadAllBytes(file);
                }
                catch (Exception e)
                {
                    Log.Error(e.ToString());
                }
            }

            Log.Error(file + " does not exist or is not a valid file");
            return null;
        }

        public static T LoadFileAs<T>(Path file)
        {
            byte[] data = LoadFileToBytes(file);
            if (data == null) return default;
            
            BinaryFormatter bf = new BinaryFormatter();
            using MemoryStream ms = new MemoryStream(data);

            try
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            return default;
        }

        public static bool SaveToFile(Path path, string data, FileMode mode)
        {
            return SaveToFile(path, Encoding.ASCII.GetBytes(data), mode);
        }

        public static bool SaveToFile(Path path, byte[] data, FileMode mode)
        {
            if (!Exists(path))
            {
                Path directory = System.IO.Path.GetDirectoryName(path);
                if (!Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            try
            {
                FileStream stream = File.Open(path, mode, FileAccess.Write);
                stream.Write(data);
                stream.Close();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }

            return true;
        }
    }
}
