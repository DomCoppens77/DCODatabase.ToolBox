using Microsoft.Extensions.DependencyInjection;
using System;

namespace DCODatabase.ToolBox.Patterns
{
    public abstract class ServLocator
    {
        protected IServiceProvider Container { get; set; }

        public ServLocator()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Container = serviceCollection.BuildServiceProvider();
        }
        public abstract void ConfigureServices(IServiceCollection serviceCollection);
    }
}
