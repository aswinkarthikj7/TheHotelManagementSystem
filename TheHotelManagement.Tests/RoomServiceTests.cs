using Core.Models;
using Core.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System.Reflection;

namespace TheHotelManagement.Tests
{
    public class RoomServiceTests
    {
        private Mock<IMongoClient> mongoClient;
        private Mock<IMongoDatabase> mongodb;
        private Mock<IMongoCollection<Room>> roomCollection;
        private List<Room> roomList;
        private Mock<IAsyncCursor<Room>> roomCursor;
        private IOptions<HotelDatabaseSettings> settings;

        public RoomServiceTests()
        {
            this.mongoClient = new Mock<IMongoClient>();
            this.roomCollection = new Mock<IMongoCollection<Room>>();
            this.mongodb = new Mock<IMongoDatabase>();
            this.roomCursor = new Mock<IAsyncCursor<Room>>();
            this.roomList = new List<Room>() { new Room() };
            this.settings = Options.Create(new HotelDatabaseSettings { RoomCollectionName = "Rooms" });
        }

        private void InitializeMongoDb()
        {
            this.mongodb.Setup(x => x.GetCollection<Room>("Rooms",
                default)).Returns(this.roomCollection.Object);
            this.mongoClient.Setup(x => x.GetDatabase(It.IsAny<string>(),
                default)).Returns(this.mongodb.Object);
        }

        private void InitializeMongoRoomCollection()
        {
            this.roomCollection.Setup(_ => _.FindAsync(It.IsAny<FilterDefinition<Room>>(), It.IsAny<FindOptions<Room, Room>>(), It.IsAny<CancellationToken>())).ReturnsAsync(this.roomCursor.Object);
            this.roomCollection.Setup(x => x.AggregateAsync(It.IsAny<PipelineDefinition<Room, Room>>(), It.IsAny<AggregateOptions>(), It.IsAny<CancellationToken>())).ReturnsAsync(this.roomCursor.Object);
            this.InitializeMongoDb();
        }

        [Fact]
        public async Task CreateAsync_ValidData_ReturnEntity()
        {
            this.InitializeMongoRoomCollection();
            var roomService = new RoomService(this.settings, this.mongoClient.Object);
            var response = await roomService.CreateAsync(new Core.Models.Room()
            {
                CategoryId = "123",
                Name = "S-101"
            });

            Assert.NotNull(response.Name);
            Assert.NotNull(response.CategoryId);
            Assert.NotNull(response.Id);
            Assert.NotEmpty(response.Name);
            Assert.NotEmpty(response.CategoryId);
            Assert.NotEmpty(response.Id);
            Assert.Equal("123", response.CategoryId);
            Assert.Equal("S-101", response.Name);
        }

        [Fact]
        public async Task GetAllAsync_ValidData_ReturnAllEntities()
        {
            this.InitializeMongoRoomCollection();

            var roomService = new RoomService(this.settings, this.mongoClient.Object);
            var response = await roomService.GetAllAsync();

            Assert.NotNull(response);
        }
    }
}