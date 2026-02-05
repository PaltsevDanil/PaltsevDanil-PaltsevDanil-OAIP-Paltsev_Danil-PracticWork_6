using System;
using static TransportClass.Program;

namespace TransportClass
{
    class Program
    {
        abstract class Transport
        {
            public string Name { get; protected set; }
            public int MaxSpeed { get; protected set; }
            public int Capacity { get; protected set; }
            public double FuelConsumption { get; protected set; }

            protected Transport(string name, int maxSpeed, int capacity, double fuelConsumption)
            {
                Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty");
                MaxSpeed = maxSpeed > 0 ? maxSpeed : throw new ArgumentException("Max speed must be positive");
                Capacity = capacity >= 0 ? capacity : throw new ArgumentException("Capacity cannot be negative");
                FuelConsumption = fuelConsumption >= 0 ? fuelConsumption : throw new ArgumentException("FuelConsumption cannot be negative");
            }

            public void TripFuel(double distance)
            {
                if (distance <= 0)
                    throw new ArgumentException("Distance must be positive");
                double fuelNeeded = (distance / 100) * FuelConsumption;
                Console.WriteLine($"{Name} needs {fuelNeeded} liters of fuel for {distance} km.");
            }

            public double SpeedTime(double distance)
            {
                if (distance <= 0)
                    throw new ArgumentException("Distance must be positive");
                double timeInHours = distance / MaxSpeed;
                Console.WriteLine($"{Name} will cover {distance} km in {timeInHours:F2} hours at maximum speed.");
                return timeInHours;
            }

            public virtual void Move()
            {
                Console.WriteLine($"{Name} is moving.");
            }

            public virtual void Stop()
            {
                Console.WriteLine($"{Name} has stopped.");
            }

            //Методы для сокрытия
            public void ShowInfo()
            {
                Console.WriteLine($"Name: {Name}\nMaxSpeed: {MaxSpeed} km/h\nCapacity: {Capacity} people\nFuelConsumption: {FuelConsumption} 1/100km");
            }

            public bool CheckSpeedLimit(int speedLimit)
            {
                bool isLegal = MaxSpeed <= speedLimit;
                Console.WriteLine($"{Name}: Speed limit {speedLimit} km/h - {(isLegal ? "Legal" : "Too fast!")}");
                return isLegal;
            }
        };

        class Motorcycle : Transport
        {
            public MotorcycleType Type { get; set; }
            public bool HasSidecar { get; set; }

            public Motorcycle(string name, int maxSpeed, int capacity, double fuelConsumption, MotorcycleType type, bool hasSidecar) : base(name, maxSpeed, capacity, fuelConsumption)
            {
                Type = type;
                HasSidecar = hasSidecar;
                if (hasSidecar)
                {
                    Capacity = Math.Max(Capacity, 1);
                }
            }

            public void DoWheelie()
            {
                if (Type == MotorcycleType.Sportbike || Type == MotorcycleType.OffRoad)
                    Console.WriteLine($"{Name} is doing wheelie!");
                else
                    Console.WriteLine($"{Name} is not stuable for wheelies.");
            }

            public void RidingStyle()
            {
                string style;
                switch (Type)
                {
                    case MotorcycleType.Sportbike:
                        style = "Sport riding (aggressive)";
                        break;
                    case MotorcycleType.Cruiser:
                        style = "Cruising (relaxed)";
                        break;
                    case MotorcycleType.Scooter:
                        style = "City commuting";
                        break;
                    case MotorcycleType.Touring:
                        style = "Long-distance touring";
                        break;
                    default:
                        style = "General riding";
                        break;
                }
                if (HasSidecar)
                    style += " with sidecar stability";
                Console.WriteLine($"Riding style: {style}");
            }

            public override void Move()
            {
                string sidecarInfo = HasSidecar ? " with sidecar" : "";
                Console.WriteLine($"{Name} ({Type}) motorcycle{sidecarInfo} is moving.");
            }

            public override void Stop()
            {
                Console.WriteLine($"{Name} motorcycle has stopped with a distinctive engine sound.");
            }

            public new void ShowInfo()
            {
                Console.WriteLine("\n=== MOTORCYCLE DETAILS ===\n");
                Console.WriteLine($"Model: {Name}");
                Console.WriteLine($"Type: {Type}");
                Console.WriteLine($"Sidecar: {(HasSidecar ? "Yes" : "No")}");
                Console.WriteLine($"Max Passengers: {Capacity}");
                Console.WriteLine($"Max Speed: {MaxSpeed} km/h");
                Console.WriteLine($"Fuel Consumption: {FuelConsumption} l/100km\n");
            }

            public new bool CheckSpeedLimit(int speedLimit)
            {
                bool baseCheck = base.CheckSpeedLimit(speedLimit);
                if (HasSidecar)
                {
                    Console.WriteLine($"With sidecar: recommended max {speedLimit * 0.8} km/h");
                    return MaxSpeed <= (speedLimit * 0.8);
                }
                return baseCheck;
            }
        };

        public enum MotorcycleType
        {
            Sportbike, Cruiser, Touring, Classic, OffRoad, Adventure, Chopper, Scooter, CafeRacer, Scrambler
        }

        static void Main()
        {
            //Мотоцикл без коляски
            Motorcycle myMotorcycle = new Motorcycle("Yamaha R1", 299, 2, fuelConsumption: 6.5, type: MotorcycleType.Sportbike, hasSidecar: false);
            //Собственные методы
            myMotorcycle.DoWheelie();
            myMotorcycle.RidingStyle();
            //Перегруженные методы
            myMotorcycle.Move();
            myMotorcycle.Stop();
            //Сокрытые методы
            myMotorcycle.ShowInfo();
            myMotorcycle.CheckSpeedLimit(120);
            //Унаследованные методы
            myMotorcycle.TripFuel(300);
            myMotorcycle.SpeedTime(500);

            Console.WriteLine("\n-----------------------------------------------------------");

            //Мотоцикл с коляской
            Motorcycle cruiser = new Motorcycle("Harley Davidson", 180, 3, 8.0, MotorcycleType.Cruiser, true);
            cruiser.ShowInfo();
            cruiser.CheckSpeedLimit(100);
            cruiser.RidingStyle();
            cruiser.DoWheelie();
            cruiser.Move();
            cruiser.Stop();
        }

    }
}