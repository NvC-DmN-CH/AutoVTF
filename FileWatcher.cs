using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoVTF;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

enum CallType
{
    OnChanged = 0,
    OnCreated = 1,
    OnDeleted = 2,
    OnRenamed = 3
}

namespace AutoVTF
{
    internal class FileWatcher
    {
        private static TimeSpan debouncerDelay = new TimeSpan(0, 0, 0, 0, 100);
        private static Dictionary<string, List<CallType>> FileCallsPair = new Dictionary<string, List<CallType>>();
        private static FileSystemWatcher? watcher = null;

        public static void StartWatcher()
        {
            if (watcher != null)
                return;

            watcher = new FileSystemWatcher(Program.MainFormInstance.GetWatchFolderTextboxValue());
            watcher.NotifyFilter = NotifyFilters.LastWrite
                | NotifyFilters.FileName
                | NotifyFilters.DirectoryName;

            watcher.Changed += FileWatcher.OnChanged;
            watcher.Created += FileWatcher.OnCreated;
            watcher.Deleted += FileWatcher.OnDeleted;
            watcher.Renamed += FileWatcher.OnRenamed;
            watcher.Error += FileWatcher.OnError;

            for (int i = 0; i < Extensions.WatcherAllowedExtensionsFilter.Length; i++)
            {
                watcher.Filters.Add(Extensions.WatcherAllowedExtensionsFilter[i]);
            }
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public static void StopWatcher()
        {
            if (watcher == null)
                return;

            watcher.Dispose();
            watcher = null;
        }

        // INTERFACE
        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            RegisterFileCall(e.FullPath, CallType.OnChanged);
        }

        public static void OnCreated(object sender, FileSystemEventArgs e)
        {
            RegisterFileCall(e.FullPath, CallType.OnCreated);
        }

        public static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            RegisterFileCall(e.FullPath, CallType.OnDeleted);
        }

        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            RegisterFileCall(e.FullPath, CallType.OnRenamed);
            //Task.Delay(new TimeSpan(0, 0, 0, 0, DEBOUNCER_DURATION_MS)).ContinueWith(o => Decisions.OnFileRenamed(e.OldFullPath, e.FullPath) );
        }

        public static void OnError(object sender, ErrorEventArgs e)
        {
            // todo! print in alert box maybe?
        }


        // INTERNAL
        private static void RegisterFileCall(string file_path, CallType call)
        {
            List<CallType> calls_list;
            FileCallsPair.TryGetValue(file_path, out calls_list);

            if (calls_list == null)
            {
                calls_list = new List<CallType>();
                FileCallsPair.Add(file_path, calls_list);
                Task.Delay(debouncerDelay).ContinueWith(o => TakeAction(file_path));
            }

            calls_list.Add(call);
        }

        private static void UnregisterFileCall(string file_path)
        {
            FileCallsPair.Remove(file_path);
        }

        private static void TakeAction(string file_path)
        {
            try
            {
                List<CallType> calls_list;
                FileCallsPair.TryGetValue(file_path, out calls_list);

                bool deleted = calls_list.Contains(CallType.OnDeleted);
                bool created = calls_list.Contains(CallType.OnCreated);
                bool changed = calls_list.Contains(CallType.OnChanged);
                bool renamed = calls_list.Contains(CallType.OnRenamed);
                UnregisterFileCall(file_path);

                if (deleted && !created && !changed && !renamed)
                {
                    //Decisions.OnFileDeleted(file_path);
                    return;
                }

                if (renamed && !created && !changed && !deleted)
                {
                    return;
                }

                if (!File.Exists(file_path))
                {
                    // yea it seems like the world of filesystems is weird, and this can happen
                    return;
                }

                Decisions.OnFileUpdated(file_path);
            }
            catch (Exception e)
            {
                Program.Alert(e.Message + "\n" + e.StackTrace);
            }
        }
    }
}
