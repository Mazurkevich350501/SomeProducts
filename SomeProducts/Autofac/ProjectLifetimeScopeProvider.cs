using System;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;

namespace SomeProducts.Autofac
{
    public class ProjectLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly IContainer _container;
        private ILifetimeScope _scope;

        public ProjectLifetimeScopeProvider(IContainer container)
        {
            _container = container;
        }

        public ILifetimeScope ApplicationContainer => _container;

        public void EndLifetimeScope()
        {
            if (_scope == null) return;
            _scope.Dispose();
            _scope = null;
        }

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction = null)
        {
            if (_scope == null)
            {
                _scope = (configurationAction == null)
                       ? ApplicationContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag)
                       : ApplicationContainer.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, configurationAction);
            }

            return _scope;
        }
    }
}