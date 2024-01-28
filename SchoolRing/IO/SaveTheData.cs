using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SchoolRing.Interfaces;
using SchoolRing.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SchoolRing.IO
{
    public class SaveTheData
    {
        static string registryKey = "Software\\AlekLishkovski\\SchoolRing";
        static string valueNameSC = "SchoolClassesData";
        static string valueNameVD = "VacationalDaysData";
        static string valueNameTD = "TimeData";
        static string valueNamePD = "PropertiesData";
        static string valueNameND = "NotesData";

        public static void SaveSchoolClasses()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKey);
            List<ISchoolClass> schoolData = Program.GetModels().ToList();
            string serializedData = JsonConvert.SerializeObject(schoolData);
            key.SetValue(valueNameSC, serializedData);
            key.Close();
        }
        public static void SaveVacation()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKey);
            List<IVacationalDays> vdData = Program.vdRepo.GetModels().ToList();
            string serializedData = JsonConvert.SerializeObject(vdData);
            key.SetValue(valueNameVD, serializedData);
            key.Close();
        }
        public static void SaveTimes()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKey);
            List<int> timeData = new List<int> { Program.ClassLength, Program.ShortBreakLength, Program.LongBreakLength, Program.LongBreakAfter };
            string serializedData = JsonConvert.SerializeObject(timeData);
            key.SetValue(valueNameTD, serializedData);
            key.Close();
        }
        public static void SaveProperties()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKey);
            Dictionary<string, string> properties = new Dictionary<string, string>();
            if (Program.HaveBeenIntoMainMenu)
                properties.Add("HaveBeenIntoMainMenu", "true");
            else
                properties.Add("HaveBeenIntoMainMenu", "false");

            if (Program.WithClassSchedule)
                properties.Add("WithClassSchedule", "true");
            else
                properties.Add("WithClassSchedule", "false");

            if (Program.isMessageShown)
                properties.Add("isMessageShown", "true");
            else
                properties.Add("isMessageShown", "false");

            if (Program.allowRinging)
                properties.Add("allowRinging", "true");
            else
                properties.Add("allowRinging", "false");
            properties.Add("fixedMelodyLength", $"{Program.fixedMelodyLength}");
            properties.Add("melodyForStartOfClassPath", $"{Program.melodyForStartOfClassPath}");
            properties.Add("melodyForEndOfClassPath", $"{Program.melodyForEndOfClassPath}");
            properties.Add("customIconPath", $"{Program.customIconPath}");
            properties.Add("textSizeForNotes", $"{Program.textSizeForNotes}");
            string serializedData = JsonConvert.SerializeObject(properties);
            key.SetValue(valueNamePD, serializedData);
            key.Close();

        }
        public static void SaveNotes()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(registryKey);
            List<INote> noteData = Program.noteRepo.GetModels().ToList();
            string serializedData = JsonConvert.SerializeObject(noteData);
            key.SetValue(valueNameND, serializedData);
            key.Close();
        }
        public static void ReadProperties()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey);
                if (key != null)
                {
                    string retrievedData = key.GetValue(valueNamePD) as string;
                    key.Close();
                    if (!string.IsNullOrEmpty(retrievedData))
                    {
                        Dictionary<string, string> properties = JsonConvert.DeserializeObject<Dictionary<string, string>>(retrievedData);
                        if (properties["HaveBeenIntoMainMenu"] == "true")
                            Program.HaveBeenIntoMainMenu = true;
                        else
                            Program.HaveBeenIntoMainMenu = false;
                        if (properties["WithClassSchedule"] == "true")
                            Program.WithClassSchedule = true;
                        else
                            Program.WithClassSchedule = false;
                        if (properties["isMessageShown"] == "true")
                            Program.isMessageShown = true;
                        else
                            Program.isMessageShown = false;
                        if (properties["allowRinging"] == "true")
                            Program.allowRinging = true;
                        else
                            Program.allowRinging = false;
                        Program.fixedMelodyLength = int.Parse(properties["fixedMelodyLength"]);
                        Program.melodyForStartOfClassPath = properties["melodyForStartOfClassPath"];
                        Program.melodyForEndOfClassPath = properties["melodyForEndOfClassPath"];
                        Program.customIconPath = properties["customIconPath"];
                        Program.textSizeForNotes = int.Parse(properties["textSizeForNotes"]);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void ReadSchoolClasses()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey);
            if (key != null)
            {
                string retrievedData = key.GetValue(valueNameSC) as string;
                key.Close();
                if (!string.IsNullOrEmpty(retrievedData))
                {
                    List<SchoolClass> decryptedData = JsonConvert.DeserializeObject<List<SchoolClass>>(retrievedData);
                    foreach (var item in decryptedData.ToList().OrderBy(x => x.Day).ThenBy(x => x.IsPurvaSmqna).ThenBy(x => x.Num))
                    {
                        if (item.IsMerging)
                        {
                            ISchoolClass schoolClass;
                            if (item.Num < 7)
                                schoolClass = decryptedData.First(x => x.Day == item.Day && x.Num == item.Num + 1 && x.IsPurvaSmqna == item.IsPurvaSmqna);
                            else
                                schoolClass = decryptedData.First(x => x.Day == item.Day && x.Num == 1 && x.IsPurvaSmqna == false);
                            item.MergeClassWith(schoolClass);
                        }
                        Program.AddRecord(item);
                    }
                }
            }
        }
        public static void ReadVacations()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey);
            if (key != null)
            {
                string retrievedData = key.GetValue(valueNameVD) as string;
                key.Close();
                if (!string.IsNullOrEmpty(retrievedData))
                {
                    List<VacationalDays> decryptedDataVacations = JsonConvert.DeserializeObject<List<VacationalDays>>(retrievedData);
                    foreach (var item in decryptedDataVacations)
                    {
                        Program.vdRepo.AddModel(item);
                    }
                }
            }
        }

        public static void ReadTimes()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey);
            if (key != null)
            {
                string retrievedData = key.GetValue(valueNameTD) as string;
                key.Close();
                if (!string.IsNullOrEmpty(retrievedData))
                {
                    List<int> decryptedDataTimes = JsonConvert.DeserializeObject<List<int>>(retrievedData);
                    Program.ClassLength = decryptedDataTimes[0];
                    Program.ShortBreakLength = decryptedDataTimes[1];
                    Program.LongBreakLength = decryptedDataTimes[2];
                    Program.LongBreakAfter = decryptedDataTimes[3];
                }
            }
        }

        public static void ReadNotes()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKey);
            if (key != null)
            {
                string retrievedData = key.GetValue(valueNameND) as string;
                key.Close();
                if (!string.IsNullOrEmpty(retrievedData))
                {
                    List<Note> decryptedDataNotes = JsonConvert.DeserializeObject<List<Note>>(retrievedData);
                    foreach (var item in decryptedDataNotes)
                    {
                        Program.noteRepo.AddModel(item);
                    }
                }
            }
        }
    }
}