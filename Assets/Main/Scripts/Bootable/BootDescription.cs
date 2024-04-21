namespace Ca2d.Toolkit.Bootable
{
    public struct BootDescription
    {
        public readonly string Id;

        public readonly IBootCondition Condition;

        public readonly IBootExecutor[] Executors;
    }
}