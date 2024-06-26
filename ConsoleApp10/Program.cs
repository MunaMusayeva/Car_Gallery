using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class Car
{
    public int Id { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Marka}-{Model}-{Year}";
    }
}

public class CarGallery
{
    public string Name { get; set; }
    public List<Car> Cars { get; set; } = new List<Car>();

    public void AddCar(Car car)
    {
        Cars.Add(car);
    }

    public void RemoveCar(int id)
    {
        Cars.RemoveAll(c => c.Id == id);
    }

    public List<Car> GetAllCars()
    {
        return Cars;
    }

    public Car GetById(int id)
    {
        return Cars.Find(c => c.Id == id);
    }

    public void Serialize()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(this, options);
        File.WriteAllText("car.json", jsonString);
    }

    public static CarGallery Deserialize()
    {
        string jsonString = File.ReadAllText("car.json");
        return JsonSerializer.Deserialize<CarGallery>(jsonString);
    }
}

class Program
{
    static void Main(string[] args)
    {
        CarGallery gallery;

        if (File.Exists("car.json"))
        {
            gallery = CarGallery.Deserialize();
            Console.WriteLine("Gallery loaded from file.");
        }
        else
        {
            gallery = new CarGallery();
            Console.WriteLine("New gallery created.");
        }

        gallery.Name = "My Car Gallery";

        while (true)
        {
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Remove Car");
            Console.WriteLine("3. Get All Cars");
            Console.WriteLine("4. Get Car By ID");
            Console.WriteLine("5. Save Gallery");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Car car = new Car();
                    Console.Write("Enter Car ID: ");
                    car.Id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Car Marka: ");
                    car.Marka = Console.ReadLine();
                    Console.Write("Enter Car Model: ");
                    car.Model = Console.ReadLine();
                    Console.Write("Enter Car Year: ");
                    car.Year = int.Parse(Console.ReadLine());
                    gallery.AddCar(car);
                    break;

                case "2":
                    Console.Write("Enter Car ID to remove: ");
                    int removeId = int.Parse(Console.ReadLine());
                    gallery.RemoveCar(removeId);
                    break;

                case "3":
                    List<Car> cars = gallery.GetAllCars();
                    foreach (var c in cars)
                    {
                        Console.WriteLine(c);
                    }
                    break;

                case "4":
                    Console.Write("Enter Car ID to get: ");
                    int getId = int.Parse(Console.ReadLine());
                    Car foundCar = gallery.GetById(getId);
                    if (foundCar != null)
                    {
                        Console.WriteLine(foundCar);
                    }
                    else
                    {
                        Console.WriteLine("Car not found.");
                    }
                    break;

                case "5":
                    gallery.Serialize();
                    Console.WriteLine("Gallery saved successfully.");
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
