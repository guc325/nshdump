public class PostProcessPass : ScriptableRenderPass
{
	// Fields
	public static Boolean sForceUberWithFinalPass; // 0x0
	public static Single sUberWithFinalPassLimitScale; // 0x4
	private RenderTextureDescriptor m_Descriptor; // 0x78
	private Rect m_DynamicViewport; // 0xac
	private RenderTargetIdentifier m_Source; // 0xc0
	private RenderTargetHandle m_Destination; // 0xe8
	private RenderTargetHandle m_Depth; // 0xf8
	private RenderTargetIdentifier m_InternalLut; // 0x108
	private RenderTargetIdentifier m_InternalLutCharacter; // 0x130
	private RenderTargetHandle m_rtTransparentEffect; // 0x158
	private RenderTargetHandle m_rtTransparentEffectDistort; // 0x168
	private RenderTargetHandle m_rtDistortStrength; // 0x178
	private RenderTargetHandle m_rtCanying; // 0x188
	private RenderTargetHandle m_rtPPBrightStrength; // 0x198
	private PostProcessEnabledTypeEnum m_PostProcessEnabledType; // 0x1a8
	private const String k_RenderPostProcessingTag; // 0x0
	private const String k_RenderFinalPostProcessingTag; // 0x0
	private MaterialLibrary m_Materials; // 0x1b0
	private PostProcessData m_Data; // 0x1b8
	private DepthOfField m_DepthOfField; // 0x1c0
	private MotionBlur m_MotionBlur; // 0x1c8
	private ObjectMotionBlur m_ObjectMotionBlur; // 0x1d0
	private PaniniProjection m_PaniniProjection; // 0x1d8
	private Bloom m_Bloom; // 0x1e0
	private LensDistortion m_LensDistortion; // 0x1e8
	private ChromaticAberration m_ChromaticAberration; // 0x1f0
	private Vignette m_Vignette; // 0x1f8
	private ColorLookup m_ColorLookup; // 0x200
	private ColorAdjustments m_ColorAdjustments; // 0x208
	private Tonemapping m_Tonemapping; // 0x210
	private FilmGrain m_FilmGrain; // 0x218
	private GodRay m_GodRay; // 0x220
	private RadialBlur m_RadialBlur; // 0x228
	private EffectColorTint m_EffectColorTint; // 0x230
	private EffectDistortion m_EffectDistortion; // 0x238
	private EffectDrunk m_EffectDrunk; // 0x240
	private AreaDissolution m_AreaDissolution; // 0x248
	private SkillGrading m_SkillGrading; // 0x250
	private AutoExposure m_AutoExposure; // 0x258
	private LensRain m_LensRain; // 0x260
	private LensSnow m_LensSnow; // 0x268
	private InvertColor m_InvertColor; // 0x270
	private TextureOverlay m_TextureOverlay; // 0x278
	private ScreenColorTint m_ScreenColorTint; // 0x280
	private Sharpen m_Sharpen; // 0x288
	private Canying m_Canying; // 0x290
	private EffectBloom m_EffectBloom; // 0x298
	private const Int32 k_maxCustomDofMipCount; // 0x0
	private const Int32 k_MaxPyramidSize; // 0x0
	private readonly GraphicsFormat m_DefaultHDRFormat; // 0x2a0
	private Boolean m_UseRGBM; // 0x2a4
	private readonly GraphicsFormat m_GaussianCoCFormat; // 0x2a8
	private Dictionary`2 m_Camera2PrevViewProjM; // 0x2b0
	private Boolean m_ResetHistory; // 0x2b8
	private Int32 m_DitheringTextureIndex; // 0x2bc
	private RenderTargetIdentifier[] m_MRT2; // 0x2c0
	private RenderTargetHandle m_FSRTemp; // 0x2c8
	private RenderTargetHandle m_FSRTempTarget; // 0x2d8
	private Vector4[] m_BokehKernel; // 0x2e8
	private Int32 m_BokehHash; // 0x2f0
	private Boolean m_IsStereo; // 0x2f4
	private Boolean m_IsFinalPass; // 0x2f5
	private Boolean m_HasFinalPost; // 0x2f6
	private Boolean m_HasFinalPass; // 0x2f7
	private Boolean m_CharacterModeOn; // 0x2f8
	private Material m_BlitMaterial; // 0x300
	private Boolean isDistortApply; // 0x308
	private Boolean isCanyingApply; // 0x309
	private Boolean isPPBrightApply; // 0x30a
	private Boolean isLensRainApply; // 0x30b
	private Boolean isLensSnowApply; // 0x30c
	private Boolean isInvertColorApply; // 0x30d
	private UInt32 mTmpEffectEnableFrameCount; // 0x310
	private Boolean m_UseHalfResDepth; // 0x314
	private FilteringSettings m_FilteringSettings; // 0x318
	private RenderStateBlock m_RenderStateBlock; // 0x330
	private List`1 m_ShaderTagIdList; // 0x3a0
	private TemporalAAPass m_TaaPass; // 0x3a8
	private Boolean isInitVolumestack; // 0x3b0
	private RenderTargetIdentifier mTempSource; // 0x3b8
	private RenderTargetIdentifier mTempDestination; // 0x3e0
	private Boolean mTempTargetUsed; // 0x408
	private Boolean mTempTarget2Used; // 0x409
	private RenderTargetIdentifier mTempSourceOverride; // 0x410
	private OpaqueAlphaBlendPass m_OpaqueAlphaBlendAfterDofPass; // 0x438
	private static Vector4[] s_DofSamples; // 0x8
	public static DepthOfFieldQuality s_DofQuality; // 0x10
	public static Boolean s_DofOptSample; // 0x14
	public static Boolean s_EnableNearDofWhenFocus; // 0x15
	private RenderTextureDescriptor curm_Descriptor; // 0x440
	private Int32 prefilterPass; // 0x474
	private Vector3 cachedata; // 0x478
	private Bloom lastbloom; // 0x488
	private Boolean curm_UseRGBM; // 0x490
	private Vector4 bloomParams; // 0x494
	private Vector4 BloomFastModeParam; // 0x4a4
	private Vector4 dirtScaleOffset; // 0x4b4
	private Single dirtIntensity; // 0x4c4
	private Texture dirtTexture; // 0x4c8
	private Boolean isinit; // 0x4d0
	public const Int32 AERangeMin; // 0x0
	public const Int32 AERangeMax; // 0x0
	private const Int32 k_Bins; // 0x0
	private const Int32 k_sampleScale; // 0x0
	private const Int32 k_sampleScale2; // 0x0
	private static ComputeBuffer mAELogHistogram; // 0x18
	private Int32 mKernelHistogramClear; // 0x4d4
	private Int32 mKernelHistogram; // 0x4d8
	private Int32 mFixedAutoExposureRT; // 0x4dc
	private Int32 mKernelAutoExposureFixed; // 0x4e0
	private Int32 mKernelAutoExposureProgressive; // 0x4e4
	private const Int32 MAX_AUTO_EXPOSURE_RT_CLEAR_COUNT; // 0x0
	private Dictionary`2 mAECameraResDict; // 0x4e8
	private static ComputeBuffer mHistogramRange; // 0x20
	private Vector4 p1; // 0x4f0
	private Vector4 p2; // 0x500
	private Boolean Ldisinit; // 0x510
	private LensDistortion lastLD; // 0x518
	private ProfilingSampler m_ProfilingSampler; // 0x520
	private Boolean isApplyTransparentEffectAfterDOF; // 0x528
	private TransparentEffectPass m_passTransparentEffectAfterDOF; // 0x530
	private Boolean isApplyEffectPC; // 0x538
	private TransparentEffectBloomDownsampleChain effectBloomDownsampleChain; // 0x540
	private TransparentEffectBloomGaussian effectBloomGaussian; // 0x548
	private const GraphicsFormat k_GodRayColorFormat; // 0x0
	private RenderTexture m_GodRayHistory1; // 0x550
	private RenderTexture m_GodRayHistory2; // 0x558
	private Boolean m_FirstTimeGodrayBlend; // 0x560
	private Matrix4x4 m_PreVP; // 0x564
	private Single m_LastUseGodRayHistoryTime; // 0x5a4
	private const Single k_GodRayRTReleaseInterval; // 0x0
	private Int32 m_pRTBright; // 0x5a8
	private Int32 m_pRTBlur; // 0x5ac
	private Int32 m_pRTScaleScene; // 0x5b0
	private const Single TEXTURE_OVERLAY_FADE_FACTOR; // 0x0
	private const Int32 DISTORT_ACTIVE_LESS_THEN_3; // 0x0
	private const Int32 DISTORT_ACTIVE_LESS_THEN_5; // 0x0
	private const Int32 DISTORT_ACTIVE_MORE_THEN_5; // 0x0
	private Int32 mDistortActiveState; // 0x5b4
	private Single mDistortEnableTime; // 0x5b8
	public static Boolean sIsDistortPassEnable; // 0x28
	public Boolean ForbidDistort; // 0x5bc

	// Properties

	// Methods
	// RVA: 0x38ad494 VA: 0x78eeaf8494
	public Void .ctor(RenderPassEvent evt, PostProcessData data, Material blitMaterial) { }
public class CameraSkillCurve : MonoBehaviour
{
	// Fields
	public DicIdAndCurve curveDict; // 0x18
	private Boolean <skillCurveShaking>k__BackingField; // 0x20
	private Single shakeTime; // 0x24
	private Single startTime; // 0x28
	private SkillShakeCurve currentCurve; // 0x30
	public DicIdAndFovCurve fovCurve; // 0x38
	private Boolean <fovShaking>k__BackingField; // 0x40
	private Single fovTime; // 0x44
	private Single fovStartTime; // 0x48
	private SkillFovCurve currentFovCurve; // 0x50

	// Properties
	public Boolean skillCurveShaking { get; set; }
public class LeafSkillForceManager
{
	// Fields
	private static LeafSkillForceManager instance; // 0x0
	private static readonly Object padlock; // 0x8
	private static List`1 mSkillForceDataList; // 0x10

	// Properties
	public static LeafSkillForceManager Instance { get; }
public class LeafSkillTest : MonoBehaviour
{
	// Fields
	public SkillForceData skillForceData; // 0x18

	// Properties

	// Methods
	// RVA: 0x45286d4 VA: 0x78ef7736d4
	public Void GenerateSkill() { }
public class LeafSolverManager
{
	// Fields
	private Int32 mSolveTimesPerFrame; // 0x10
	private Int32 mIterationTimes; // 0x14
	private Int32 mMaxFootForceCount; // 0x18
	private Int32 mMaxSkillForceCount; // 0x1c
	private Int32 mMaxRandomForceCount; // 0x20
	private ComputeBuffer mActiveInstanceAppendBuffer; // 0x28
	private ComputeBuffer mActiveCountBuffer; // 0x30
	private ComputeBuffer mActiveInstanceIndirectBuffer; // 0x38
	private ComputeBuffer mActiveParticleIndirectBuffer; // 0x40
	private ComputeBuffer mActiveTriangleIndirectBuffer; // 0x48
	private ComputeBuffer mActiveDistanceIndirectBuffer; // 0x50
	private ComputeBuffer mActiveBendingIndirectBuffer; // 0x58
	private ComputeBuffer mActiveParticleIndicesBuffer; // 0x60
	private ComputeBuffer mActiveTriangleIndicesBuffer; // 0x68
	private ComputeBuffer mActiveDistanceIndicesBuffer; // 0x70
	private ComputeBuffer mActiveBendingIndicesBuffer; // 0x78
	private ComputeBuffer mFootForceFieldArrayBuffer; // 0x80
	private ComputeBuffer mSkillForceFieldArrayBuffer; // 0x88
	private ComputeBuffer mRandomForceFieledArrayBuffer; // 0x90
	private ComputeBuffer mProjectedPositionsBuffer; // 0x98
	private ComputeBuffer mTriangleIndicesBuffer; // 0xa0
	private ComputeBuffer mAccumulativePosDeltaIntBuffer; // 0xa8
	private ComputeBuffer mAccumulativeCountBuffer; // 0xb0
	private ComputeBuffer mDistanceConstraintsBuffer; // 0xb8
	private ComputeBuffer mBendingConstraintsBuffer; // 0xc0
	private ComputeBuffer mAccumulativeVertexNormalsBuffer; // 0xc8
	private ComputeBuffer mAccumulativeVertexTangentsBuffer; // 0xd0

	// Properties

	// Methods
	// RVA: 0x4529134 VA: 0x78ef774134
	public Void Init(LeafUpdater updater) { }
public class EventTrackAgent
{
	// Fields
	private Animator m_animator; // 0x10
	private AnimatorTrackSetDef m_animatorAllTrack; // 0x18
	private CRuntimeEventTracks[] m_layersEventTrack; // 0x20
	private static List`1 listExitSM; // 0x0
	private static List`1 listEnterSM; // 0x8
	private Boolean m_bNeedResendUnProcessedDefaultState; // 0x28
	private AnimatorStateInfo m_stateInfoUnProcessed; // 0x2c
	private Boolean <IsReloaded>k__BackingField; // 0x6c
	public Action m_onReloaded; // 0x70
	private static Int32 hashSMSkill; // 0x10

	// Properties
	public Boolean IsReloaded { get; set; }
public class MainCameraComponent : MonoBehaviour
{
	// Fields
	private static Single m_LookAtEnterTriggerDistance; // 0x0
	private static Single m_LookAtLeaveTriggerDistance; // 0x4
	private static Single m_LookAtEnterTime; // 0x8
	private static Single m_LookAtLeaveTime; // 0xc
	private static Single m_LookAtEnterDefaultDistance; // 0x10
	private static Single m_LookAtLeaveDefaultDistance; // 0x14
	private static Single m_LookAtPitch; // 0x18
	private static Single m_LookAtFocalLength; // 0x1c
	private static Single m_LookAtAperture; // 0x20
	private static LuaTable m_EnterLookAtTb; // 0x28
	private static LuaTable m_LeaveLookAt25DTb; // 0x30
	private static LuaTable m_LeaveLookAt3DTb; // 0x38
	private static Action`1 m_fOnNearLookAtStateChanged; // 0x40
	private Boolean m_bNearLookAtOpen; // 0x18
	private Vector3 m_EnterNearLookAngle; // 0x1c
	private Boolean m_NeedCheckNearLookAtOpen; // 0x28
	private Int32 m_CameraModeBeforeShowTime; // 0x2c
	public CameraShowTime cameraShowTimeType; // 0x30
	private CameraShowTime m_CameraShowTimeTypeBeforeRecover; // 0x34
	private Boolean m_CameraShowTimeAnimRandomCycle; // 0x38
	private List`1 m_CameraShowTimeAnims; // 0x40
	private Int32 m_CurrentCameraShowTimeAnimIdx; // 0x48
	private Boolean m_CameraShowTimeAnim_FollowPlayer; // 0x4c
	public Vector3 m_CameraShowTimeAnimScale; // 0x50
	private Boolean m_CameraShowTimeAnimWithDest; // 0x5c
	public Boolean m_CameraShowTimeAnimCheckCollider; // 0x5d
	private String m_CurrentCameraShowTimeTimelineName; // 0x60
	private Single m_CameraShowTimeAnimLastFrameDistance; // 0x68
	private static Single m_CameraShowTimeAnimAdjustDistanceSpeed; // 0x48
	private static Dictionary`2 m_ShowTimeCameraFrameDatas; // 0x50
	private ShowTimeCameraFrameData m_ShowTimeCameraFrameData; // 0x70
	private Single m_ShowTimeCameraStartTime; // 0x78
	private Vector3 m_ShowTimeCameraStartPosition; // 0x7c
	private Quaternion m_ShowTimeCameraStartRotation; // 0x88
	private Single m_ShowTimeCameraStartFov; // 0x98
	private const Single DEFAULT_CAMERA_SHOW_TIME_RECOVER_TIME; // 0x0
	private Single m_CameraShowTimeRecoverStartTime; // 0x9c
	private Single m_CameraShowTimeRecoverTime; // 0xa0
	private Vector3 m_CameraShowTimeRecoverStartPosition; // 0xa4
	private Single m_CameraShowTimeRecoverDistance; // 0xb0
	private Quaternion m_CameraShowTimeRecoverStartRotation; // 0xb4
	private Single m_CameraShowTimeRecoverStartFov; // 0xc4
	private Boolean m_bPlayerRotateByCVM; // 0xc8
	private Single m_PlayerRotateByCVMAutoFollowScale; // 0xcc
	private Boolean m_bNaviRotate; // 0xd0
	private Boolean m_bBattleRotate; // 0xd1
	private Boolean m_bSoloBossRotate; // 0xd2
	private Boolean m_bPauseAutoRotate; // 0xd3
	private Boolean m_bSurfRotate; // 0xd4
	private Boolean m_bSwordFlightRotate; // 0xd5
	private Boolean m_bMagneticMasterRotate; // 0xd6
	private Boolean m_bBattleRoughLock; // 0xd7
	private Boolean m_bBattleAccurateLock; // 0xd8
	private Single m_LastDragOrZoomCameraTime; // 0xdc
	private Single m_LastDragTime; // 0xe0
	private Single m_LastZoomTime; // 0xe4
	private Single m_AutoRotateStartTime; // 0xe8
	private Single m_NaviMoveDirection; // 0xec
	private Boolean m_NaviMoveDirectionEnable; // 0xf0
	private Single m_SurfMoveTime; // 0xf4
	private Single m_SwordFlightMoveTime; // 0xf8
	private Single m_MagneticMasterMoveTime; // 0xfc
	private Boolean m_bCameraIsAccurate; // 0x100
	private Single m_LastMoveTime; // 0x104
	private ScreenTargetStrategy m_ScreenTargetStrategy; // 0x108
	private Boolean m_AdjustRotateWhenCastSkill; // 0x10c
	private Boolean m_CurrentFrameCastSkill; // 0x10d
	private static Single m_PauseAutoRotateAfterDragTime; // 0x58
	private static Single m_FollowYawTo; // 0x5c
	private Single m_BattleLastTargetDirection; // 0x110
	private Single m_BattleLastDistance; // 0x114
	private Single m_BattleLastChangeTime; // 0x118
	private Single m_BattleFollowLastReactTime; // 0x11c
	private static Single m_BattleFollowReactTime; // 0x60
	private static Single m_BattleDefaultDistance; // 0x64
	private static Single m_BattleMinDistanceToReact; // 0x68
	private static Single m_BattleMaxDistanceToReact; // 0x6c
	private static Single m_BattleDefaultPitch; // 0x70
	private static Single m_BattleMinPitchToReact; // 0x74
	private static Single m_BattleMaxPitchToReact; // 0x78
	private static Single m_BattleYawToReact; // 0x7c
	private static Single m_AutoFollowScale; // 0x80
	private static Single m_BattleAutoFollowScale; // 0x84
	private static Single m_BattleYawReactDistance; // 0x88
	private static Single m_BattleTargetDistanceToReact; // 0x8c
	private static Single m_PauseAutoBattleRotateAfterDragTime; // 0x90
	private static Single m_BattleYawToReactAccurate; // 0x94
	private static Single m_BattleAutoFollowScaleAccurate; // 0x98
	private static Single m_BattleTargetDistanceToReactAccurate; // 0x9c
	private static Single m_BattleYawToRectLock; // 0xa0
	private static Single m_FollowYawToLock; // 0xa4
	private static Single m_BattleFollowReactTimeLock; // 0xa8
	private static Single m_PauseAutoBattleRotateAfterDragTimeLock; // 0xac
	private static Single m_PauseAutoBattleRotateAfterMoveTime; // 0xb0
	private static SoloBossCameraParams soloParams; // 0xb8
	private static BigFlyingCameraParams bigFlyingParams; // 0xc0
	private static QisheCameraParams qisheParams; // 0xc8
	private static HostAutoAdjustParams hostAutoAdjustParams; // 0xd0
	private static NPCInteractionCamera m_NPCCameraParams; // 0xd8
	private static Single m_JumpWallRotateScale; // 0xe0
	private Int32 m_YawFollowStrategy; // 0x120
	private Boolean m_TargetRenderObjectIsManual; // 0x124
	private RenderObject m_OccasionTargetRenderObject; // 0x128
	private Boolean m_ManualSelectSatisfy; // 0x130
	private Boolean m_bIsBattleAutoRotating; // 0x131
	private Boolean m_bIsBattleAutoDistance; // 0x132
	private Boolean m_bInBattle; // 0x133
	private Single m_LockLookAtPosEndTime; // 0x134
	private Int32 m_LockLookAtMode; // 0x138
	private Vector3 m_LockLookAtTargetPos; // 0x13c
	private RenderObject m_LockLookAtTargetRO; // 0x148
	private Boolean m_LockLookAtTargetPosSmart; // 0x150
	private Single m_LockLookAtMaxDistance; // 0x154
	private Single m_LockLookAtXAngleRange; // 0x158
	private Boolean m_25DRotationAdjust; // 0x15c
	private Single m_25DRotationRecoverVelocity; // 0x160
	private static Single m_25DRotationRecoveryTime; // 0xe4
	private Boolean m_25DRotationUnfreeze; // 0x164
	private Single _m_PauseRotateAfterDragtime; // 0x168
	private Single _m_BattleYawToReact; // 0x16c
	private Single _m_BattleAutoFollowScale; // 0x170
	private Single _m_BattleTargetDistanceToReact; // 0x174
	public Vector3 qishe_Rotation; // 0x178
	private static Single m_LandScapeMinDistance; // 0xe8
	private static Single m_LandScapeMaxDistance; // 0xec
	private static Single m_DefaultObjectCameraParam; // 0xf0
	private static List`1 m_MainPlayerAlphaSettingList; // 0xf8
	private static Single m_CameraForbidScreenOffsetDistance; // 0x100
	public Transform m_MainCameraTrans; // 0x188
	public Transform m_LocalCameraTrans; // 0x190
	private Vector3 m_DeltaLocalCameraPosCache; // 0x198
	private Transform m_CameraAnimTrans; // 0x1a8
	private Transform m_CameraAnimTargetTrans; // 0x1b0
	private Camera m_CameraAnimCamera; // 0x1b8
	public VirtualCameraBase m_VirutalCamera; // 0x1c0
	public CinemachineVirtualCamera m_CinemachineVirtualCamera; // 0x1c8
	private Transform m_CameraDest; // 0x1d0
	private Vector3 m_FocusOnPosition; // 0x1d8
	private Boolean m_bUseEnginePosition; // 0x1e4
	private Boolean m_bChangingViewUseUpdateRotate; // 0x1e5
	private Boolean m_bForbidDragShortKey; // 0x1e6
	private Boolean m_bDestInWater; // 0x1e7
	private Boolean m_bUseSimpleCollisionCheck; // 0x1e8
	private Single m_SimpleCollisionRadious; // 0x1ec
	private Int32 m_SimpleCollisionLayerMask; // 0x1f0
	private Boolean m_NeedAdjustCameraPositionInLegoBuild; // 0x1f4
	private Int32 m_LegoBuildCollisionLayerMask; // 0x1f8
	private Vector3 m_LastFrameFocusPosition; // 0x1fc
	private Quaternion m_LastFrameMainCameraRotate; // 0x208
	private Vector3 m_LastFrameExtraOffset; // 0x218
	private Single m_SimpleCollisionDistance; // 0x224
	private Single m_SimpleCollisionCameraRestrictY; // 0x228
	private Single m_SimpleCollisionCameraJumpY; // 0x22c
	private Boolean m_SpecialCameraHostCameraAutoAdjust; // 0x230
	public AnimationCurve m_AdjustDistanceRecoverCurve; // 0x238
	public AnimationCurve m_CameraHitOnGroundAdjustDistanceOffset; // 0x240
	public Boolean m_bCameraHitOnGroundAdjustDistance; // 0x248
	public Int32 m_IngoreSmallRayCastNum; // 0x24c
	public Single m_HitOnGroundAdjustDistanceSmoothTime; // 0x250
	private Single m_HitOnGroundAdjustDistanceSmoothStartTime; // 0x254
	private Single m_fCurHitOnGroundAdjustDistanceOffset; // 0x258
	private Boolean m_bColliderLerpAdjustDistance; // 0x25c
	private Boolean m_bLastFrameAdjustDistance; // 0x25d
	private Boolean m_bCurrentFrameAdjustDistance; // 0x25e
	private Single m_CollisionAdjustDistanceCD; // 0x260
	private static Single m_Config_CollisionAdustDistanceCD; // 0x104
	private RaycastHit[] m_CommonRaycastHitArr; // 0x268
	private Dictionary`2 m_reverseHitToDestHitRetDict; // 0x270
	private Int32 m_RayCastHistPreAllocatedSize; // 0x278
	private ColliderCheckExtraInfo m_ColliderCheckExtraInfo; // 0x280
	private ColliderCheckExtraInfo m_NormalColliderCheckExtraInfo; // 0x288
	private ColliderCheckReturnInfo m_CVMColliderReturnInfo; // 0x290
	private CameraRecord m_RoTateRecord; // 0x298
	private CameraRecord m_PositionRecord; // 0x2a0
	private CameraRecord m_TargetPosRecord; // 0x2a8
	private Boolean m_CameraRecordIngoreCurentFrame; // 0x2b0
	private Boolean m_CameraRecordOverThreshold; // 0x2b1
	private InputManager inputObj; // 0x2b8
	public Boolean m_CameraDestFollowRORotaion; // 0x2c0
	public Boolean m_bCameraFollowRONode; // 0x2c1
	public String m_bCameraFollowRONodeName; // 0x2c8
	private Vector3 m_FollowRONodePostion; // 0x2d0
	private Single _OriginDestHeight; // 0x2dc
	private RenderObject m_RenderObject; // 0x2e0
	private RenderObject m_LastFrameRenderObject; // 0x2e8
	private Single m_TmpROTransparent; // 0x2f0
	private Single m_ROTransparent; // 0x2f4
	private String m_DestChildName; // 0x2f8
	private Vector3 tmp; // 0x300
	private Vector3 m_TargetLocalPosition; // 0x30c
	private Vector3 m_CameraPositionExtraOffset; // 0x318
	private Single m_fRotationSpeed; // 0x324
	public Single m_ArrowRotationSpeed; // 0x328
	public Boolean m_bIsRotating; // 0x32c
	private Boolean m_bIsUpdatingDistance; // 0x32d
	public AnimationCurve m_CameraDistance2Fov; // 0x330
	public AnimationCurve m_HostCameraDistance2Fov; // 0x338
	private AnimationCurve m_FinalCameraDistance2Fov; // 0x340
	public AnimationCurve m_CameraDistance2OffsetY; // 0x348
	public Int32 m_lowHeightRemoveIndex; // 0x350
	public AnimationCurve m_CameraDistance2OffsetYFixed; // 0x358
	private Single m_CurrentFOV; // 0x360
	private Single m_StartFOV; // 0x364
	private Single m_TargetFOV; // 0x368
	private static Single INVALID_DOF_PARAM; // 0x108
	private Single m_StartFocusDistance; // 0x36c
	private Single m_StartFocalLength; // 0x370
	private Single m_StartAperture; // 0x374
	private Single m_TargetFocusDistance; // 0x378
	private Single m_TargetFocalLength; // 0x37c
	private Single m_TargetAperture; // 0x380
	private Boolean m_bFocusMainPlayerTransparentOnDOF; // 0x384
	private Int32 m_EnableSSS; // 0x388
	private Quaternion m_TargetRotation; // 0x38c
	public Single m_CurrentDistance; // 0x39c
	public Single m_ExtensionDistance; // 0x3a0
	private Single _tempTargetDistance; // 0x3a4
	public Single m_MaxDistance; // 0x3a8
	public Single m_MinDistance; // 0x3ac
	public AnimationCurve m_ScaleCameraDistance2Speed; // 0x3b0
	public AnimationCurve m_HostAutoAdjustCameraDistance2Speed; // 0x3b8
	public AnimationCurve m_BattleScaleCameraDistance2Speed; // 0x3c0
	public AnimationCurve m_SpeedDragCurve; // 0x3c8
	private Single m_DragSpeedRatio; // 0x3d0
	private Vector3 m_LastPlayerPos; // 0x3d4
	public AnimationCurve m_DragCameraAngle2Speed; // 0x3e0
	public AnimationCurve m_NormalCameraAngle2Speed; // 0x3e8
	public AnimationCurve m_HostAutoAdjustCameraAngle2Speed; // 0x3f0
	public AnimationCurve m_BattleCameraAngle2Speed; // 0x3f8
	public Single m_HostAutoAdjustYawSpeed; // 0x400
	public Single m_HostAutoAdjustPitchSpeed; // 0x404
	public Single m_ZoomSensitivity; // 0x408
	public Single m_PitchSensitivity; // 0x40c
	public Single m_YawSensitivity; // 0x410
	public AnimationCurve m_ZoomSensitivityCurve; // 0x418
	public AnimationCurve m_PitchSensitivityCurve; // 0x420
	public AnimationCurve m_YawSensitivityCurve; // 0x428
	private const Single DEFAULT_MIN_PITCH; // 0x0
	private const Single DEFAULT_MAX_PITCH; // 0x0
	private const Single DEFAULT_MIN_YAW; // 0x0
	private const Single DEFAULT_MAX_YAW; // 0x0
	public Single m_MinPitch; // 0x430
	public Single m_MaxPitch; // 0x434
	private Single m_MinYaw; // 0x438
	private Single m_MaxYaw; // 0x43c
	private Boolean m_OpenSpecialYawLimit; // 0x440
	private Single m_SpecialYawLimit; // 0x444
	private Single m_SpecialYawBegin; // 0x448
	private Int32 m_LastDragDir; // 0x44c
	public Boolean m_bNoFollowDelay; // 0x450
	public Boolean m_bFollowTarget; // 0x451
	private const Single DEFAULT_WATER_MIN_PITCH; // 0x0
	private const Single PLAYER_WATER_HEIGHT_NEED_PITCH_LIMIT; // 0x0
	private const Single CAMERA_WATER_HEIGHT_NEED_PITCH_LIMIT; // 0x0
	public Single m_MinPitchInWater; // 0x454
	public static Single m_WallRunOffest; // 0x10c
	public static Single m_WallRunOffsetPitchLimit; // 0x110
	public LayerMask m_CollisionLayers; // 0x458
	public LayerMask m_CollisionLayersParkour; // 0x45c
	public LayerMask m_CollisionLayersCanNotCross; // 0x460
	private CameraColliderStrategy m_CameraColliderStrategy; // 0x468
	private CameraFollowDelay m_CameraFollowDelay; // 0x470
	private CameraMoveFollowDelay m_CameraMoveFollowDelay; // 0x478
	private Single m_CameraNearZ; // 0x480
	private Single m_CameraFarZ; // 0x484
	private Single m_CameraNearZ_Android; // 0x488
	private Single m_CameraFarZ_Android; // 0x48c
	private Boolean m_bInited; // 0x490
	public Boolean m_bEnableWaterLimit; // 0x491
	public Boolean m_NeedHeadOffset; // 0x492
	private CameraShakeType m_CurrentShakeType; // 0x494
	private Animation m_Anim; // 0x498
	private CameraSkillCurve m_ShakeCurve; // 0x4a0
	public AnimationCurve m_ShakeScaleCurve; // 0x4a8
	private CameraSimpleShake m_SimpleShake; // 0x4b0
	private CameraDirectionalShake m_DirectionalShake; // 0x4b8
	private Boolean m_CameraRotateIsDragChange; // 0x4c0
	private static Single m_25DDefaultPitchValue; // 0x114
	private Int32 m_CameraMode; // 0x4c4
	public Boolean m_bPauseUpdateCamera; // 0x4c8
	private Boolean m_CameraPositionImmedatelyInCVM; // 0x4c9
	private EnumCameraUpdateState m_CurrentUpdateState; // 0x4cc
	private Single m_fChangingViewModeRatio; // 0x4d0
	private static Single DEFAULT_CHANGE_TIME; // 0x118
	public Single m_ChangingViewModeTime; // 0x4d4
	private Boolean m_bIgnoreCollider; // 0x4d8
	private Boolean m_ColliderOnlyFoucsGround; // 0x4d9
	public AnimationCurve m_Dist2TimeInCVM; // 0x4e0
	private Single m_StartTimeInCVM; // 0x4e8
	private Single m_StartDistInCVM; // 0x4ec
	private Vector3 m_StartPosInCVM; // 0x4f0
	private Vector3 m_StartLocalPosInCVM; // 0x4fc
	private Quaternion m_StartRotationInCVM; // 0x508
	private Vector3 m_StartPositionInCVM; // 0x518
	private Boolean m_bKeepFOVWithCurveInCVM; // 0x524
	private Single m_TmpFollowSpeedScale; // 0x528
	public Vector3 m_CameraForwardSavedForMove; // 0x52c
	private static Single m_MouseDragX; // 0x11c
	private static Single m_MouseDragY; // 0x120
	private static Single m_ScrollWheel; // 0x124
	private String m_ROSpineBoneName; // 0x538
	private Int32 m_ROHaveSpineBone; // 0x540
	private Vector3 m_TmpPositionForRODestroy; // 0x544
	private Boolean m_bForbidScreenOffset; // 0x550
	public Single zoomDirection; // 0x554
	private Single mDynamicCascade2Split; // 0x558
	private Single mDynamicShadowPrecision; // 0x55c
	private Single mDynamicShadowBegin; // 0x560
	private Boolean m_bNoCascadesDynamicShadow; // 0x564
	private Single m_LastExtensionDistance; // 0x568
	private Vector2 m_TargetPositionLockCenter; // 0x56c
	private Vector2 m_TargetPositionLockWH; // 0x574
	private Vector2 m_HalfViewportToWorldSize; // 0x57c
	private Single smoothVelocity; // 0x584
	public Single m_CameraFollowSmoothTime; // 0x588
	public Single m_CameraFollowSmoothMaxDistance; // 0x58c
	public Int32 m_NewDelayFollowStrategy; // 0x590
	public Vector3 m_NewFollowDelayDamping; // 0x594
	public Vector3 m_NewFollowDelayTargetDamping; // 0x5a0
	public Vector3 m_TargetScreenLimit; // 0x5ac
	public Vector3 m_CurScreenLimit; // 0x5b8
	private Vector3 m_NewFollowDelayDampingTime; // 0x5c4
	private Vector3 m_NewFollowDelayCurTime; // 0x5d0
	private Vector3 m_NewFollowDelayStartDamp; // 0x5dc
	private Vector2 m_ScreenLimitTime; // 0x5e8
	private Vector2 m_ScreenLimitCurTime; // 0x5f0
	private Vector2 m_ScreenStartLimit; // 0x5f8
	private Vector3 m_CacheTargetPosition; // 0x600
	public Boolean m_OpenScreenClamp; // 0x60c
	public Boolean m_IsNewFollowDelayTargetDampUpdate; // 0x60d
	public Color m_DeadZoneColor; // 0x610
	public Boolean m_DrawScreenLimit; // 0x620
	public Color m_DestPosColor; // 0x624
	public Color m_CacheTargetPosColor; // 0x634
	public Color m_TargetPosColor; // 0x644
	public AnimationCurve m_Distance2ScreenIntensityCurve; // 0x658
	private Single m_Distance2ScreeInensity; // 0x660
	public Boolean m_DebugInfo; // 0x664
	private Boolean gui_fight_open; // 0x665
	private Single gui_currentRotateSpeed; // 0x668
	private Single gui_diffangle; // 0x66c
	private Single gui_currentDistanceSpeed; // 0x670
	private Vector2 scrollPosition; // 0x674
	private GUIStyle uIStyle; // 0x680
	private Vector3 debug_lastTargetPosition; // 0x688
	private Vector3 debug_TPPosition; // 0x694
	private Vector3 debug_lastenginePos; // 0x6a0
	private Vector3 debug_enginePosDelta; // 0x6ac
	private Vector3 debug_lastropos; // 0x6b8
	private Vector3 debug_roposDelta; // 0x6c4
	private Collider[] m_SimpleCheckColliderInfo; // 0x6d0
	private Vector3 m_TempValue; // 0x6d8
	private Single m_QAnear; // 0x6e4
	private Single m_QAfar; // 0x6e8
	private Boolean m_QAfarFirst; // 0x6ec
	private Single m_QAFarBefore; // 0x6f0
	private Boolean forceResetLocalTrans; // 0x6f4
	private Boolean m_ForbidShake; // 0x6f5
	private Single m_AdjustDistance; // 0x6f8
	private Vector3 m_AdjustCameraPostionExtraOffset; // 0x6fc
	private Boolean m_bNeedAdjustCameraPostionExtraOffset; // 0x708
	private Boolean m_CameraDistanceAdjusted; // 0x709
	private Single m_CameraColliderAdjustExtra; // 0x70c
	public Boolean bLerpSwitch; // 0x710
	private ColliderCheckReturnInfo m_FollowCameraCheckReturnInfo; // 0x718
	private ColliderCheckReturnInfo m_FollowCameraIngoreSmallReturnInfo; // 0x720
	private Boolean m_ExtraPosCollisionCheck; // 0x728
	private ColliderCheckReturnInfo m_SimpleCameraCheckReturnInfo; // 0x730
	private Dictionary`2 m_TmpHitCollider; // 0x738
	private Single m_OldDistanceToTarget; // 0x740
	private Boolean m_RenderObjectIsWallRun; // 0x744
	private Vector3 m_RenderObjectLastFramePos; // 0x748
	private Vector3 m_RenderObjectLastFrameEnginePos; // 0x754
	private Boolean m_CanCalculateROMoveSpeed; // 0x760
	public Vector2 m_RenderObjectMoveSpeed; // 0x764
	public Vector3 m_RenderObjectMoveSpeed3D; // 0x76c
	public Boolean m_UseEnginePosCaclSpeed; // 0x778
	private Vector3 _TempCameraDestPos; // 0x77c
	private Boolean m_FrameIsTopCamera; // 0x788
	private Boolean _StopUpdateMainPlayerCamera; // 0x789
	private static OnCameraDistanceChangedDelegate m_fOnCameraDistanceChanged; // 0x128
	private Boolean m_IndependentUpdateYawAndPitch; // 0x78a
	private static GetCameraDestOffsetByTrackDelegate m_fGetCameraDestOffsetByTrack; // 0x130
	private Vector3 m_CameraDestOffsetByTrack; // 0x78c
	private static Single INVALID_CAMERA_DEST_OFFSET_VALUE; // 0x138
	private static Vector3 INVALID_CAMERA_DEST_OFFSET; // 0x13c
	private static Single INVALID_CAMERA_MIN_PITCH_VALUE; // 0x148
	private Single m_CameraMinPitchByTrack; // 0x798
	private Vector3 m_CameraDestOffsetByVehicle; // 0x79c
	private Vector3 m_CameraDestOffsetByCarrier; // 0x7a8
	private Vector3 m_CameraDestOffsetByIndicatorEffect; // 0x7b4
	private ChangeCameraParam m_changeCameraParam; // 0x7c0
	private LowLevelChangeCameraParams m_LowLevelChangeCameraParams; // 0x7c8
	public Single rotationRateMaxX; // 0x7d0
	public Single rotationRateMaxY; // 0x7d4
	public Single rotationRateMaxZ; // 0x7d8
	private Vector3 gyroInput; // 0x7dc
	private Vector3 curEuler; // 0x7e8
	private Int32 updateRate; // 0x7f4
	private Single gyroFactor; // 0x7f8
	private Vector3 cameraInitEuler; // 0x7fc
	private const Single LOCK_ROTATION_SPEED; // 0x0
	private CameraLockData mCameraLockData; // 0x808
	private List`1 m_NoFollowDelayReasonTb; // 0x810
	public String m_BoneName; // 0x818
	private List`1 m_EnginePosReasonTb; // 0x820
	private Rect m_ScreenLimitRect; // 0x828

	// Properties
	public Boolean IsPlayingCameraShowTimeAnim { get; }
public class RenderObject
{
	// Fields
	private SFollowROInfo m_FollowROInfo; // 0x10
	private static Int32 g_nGloableRenderObjectID; // 0x0
	private static Dictionary`2 s_AllRenderObject; // 0x8
	private static Dictionary`2 s_AllRenderObjectId2RO; // 0x10
	public static Dictionary`2 s_instanceLMDForDesigner; // 0x18
	private Int32 m_nRenderObjectID; // 0x18
	private GameObject m_gameObject; // 0x20
	private RenderObjectComponent roComp; // 0x28
	private Transform m_ColliderRoot; // 0x30
	private List`1 GroupRenderObjects; // 0x38
	private GameObject m_PrefabAsset; // 0x40
	private CModel m_model; // 0x48
	private RagdollComponent m_ragdollComponent; // 0x50
	private Boolean m_bUseBakedLightmap; // 0x58
	private Int32 m_shadowType; // 0x5c
	private Int32 m_OringinLayer; // 0x60
	private CRenderObjectUpdateContext m_updateContext; // 0x68
	private DynamicBoneBuiltin[] m_dynamicBones; // 0x70
	private Transform m_pelvis; // 0x78
	private Transform m_Bip001Head; // 0x80
	private Int32 m_Bip001HeadIndex; // 0x88
	private Int32 m_BipPelvisIndex; // 0x8c
	private Int32 m_Bip001FootstepsIndex; // 0x90
	private Int32 m_ColliderRootBindingBoneIndex; // 0x94
	private BoneAttachment m_ColliderBoneAttach; // 0x98
	public Int32 m_PositionObjectId; // 0xa0
	public Action`2 OnFirstLateUpdate; // 0xa8
	public Action`1 OnEnterOrLeaveSMSkill; // 0xb0
	public Action OnStateChange; // 0xb8
	private Int32 m_lateUpdateCntAfterLoaded; // 0xc0
	public UInt32 EngineObjectId; // 0xc4
	private Boolean m_IsScreenSpaceSSSEnable; // 0xc8
	public Int32 BehaviorTableId; // 0xcc
	private Single m_EngineDirection; // 0xd0
	private CLocomotionState m_locomotionState; // 0xd8
	private Vector3 m_f3Scale; // 0xe0
	private EObj_Lua_Type m_ObjLuaType; // 0xec
	private Vector3 m_PosOffset; // 0xf0
	private Vector3 m_Pos; // 0xfc
	private Vector3 m_LastPos; // 0x108
	private Vector3 m_MoveBeginPos; // 0x114
	private Vector3 m_MoveEndPos; // 0x120
	private EnumGender m_GenderType; // 0x12c
	private Single m_fStepSpeed; // 0x130
	private Boolean m_bDash; // 0x134
	private Boolean m_bSurf; // 0x135
	public Boolean bForceUseRoHeight; // 0x136
	public Boolean m_hasCloth; // 0x137
	public Boolean m_hasClothPluggableCollider; // 0x138
	public Boolean m_hasWarpAttachedColliders; // 0x139
	private Boolean m_bInWaterRegion; // 0x13a
	private BoxCollider collider; // 0x140
	private RenderObjectStatus m_Status; // 0x148
	private Boolean m_NeedCacheInPool; // 0x14c
	private String _m_Prefab; // 0x150
	private RenderObjectResType m_ResType; // 0x158
	private Action`2 m_OnResLoaded; // 0x160
	private Action`1 m_OnResLoadedAdditionalAction; // 0x168
	private Boolean m_bNeedRefreshVisibleAfterResLoaded; // 0x170
	private Boolean m_DelayDestroy; // 0x171
	public Boolean m_IsUsedByAttachmentManager; // 0x172
	private UInt32 m_ROInstanceId; // 0x174
	private static Vector3 SET_POSITION_START_OFFSET; // 0x20
	private static Int32 SET_POSITION_RAY_LENGTH; // 0x2c
	private static Single POSITION_NERAEST_UP_INIT; // 0x30
	private RaycastHit[] getStandingHeightHits; // 0x178
	private static Single SMOOTH_DESIRE_MOVE_SPEED; // 0x34
	private static Single SMOOTH_SPEED; // 0x38
	private CharacterMoveStat m_moveStat; // 0x180
	private const Int32 MAX_PREDICT_STEPS; // 0x0
	private const Int32 MAX_PREDICT_STEPS_VALUE_COUNT; // 0x0
	private static NativeArray`1 _NextFewSteps; // 0x40
	private Boolean m_bForcePosition; // 0x188
	private Vector3 m_CameraTemplateParam; // 0x18c
	private const String PunchTweenID; // 0x0
	private const String ShakeTweenID; // 0x0
	private const String MoveTweenID; // 0x0
	private const String RotateTweenID; // 0x0
	private const Single NOT_SET_DEGREE_DIRECTION; // 0x0
	private Single m_DegreeDirection; // 0x198
	private Single m_LastDir; // 0x19c
	private Single m_OffsetDir; // 0x1a0
	private GameObject m_attackGO; // 0x1a8
	private static List`1 s_TempRenderers; // 0x50
	private Boolean AABBSetByDesign; // 0x1b0
	private static List`1 s_TempVector3s; // 0x58
	public String InitAnimationEvent; // 0x1b8
	private Single m_FollowTrackStartTime; // 0x1c0
	private Single m_FollowTrackTime; // 0x1c4
	private Single m_FollowTrackProgress; // 0x1c8
	private Vector3 m_FollowTrackPos; // 0x1cc
	private Vector3 m_FollowTrackStartPos; // 0x1d8
	private ROTrackComponent m_FollowTrack; // 0x1e8
	private Boolean m_bFirstUpdate; // 0x1f0
	private Boolean isOtherPlayer; // 0x1f1
	private Boolean m_RelativeBoundsInited; // 0x1f2
	private Bounds m_RelativeBounds; // 0x1f4
	private static HashSet`1 m_LuaTypeHideByCamera; // 0x60
	private Boolean _bAlwaysVisible; // 0x20c
	private Boolean _bHideWhenCameraInside; // 0x20d
	public Int32 nielianServerVersion; // 0x210
	public Int32 nielianClientVersion; // 0x214
	private Boolean m_bVisibilityDirty; // 0x218
	private Boolean m_bSelfVisible; // 0x219
	private Boolean m_bHideByCamera; // 0x21a
	public Boolean m_bHideForBornAni; // 0x21b
	public EVisibleHideReasons visibleHideReason; // 0x21c
	private static Queue`1 g_VisibleDirtyROs; // 0x68
	public static Int32[] g_ObjLuaTypeInvisible; // 0x70
	private Int32 effectHideCount; // 0x220
	private Int32 m_forceVisible; // 0x224
	private Int32 m_forceHide; // 0x228
	private GameObject m_SeatObj; // 0x230
	private Int32 m_LastUpdateSeatCnt; // 0x238
	private String m_SeatAttachName; // 0x240
	private Transform m_LeaveTransform; // 0x248
	private String m_SeatParentAttachName; // 0x250
	private RenderObject m_DynamicSeatRO; // 0x258
	private GameObject m_DynamicSeatObj; // 0x260
	private Vector3 m_SeatOffset; // 0x268
	private Boolean m_UpdateRot; // 0x274
	private Quaternion m_SeatDefaultInverseRot; // 0x278
	private KiteA m_KiteA; // 0x288
	private Vector3 m_vKiteOffset; // 0x290
	private KiteB m_KiteB; // 0x2a0
	public Vector3 m_CameraDestOffsetByVehicle; // 0x2a8
	private RenderObject m_Vehicle; // 0x2b8
	private RenderObject m_Driver; // 0x2c0
	private RenderObject m_Passenger; // 0x2c8
	private RenderObject m_Carrier; // 0x2d0
	private WallRunState m_WallRunState; // 0x2d8
	public Boolean bInspectModeEnabledByAnimation; // 0x2e0
	private Boolean bInsepectModeEnabled_lua; // 0x2e1
	private Single m_LastSlope; // 0x2e4
	private Single m_SlopeDegree; // 0x2e8
	private SimpleFloatObject floatingComp; // 0x2f0
	private static Vector3[] JUDGE_IS_IN_SCREEN_VERTICES; // 0x78
	private static Vector3[] JUDGE_IS_IN_SCREEN_SCALES; // 0x80
	private GameObject m_BeizerMoveGO; // 0x2f8
	private BezierSpline m_BezierMoveSpline; // 0x300
	private UInt32 m_BezierMoveId; // 0x308
	private Single m_BezierMoveSpeed; // 0x30c
	private LegoSwing m_LegoSwing; // 0x310
	private LegoSeesaw m_LegoSeesaw; // 0x318
	private String m_DockingBoneName; // 0x320
	private Vector3 m_DockingPosition; // 0x328
	private Int32 m_DockingBoneIndex; // 0x334
	private AssetTaskOperation op2u_d; // 0x338
	private AssetTaskOperation op2u_nm; // 0x340
	private AssetTaskOperation op2u_s; // 0x348
	private Boolean m_HasExclusiveVisibleAttachment; // 0x350
	private Boolean m_ExclusiveVisibleByPrefabPath; // 0x351
	public static Boolean m_bApplyRootMotion; // 0x88
	private Boolean m_bIgnoreHideWeaponNextFrame; // 0x352
	private static CEventTrackUpdateContext s_trackUpdateContext; // 0x90
	private static TrackEventMessage s_eventMsg; // 0x98
	private Boolean m_bEnableEvent; // 0x353
	private List`1 m_OnlyEnableEvents; // 0x358
	private static List`1 listParamType; // 0xa0
	private Int32 m_nRefreshVisibilityFrame; // 0x360
	private Dictionary`2 m_dicBoneName2Index; // 0x368
	public static Boolean m_bDrawBoundingBox; // 0xa8
	private RenderObject _m_parentRO; // 0x370
	private Int32 _depth; // 0x378
	public Boolean hideAttachment; // 0x37c
	public List`1 m_listAttachment; // 0x380
	public List`1 m_listBoneAttachment; // 0x388
	private Action onFirstLateUppdateForAttach; // 0x390
	public Int32 weaponIndex; // 0x398
	public static Boolean FABBIKOptSwitch; // 0xa9
	public Boolean _FABBIKDisabled; // 0x39c
	public static Single MAX_IK_CLIMB_RAYCAST_DISTANCE; // 0xac
	public static Single MAX_IK_CLIMB_RAYCAST_DISTANCE_BACKFORWARD; // 0xb0
	public static Single MAX_IK_CLIMB_HIGH_COLLIDER_RAYCST_DISTANCE; // 0xb4
	public static Single MAX_IK_CLIMB_HAND_TO_PEVIS_DISTANCE_VERTICAL; // 0xb8
	public static Single MAX_IK_CLIMB_FOOT_TO_PEVIS_DISTANCE_VERTICAL; // 0xbc
	public static Single MAX_IK_CLIMB_HAND_TO_PEVIS_DISTANCE_HORIZONTAL; // 0xc0
	public static Single MAX_IK_CLIMB_FOOT_TO_PEVIS_DISTANCE_HORIZONTAL; // 0xc4
	private Boolean _m_IsClimbing; // 0x39d
	private Boolean m_IsAniAllowFBBIK; // 0x39e
	private FullBodyBipedIK m_FBBIKComponent; // 0x3a0
	private LayerMask m_ClimbLayers; // 0x3a8
	private Dictionary`2 m_IKEffectors; // 0x3b0
	private ClimbIKParams m_DefaultParams; // 0x3b8
	private ClimbIKParams m_CurrentParams; // 0x3c0
	private List`1 m_LastRaycastOrigins; // 0x3c8
	private List`1 m_LastRaycastTargets; // 0x3d0
	private List`1 m_LastRaycastNormals; // 0x3d8
	public Boolean m_IsROClimbParamsInited; // 0x3e0
	private RaycastHit m_ClimbForwardCast; // 0x3e4
	private RaycastHit m_ClimbBackwardCast; // 0x410
	public Vector3[] ColliderHitPoints; // 0x440
	public Boolean ClimbUseColliderCenter; // 0x448
	private Collider m_ClimbLowCollider; // 0x450
	private RaycastHit m_ClimbLowColliderCast; // 0x458
	private Vector3 m_ClimbLowColliderHitPos; // 0x484
	private Vector3 m_ClimbLowColliderHitNormal; // 0x490
	private Collider m_ClimbHighCollider; // 0x4a0
	private RaycastHit m_ClimbHighColliderCast; // 0x4a8
	private Vector3 m_ClimbHighColliderHitPos; // 0x4d4
	private Vector3 m_ClimbHighColliderHitNormal; // 0x4e0
	private Boolean m_LeftLegHitNothing; // 0x4ec
	private Boolean m_RightLegHitNothing; // 0x4ed
	public static Single ClimbSphereCollisionRadiusMin; // 0xc8
	public static Single ClimbSphereCollisionRadiusMax; // 0xcc
	public static Single ClimbSphereCollisionMaxDistance; // 0xd0
	public Single ClimbSphereColliderCurrentRadius; // 0x4f0
	public Vector3[] SphereHitPoints; // 0x4f8
	public Vector3[] SphereOriginsPoints; // 0x500
	public Vector3[] ClimbSpherecastDirections; // 0x508
	public Single[] ClimbSpherecastMaxDistance; // 0x510
	public GameObject ClimbSphereLeftHit; // 0x518
	public GameObject ClimbSphereRightHit; // 0x520
	private RaycastHit m_ClimbSphereCast; // 0x528
	private Dictionary`2 m_Colliders; // 0x558
	private Vector3 m_ClimbPositionCached; // 0x560
	private Boolean m_ClimbSamePosition; // 0x56c
	private static Single MAX_IK_CLIMB_DIFF_SAME_POSITION; // 0xd4
	private Boolean m_IsClimbEffectorInited; // 0x56d
	public Boolean showClimbForward; // 0x56e
	public static Boolean IsGroundQuadrupedIKOpen; // 0xd8
	private static UInt32 MAX_GROUNDER_Quadruped_IK; // 0xdc
	private static Dictionary`2 m_GroundQuadrupedDic; // 0xe0
	private GrounderQuadruped m_GrounderQuadruped; // 0x570
	private Int32 m_ReinRightHandIndex; // 0x578
	private Int32 m_ReinLeftHandIndex; // 0x57c
	private Int32 m_DriverRightHandIndex; // 0x580
	private Int32 m_DriverLeftHandIndex; // 0x584
	private Boolean _m_IsGroundQuadruped; // 0x588
	public static Boolean IsGroundIKOpen; // 0xe8
	private static Boolean m_OtherPlayerGroundIK; // 0xe9
	private static UInt32 MAX_GROUNDER_IK; // 0xec
	private static Dictionary`2 m_GroundingDic; // 0xf0
	private Boolean _m_IsGrounding; // 0x589
	public static Single GrounderIKDistance; // 0xf8
	private Boolean _m_IsOnIce; // 0x58a
	private GrounderFBBIK m_GrounderFBBIK; // 0x590
	private Int32 m_GrounderIKLeftThighIndex; // 0x598
	private Int32 m_GrounderIKRightThighIndex; // 0x59c
	private Int32 m_GrounderIKLeftCalfIndex; // 0x5a0
	private Int32 m_GrounderIKRightCalfIndex; // 0x5a4
	public Matrix4x4 m_GrounderIKLeftThighPreMat; // 0x5a8
	public Matrix4x4 m_GrounderIKRightThighPreMat; // 0x5e8
	public Matrix4x4 m_GrounderIKLeftThighPostMat; // 0x628
	public Matrix4x4 m_GrounderIKRightThighPostMat; // 0x668
	public Matrix4x4 m_GrounderIKLeftCalfPreMat; // 0x6a8
	public Matrix4x4 m_GrounderIKRightCalfPreMat; // 0x6e8
	public Matrix4x4 m_GrounderIKLeftCalfPostMat; // 0x728
	public Matrix4x4 m_GrounderIKRightCalfPostMat; // 0x768
	public Boolean m_GrounderIKClothAdjust; // 0x7a8
	private Boolean _m_WallSmoothing; // 0x7a9
	public Boolean m_EnableWallSmooth; // 0x7aa
	private Int32 m_WallRunStage; // 0x7ac
	private Int32 m_LastWallRunStage; // 0x7b0
	public Quaternion onWallRotaion; // 0x7b4
	public Vector3 onWallDelta; // 0x7c4
	private Transform m_SeatIKLeftHand; // 0x7d0
	private Transform m_SeatIKRightHand; // 0x7d8
	private Int32 m_SeatIKLeftHandBoneIndex; // 0x7e0
	private Int32 m_SeatIKRightHandBoneIndex; // 0x7e4
	private LegoSwing m_SeatLego; // 0x7e8
	private Boolean m_IsSeating; // 0x7f0
	private Boolean _m_bEnableCatchObjectIK; // 0x7f1
	private Int32 m_nCatchedRenderObjectId; // 0x7f4
	private Int32[] m_arrayCatchBoneIndex; // 0x7f8
	private Single[] m_fCatchWeight; // 0x800
	private Single[] m_fCachReach; // 0x808
	private Vector3[] m_arrayCatchOffsetOfBone; // 0x810
	private Boolean _m_IsOrientatingWind; // 0x818
	public Single[] m_OrientationIKRadius; // 0x820
	public Single[] m_OrientationIKHeight; // 0x828
	public Single[] m_OrientationIKLeftRange; // 0x830
	public Single[] m_OrientationRightLeftRange; // 0x838
	public Vector3 m_OrientationIKLeftHandPos; // 0x840
	public Vector3 m_OrientationIKRightHandPos; // 0x84c
	public Single m_LeftHandAngle; // 0x858
	public Single m_RightHandAngle; // 0x85c
	private Single m_WindOrientation; // 0x860
	private LimbIK m_LeftArmIK; // 0x868
	private LimbIK m_RightArmIK; // 0x870
	private Boolean m_IsTrackLeftHandIKEnabled; // 0x878
	private Boolean m_IsTrackRightHandIKEnabled; // 0x879
	private Transform m_LeftHandIKTransform_Track; // 0x880
	private Animator m_LeftHandIKAnimator_Track; // 0x888
	private String m_LeftHandIKBoneName_Track; // 0x890
	private Int32 m_LeftHandIKBoneIndex_Track; // 0x898
	private Vector3 m_LeftHandIKOffset_Track; // 0x89c
	private Transform m_RightHandIKTransform_Track; // 0x8a8
	private Animator m_RightHandIKAnimator_Track; // 0x8b0
	private String m_RightHandIKBoneName_Track; // 0x8b8
	private Int32 m_RightHandIKBoneIndex_Track; // 0x8c0
	private Vector3 m_RightHandIKOffset_Track; // 0x8c4
	private Boolean m_IsTrackLookAtEnabled; // 0x8d0
	private Transform m_LookAtIKTransform_Track; // 0x8d8
	private Animator m_LookAtIKAnimator_Track; // 0x8e0
	private String m_LookAtIKBoneName_Track; // 0x8e8
	private Int32 m_LookAtIKBoneIndex_Track; // 0x8f0
	public static LayerMask MoveColiisionLayers; // 0xfc
	public static LayerMask SphereCollisionLayers; // 0x100
	private Single ColliderDist; // 0x8f4
	private static Single DefaultColliderDist; // 0x104
	private Single ColliderCheckMaxDist; // 0x8f8
	private Single ColliderHeadHeightOffset; // 0x8fc
	private Boolean EnableSphereCheckForTransition; // 0x900
	private Single ColliderSphereRadius; // 0x904
	private static Single DefaultColliderSphereRaduis; // 0x108
	private Single ColliderSphereHeightOffset; // 0x908
	public Vector3 ColliderCheckBinNormal; // 0x90c
	private static Single AngleIgnoredInColliderCheck; // 0x10c
	private static Single AnglePointInColliderCheck; // 0x110
	private Vector3 <CollisionHeadPosition>k__BackingField; // 0x918
	private Vector3 <CollisionROPosition>k__BackingField; // 0x924
	private Vector3 <CollisionForward>k__BackingField; // 0x930
	public RaycastHit headHit; // 0x93c
	public RaycastHit ROHit; // 0x968
	private Single HorseCheckBias; // 0x994
	private Single AutoFindPathCheckDist; // 0x998
	private Boolean ROCollisionShow; // 0x99c
	private String[] SphereBoneNames; // 0x9a0
	public GameObject[] FrontSpheres; // 0x9a8
	private Int32[] SphereBoneIndex; // 0x9b0
	private Boolean <IsFrontSpheresInited>k__BackingField; // 0x9b8
	public List`1 SphereHitGOs; // 0x9c0
	public Collider m_collider; // 0x9c8
	public EEffectRelationshipType m_FxRelationShip; // 0x9d0
	public Boolean setOwnerRenderObject; // 0x9d4
	private RenderObject m_OwnerRenderObject; // 0x9d8
	private UInt32 m_OwnerRenderObjectID; // 0x9e0
	private EObj_Lua_Type m_OwnerRenderObjectType; // 0x9e4
	private Boolean disableTrackEffectScale; // 0x9e8
	public Boolean supportCombatFx; // 0x9e9
	public Dictionary`2 replaceRules; // 0x9f0
	private String footprintReplaceFashion; // 0x9f8
	private String footprintReplaceWaterSurface; // 0xa00
	private Vector3 weaponHsv0; // 0xa08
	private Vector3 weaponHsv1; // 0xa14
	private Boolean isFashionAttachment; // 0xa20
	private AssetTaskOperation m_RippleEffectLoadTaskOperation; // 0xa28
	private List`1 rippleEffectGo; // 0xa30
	private List`1 ripplePosOffset; // 0xa38
	private Single rippleStrength; // 0xa40
	public Boolean forceEnableFacialAnim; // 0xa44
	private Material YuanMengLeMat; // 0xa48
	private Single emojiNum; // 0xa50
	private OverrideMaterialPropertiesBlock m_HairBlock; // 0xa58
	public const Int32 InvalidateLODRefPointFrames; // 0x0
	public const Single SqrLODCullStateChangeDelta; // 0x0
	private Int32 m_LODRefPointFrame; // 0xa60
	private Vector3 m_LODRefPoint; // 0xa64
	private Int32 m_LastGetBoundingRadiusFrame; // 0xa70
	public Int32 LODEntityIndex; // 0xa74
	private Single m_BoundingRadius; // 0xa78
	private Boolean m_CullingStateChanged; // 0xa7c
	private LODType m_LODType; // 0xa80
	private LODType m_CustomLODType; // 0xa84
	public Single ScreenSize; // 0xa88
	private ERenderObjectLODLayer m_FixedLODLayer; // 0xa8c
	private Int32 m_LODLevel; // 0xa90
	private Byte m_LODSettingVersion; // 0xa94
	public static Boolean LookAtIKOptSwitch; // 0x114
	public Boolean LookAtSwitch; // 0xa95
	public LookAtState m_LookAtState; // 0xa98
	private LookAtIK m_lookAtIKComponent; // 0xaa0
	private Single m_LookAtWeight; // 0xaa8
	private Single <LookAtWeightCurved>k__BackingField; // 0xaac
	private Boolean m_LookAtEnable; // 0xab0
	private Boolean m_IsIdle; // 0xab1
	private Boolean TryAddLookAt; // 0xab2
	public RenderObject LookAtTarget; // 0xab8
	public RenderObject SelectTarget; // 0xac0
	public Boolean ForbidLookToSelect; // 0xac8
	public Boolean ForbidLookToCamera; // 0xac9
	private Single m_TurnSpeed; // 0xacc
	private Dictionary`2 m_LookParams; // 0xad0
	private LookAtParam m_lookAtParam; // 0xad8
	private Int32 m_nLookAtParamIndex; // 0xae0
	public Boolean NeedAddLookAtIK; // 0xae4
	public Single m_LookAtMinDist; // 0xae8
	public Vector3 IKTargetPosition; // 0xaec
	private Boolean bMainPlayerChangePosition; // 0xaf8
	private static UpdateEngineObjectPos m_RoChangePostionCallback; // 0x118
	private static GetMainPlayerEngineObjectPos s_GetMainPlayerEngineObjectPos; // 0x120
	private Boolean m_bMainPlayerInited; // 0xaf9
	private List`1 PinList; // 0xb00
	protected AssetTaskOperation _prefabLoadOperation; // 0xb08
	protected InstantiateTask _PrefabInstantiateTask; // 0xb10
	protected AssetTaskOperation _effectPartDataLoadOperation; // 0xb18
	private ERenderObjectPerfType m_CustomPerfType; // 0xb20
	public Boolean forceDisableDynamicBone; // 0xb24
	public Boolean SMRBoundBoxDirty; // 0xb25
	private Boolean _ClothDirty; // 0xb26
	private Boolean m_IsSufferNearWall; // 0xb27
	private Transform m_SufferWall; // 0xb28
	public RaycastHit m_SufferRaycast; // 0xb30
	private Vector3 m_SufferWallLastForward; // 0xb5c
	private Boolean isSwimming; // 0xb68
	private Boolean m_bWaterDash; // 0xb69
	private Boolean m_bWaterSurf; // 0xb6a
	public Single swimmingDepth; // 0xb6c
	public Single swimmingDepthMoving; // 0xb70
	public Single dashDepth; // 0xb74
	private Nullable`1 lastWaterDepth; // 0xb78
	public Boolean bInWaterTriggerArea; // 0xb80
	private Boolean bLastInWater; // 0xb81
	private Material yuanmengleMaterial; // 0xb88
	public DynamicBoneReferenceInfo dynamicBoneReferenceInfo; // 0xb90

	// Properties
	public static Dictionary`2 AllRenderObjects { get; }
