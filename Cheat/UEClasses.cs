using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BATTLEGROUNDS_EXERNAL
{
    #region Enums
    enum EMovementMode : byte
    {
        MOVE_None                      = 0,
	    MOVE_Walking                   = 1,
	    MOVE_NavWalking                = 2,
	    MOVE_Falling                   = 3,
	    MOVE_Swimming                  = 4,
	    MOVE_Flying                    = 5,
	    MOVE_Custom                    = 6,
	    MOVE_MAX                       = 7
    }
    public enum EFiringMode : byte
    {
        Normal                         = 0,
	    Burst                          = 1,
	    FullAuto                       = 2,
	    EFiringMode_MAX                = 3
    }
    public enum EWeaponReloadMethod : byte
    {
        Magazine                       = 0,
	    OneByOne                       = 1,
	    OneByOneAndClip                = 2,
	    EWeaponReloadMethod_MAX        = 3
    }
    public enum EWeaponGripLeftHand : byte
    {
        EWeaponGripLeftHand__NormalRifle = 0,
	    EWeaponGripLeftHand__Foregrip1 = 1,
	    EWeaponGripLeftHand__Foregrip2 = 2,
	    EWeaponGripLeftHand__Thompson  = 3,
	    EWeaponGripLeftHand__EWeaponGripLeftHand_MAX = 4
    }
    public enum EWeaponClass : byte
    {
        Class_Pistol                   = 0,
	    Class_SMG                      = 1,
	    Class_Rifle                    = 2,
	    Class_Carbine                  = 3,
	    Class_Shotgun                  = 4,
	    Class_Sniper                   = 5,
	    Class_DMR                      = 6,
	    Class_LMG                      = 7,
	    Class_Melee                    = 8,
	    Class_Throwable                = 9,
	    Class_MAX                      = 10
    }
    public enum EThrownWeaponType : byte
    {
        Thrown_Grenade                 = 0,
	    Thrown_Molotov                 = 1,
	    Thrown_Other                   = 2,
	    Thrown_MAX                     = 3
    }
    public enum EStanceMode : byte
    {
        STANCE_None = 0,
        STANCE_Stand = 1,
        STANCE_Crouch = 2,
        STANCE_Prone = 3,
        STANCE_MAX = 4
    }
    #endregion
    #region Structs
    [StructLayout(LayoutKind.Explicit)]
    public struct UWorld
    {
        [FieldOffset(0x30)]
        public IntPtr pPersistentLevel;

        [FieldOffset(0x58)]
        public IntPtr pNetworkManager;

        [FieldOffset(0x140)]
        public IntPtr pOwningGameInstance;

        public AGameNetworkManager NetworkManager
        {
            get
            {
                return M.Read<AGameNetworkManager>(this.pNetworkManager);
            }
        }

    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AGameNetworkManager
    {
        private fixed byte pad0[0x0388];
        public int AdjustedNetSpeed;                // 0x0388(0x0004) (CPF_ZeroCo
        public float LastNetSpeedUpdateTime;            // 0x038C(0x0004) (CPF_ZeroCo
        public int TotalNetBandwidth;               // 0x0390(0x0004) (CPF_ZeroCo
        public int MinDynamicBandwidth;             // 0x0394(0x0004) (CPF_ZeroCo
        public int MaxDynamicBandwidth;                 // 0x0398(0x0004) (CPF_ZeroCo
        public byte bIsStandbyCheckingEnabled;   // 0x039C(0x0001) (CPF_Config
        public byte bHasStandbyCheatTriggered;   // 0x039C(0x0001)
        fixed byte UnknownData00[0x3];          // 0x039D(0x0003) MISSED OFFS
        public float StandbyRxCheatTime;                                       // 0x03A0(0x0004) (CPF_ZeroCo
        public float StandbyTxCheatTime;                                       // 0x03A4(0x0004) (CPF_ZeroCo
        public int BadPingThreshold;                                       // 0x03A8(0x0004) (CPF_ZeroCo
        public float PercentMissingForRxStandby;                               // 0x03AC(0x0004) (CPF_ZeroCo
        public float PercentMissingForTxStandby;                               // 0x03B0(0x0004) (CPF_ZeroCo
        public float PercentForBadPing;                                        // 0x03B4(0x0004) (CPF_ZeroCo
        public float JoinInProgressStandbyWaitTime;                            // 0x03B8(0x0004) (CPF_ZeroCo
        public float MoveRepSize;                                              // 0x03BC(0x0004) (CPF_ZeroCo
        public float MAXPOSITIONERRORSQUARED;                                  // 0x03C0(0x0004) (CPF_ZeroCo
        public float MAXNEARZEROVELOCITYSQUARED;                               // 0x03C4(0x0004) (CPF_ZeroCo
        public float CLIENTADJUSTUPDATECOST;                                   // 0x03C8(0x0004) (CPF_ZeroCo
        public float MAXCLIENTUPDATEINTERVAL;                                  // 0x03CC(0x0004) (CPF_ZeroCo
        public float MaxMoveDeltaTime;                                         // 0x03D0(0x0004) (CPF_ZeroCo
        public byte ClientAuthorativePosition;                         // 0x03D4(0x0001) (CPF_ZeroCo
        fixed byte UnknownData01[0x3];                                    // 0x03D5(0x0003) MISSED OFFS
        public float ClientErrorUpdateRateLimit;                               // 0x03D8(0x0004) (CPF_ZeroCo
        public byte bMovementTimeDiscrepancyDetection;                 // 0x03DC(0x0001) (CPF_ZeroCo
        public byte bMovementTimeDiscrepancyResolution;                // 0x03DD(0x0001) (CPF_ZeroCo
        fixed byte UnknownData02[0x2];                                  // 0x03DE(0x0002) MISSED OFFS
        public float MovementTimeDiscrepancyMaxTimeMargin;                     // 0x03E0(0x0004) (CPF_ZeroCo
        public float MovementTimeDiscrepancyMinTimeMargin;                     // 0x03E4(0x0004) (CPF_ZeroCo
        public float MovementTimeDiscrepancyResolutionRate;                    // 0x03E8(0x0004) (CPF_ZeroCo
        public float MovementTimeDiscrepancyDriftAllowance;                    // 0x03EC(0x0004) (CPF_ZeroCo
        public byte bMovementTimeDiscrepancyForceCorrectionsDuringResolution;  // 0x03F0(0x0001)
        public byte bUseDistanceBasedRelevancy;                   		// 0x03F1(0x0001) (CPF_ZeroCo

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct ULevel
    {
        [FieldOffset(0xA0)]
        public TArray<AActor> AActors;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct AActor
    {
        [FieldOffset(0x0)]
        public IntPtr BasePointer;

        [FieldOffset(0x18)]
        public int Id;
        
        [FieldOffset(0x150)]
        public IntPtr pPawn;

        [FieldOffset(0x168)]
        public IntPtr pRootComponent;

        [FieldOffset(0x0394)]
        public float BaseEyeHeight;

        [FieldOffset(0x2C0)]
        public TArray<pADroppedItem> DroppedItemArray;
        
        [FieldOffset(0x03A8)]
        public IntPtr pPlayerState;
        
        [FieldOffset(0x03F0)]
        public IntPtr pCharacterMovement;

        [FieldOffset(0x04A8)]
        public float CrouchedEyeHeight;
        
        [FieldOffset(0x0938)]
        public IntPtr pWeaponProcessor;

        [FieldOffset(0x09F0)]
        public byte CharacterState;
        
        [FieldOffset(0x0A50)]
        public float Punch_Damage;

        [FieldOffset(0x0A54)]
        public float HeavyPunch_Damage;

        [FieldOffset(0x0AC4)]
        public float Sprint_MaxSpeed;

        [FieldOffset(0x0B90)]
        public float ReleasingParachuteAltitude;

        [FieldOffset(0x0B94)]
        public float ForceReleasingParachuteAltitude;

        [FieldOffset(0x107C)]
        public float Health;

        [FieldOffset(0x1080)]
        public float HealthMax;
        
        [FieldOffset(0x0D9C)]
        public float TargetingSpeedModifier;

        [FieldOffset(0x0DA8)]
        public float Stand_SprintingSpeedModifier;

        [FieldOffset(0x0DAC)]
        public float Stand_SprintingBigGunModifier;

        [FieldOffset(0x0DB0)]
        public float Stand_SprintingRifleModifier;
        
        [FieldOffset(0x0DB4)]
        public float Stand_SprintingSmallGunMOdifier;
        
        [FieldOffset(0x0DB8)]
        public float Crouch_RunningSpeedModifier;
        
        [FieldOffset(0x0DBC)]
        public float Crouch_SprintingSpeedModifier;

        [FieldOffset(0x0DC0)]
        public float Prone_RunningSpeedModifier;
        
        [FieldOffset(0x0DC4)]
        public float Prone_SprintingSpeedModifier;
        
        public bool IsPlayer
        {
            get
            {
                return this.Id >= 60700 && this.Id <= 60800;
            }
        }
        public bool IsDroppedItemInteractionComponent
        {
            get
            {
                return this.Id == 6854;
            }
        }
        public bool IsDroppedItemGroup
        {
            get
            {
                return this.Id == 6834;
            }
        }
        public bool IsVehicle
        {
            get
            {
                switch (this.Id)
                {
                    /*UAZ*/
                    case 76152:
                    case 76156:
                    case 76166:
                    case 76165:
                    case 76169:
                        return true;

                    /*Dacia*/
                    case 75657:
                    case 75653:
                    case 75665:
                    case 75679:
                    case 75675:
                        return true;


                    /*Buggy*/
                    case 75371:
                    case 75375:
                    case 75379:
                    case 75401:
                        return true;


                    /*Jeep*/
                    case 76157:
                        return true;

                    /*Boat*/
                    case 75301:
                        return true;

                    /*Motorbike*/
                    case 75820:
                        return true;
                        
                default:
                        return false;
                }

                //return this.Id >= 75000 && this.Id <= 77000;
            }
        }
        public bool IsAlive
        {
            get
            {
                return this.Health > 0;
            }
        }
        public USceneComponent RootComponent
        {
            get
            {
                return M.Read<USceneComponent>(this.pRootComponent);
            }
        }
        public UCharacterMovementComponent CharacterMovement
        {
            get
            {
                return M.Read<UCharacterMovementComponent>(this.pCharacterMovement);
            }
        }
        public AWeaponProcessor WeaponProcessor
        {
            get
            {
                return M.Read<AWeaponProcessor>(this.pWeaponProcessor);
            }
        }

        public Vector3 Location
        {
            get
            {
                return this.RootComponent.RelativeLocation;
            }
        }
        public Vector3 EyeLocation
        {
            get
            {
                var location = this.Location;

                location.Z += this.BaseEyeHeight;

                return location;
            }
        }

        public static IntPtr g_pLocalPlayer = IntPtr.Zero;
        public static AActor GetLocalPlayer()
        {
            if (g_pLocalPlayer != IntPtr.Zero)
            {
                var localplayer = M.Read<AActor>(g_pLocalPlayer);
                localplayer.BasePointer = g_pLocalPlayer;
                return localplayer;
            }

            g_pLocalPlayer = G.OwningGameInstance.LocalPlayer.PlayerController.pLocalPlayer;
            return M.Read<AActor>(g_pLocalPlayer);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct AWeaponProcessor
    {
        [FieldOffset(0x0398)]
        public TArray<ATslWeapon> EquippedWeapons;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct pADroppedItem
    {
        public IntPtr pActor;
        fixed byte pad0[0x8];
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FText
    {
        fixed byte pad0[0x28];
        public FString fstring;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FString
    {
        fixed sbyte arrName[64];

        public override string ToString()
        {
            fixed (sbyte* pName = arrName)
                return new string(pName);
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct ATslWeapon
    {
        [FieldOffset(0x0)]
        public IntPtr BasePointer;

        [FieldOffset(0x0518)]
        public FWeaponData WeaponConfig;

        [FieldOffset(0x0780)]
        public int AmmoPerClip;

        [FieldOffset(0x0784)]
        public int CurrentAmmoInClip;

        [FieldOffset(0x07F8)]
        public FWeaponGunData WeaponGunConfig;

        [FieldOffset(0x08B8)]
        public FWeaponGunAnim WeaponGunAnim;

        [FieldOffset(0x0990)]
        public float TrajectoryGravityZ; 

        [FieldOffset(0x0994)]
        public float RecoilSpreadScale;                                		

        [FieldOffset(0x0998)]
        public byte FireAtViewPoint; 

        [FieldOffset(0x099C)]
        public float DefaultTimerFrequency;                            		

        [FieldOffset(0x09A0)]
        public float CrouchSpreadModifier;                             		

        [FieldOffset(0x09A4)]
        public float ProneSpreadModifier;                              		

        [FieldOffset(0x09A8)]
        public float WalkSpread;                                       		

        [FieldOffset(0x09AC)]
        public float RunSpread;                                        		

        [FieldOffset(0x09B0)]
        public float JumpSpread;

        [FieldOffset(0x0878)]
        public FWeaponDeviationData WeaponDeviationConfig;

        [FieldOffset(0x09B8)]
        public FTrajectoryWeaponData TrajectoryConfig;

        [FieldOffset(0x0A18)]
        public FRecoilInfo RecoilInfo;
        
        #region ATslWeapon_Melee Variables
        [FieldOffset(0x0790)]
        public float Melee_Damage;

        [FieldOffset(0x0794)]
        public float Melee_WeaponImpact;

        [FieldOffset(0x07A4)]
        public float Melee_AllowedHitRangeLeeway;
        #endregion

        public void SetAmmoInClip(int nAmmo)
        {
            M.Write<int>(nAmmo, this.BasePointer + 0x784/*CurrentAmmoInClip*/);
        }
        public void SetBulletGravity(float flTrajectoryGravityZ)
        {
            M.Write<float>(flTrajectoryGravityZ, this.BasePointer + 0x0990/*TrajectoryGravityZ*/);
        }

        public void SetRecoilSpreadScale(float flRecoilSpreadScale)
        {
            M.Write<float>(flRecoilSpreadScale, this.BasePointer + 0x0994/*RecoilSpreadScale*/);
        }

        public void SetWalkSpread(float flWalkSpread)
        {
            M.Write<float>(flWalkSpread, this.BasePointer + 0x09A8/*WalkSpread*/);
        }
        public void SetRunSpread(float flRunSpread)
        {
            M.Write<float>(flRunSpread, this.BasePointer + 0x09AC/*RunSpread*/);
        }
        public void SetJumpSpread(float flJumpSpread)
        {
            M.Write<float>(flJumpSpread, this.BasePointer + 0x09B0/*JumpSpread*/);
        }

        public void SetFiringMode(int nIndex, EFiringMode FiringMode) => this.WeaponGunConfig.FiringModes.SetValue((byte)FiringMode, nIndex, false);

        public void WriteStruct(FRecoilInfo RecoilInfo)
        {
            M.Write(RecoilInfo, BasePointer + 0xA18);
        }
        public void WriteStruct(FTrajectoryWeaponData TrajectoryConfig)
        {
            M.Write(TrajectoryConfig, BasePointer + 0x9B8);
        }
        public void WriteStruct(FWeaponGunData WeaponGunData)
        {
            M.Write(WeaponGunData, BasePointer + 0x07F8);
        }
        public void WriteStruct(FWeaponData WeaponConfig)
        {
            M.Write(WeaponConfig, BasePointer + 0x0518);
        }
        public void WriteStruct(FWeaponGunAnim WeaponGunAnim)
        {
            M.Write(WeaponGunAnim, BasePointer + 0x08B8);
        }
        public void WriteStruct(FWeaponDeviationData WeaponDeviationConfig)
        {
            M.Write(WeaponDeviationConfig, BasePointer + 0x0878);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FWeaponData
    {
        public float TargetingFOV;
        public float HoldBreathFOV;
        public IntPtr Rarity;
        public Vector3 SocketOffset_Shoulder;                            		
        Vector3 SocketOffset_Hand;                                		
        byte bApplyGripPoseLeft;
        EWeaponGripLeftHand WeaponGripLeft;                                   		
        EWeaponClass WeaponClass;                                      		
        byte bUseDefaultScoreMultiplier;
        public float ScoreMultiplierByDamage;
        public float ScoreMultiplierByKill;
        public float SwayModifier_Pitch;
        public float SwayModifier_YawOffset;
        public float SwayModifier_Movement;
        public float SwayModifier_Stand;
        public float SwayModifier_Crouch;
        public float SwayModifier_Prone;
        public float CameraDOF_Range;
        public float CameraDOF_NearRange;
        public float CameraDOF_Power;
        public byte bUseDynamicReverbAK;
        private fixed byte UnknownData00[0x3];
        public float CurrentWeaponZero;
        public float MinWeaponZero;
        public float MaxWeaponZero;
        public float AnimationKick;
        private fixed byte UnknownData01[0x4];
        public IntPtr RecoilMontage;
        public byte DestructibleDoor;
        public EThrownWeaponType ThrownType;
        private fixed byte UnknownData02[0x2];
        public float WeaponEquipDuration;
        public float WeaponReadyDuration;
        public byte bForceFireAfterEquip;
        private fixed byte UnknownData03[0x3];
        public float PhysicalBodyHitImpactPower;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct FWeaponDeviationData
    {
        public float DeviationBase;
        public float DeviationBaseAim;
        public float DeviationBaseADS;
        public float DeviationRecoilGain;
        public float DeviationRecoilGainAim;
        public float DeviationRecoilGainADS;
        public float DeviationMax;
        public float DeviationMinMove;
        public float DeviationMaxMove;
        public float DeviationMoveMultiplier;
        public float DeviationMoveMinReferenceVelocity;
        public float DeviationMoveMaxReferenceVelocity;
        public float DeviationStanceStand;
        public float DeviationStanceCrouch;
        public float DeviationStanceProne;
        public float DeviationStanceJump;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FWeaponGunAnim
    {
        public IntPtr Fire;                         // 0x0000
        public byte bLoopedFire;                    // 0x0008
        private fixed byte UnknownData00[0x7];      // 0x0009
        public IntPtr Reload;                       // 0x0010
        public IntPtr CharacterGripBlendspace;      // 0x0018
        public IntPtr CharacterLHGripBlendspace;    // 0x0020
        public IntPtr CharacterFire;                // 0x0028
        public IntPtr CharacterFireCycle;           // 0x0030
        public IntPtr CharacterFireSelector;        // 0x0038
        public IntPtr CharacterReloadTactical;      // 0x0040
        public IntPtr CharacterReloadCharge;        // 0x0048
        public IntPtr CharacterReloadByOneStart;    // 0x0050
        public IntPtr CharacterReloadByOneStop;     // 0x0058
        public IntPtr CharacterReloadByOneSingle;   // 0x0060
        public IntPtr WeaponReloadTactical;         // 0x0068
        public IntPtr WeaponReloadCharge;           // 0x0070
        public float ReloadDurationTactical;        // 0x0078
        public float ReloadDurationCharge;          // 0x007C
        public float ReloadDurationStart;           // 0x0080
        public float ReloadDurationLoop;            // 0x0084
        public float ReloadDurationMagOut;          // 0x0088
        public float FireCycleDelay;                // 0x008C
        public float FireCycleDuration;             // 0x0090
        public byte bCycleAfterLastShot;            // 0x0094
        public byte bCycleDuringReload;             // 0x0095
        private fixed byte UnknownData01[0x2];      // 0x0096
        public IntPtr ShotCameraShake;              // 0x0098
        public IntPtr ShotCameraShakeIronsight;     // 0x00A0
        public IntPtr ShotCameraShakeADS;           // 0x00A8
        public IntPtr CycleCameraAnim;              // 0x00B0
        public float RecoilKickADS;                 // 0x00B8
        public Vector3 MagDropLinearVelocity;       // 0x00BC
        public Vector3 MagDropAngularVelocity;      // 0x00C8
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FWeaponGunData
    {
        public IntPtr AmmoItemClass;                    // 0x0000(0x0008) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public IntPtr TracerClass;                      // 0x0008(0x0008) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public int AmmoPerClip;                         // 0x0010(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float TimeBetweenShots;                  // 0x0014(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float NoAnimReloadDuration;              // 0x0018(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        private fixed byte UnknownData00[0x4];          // 0x001C(0x0004) MISSED OFFSET
        public TArray<byte> FiringModes;         // 0x0020(0x0010) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance)
        public int BurstShots;                          // 0x0030(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float BurstDelay;                        // 0x0034(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public int BulletPerFiring;                     // 0x0038(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_BlueprintReadOnly, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float FiringBulletsSpread;               // 0x003C(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public byte bIsArrowProjectile;                 // 0x0040(0x0001) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public byte bRotationFromBarrelWhenScoped;      // 0x0041(0x0001) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public EWeaponReloadMethod ReloadMethod;        // 0x0042(0x0001) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public byte UnknownData01;                      // 0x0043(0x0001) MISSED OFFSET
        public float ReloadDurationByOneInitial;        // 0x0044(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float ReloadDurationByOneLoop;           // 0x0048(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float MovementModifierAim;               // 0x004C(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float MovementModifierScope;             // 0x0050(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float WeaponLength;                      // 0x0054(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float ShoulderLength;                    // 0x0058(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float WeaponSuppressorLength;            // 0x005C(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float TraceRadius;                       // 0x0060(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public float TraceAdditiveZ;                    // 0x0064(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public byte DebugWeaponCollision;               // 0x0068(0x0001) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        private fixed byte UnknownData02[0x3];          // 0x0069(0x0003) MISSED OFFSET
        public Vector3 HandWeaponOffset;                // 0x006C(0x000C) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        public byte bManualCycleAfterShot;              // 0x0078(0x0001) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
        private fixed byte UnknownData03[0x3];          // 0x0079(0x0003) MISSED OFFSET
        public float LongTailDelay;                     // 0x007C(0x0004) (CPF_Edit, CPF_ZeroConstructor, CPF_DisableEditOnInstance, CPF_IsPlainOldData)
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct FTrajectoryWeaponData
    {
        public float WeaponSpread;
        public float AimingSpreadModifier;
        public float ScopingSpreadModifier;
        public float FiringSpreadBase;
        public float StandRecoveryTime;
        public float CrouchRecoveryTime;
        public float ProneRecoveryTime;
        public float RecoveryInterval;
        public float RecoilSpeed;
        public float RecoilRecoverySpeed;
        public float RecoilPatternScale;
        public float InitialSpeed;
        public int HitDamage;
        public float RangeModifier;
        public float ReferenceDistance;
        public float TravelDistanceMax;
        public byte IsPenetrable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FRecoilInfo
    {
        //[FieldOffset(0x50)]
        //public IntPtr BasePointer;

        public float VerticalRecoilMin;
        public float VerticalRecoilMax;
        public float VerticalRecoilVariation;
        public float VerticalRecoveryModifier;
        public float VerticalRecoveryClamp;
        public float VerticalRecoveryMax;
        public float LeftMax;
        public float RightMax;
        public float HorizontalTendency;
        private fixed byte UnknownData00[0x4];
        public IntPtr RecoilCurve;
        public int BulletsPerSwitch;
        public float TimePerSwitch;
        public byte bSwitchOnTime;
        private fixed byte UnknownData01[0x3];
        public float RecoilSpeed_Vertical;
        public float RecoilSpeed_Horizontal;
        public float RecoverySpeed_Vertical;
        public float RecoilValue_Climb;
        public float RecoilValue_Fall;
        public float RecoilModifier_Stand;
        public float RecoilModifier_Crouch;
        public float RecoilModifier_Prone;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UTslSettings
    {
        [FieldOffset(0x28)]
        public float RepDistance_Item;

        [FieldOffset(0x2C)]
        public float RepDistance_ItemGroup;

        [FieldOffset(0x30)]
        public float RepDistance_Character;

        [FieldOffset(0x34)]
        public float RepDistance_Weapon;

        [FieldOffset(0x38)]
        public float RepDistance_Vehicle;

        [FieldOffset(0x3C)]
        public float RepDistance_Parachute;

        [FieldOffset(0x3C)]
        public float RepDistance_Door;

        [FieldOffset(0x40)]
        public float RepDistance_Window;

        [FieldOffset(0x44)]
        public float RepFrequency_Character;

        [FieldOffset(0x48)]
        public float RepFrequency_WheeledVehicle;

        [FieldOffset(0x4C)]
        public float RepFrequency_FloatingVehicle;

        [FieldOffset(0x50)]
        public float RepFrequency_Parachute;

        [FieldOffset(0x54)]
        public float RepFrequency_Aircraft;

        [FieldOffset(0x58)]
        public float RepFrequency_CarePackage;

        [FieldOffset(0x8C)]
        public bool bBattlEyeEnabled;

        [FieldOffset(0x8D)]
        public bool bBattlEyeEnabledInPIE;

        [FieldOffset(0x90)]
        public float BattlEyeReliablePacketIntervalOnClient;

        [FieldOffset(0x94)]
        public float BattlEyeReliablePacketIntervalOnServer;

        [FieldOffset(0x9C)]
        public float GameStateLogInterval;

        [FieldOffset(0xA0)]
        public float ServerStatLogInterval;

        [FieldOffset(0xA4)]
        public float CharacterPositionLogInterval;

        [FieldOffset(0xA8)]
        public float DestructibleComponentMaxDrawDistance;

        [FieldOffset(0xDF)]
        public float StoppedVehicleSpeedThreshold;

        [FieldOffset(0xF0)]
        public float LastDriverDuration;

        [FieldOffset(0xF4)]
        public float InteractableDistanceToleranceOnDedicatedServer;

        [FieldOffset(0xF8)]
        public float InteractableDistance_ItemDefault;

        [FieldOffset(0x128)]
        public bool bPreventFinishMatchInPIE;
        
        [FieldOffset(0x129)]
        public bool bEnableInitialItemDonator;

        [FieldOffset(0x12C)]
        public float InventoryMaxSpaceDefault;

        [FieldOffset(0x15C)]
        public float GameTimeMultiplier;

        [FieldOffset(0x160)]
        public byte AimOffsetRayCast;

        [FieldOffset(0x288)]
        public float ClientSideHitLeeway;

        [FieldOffset(0x290)]
        public float ClientSideOriginDistanceLeeway;

        [FieldOffset(0x294)]
        public float TravelDistanceLeeway;

        [FieldOffset(0x218)]
        public bool bUseForceItemActorSpawn;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UGameInstance
    {
        [FieldOffset(0x38)]
        public IntPtr pULocalPlayer; // TArray<class ULocalPlayer*>, deref twice

        public ULocalPlayer LocalPlayer
        {
            get
            {
                IntPtr pLocalPlayer = M.Read<IntPtr>(this.pULocalPlayer);
                var result = M.Read<ULocalPlayer>(pLocalPlayer);
                result.pBase = pLocalPlayer;
                return result;
            }
        }
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct ULocalPlayer
    {
        [FieldOffset(0x00)]
        public IntPtr pBase;

        [FieldOffset(0x30)]
        public IntPtr pPlayerController;

        [FieldOffset(0x58)]
        public IntPtr pViewportClient;

        [FieldOffset(0x70)]
        public Vector3 Location;
        
        public void SetLocation(Vector3 vecLocation)
        {
            M.Write<Vector3>(vecLocation, pBase + 0x70/*Location*/);
        }

        public APlayerController PlayerController
        {
            get
            {
                return M.Read<APlayerController>(this.pPlayerController);
            }
        }
        public UGameViewportClient ViewportClient
        {
            get
            {
                return M.Read<UGameViewportClient>(this.pViewportClient);
            }
        }

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct UGameViewportClient
    {
        [FieldOffset(0x80)]
        public IntPtr pUWorld;

    }
    [StructLayout(LayoutKind.Explicit)]
    public struct APlayerController
    {
        [FieldOffset(0x0390)]
        public IntPtr pLocalPlayer;

        [FieldOffset(0x03B8)]
        public FRotator ControlRotation;

        [FieldOffset(0x0420)]
        public IntPtr pPlayerCameraManager;

        public APlayerCameraManager PlayerCameraManager
        {
            get
            {
                return M.Read<APlayerCameraManager>(pPlayerCameraManager);
            }
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct APlayerCameraManager
    {
        [FieldOffset(0x3B8)]
        public float DefaultFOV;

        [FieldOffset(0x3C0)]
        public float DefaultOrthoWidth;
        
        [FieldOffset(0x3C8)]
        public float DefaultAspectRatio;

        [FieldOffset(0x410)]
        public FCameraCacheEntry CameraCache;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FCameraCacheEntry
    {
        [FieldOffset(0x10)]
        public FMinimalViewInfo POV;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FMinimalViewInfo
    {
        public Vector3 Location;               // 0x0000(0x000C) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
	    public FRotator Rotation;              // 0x000C(0x000C) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
	    public float FOV;                      // 0x0018(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public float OrthoWidth;               // 0x001C(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public float OrthoNearClipPlane;       // 0x0020(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public float OrthoFarClipPlane;        // 0x0024(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public float AspectRatio;              // 0x0028(0x0004) (CPF_Edit, CPF_BlueprintVisible, CPF_ZeroConstructor, CPF_IsPlainOldData)
        public byte bConstrainAspectRatio;     // 0x002C(0x0001) (CPF_Edit, CPF_BlueprintVisible)
        public byte bUseFieldOfViewForLOD;     // 0x002C(0x0001) (CPF_Edit, CPF_BlueprintVisible)
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct FRotator
    {
        public float Pitch;
        public float Yaw;  
        public float Roll;

        public FRotator(float flPitch, float flYaw, float flRoll)
        {
            Pitch = flPitch;
            Yaw = flYaw;
            Roll = flRoll;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.Pitch * this.Pitch + this.Yaw * this.Yaw + this.Roll * this.Roll);
            }
        }

        public FRotator Clamp()
        {
            var result = this;

            if (result.Pitch > 180)
                result.Pitch -= 360;

            else if (result.Pitch < -180)
                result.Pitch += 360;

            if (result.Yaw > 180)
                result.Yaw -= 360;

            else if (result.Yaw < -180)
                result.Yaw += 360;

            if (result.Pitch < -89)
                result.Pitch = -89;

            if (result.Pitch > 89)
                result.Pitch = 89;

            while (result.Yaw < -180.0f)
                result.Yaw += 360.0f;

            while (result.Yaw > 180.0f)
                result.Yaw -= 360.0f;

            result.Roll = 0;

            return result;
        }

        public void GetAxes(out Vector3 x, out Vector3 y, out Vector3 z)
        {
            var m = ToMatrix();

            x = new Vector3(m.M11, m.M12, m.M13);
            y = new Vector3(m.M21, m.M22, m.M23);
            z = new Vector3(m.M31, m.M32, m.M33);
        }

        public Vector3 ToVector()
        {
            float radPitch = (float)(this.Pitch * Math.PI / 180f);
            float radYaw = (float)(this.Yaw * Math.PI / 180f);

            float SP = (float)Math.Sin(radPitch);
            float CP = (float)Math.Cos(radPitch);
            float SY = (float)Math.Sin(radYaw);
            float CY = (float)Math.Cos(radYaw);
            
            return new Vector3(CP * CY, CP * SY, SP);
        }

        public SharpDX.Matrix ToMatrix(Vector3 origin = default(Vector3))
        {

            float radPitch = (float)(this.Pitch * Math.PI / 180f);
            float radYaw = (float)(this.Yaw * Math.PI / 180f);
            float radRoll = (float)(this.Roll * Math.PI / 180f);

            float SP = (float)Math.Sin(radPitch);
            float CP = (float)Math.Cos(radPitch);
            float SY = (float)Math.Sin(radYaw);
            float CY = (float)Math.Cos(radYaw);
            float SR = (float)Math.Sin(radRoll);
            float CR = (float)Math.Cos(radRoll);

            SharpDX.Matrix m = new SharpDX.Matrix();
            m[0, 0] = CP * CY;
            m[0, 1] = CP * SY;
            m[0, 2] = SP;
            m[0, 3] = 0f;

            m[1, 0] = SR * SP * CY - CR * SY;
            m[1, 1] = SR * SP * SY + CR * CY;
            m[1, 2] = -SR * CP;
            m[1, 3] = 0f;

            m[2, 0] = -(CR * SP * CY + SR * SY);
            m[2, 1] = CY * SR - CR * SP * SY;
            m[2, 2] = CR * CP;
            m[2, 3] = 0f;

            m[3, 0] = origin.X;
            m[3, 1] = origin.Y;
            m[3, 2] = origin.Z;
            m[3, 3] = 1f;
            return m;
        }

        public static FRotator operator +(FRotator angA, FRotator angB) => new FRotator(angA.Pitch + angB.Pitch, angA.Yaw + angB.Yaw, angA.Roll + angB.Roll);
        public static FRotator operator -(FRotator angA, FRotator angB) => new FRotator(angA.Pitch - angB.Pitch, angA.Yaw - angB.Yaw, angA.Roll - angB.Roll);
        public static FRotator operator /(FRotator angA, float flNum) => new FRotator(angA.Pitch / flNum, angA.Yaw / flNum, angA.Roll / flNum);
        public static FRotator operator *(FRotator angA, float flNum) => new FRotator(angA.Pitch * flNum, angA.Yaw * flNum, angA.Roll * flNum);
        public static bool operator ==(FRotator angA, FRotator angB) => angA.Pitch == angB.Pitch && angA.Yaw == angB.Yaw && angA.Yaw == angB.Yaw;
        public static bool operator !=(FRotator angA, FRotator angB) => angA.Pitch != angB.Pitch || angA.Yaw != angB.Yaw || angA.Yaw != angB.Yaw;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
        
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            }
        }
        public Vector2 To2D()
        {
            return new Vector2(this.X, this.Y);
        }
        public FRotator ToFRotator()
        {
            FRotator rot = new FRotator();

            float RADPI = (float)(180 / Math.PI);
            rot.Yaw = (float)Math.Atan2(this.Y, this.X) * RADPI;
            rot.Pitch = (float)Math.Atan2(this.Z, Math.Sqrt((this.X * this.X) + (this.Y * this.Y))) * RADPI;
            rot.Roll = 0;

            return rot;
        }

        public static float DotProduct(Vector3 vecA, Vector3 vecB) => vecA.X * vecB.X + vecA.Y * vecB.Y + vecA.Z * vecB.Z;

        #region Overrides
        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }
        #endregion
        #region Operators
        public static Vector3 operator +(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X + vecB.X, vecA.Y + vecB.Y, vecA.Z + vecB.Z);
        public static Vector3 operator -(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X - vecB.X, vecA.Y - vecB.Y, vecA.Z - vecB.Z);
        public static Vector3 operator *(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X * vecB.X, vecA.Y * vecB.Y, vecA.Z * vecB.Z);
        public static Vector3 operator *(Vector3 vecA, int n) => new Vector3(vecA.X * n, vecA.Y * n, vecA.Z * n);
        public static Vector3 operator /(Vector3 vecA, Vector3 vecB) => new Vector3(vecA.X / vecB.X, vecA.Y / vecB.Y, vecA.Z / vecB.Z);

        public static bool operator ==(Vector3 vecA, Vector3 vecB) =>  vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Y == vecB.Y;
        public static bool operator !=(Vector3 vecA, Vector3 vecB) =>  vecA.X != vecB.X || vecA.Y != vecB.Y || vecA.Y != vecB.Y;
        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }
        #region Functions
        public Vector2 Rotate(Vector2 centerpoint, double angle, bool bAngleInRadians = false)
        {
            if (!bAngleInRadians)
                angle = Math.PI * angle / 180.0;

            return new Vector2(this.X * Math.Cos(angle) - this.Y * Math.Sin(angle), this.X * Math.Sin(angle) + this.Y * Math.Cos(angle));
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"{X},{Y}";
        }
        #endregion
        #region Operators
        public static Vector2 operator +(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X + vecB.X, vecA.Y + vecB.Y);
        public static Vector2 operator -(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X - vecB.X, vecA.Y - vecB.Y);
        public static Vector2 operator *(Vector2 vecA, int n) => new Vector2(vecA.X * n, vecA.Y * n);
        public static Vector2 operator /(Vector2 vecA, Vector2 vecB) => new Vector2(vecA.X / vecB.X, vecA.Y / vecB.Y);

        public static Vector2 operator +(Vector2 vecA, float val) => new Vector2(vecA.X + val, vecA.Y + val);
        public static Vector2 operator -(Vector2 vecA, float val) => new Vector2(vecA.X - val, vecA.Y - val);
        public static Vector2 operator *(Vector2 vecA, float val) => new Vector2(vecA.X * val, vecA.Y * val);
        public static Vector2 operator /(Vector2 vecA, float val) => new Vector2(vecA.X / val, vecA.Y / val);


        public static bool operator ==(Vector2 vecA, Vector2 vecB) => vecA.X == vecB.X && vecA.Y == vecB.Y && vecA.Y == vecB.Y;
        public static bool operator !=(Vector2 vecA, Vector2 vecB) => vecA.X != vecB.X || vecA.Y != vecB.Y || vecA.Y != vecB.Y;
        #endregion
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct TArray<T> where T : struct
    {
        public IntPtr pData;
        public int Count;
        public int Max;
        
        public IntPtr this[int nIndex]
        {
            get
            {
                return pData + nIndex * IntPtr.Size;
            }
        }
        public T ReadValue(int nIndex, bool bDeref)
        {
            IntPtr ptrData = pData + nIndex * IntPtr.Size;

            if (bDeref)
                ptrData = M.Read<IntPtr>(ptrData);

            return M.Read<T>(ptrData);
        }
        public void SetValue(T value, int nIndex, bool bDeref)
        {
            IntPtr ptrData = pData + nIndex * IntPtr.Size;

            if (bDeref)
                ptrData = M.Read<IntPtr>(ptrData);

            M.Write<T>(value, ptrData);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct USceneComponent
    {
        [FieldOffset(0x0160)]
        public byte nBitFlags;

        [FieldOffset(0x1E0)]
        public Vector3 RelativeLocation;

        [FieldOffset(0x1EC)]
        public Vector3 RelativeRotation; // Pitch, Yaw, Roll

        [FieldOffset(0x0220)]
        public Vector3 RelativeScale3D;

        [FieldOffset(0x258)]
        public Vector3 ComponentVelocity;

        public static void SetLocation(IntPtr pUSceneComponent, Vector3 value)
        {
            M.Write<Vector3>(value, pUSceneComponent + 0x1A0);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UCharacterMovementComponent
    {
        [FieldOffset(0x198)]
        public float GravityScale;

        [FieldOffset(0x01A0)]
        public float JumpZVelocity;

        [FieldOffset(0x01BC)]
        public byte MovementMode;

        [FieldOffset(0x1E4)]
        public float MaxWalkSpeed;

        [FieldOffset(0x1E8)]
        public float MaxWalkSpeedCrouched;

        [FieldOffset(0x1F8)]
        public float MaxAcceleration;

        [FieldOffset(0x021C)]
        public float AirControlBoostMultiplier;

        [FieldOffset(0x0290)]
        public float CrouchedSpeedMultiplier;

        [FieldOffset(0x0298)]
        public Vector3 Acceleration;

        [FieldOffset(0x02A4)]
        public Vector3 LastUpdateLocation;

        [FieldOffset(0x03CD)]
        public bool Flying;

        [FieldOffset(0x03D0)]
        private byte StanceMode;

        [FieldOffset(0x0758)]
        public float MaxProneSpeed;
        
        [FieldOffset(0x0778)]
        public float MinWalkSpeedModifier;
        
        [FieldOffset(0x077C)]
        public float WalkSpeedModifierUnit;

        public static void SetGravityScale(IntPtr pMovement, float scale)
        {
            M.Write<float>(scale, pMovement + 0x190);
        }
        public EStanceMode Stance
        {
            get
            {
                return (EStanceMode)this.StanceMode;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GNames
    {
        public IntPtr pData;
        public int Num;
        public int Max;

        public TStaticIndirectArrayThreadSafeRead GetStaticArray()
        {
            return M.Read<TStaticIndirectArrayThreadSafeRead>(this.pData);
        }
    }

    // Thanks ApocDev :)
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TStaticIndirectArrayThreadSafeRead
    {
        private const int MaxTotalElements = 0x200000;
        private const int ElementsPerChunk = 0x4000;
        private const int ChunkCount = 128;

        public fixed long Chunks[ChunkCount];

        public int NumElements;
        public int NumChunks;

        public List<byte[]> ChunkData
        {
            get
            {
                List<byte[]> listChunkdata = new List<byte[]>();


                fixed (long* arrChunkPointers = Chunks)
                    for (int nChunk = 0; nChunk < ChunkCount; nChunk++)
                        listChunkdata.Add(M.Read((IntPtr)arrChunkPointers[nChunk], ElementsPerChunk * IntPtr.Size));

                return listChunkdata;
            }
        }

        public IntPtr this[int nIndex]
        {
            get
            {
                fixed (long* chunks = Chunks)
                {
                    var pChunk = (IntPtr)chunks[nIndex / ElementsPerChunk];
                    var arrChunkData = ChunkData[nIndex / ElementsPerChunk];
                    return (IntPtr)BitConverter.ToInt64(arrChunkData, nIndex % ElementsPerChunk * IntPtr.Size);
                }
            }
        }
        public string[] DumpNames()
        {
            string[] arrNames = new string[MaxTotalElements];
            var arrChunkData = ChunkData;
            
            for (int nIndex = 0; nIndex < MaxTotalElements; nIndex++)
            {
                byte[] arrCurrentChunkData = arrChunkData[nIndex / ElementsPerChunk];
                
                IntPtr pName = (IntPtr)BitConverter.ToInt64(arrCurrentChunkData, nIndex % ElementsPerChunk * IntPtr.Size);

                // Neccessary?
                //if (pName == IntPtr.Zero)
                //    arrNames[nIndex] = "NO_NAME";
                //else
                if (pName != IntPtr.Zero)
                    arrNames[nIndex] = M.Read<FNameEntry>(pName).ToString();
            }

            return arrNames;
        }

    }


    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct FNameEntry
    {
        [FieldOffset(0x10)]
        fixed sbyte arrName[64];

        public override string ToString()
        {
            fixed (sbyte* pName = arrName)
                return new string(pName);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Template
    {

    }

    #endregion
}
