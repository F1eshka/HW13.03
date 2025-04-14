using System;
using System.Data.SqlClient;

class Program
{
    private static readonly string connectionString =
        @"Server=DESKTOP-Q4ID39U\SQLEXPRESS;Database=VAndF;Trusted_Connection=True;";
    private static SqlConnection _connection = null;

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. -> Подключиться к базе данных");
            Console.WriteLine("2. -> Отключиться от базы данных");
            Console.WriteLine("3. -> Выход");
            Console.Write("Выберите опцию (1-3): ");

            string choice = Console.ReadLine();
            if (!int.TryParse(choice, out int option) || option < 1 || option > 3)
            {
                Console.WriteLine("Попробуйте еще раз");
                continue;
            }

            switch (option)
            {
                case 1:
                    ConnectToDatabase();
                    break;
                case 2:
                    DisconnectFromDatabase();
                    break;
                case 3:
                    Console.WriteLine("Программа завершена");
                    return;
            }
        }
    }

    static void ConnectToDatabase()
    {
        try
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            Console.WriteLine("Подключение к базе данных успешно!");
            Console.WriteLine($"Название базы данных: {_connection.Database}");
            Console.WriteLine($"Сервер: {_connection.DataSource}");
            Console.WriteLine($"Состояние: {_connection.State}");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Ошибка подключения к базе данных:");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Неизвестная ошибка:");
            Console.WriteLine(ex.Message);
        }
    }

    static void DisconnectFromDatabase()
    {
        try
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                Console.WriteLine("Отключение от базы данных успешно!");
            }
            else
            {
                Console.WriteLine("Подключение уже закрыто или не инициализировано");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при отключении:");
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}