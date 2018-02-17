using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BATTLEGROUNDS_EXERNAL
{
    public static class G
    {   //Define Helpers
        public static IntPtr hProcess { get; set; }
        public static Memory Memory { get; set; }

        public static List<AActor> Entities { get; set; }
        public static List<AActor> Vehicles { get; set; }
        public static List<AActor> Players { get; set; }

        public static IntPtr pUWorld { get; set; }
        public static UWorld UWorld { get; set; }
        public static ULevel ULevel { get; set; }
        public static UGameInstance OwningGameInstance { get; set; }

        public static string[] Names { get; set; }
    }
}
