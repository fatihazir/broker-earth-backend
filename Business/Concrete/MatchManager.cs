using System;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Entities.Concrete;
using Core.DataAccess.Predicate;

namespace Business.Concrete
{
	public class MatchManager : IMatchService
	{
        IShipDal _shipDal;
        ILoadDal _loadDal;
        IBrokerDal _brokerDal;
        private readonly UserManager<ApplicationUser> _userManager;

        public MatchManager(IShipDal shipDal, IBrokerDal brokerDal, UserManager<ApplicationUser> userManager, ILoadDal loadDal)
        {
            _shipDal = shipDal;
            _brokerDal = brokerDal;
            _userManager = userManager;
            _loadDal = loadDal;
        }

        public async Task<IResult> GetAvailableLoadsForShip(int shipId, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Ship? ship = await _shipDal.Get(s => s.Id == shipId);

            if (ship is null)
                return new ErrorResult("Ship can not be found.");

            double maxWeight = (double)(ship.DeadWeight * (1 + ship.ConfidenceInterval));

            var dbFilter = PredicateBuilder.True<Load>();
            dbFilter = dbFilter.And(l => l.BrokerId == usersBroker.Id);
            dbFilter = dbFilter.And(l => l.IsDeleted == false);
            dbFilter = dbFilter.And(l => l.LoadWeight <= maxWeight);
            dbFilter = dbFilter.And(l => l.LayCanFrom <= ship.AvailableFrom && l.LayCanTo >= ship.AvailableFrom);

            List<Load> loads = await _loadDal.GetAll(dbFilter);

            // Yüklerin gemiye olan mesafelerini hesaplayıp bir tuple listesi olarak tutacağız
            var distances = loads.Select(load => new Tuple<Load, double>(load, CalculateDistance((double)ship.Latitude, (double)ship.Longtitude, (double)load.Latitude, (double)load.Longtitude))).ToList();

            // Mesafelere göre yükleri sıralıyoruz (en yakından uzağa)
            distances.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            // Sıralı yükleri alıyoruz
            List<Load> sortedLoads = distances.Select(distance => distance.Item1).ToList();

            return new SuccessDataResult<List<Load>>(sortedLoads);
        }

        public async Task<IResult> GetAvailableShipsForLoad(int loadId, ApplicationUser user)
        {
            Broker? usersBroker = await _brokerDal.Get(b => b.BrokerId == user.Id || b.AssistantId == user.Id);

            if (usersBroker is null)
                return new ErrorResult("Your broker account does not exist.");

            Load? load = await _loadDal.Get(l => l.Id == loadId);

            if (load is null)
                return new ErrorResult("Load can not be found.");

            var dbFilter = PredicateBuilder.True<Ship>();
            dbFilter = dbFilter.And(s => s.BrokerId == usersBroker.Id);
            dbFilter = dbFilter.And(s => s.IsDeleted == false);
            dbFilter = dbFilter.And(s => s.DeadWeight * (1 + s.ConfidenceInterval) >= load.LoadWeight);
            dbFilter = dbFilter.And(s => s.AvailableFrom >= load.LayCanFrom && s.AvailableFrom <= load.LayCanTo);

            List<Ship> ships = await _shipDal.GetAll(dbFilter);

            // Gemileri filtreledikten sonra, her bir gemi ile yük arasındaki mesafeyi hesapla
            var distances = ships.Select(ship => new Tuple<Ship, double>(ship, CalculateDistance((double)load.Latitude, (double)load.Longtitude, (double)ship.Latitude, (double)ship.Longtitude))).ToList();

            // Mesafelere göre gemileri sırala (en yakından uzağa)
            distances.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            // Sıralı gemileri al
            List<Ship> sortedShips = distances.Select(distance => distance.Item1).ToList();

            return new SuccessDataResult<List<Ship>>(sortedShips);
        }


        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // İki koordinat arasındaki mesafeyi hesaplamak için uygun bir formül kullanılabilir
            // Burada basit bir örnek olarak, iki nokta arasındaki Euclidean mesafeyi kullanıyoruz
            double deltaX = lat1 - lat2;
            double deltaY = lon1 - lon2;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}

