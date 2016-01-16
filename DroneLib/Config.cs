using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.ComponentModel;

namespace DroneLibrary
{
    /// <summary>
    /// Stellt verschiedene Einstellungen für die Kugelmatik bereit.
    /// </summary>
    public class Config
    {
        
        [Category("Network Log")]
        [Description("Wenn true, dann werden gesendete Pakete in die Konsole geschrieben")]
        public bool VerbosePacketSending { get; set; }

        [Category("Network Log")]
        [Description("Wenn true, dann werden empfangene Pakete in die Konsole geschrieben")]
        public bool VerbosePacketReceive { get; set; }

        [Category("Network Log")]
        [Description("Wenn true, dann werden Ping-Pakete in die Konsole geschrieben")]
        public bool LogPingPacket { get; set; }

        [Category("Network")]
        [Description("Hello-Port der UDP-Verbindung zu der Drohne")]
        [Range(0, 65535)]
        public int ProtocolHelloPort { get; set; }

        [Category("Network")]
        [Description("Control-Port der UDP-Verbindung zu der Drohne")]
        [Range(0, 65535)]
        public int ProtocolControlPort { get; set; }

        [Category("Network")]
        [Description("Daten-Port der UDP-Verbindung zu der Drohne")]
        [Range(0, 65535)]
        public int ProtocolDataPort { get; set; }

        [Category("Network")]
        [Description("Die Zeitspanne vom Abschicken eines Pakets bis das Paket neugeschickt wird")]
        [Range(10, int.MaxValue)]
        public int AcknowlegdeTime { get; set; }

        [Category("Network")]
        [Description("Wenn true, dann werden Pakete (außer Ping) nicht gesendet, wenn das Drone offline ist.")]
        public bool IgnorePacketsWhenOffline { get; set; }

        [Category("Network")]
        [Description("Wenn true, dann wird der Guaranteed-Flag ignoriert, wenn das Drone offline ist.")]
        public bool IgnoreGuaranteedWhenOffline { get; set; }

        public Config()
        {
            ProtocolHelloPort = 4710;
            ProtocolControlPort = 4711;
            ProtocolDataPort = 4712;
            VerbosePacketSending = true;
            VerbosePacketReceive = true;
            AcknowlegdeTime = 20;
            IgnorePacketsWhenOffline = true;
            IgnoreGuaranteedWhenOffline = true;
        }

        public void PrintToConsole()
        {
            Console.WriteLine("Config:");
            foreach (PropertyInfo info in GetType().GetProperties())
                Console.WriteLine("{0} {1} = {2}", info.PropertyType.Name, info.Name, info.GetValue(this));
            Console.WriteLine();
        }

        public void SaveToFile(string file)
        {
            using (FileStream stream = File.OpenWrite(file))
            {
                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine("# Standard Einstellungen für die Drohne");
                writer.WriteLine();

                foreach (PropertyInfo info in GetType().GetProperties())
                {
                    DescriptionAttribute help = info.GetCustomAttribute<DescriptionAttribute>();
                    if (help != null)
                        writer.WriteLine("# {0}", help.Description);
                    Range range = info.GetCustomAttribute<Range>();
                    if (range != null)
                    {
                        if (range.Min == int.MinValue)
                            writer.WriteLine("# Bereich: <= {0}", range.Max);
                        else if (range.Max == int.MaxValue)
                            writer.WriteLine("# Bereich: >= {0}", range.Min);
                        else
                            writer.WriteLine("# Bereich: {0} - {1}", range.Min, range.Max);
                    }

                    writer.Write(info.Name);
                    writer.Write(" = ");
                    writer.WriteLine(info.GetValue(this));
                    writer.WriteLine();
                }

                writer.Flush();
            }
        }

        /// <summary>
        /// Lädt die Einstellungen aus einer Textdatei.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Config LoadConfigFromFile(string file)
        {
            string[] fileLines = File.ReadAllLines(file);
            Config config = new Config();
            Type configType = config.GetType();

            for (int i = 0; i < fileLines.Length; i++)
            {
                string line = fileLines[i].Trim();
                if (line.Length == 0 || line.StartsWith("#")) // # wird für Kommentare benutzt
                    continue;

                string[] keyValue = line.Split('=');
                if (keyValue.Length == 0)
                    throw new InvalidDataException(string.Format("Unexpected line {0} in config file: '=' was not found.", i + 1));

                // Feld in der Config-Klasse finden
                PropertyInfo field = configType.GetProperty(keyValue[0].Trim(), BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (field == null) // wenn Feld nicht gefunden dann ignorieren
                    continue;

                if (field.PropertyType == typeof(string))
                    field.SetValue(config, keyValue[1].Trim());
                else if (field.PropertyType == typeof(int))
                {
                    int value;
                    if (!TryParseInt(keyValue[1].Trim(), out value))
                        throw new InvalidDataException(string.Format("Unexpected line {0} in config file: can not parse number.", i + 1));

                    Range range = field.GetCustomAttribute<Range>();
                    if (range != null)
                    {
                        if (value < range.Min)
                            throw new InvalidDataException(string.Format("Unexpected line {0} in config file: value is smaller then {1}.", i + 1, range.Min));
                        else if (value > range.Max)
                            throw new InvalidDataException(string.Format("Unexpected line {0} in config file: value is bigger then {1}.", i + 1, range.Max));
                    }
                    field.SetValue(config, value);
                }
                else if (field.PropertyType == typeof(bool))
                {
                    string value = keyValue[1].Trim().ToLower();
                    if (value == "true" || value == "yes")
                        field.SetValue(config, true);
                    else if (value == "false" || value == "no")
                        field.SetValue(config, false);
                    else
                        throw new InvalidDataException(string.Format("Unexpected line {0} in config file: can not parse boolean value.", i + 1));
                }
                else if (field.PropertyType.IsEnum)
                {
                    string value = keyValue[1].Trim().ToLower();
                    field.SetValue(config, Enum.Parse(field.PropertyType, value, true));
                }
                else
                    throw new NotImplementedException();
            }

            return config;
        }

        private static bool TryParseInt(string s, out int result)
        {
            if (s.StartsWith("0x"))
                return int.TryParse(s.Remove(0, "0x".Length), NumberStyles.AllowHexSpecifier, null, out result);
            return int.TryParse(s, out result);
        }
    }
}
