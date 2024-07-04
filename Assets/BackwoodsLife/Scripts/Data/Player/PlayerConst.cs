namespace Game.Scripts.Player.Const
{
    public static class PlayerConst
    {
        public const string PlayerIdPrefsKey = "PlayerIdKey";

        // Animator
        // Animations names
        public const string Idle = "Idle";
        public const string IdleCombat = "IdleCombat";
        public const string Buff = "Buff";
        public const string Run = "RunForward";

        public const string Walk = "WalkForward";

        // Animations settings
        public const float AnimationCrossFade = .3f;

        public const string Gather = "Gathering";
        public const string Mining = "MiningLoop";

        // NavMesh
        public const float NavMeshStopDistance = .1f;
    }
}
