using System;
using System.Collections.Generic;
using System.IO;

class Program {
    static void Main() {
        var targetKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "FullscreenMode",
            "LastConfirmedFullscreenMode",
            "PreferredFullscreenMode"
        };

        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string iniPath = Path.Combine(localAppData, "Astro", "Saved", "Config", "WindowsNoEditor", "GameUserSettings.ini");

        Console.WriteLine("Astroneer Fullscreen Fix");
        Console.WriteLine("Target: " + iniPath + "\n");

        if (!File.Exists(iniPath)) {
            Console.WriteLine("ERROR: GameUserSettings.ini not found.");
            Console.WriteLine("Launch Astroneer at least once, then run this again.");
            Pause();
            return;
        }

        try {
            File.Copy(iniPath, iniPath + ".bak", overwrite: true);   // back up first

            string[] lines = File.ReadAllLines(iniPath);
            var changed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < lines.Length; i++) {
                int eq = lines[i].IndexOf('=');
                if (eq <= 0) continue;                       // not a key=value line

                string key = lines[i].Substring(0, eq).Trim();
                if (targetKeys.Contains(key))                // exact key match — avoids the substring trap
                {
                    lines[i] = key + "=0";
                    changed.Add(key);
                }
            }

            File.WriteAllLines(iniPath, lines);

            Console.WriteLine($"Updated {changed.Count} of {targetKeys.Count} settings:");
            foreach (var key in targetKeys)
                Console.WriteLine("  " + key + (changed.Contains(key) ? " -> 0" : "  (NOT FOUND)"));
            Console.WriteLine("\nBackup saved alongside the original (.bak).");
        }
        catch (UnauthorizedAccessException) {
            Console.WriteLine("ERROR: Permission denied. Close Astroneer, or run as administrator.");
        }
        catch (IOException ex) {
            Console.WriteLine("ERROR: Couldn't write the file — is Astroneer still running?\n" + ex.Message);
        }

        Pause();
    }

    static void Pause() {
        Console.WriteLine("\nPress any key to close.");
        Console.ReadKey();
    }
}