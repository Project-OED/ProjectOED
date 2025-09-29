using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifeScope : LifetimeScope
{
    [SerializeField] private ToggleUI _mainUI;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<UIManager>(Lifetime.Singleton);

        builder.RegisterComponent(_mainUI);
    }
}
