using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // Додаємо цей простір імен

namespace conditions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int one = 1; int two=2; int three = 3;
            int four = 4; int five = 5; int six = 6;
            int seven = 7; int ten = 10; int eleven = 11;
            //==;<=;>=;<;>;!=;
            if (one == two)
            {
                Console.WriteLine("one=two");
            }
            else
            {
                Console.WriteLine("one!=two");
            }

            int[] digits = { 11, 2, 6, 9, 5,3,2 };
            Console.WriteLine(digits.Length);
            for (int i = 0; i < digits.Length; i++)
            {
                Console.WriteLine(digits[i]);
            }



            string connString = "server=localhost;port=3306;database=dev_db;user=root;password=2RW4X5lRv;";
            MySqlConnection connection = new MySqlConnection(connString);
            try
            {
                connection.Open();
                Console.WriteLine("З'єднання встановлено успішно!");

                string query = "SELECT * FROM users";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Припустимо, у вас є колонка "name"
                        Console.Write(reader["name"].ToString());

                        Console.Write("    ");
                        Console.Write(reader["last_name"].ToString());

                        Console.Write("    ");

                        Console.Write(reader["age"].ToString()); Console.Write("    ");
                        Console.Write(reader["position"].ToString());
                        Console.WriteLine("    ");
                        }
                    }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
