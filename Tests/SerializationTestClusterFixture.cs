using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.TestingHost;

namespace Tests;

public class SerializationTestClusterFixture : IDisposable
{
    public const string CollectionName = "SerializationTestClusterFixture";

    [CollectionDefinition(CollectionName)]
    public class ClusterCollection : ICollectionFixture<SerializationTestClusterFixture>
    {
    }

    public SerializationTestClusterFixture()
    {
        var builder = new TestClusterBuilder(3);
        builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();
        builder.AddClientBuilderConfigurator<TestSiloClientBuilder>();

        Cluster = builder.Build();
        Cluster.Deploy();
    }

    public void Dispose()
    {
        Cluster.StopAllSilos();
    }

    public TestCluster Cluster { get; private set; }

    public class TestSiloClientBuilder : IClientBuilderConfigurator
    {
        public void Configure(IConfiguration configuration, IClientBuilder clientBuilder)
        {
        }
    }

    public class TestSiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder builder)
        {
            builder.ConfigureServices(services => { });
        }
    }
}