using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Serialization;

namespace Tests
{
    [Collection(SerializationTestClusterFixture.CollectionName)]
    public class SerializationIssueTests
    {
        private readonly Serializer _serializer;

        public SerializationIssueTests(SerializationTestClusterFixture fixture)
        {
            _serializer = fixture.Cluster.Client.ServiceProvider.GetRequiredService<Serializer>();
        }

        [Fact]
        public void Can_Roundtrip_WithListOfObject_Fruit()
        {
            var original = new WithListOfObject
            {
                Items = new List<object> { new Fruit("Banana"), new Fruit("Mango") },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfObject>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_WithListOfObject_Apple()
        {
            var original = new WithListOfObject
            {
                Items = new List<object> { new Apple("Golden Delicious"), new Apple("Granny Smith") },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfObject>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_WithListOfObject_Pear()
        {
            var original = new WithListOfObject
            {
                Items = new List<object> { new Pear(1, "Conference"), },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfObject>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_WithListOfFruit_Fruit()
        {
            var original = new WithListOfFruit()
            {
                Items = new List<Fruit> { new Fruit("Banana"), new Fruit("Mango") },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfFruit>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_WithListOfFruit_Apple()
        {
            var original = new WithListOfFruit()
            {
                Items = new List<Fruit> { new Apple("Golden Delicious"), new Apple("Granny Smith") },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfFruit>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_WithListOfApple_Apple()
        {
            var original = new WithListOfApple()
            {
                Items = new List<Apple> { new Apple("Golden Delicious"), new Apple("Granny Smith") },
                Bar = new Foo(Guid.NewGuid())
            };

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<WithListOfApple>(bytes);

            Assert.NotNull(deserialized.Bar);
            Assert.Equal(original.Bar, deserialized.Bar);
        }

        [Fact]
        public void Can_Roundtrip_Fruit_Apple()
        {
            Fruit original = new Apple("Golden Delicious");

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<Fruit>(bytes);

            Assert.NotNull(deserialized);
            Assert.Equal(original, deserialized);
        }

        [Fact]
        public void Can_Roundtrip_Foo()
        {
            var original = new Foo(Guid.NewGuid());

            var bytes = _serializer.SerializeToArray(original);

            var deserialized = _serializer.Deserialize<Foo>(bytes);

            Assert.NotNull(deserialized);
            Assert.Equal(original, deserialized);
        }
    }

    [GenerateSerializer]
    public class WithListOfObject
    {
        [Id(1)]
        public List<object> Items { get; set; }

        [Id(2)]
        public Foo Bar { get; set; }
    }

    [GenerateSerializer]
    public class WithListOfFruit
    {
        [Id(1)]
        public List<Fruit> Items { get; set; }

        [Id(2)]
        public Foo Bar { get; set; }
    }

    [GenerateSerializer]
    public class WithListOfApple
    {
        [Id(1)]
        public List<Apple> Items { get; set; }

        [Id(2)]
        public Foo Bar { get; set; }
    }

    [GenerateSerializer]
    public record Fruit([property: Id(1)] string Name);

    [GenerateSerializer]
    public record Apple(string Name) : Fruit(Name);


    [GenerateSerializer]
    public record Pear([property: Id(1)] int Id, string Name) : Fruit(Name);


    [GenerateSerializer]
    public record Foo([property: Id(0)] Guid Id);
}