using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace BATTLEGROUNDS_EXERNAL
{
    public partial class CheatForm : MaterialForm
    {
        public CheatForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void CheatForm_Load(object sender, EventArgs e)
        {
            // SET UP SIGSCANNER
            Helpers.SigScanSharp SigScan = new Helpers.SigScanSharp(G.hProcess);

            // GET BASE ADDRESS OF GAME PROCESS
            IntPtr[] hMods = new IntPtr[1024];
            var pModules = GCHandle.Alloc(hMods, GCHandleType.Pinned);

            uint size = (uint)IntPtr.Size * 1024;
            uint cbNeeded;
            if (Win32.EnumProcessModules(G.hProcess, pModules.AddrOfPinnedObject(), size, out cbNeeded))
            {
                G.Memory = new Memory(G.hProcess, hMods[0]); // INITIALISE MEMORY CLASS

                int cb = Marshal.SizeOf(typeof(Win32._MODULEINFO));
                Win32._MODULEINFO modinfo;
                Win32.GetModuleInformation(G.hProcess, hMods[0], out modinfo, cb);
                
                // GET OFFSETS
                if (SigScan.SelectModule(hMods[0]/*MAIN MODULE*/, modinfo.SizeOfImage))
                {   
                    long lTime = 0;
                    var GWorldAddress = (IntPtr)SigScan.FindPattern("48 8B 1D ? ? ? ? 74 40", out lTime);
                    var GWorldOffset = M.Read<uint>(GWorldAddress + 3) + 7;
                    var ppUWorld = (IntPtr)((ulong)GWorldAddress + GWorldOffset);
                    G.pUWorld = M.Read<IntPtr>(ppUWorld);
                    Console.WriteLine($"Found UWorld at 0x{((ulong)ppUWorld - (ulong)hMods[0]).ToString("x2")} - {lTime}ms");

                    var GNamesAddress = SigScan.FindPattern("48 89 1D ? ? ? ? 48 8B 5C 24 ? 48 83 C4 28 C3 48 8B 5C 24 ? 48 89 05 ? ? ? ? 48 83 C4 28 C3", out lTime);
                    var GNamesOffset = M.Read<uint>((IntPtr)GNamesAddress + 3);
                    GNamesAddress += GNamesOffset + 7;
                    Console.WriteLine($"Found GNames at 0x{(GNamesAddress - (ulong)hMods[0]).ToString("x2")} - {lTime}ms");
                    
                    GNames namearray = M.Read<GNames>((IntPtr)GNamesAddress);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    G.Names = namearray.GetStaticArray().DumpNames();
                    stopwatch.Stop();
                    
                    Console.WriteLine($"Dump GNames - {stopwatch.ElapsedMilliseconds}ms");
                }
            }

            // NO NASTY MEMORY LEAKS HERE
            pModules.Free();


            new Thread(Cheat.EntityLoop).Start();
            new Thread(Cheat.MainLoop).Start();

            // SHOW OVERLAY
            new OverlayForm().Show();
        }
        
        #region Aimbot
        private void chkAimbot_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.Aimbot = chkAimbot.Checked;
        }
        #endregion
        #region Visuals
        private void chkVisualsEnabled_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.Visuals = chkVisualsEnabled.Checked;
        }

        private void chkLineESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.LineESP = chkLineESP.Checked;
        }

        private void chkBoxESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.BoxESP = chkBoxESP.Checked;
        }

        private void chkDistanceESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.DistanceESP = chkDistanceESP.Checked;
        }

        private void chkVehicleESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.VehicleESP = chkVehicleESP.Checked;
        }

        private void chkLootESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.LootESP = chkLootESP.Checked;
        }

        private void chkBoneESP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.BoneESP = chkBoneESP.Checked;
        }

        #endregion
        #region Weapon Modifications
        private void chkNoRecoil_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.NoRecoil = chkNoRecoil.Checked;
        }

        private void chkNoSpread_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.NoSpread = chkNoSpread.Checked;
        }

        private void chkInfiniteAmmo_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.InfiniteAmmo = chkInfiniteAmmo.Checked;
        }

        private void chkInstantHit_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.InstantHit = chkInstantHit.Checked;
        }

        private void chkFullAuto_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.FullAuto = chkFullAuto.Checked;
        }

        private void chkMagicBullets_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.MagicBullets = chkMagicBullets.Checked;
        }

        private void chkNoMuzzleFlash_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.NoMuzzle = chkNoMuzzleFlash.Checked;
        }

        private void chkNoSway_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.NoSway = chkNoSway.Checked;
        }

        #endregion

        #region Miscellaneous
        private void chkMassTP_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.MassTeleport = chkMassTP.Checked;
        }

        private void chkFlying_CheckedChanged(object sender, EventArgs e)
        {
            CheatSettings.Flying = chkFlying.Checked;
        }
        #endregion


        private static class Win32
        {
            [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern bool EnumProcessModules(IntPtr hProcess, [Out] IntPtr lphModule, uint cb, out uint lpcbNeeded);

            [DllImport("psapi.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
            public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out _MODULEINFO lpModInfo, int cb);


            [StructLayout(LayoutKind.Sequential)]
            public struct _MODULEINFO
            {
                public IntPtr lpBaseOfDll;
                public uint SizeOfImage;
                public IntPtr EntryPoint;
            }
        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }
    }
}
