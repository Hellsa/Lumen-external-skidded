namespace SkiddingApp
{
    /// <summary>
    /// Direct mirror of Offsets.hpp - Names and values strictly match the C++ dump.
    /// version-ec412128eba3476e
    /// </summary>
    internal static class Offsets
    {
        public static class VisualEngine
        {
            public const long ViewMatrix = 0x140;
            public const long RenderView = 0xb80;
            public const long Pointer = 0x7bd71f8;
            public const long FakeDataModel = 0x1d0;
        }

        public static class DataModel
        {
            public const long PlaceId = 0x1a0;
            public const long GameId = 0x198;
            public const long GameLoaded = 0x638;
            public const long CreatorId = 0x190;
            public const long Workspace = 0x178;
            public const long ServerIP = 0x620;
            public const long ScriptContext = 0x430;
            public const long JobId = 0x138;
        }

        public static class Instance
        {
            public const long Parent = 0x70;
            public const long ClassDescriptor = 0x18;
            public const long ChildrenStart = 0x78;
            public const long ChildrenEnd = 0x8;
            public const long Name = 0xb0;
            public const long ClassName = 0x8;
        }

        public static class Player
        {
            public const long LocalPlayer = 0x138;
            public const long ModelInstance = 0x3a8;
            public const long UserId = 0x2d8;
            public const long DisplayName = 0x130;
            public const long TeamColor = 0x374;
            public const long Team = 0x2b0;
            public const long CameraMaxZoomDist = 0x330;
            public const long CameraMinZoomDist = 0x334;
        }

        public static class BasePart
        {
            public const long Primitive = 0x148;
            public const long Reflectance = 0xec;
            public const long Color3 = 0x194;
            public const long Transparency = 0xf0;
            public const long CastShadow = 0xf5;
            public const long Locked = 0xf6;
            public const long Massless = 0xf7;
        }

        public static class Primitive
        {
            public const long Position = 0xec;
            public const long CFrame = 0xc8;
            public const long Rotation = 0xc8;
            public const long Size = 0x1b8;
            public const long AssemblyLinearVelocity = 0xf8;
            public const long AssemblyAngularVelocity = 0x104;
            public const long Material = 0x236;
            public const long Shape = 0x1b1;
            public const long PrimitiveFlags = 0x1b6;
            public const long Owner = 0x200;
        }

        public static class Humanoid
        {
            public const long CameraOffset = 0x140;
            public const long UseJumpPower = 0x1ec;
            public const long AutoJumpEnabled = 0x1e0;
            public const long Health = 0x194;
            public const long MaxHealth = 0x1b4;
            public const long WalkSpeed = 0x1dc;
            public const long WalkSpeedCheck = 0x3c4;
            public const long JumpPower = 0x1b0;
            public const long JumpHeight = 0x1ac;
            public const long HipHeight = 0x1a0;
            public const long HealthDisplayDistance = 0x198;
            public const long MaxSlopeAngle = 0x1b8;
            public const long NameDisplayDistance = 0x1bc;
            public const long WalkToPoint = 0x17c;
            public const long FloorMaterial = 0x190;
            public const long WalkTimer = 0x408;
            public const long WalkToPart = 0x130;
            public const long AutoRotate = 0x1e1;
            public const long Sit = 0x1ea;
            public const long BreakJointsOnDeath = 0x1e3;
            public const long RequiresNeck = 0x1e9;
            public const long EvaluateStateMachine = 0x1e4;
            public const long RigType = 0x1cc;
            public const long TargetPoint = 0x164;
            public const long HumanoidState = 0x8a8;
            public const long HumanoidStateID = 0x20;
        }

        public static class Workspace
        {
            public const long World = 0x408;
            public const long ReadOnlyGravity = 0x9e0;
            public const long CurrentCamera = 0x4b0;
        }

        public static class Camera
        {
            public const long DiagonalFieldOfView = 0x15c;
            public const long MaxAxisFieldOfView = 0x15c;
            public const long FieldOfView = 0x160;
            public const long Position = 0x11c;
            public const long CFrame = 0xf8;
            public const long ViewportInt16 = 0x2ac;
            public const long ViewportSize = 0x2e8;
            public const long CameraSubject = 0xe8;
            public const long CameraType = 0x158;
        }

        public static class Model
        {
            public const long PrimaryPart = 0x278;
            public const long Scale = 0x164;
        }

        public static class FakeDataModel
        {
            public const long Pointer = 0x74f8758;
            public const long RealDataModel = 0x1d0;
        }
    }
}
