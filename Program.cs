using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    enum NotificationChannel
    {
        BE,    // Backend
        FE,    // Frontend
        QA,    // Quality Assurance
        Urgent // Urgent
    }

    static readonly Dictionary<string, NotificationChannel> TagToChannelMap = new Dictionary<string, NotificationChannel>
    {
        { "BE", NotificationChannel.BE },
        { "FE", NotificationChannel.FE },
        { "QA", NotificationChannel.QA },
        { "Urgent", NotificationChannel.Urgent }
    };

    static void Main()
    {
        string[] titles = {
            "[BE][FE][Urgent] there is error",
            "[BE][QA][HAHA][Urgent] there is error"
        };

        foreach (string title in titles)
        {
            List<NotificationChannel> channels = ParseNotificationTitle(title);
            DisplayNotificationChannels(channels);
        }
    }

    static List<NotificationChannel> ParseNotificationTitle(string title)
    {
        HashSet<NotificationChannel> channels = new HashSet<NotificationChannel>();

        Regex regex = new Regex(@"\[([^\]]+)\]");
        MatchCollection matches = regex.Matches(title);

        foreach (Match match in matches)
        {
            string tag = match.Groups[1].Value;
            if (TagToChannelMap.TryGetValue(tag, out NotificationChannel channel))
            {
                channels.Add(channel);
            }
        }

        return channels.ToList();
    }

    static void DisplayNotificationChannels(List<NotificationChannel> channels)
    {
        string output = $"Receive channels: {string.Join(", ", channels.Select(c => c.ToString()))}";
        Console.WriteLine(output);
    }
}
