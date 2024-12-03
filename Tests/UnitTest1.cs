using DAL.AdditionalEntities;
using DAL.Entities;
using HM2.Model;
using System.Collections.ObjectModel;

namespace Tests
{
    public class BookingRoomsModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void recalculateTotalSumPositive()
        {
            //Assert.Pass();
            BookingRoomsModel bookingRoomsModel = new BookingRoomsModel();
            var start = new DateTime(2024, 12, 3);
            var end = new DateTime(2024, 12, 10);

            Capacity capacity = new Capacity();
            capacity.Id = 1;
            capacity.name = "одноместный";
            Comfort comfort = new Comfort();
            comfort.Id = 1;
            comfort.name = "стандартный";

            TypeRoom typeRoom = new TypeRoom();
            typeRoom.Id = 1;
            typeRoom.Capacity = capacity;
            typeRoom.Comfort = comfort;
            typeRoom.IdSize = 1;
            typeRoom.IdComfort = 1;
            typeRoom.cost = 1000;
            typeRoom.description = string.Empty;
            typeRoom.name = string.Empty;
            typeRoom.deleteDate = null;

            TypeRoomExtension type = new TypeRoomExtension(typeRoom , typeRoom.Capacity , typeRoom.Comfort);

            Room room = new Room();
            room.Id = 1;
            room.number = 1;
            room.floor = 2;
            room.IdTypeRoom = 1;
            room.TypeRoom = typeRoom;
            room.CreateDate = DateTime.Now;
            room.DeleteDate = null;

            RoomExtension roomExtension = new RoomExtension(room , type);
            ObservableCollection<StringServiceExtension> stringServiceExtensions = new ObservableCollection<StringServiceExtension>();

            var discountSize = 0.07;

            var result = bookingRoomsModel.recalculateTotalSum(start , end , roomExtension , stringServiceExtensions , discountSize);
            Assert.AreEqual(6510, result);
            
        }

        [Test]
        public void recalculateTotalSumPositiveWithAddServices()
        {
            //Assert.Pass();
            BookingRoomsModel bookingRoomsModel = new BookingRoomsModel();
            var start = new DateTime(2024, 12, 3);
            var end = new DateTime(2024, 12, 10);

            Capacity capacity = new Capacity();
            capacity.Id = 1;
            capacity.name = "одноместный";
            Comfort comfort = new Comfort();
            comfort.Id = 1;
            comfort.name = "стандартный";

            TypeRoom typeRoom = new TypeRoom();
            typeRoom.Id = 1;
            typeRoom.Capacity = capacity;
            typeRoom.Comfort = comfort;
            typeRoom.IdSize = 1;
            typeRoom.IdComfort = 1;
            typeRoom.cost = 1000;
            typeRoom.description = string.Empty;
            typeRoom.name = string.Empty;
            typeRoom.deleteDate = null;

            TypeRoomExtension type = new TypeRoomExtension(typeRoom, typeRoom.Capacity, typeRoom.Comfort);

            Room room = new Room();
            room.Id = 1;
            room.number = 1;
            room.floor = 2;
            room.IdTypeRoom = 1;
            room.TypeRoom = typeRoom;
            room.CreateDate = DateTime.Now;
            room.DeleteDate = null;

            RoomExtension roomExtension = new RoomExtension(room, type);

            AddService addService = new AddService();
            addService.Id = 1;
            addService.name = "¬ентил€тор";
            addService.cost = 500;

            ObservableCollection<StringServiceExtension> stringServiceExtensions = new ObservableCollection<StringServiceExtension>();

            StringService stringService = new StringService();
            stringService.Id = 1;
            stringService.IdBooking = 10;
            stringService.IdAddService = 1;
            stringService.AddService = addService;
            stringService.count = 1;
            stringService.cost = addService.cost;

            stringServiceExtensions.Add(new StringServiceExtension(stringService));

            var discountSize = 0.07;

            var result = bookingRoomsModel.recalculateTotalSum(start, end, roomExtension, stringServiceExtensions, discountSize);
            Assert.AreEqual(6975, result);

        }

        [Test]
        public void recalculateTotalSumWithoutDiscount()
        {
            //Assert.Pass();
            BookingRoomsModel bookingRoomsModel = new BookingRoomsModel();
            var start = new DateTime(2024, 12, 3);
            var end = new DateTime(2024, 12, 10);

            Capacity capacity = new Capacity();
            capacity.Id = 1;
            capacity.name = "одноместный";
            Comfort comfort = new Comfort();
            comfort.Id = 1;
            comfort.name = "стандартный";

            TypeRoom typeRoom = new TypeRoom();
            typeRoom.Id = 1;
            typeRoom.Capacity = capacity;
            typeRoom.Comfort = comfort;
            typeRoom.IdSize = 1;
            typeRoom.IdComfort = 1;
            typeRoom.cost = 1000;
            typeRoom.description = string.Empty;
            typeRoom.name = string.Empty;
            typeRoom.deleteDate = null;

            TypeRoomExtension type = new TypeRoomExtension(typeRoom, typeRoom.Capacity, typeRoom.Comfort);

            Room room = new Room();
            room.Id = 1;
            room.number = 1;
            room.floor = 2;
            room.IdTypeRoom = 1;
            room.TypeRoom = typeRoom;
            room.CreateDate = DateTime.Now;
            room.DeleteDate = null;

            RoomExtension roomExtension = new RoomExtension(room, type);

            AddService addService = new AddService();
            addService.Id = 1;
            addService.name = "¬ентил€тор";
            addService.cost = 500;

            ObservableCollection<StringServiceExtension> stringServiceExtensions = new ObservableCollection<StringServiceExtension>();

            StringService stringService = new StringService();
            stringService.Id = 1;
            stringService.IdBooking = 10;
            stringService.IdAddService = 1;
            stringService.AddService = addService;
            stringService.count = 1;
            stringService.cost = addService.cost;

            stringServiceExtensions.Add(new StringServiceExtension(stringService));

            var discountSize = 1;

            var result = bookingRoomsModel.recalculateTotalSum(start, end, roomExtension, stringServiceExtensions, discountSize);
            Assert.AreEqual(7500, result);
        }

        [Test]
        public void recalculateTotalSumNegative()
        {
            //Assert.Pass();
            BookingRoomsModel bookingRoomsModel = new BookingRoomsModel();
            var start = new DateTime(2024, 12, 10);
            var end = new DateTime(2024, 12, 3);

            Capacity capacity = new Capacity();
            capacity.Id = 1;
            capacity.name = "одноместный";
            Comfort comfort = new Comfort();
            comfort.Id = 1;
            comfort.name = "стандартный";

            TypeRoom typeRoom = new TypeRoom();
            typeRoom.Id = 1;
            typeRoom.Capacity = capacity;
            typeRoom.Comfort = comfort;
            typeRoom.IdSize = 1;
            typeRoom.IdComfort = 1;
            typeRoom.cost = 1000;
            typeRoom.description = string.Empty;
            typeRoom.name = string.Empty;
            typeRoom.deleteDate = null;

            TypeRoomExtension type = new TypeRoomExtension(typeRoom, typeRoom.Capacity, typeRoom.Comfort);

            Room room = new Room();
            room.Id = 1;
            room.number = 1;
            room.floor = 2;
            room.IdTypeRoom = 1;
            room.TypeRoom = typeRoom;
            room.CreateDate = DateTime.Now;
            room.DeleteDate = null;

            RoomExtension roomExtension = new RoomExtension(room, type);

            AddService addService = new AddService();
            addService.Id = 1;
            addService.name = "¬ентил€тор";
            addService.cost = 500;

            ObservableCollection<StringServiceExtension> stringServiceExtensions = new ObservableCollection<StringServiceExtension>();

            StringService stringService = new StringService();
            stringService.Id = 1;
            stringService.IdBooking = 10;
            stringService.IdAddService = 1;
            stringService.AddService = addService;
            stringService.count = 1;
            stringService.cost = addService.cost;

            stringServiceExtensions.Add(new StringServiceExtension(stringService));

            var discountSize = 1;

            //var result = bookingRoomsModel.recalculateTotalSum(start, end, roomExtension, stringServiceExtensions, discountSize);
            var ex = Assert.Throws<Exception>(() => bookingRoomsModel.recalculateTotalSum(start, end, roomExtension, stringServiceExtensions, discountSize));
            Assert.AreEqual("Ќеверна€ дата" , ex.Message);
        }
    }
}