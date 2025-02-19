
// Вариант 4

/*
Класс – автомобиль (например, для салона продажи). 
Примеры свойств: мощность, количество пассажирских мест, модель. 
Примеры методов: Перекрасить, Списать, Заказать у производителя. 
*/

internal class Program
{
    private static void Main(string[] args)
    {
        Car Car1 = new Car();
        Car Car2 = new Car(1234, "BMW", "M5", "Black", 550, 4);
        Car Car3 = new Car(Car2);
        Car3.Repaint("Red");
        Car1.setStateNumber(343);
        Car1.Repaint(Colour.Blue);
        Car2.addPower(50);
        Car3.setStateNumber(2342);
        Car.getClassInfo();
        Console.WriteLine("\nCar 1");
        Car1.getCarsInfo();
        Console.WriteLine("\nCar 2");
        Car2.getCarsInfo();
        Console.WriteLine("\nCar 3");
        Car3.getCarsInfo();
    }
}


enum Colour
{
    Red,
    Blue,
    Green,
    Yellow,
    Black,
    White
}


internal class Car
{
    public const int yearOfProduction = 1991;
    private int stateNumber;
    public string brand;
    public string model;
    public string colour;
    public int power;
    public int seats;
    public Car()
    {
        stateNumber = 0;
        brand = "Not defined";
        model = "Not defined";
        colour = "Not defined";
        power = 0;
        seats = 0;
    }
    public Car(int stateNumber, string brand, string model, string colour , int power, int seats)
    {
        this.stateNumber = stateNumber;
        this.brand = brand;
        this.model = model;
        this.colour = colour;
        this.power = power;
        this.seats = seats;
    }
    public Car(Car car)
    {
        this.stateNumber = car.stateNumber;
        this.brand = car.brand;
        this.model = car.model;
        this.colour = car.colour;
        this.power = car.power;
        this.seats = car.seats;
    }
    public void Repaint(string colour)
    {
        this.colour = colour;
    }
    public void Repaint(Colour colour)
    {
        switch(colour)
        {
            case Colour.Red:
                this.colour = "Red";
                break;
            case Colour.Blue:
                this.colour = "Blue";
                break;
            case Colour.Green:
                this.colour = "Green";
                break;
            case Colour.Yellow:
                this.colour = "Yellow";
                break;
            case Colour.Black:
                this.colour = "Black";
                break;
            case Colour.White:
                this.colour = "White";
                break;
        }
    }
    public void setStateNumber(int newNumber)
    {
        stateNumber = newNumber;
    }
    public void getStateNumber()
    {
        Console.WriteLine("State number of your car: " + stateNumber);
    }
    public void addPower(int power)
    {
        this.power += power;
    }

    public void getCarsInfo()
    {
        Console.WriteLine("State number: " + stateNumber);
        Console.WriteLine("Brand: " + brand);
        Console.WriteLine("Model: " + model);
        Console.WriteLine("Colour: " + colour);
        Console.WriteLine("Power: " + power);
        Console.WriteLine("Seats: " + seats);
    }

    static public void getClassInfo()
    {
        Console.WriteLine("This is a class for cars");
    }
    ~Car()
    {
        Console.WriteLine("The car is removed");
    }
}
