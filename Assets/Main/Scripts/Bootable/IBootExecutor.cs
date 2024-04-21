using System;
using Cysharp.Threading.Tasks;

namespace Ca2d.Toolkit.Bootable
{
    public interface IBootExecutor
    {
        public bool FatalAsExit { get; }
        
        public BootState RegisteredStage { get; }

        public Func<UniTask<bool>> Runner { get; }
    }
}