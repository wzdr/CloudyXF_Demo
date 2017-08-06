using System;
using System.Collections.Generic;
using System.Text;

using CloudyXF.Models;

namespace CloudyXF.Helpers
{
    public static class UserSettings
    {
        public enum LocalTimeNotationEnum { offValue, onValue };

        public static string SelectedLocation { get; set; }
        public static int LocalTimeOnValue { get; private set; }

        public const double DefaultLatitude = -43.5115280099653;
        public const double DefaultLongitude = 172.56435427213;

        public const string DefaultSelectedLocation = "My Location";

    }
}
