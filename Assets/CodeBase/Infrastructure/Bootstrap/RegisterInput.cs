using CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Bootstrap
{
    public class RegisterInput : MonoInstaller
    {
        [SerializeField] private InputService inputService;
        public override void InstallBindings() => 
            BindInputService();

        private void BindInputService()
        {
            Container.
                Bind<IInputService>().
                To<InputService>().
                FromInstance(inputService).
                AsSingle();
        }
    }
}