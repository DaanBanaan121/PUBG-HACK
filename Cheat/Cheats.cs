using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BATTLEGROUNDS_EXERNAL
{
    public static class CheatSettings
    {
        public static bool InfiniteAmmo { get; internal set; }
        public static bool NoRecoil     { get; internal set; }
        public static bool NoSpread     { get; internal set; }
        public static bool MagicBullets { get; internal set; }
        public static bool MassTeleport { get; internal set; }
        public static bool Flying       { get; internal set; }
        public static bool RadarESP     { get; internal set; }
        public static bool DistanceESP  { get; internal set; }
        public static bool LineESP      { get; internal set; }
        public static bool Aimbot       { get; internal set; }
        public static bool LogIDs       { get; internal set; }
        public static bool VehicleESP   { get; internal set; }
        public static bool LootESP      { get; internal set; }
        public static bool Visuals      { get; internal set; }
        public static bool BoxESP       { get; internal set; }
        public static bool BoneESP      { get; internal set; }
        public static bool InstantHit   { get; internal set; }
        public static bool FullAuto     { get; internal set; }
        public static bool NoMuzzle     { get; internal set; }
        public static bool NoSway       { get; internal set; }
    }

    public class Cheat
    {
        public static void Update()
        {
            // FIX EMPTY ENTITY LIST
            var tempworld = M.Read<UWorld>(G.pUWorld);
            var tempOwningGameInstance = M.Read<UGameInstance>(tempworld.pOwningGameInstance);
            // END FIX 

            G.UWorld = M.Read<UWorld>(/*G.pUWorld*/tempOwningGameInstance.LocalPlayer.ViewportClient.pUWorld);
            G.ULevel = M.Read<ULevel>(G.UWorld.pPersistentLevel);
            G.OwningGameInstance = M.Read<UGameInstance>(G.UWorld.pOwningGameInstance);
            AActor.g_pLocalPlayer = G.OwningGameInstance.LocalPlayer.PlayerController.pLocalPlayer;
        }

        public static void EntityLoop()
        {
            while (true)
            {
                List<AActor> PlayerList = new List<AActor>();
                List<AActor> VehicleList = new List<AActor>();
                List<AActor> EntityList = new List<AActor>();

                for (int nIndex = 0; nIndex < G.ULevel.AActors.Count; nIndex++)
                {
                    AActor Actor = G.ULevel.AActors.ReadValue(nIndex, true);
                    Actor.BasePointer = M.Read<IntPtr>(G.ULevel.AActors[nIndex]);

                    if (Actor.BasePointer == IntPtr.Zero)
                        continue;

                    EntityList.Add(Actor);
                    
                    if (Actor.IsVehicle)
                    {
                        VehicleList.Add(Actor);
                        continue;
                    }

                    if (Actor.BasePointer != AActor.g_pLocalPlayer && Actor.IsPlayer && Actor.IsAlive)
                    {
                        PlayerList.Add(Actor);
                        continue;
                    }
                }

                G.Players = PlayerList;
                G.Entities = EntityList;
                G.Vehicles = VehicleList;

                Thread.Sleep(250);
            }
        }

        public static void MainLoop()
        {
            while (true)
            {
                Cheat.Update();

                AActor localplayer = AActor.GetLocalPlayer();

                #region Weapon Mods
                if (CheatSettings.InfiniteAmmo || CheatSettings.NoRecoil || CheatSettings.NoSpread || CheatSettings.MagicBullets)
                {
                    AWeaponProcessor WeaponProcessor = localplayer.WeaponProcessor;
                    TArray<ATslWeapon> EquippedWeapons = WeaponProcessor.EquippedWeapons;

                    for (int nItemSlot = 0; nItemSlot < 3; nItemSlot++)
                    {
                        var Weapon = EquippedWeapons.ReadValue(nItemSlot, true);

                        Weapon.BasePointer = M.Read<IntPtr>(EquippedWeapons[nItemSlot]);

                        if (Weapon.BasePointer == IntPtr.Zero)
                            continue;
                        
                        FTrajectoryWeaponData TrajectoryConfig = Weapon.TrajectoryConfig;
                        FWeaponGunData WeaponGunConfig = Weapon.WeaponGunConfig;
                        FWeaponData WeaponConfig = Weapon.WeaponConfig;
                        FRecoilInfo RecoilInfo = Weapon.RecoilInfo;
                        FWeaponGunAnim WeaponGunAnim = Weapon.WeaponGunAnim;
                        FWeaponDeviationData WeaponDeviationConfig = Weapon.WeaponDeviationConfig;

                        if (CheatSettings.InfiniteAmmo)
                            Weapon.SetAmmoInClip(999);
                        
                        if (CheatSettings.MagicBullets)
                            WeaponGunConfig.TimeBetweenShots = 0.03f;

                        if (CheatSettings.FullAuto)
                            Weapon.SetFiringMode(0, EFiringMode.FullAuto);

                        if (CheatSettings.NoMuzzle)
                        {
                            WeaponGunAnim.CharacterFire = IntPtr.Zero;
                        }

                        if (CheatSettings.NoSway)
                        {
                            WeaponConfig.SwayModifier_Crouch = 0;
                            WeaponConfig.SwayModifier_Movement = 0;
                            WeaponConfig.SwayModifier_Pitch = 0;
                            WeaponConfig.SwayModifier_Prone = 0;
                            WeaponConfig.SwayModifier_Stand = 0;
                            WeaponConfig.SwayModifier_YawOffset = 0;
                        }

                        if (CheatSettings.InstantHit)
                        {
                            TrajectoryConfig.InitialSpeed = float.MaxValue;
                            Weapon.SetBulletGravity(0f);
                        }

                        if (CheatSettings.NoRecoil)
                        {
                            // TRAJECTORY CONFIG
                            TrajectoryConfig.RecoilPatternScale = 0;
                            TrajectoryConfig.RecoilRecoverySpeed = 0;
                            TrajectoryConfig.RecoilSpeed = 0;

                            // RECOIL INFO
                            RecoilInfo.VerticalRecoilMin = 0;
                            RecoilInfo.VerticalRecoilMax = 0;
                            RecoilInfo.RecoilValue_Climb = 0;
                            RecoilInfo.RecoilValue_Fall = 0;
                            RecoilInfo.RecoilModifier_Stand = 0;
                            RecoilInfo.RecoilModifier_Crouch = 0;
                            RecoilInfo.RecoilModifier_Prone = 0;
                            RecoilInfo.RecoilSpeed_Horizontal = 0;
                            RecoilInfo.RecoilSpeed_Vertical = 0;
                            RecoilInfo.RecoverySpeed_Vertical = 0;
                            RecoilInfo.VerticalRecoveryModifier = 0;

                            // WEAPON GUN ANIM
                            WeaponGunAnim.ShotCameraShake = IntPtr.Zero;
                            WeaponGunAnim.ShotCameraShakeADS = IntPtr.Zero;
                            WeaponGunAnim.ShotCameraShakeIronsight = IntPtr.Zero;
                        }

                        if (CheatSettings.NoSpread)
                        {
                            // TRAJECTORY CONFIG
                            TrajectoryConfig.WeaponSpread = 0;
                            TrajectoryConfig.AimingSpreadModifier = 0;
                            TrajectoryConfig.FiringSpreadBase = 0;
                            TrajectoryConfig.ProneRecoveryTime = 0;
                            TrajectoryConfig.ScopingSpreadModifier = 0;
                            
                            // WEAPON GUN CONFIG
                            WeaponGunConfig.FiringBulletsSpread = 0;

                            // WEAPON DEVIATION CONFIG
                            WeaponDeviationConfig.DeviationBase = 0;
                            WeaponDeviationConfig.DeviationBaseADS = 0;
                            WeaponDeviationConfig.DeviationBaseAim = 0;
                            WeaponDeviationConfig.DeviationMax = 0;
                            WeaponDeviationConfig.DeviationMaxMove = 0;
                            WeaponDeviationConfig.DeviationMinMove = 0;
                            WeaponDeviationConfig.DeviationMoveMaxReferenceVelocity = 0;
                            WeaponDeviationConfig.DeviationMoveMinReferenceVelocity = 0;
                            WeaponDeviationConfig.DeviationMoveMultiplier = 0;
                            WeaponDeviationConfig.DeviationRecoilGain = 0;
                            WeaponDeviationConfig.DeviationRecoilGainADS = 0;
                            WeaponDeviationConfig.DeviationRecoilGainAim = 0;
                            WeaponDeviationConfig.DeviationStanceCrouch = 0;
                            WeaponDeviationConfig.DeviationStanceJump = 0;
                            WeaponDeviationConfig.DeviationStanceProne = 0;
                            WeaponDeviationConfig.DeviationStanceStand = 0;

                            // ATslWeapon
                            Weapon.SetRecoilSpreadScale(0f);
                            Weapon.SetRunSpread(0f);
                            Weapon.SetWalkSpread(0f);
                            Weapon.SetJumpSpread(0f);
                        }

                        // WRITE WEAPON INFORMATION
                        Weapon.WriteStruct(RecoilInfo);
                        Weapon.WriteStruct(TrajectoryConfig);
                        Weapon.WriteStruct(WeaponGunConfig);
                        Weapon.WriteStruct(WeaponGunAnim);
                        Weapon.WriteStruct(WeaponDeviationConfig);
                    }
                }
                #endregion

                #region Mass TP
                if (CheatSettings.MassTeleport)
                {
                    var location = localplayer.RootComponent.RelativeLocation;

                    location.X += 100;
                    location.Y += 100;
                    location.Z += 50;

                    foreach (var player in G.Players.ToList())
                        if ((location - player.Location).Length <= 10000)
                            USceneComponent.SetLocation(player.pRootComponent, location);
                }
                #endregion

                #region Miscelanneous
                if (CheatSettings.Flying)
                    M.Write<byte>((byte)EMovementMode.MOVE_Flying, localplayer.pCharacterMovement + 0x01B4); // MovementMode
                #endregion

                #region Aimbot
                if (CheatSettings.Aimbot)
                {
                    var myplayercontroller = G.OwningGameInstance.LocalPlayer.PlayerController;
                    var vecLocalEyeLocation = myplayercontroller.PlayerCameraManager.CameraCache.POV.Location;
                    var angLocalAngles = myplayercontroller.ControlRotation;

                    float flFov = 90f;
                    FRotator flBestAngDelta = new FRotator();

                    bool bFoundTarget = false;

                    foreach (var Player in G.Players.ToList())
                    {
                        var vecTargetCenterOfMass = Player.Location;

                        switch (Player.CharacterMovement.Stance)
                        {
                            case EStanceMode.STANCE_Stand:
                                vecTargetCenterOfMass.Z += 30;
                                break;

                            case EStanceMode.STANCE_Crouch:
                                vecTargetCenterOfMass.Z += 10;
                                break;

                            case EStanceMode.STANCE_Prone:
                                vecTargetCenterOfMass.Z -= 15;
                                break;
                        }


                        var delta = vecTargetCenterOfMass - vecLocalEyeLocation;
                        var angDelta = (delta.ToFRotator() - angLocalAngles).Clamp();

                        if (angDelta.Length <= flFov)
                        {
                            flFov = (float)angDelta.Length;
                            flBestAngDelta = angDelta;
                            bFoundTarget = true;
                        }
                    }

                    // TODO: Keyboard Hook
                    if (bFoundTarget && (Win32.GetAsyncKeyState(Keys.XButton1) & 0x8000) != 0) // ADD HOLD KEY OR WHATEVER
                        M.Write<FRotator>(angLocalAngles + flBestAngDelta, G.OwningGameInstance.LocalPlayer.pPlayerController + 0x03B8);
                }
                #endregion

                // Increase for speed, decrease for performance.
                Thread.Sleep(5);
            }
        }

        private static class Win32
        {
            [DllImport("user32.dll")]
            public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        }
    }
}
