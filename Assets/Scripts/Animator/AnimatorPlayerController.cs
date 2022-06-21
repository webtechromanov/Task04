public static class AnimatorPlayerController
{
    public static class Params
    {
        public const string Run = nameof(Run);
        public const string Jump = nameof(Jump);
        public const string Hurt = nameof(Hurt);
        public const string Death = nameof(Death);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Run = nameof(Run);
        public const string Walk = nameof(Walk);
        public const string Hurt = nameof(Hurt);
        public const string Death = nameof(Death);
    }
}