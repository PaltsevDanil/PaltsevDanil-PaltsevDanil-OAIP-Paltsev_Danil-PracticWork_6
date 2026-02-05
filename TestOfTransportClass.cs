using System;
using TransportClass;

class Program
{
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

};