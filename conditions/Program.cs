using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // Додаємо цей простір імен

namespace conditions
{
    internal class Program
    {
        struct Student
        {
            public string Name, Surname; public DateTime Birthday;
            public byte Maths, Physics, History, Ukrainian; public double Average;
        }
        static void Main(string[] args)
        {
            //int one = 1; int two = 2; int three = 3;
            //int four = 4; int five = 5; int six = 6;
            //int seven = 7; int ten = 10; int eleven = 11;
            ////==;<=;>=;<;>;!=;
            //if (one == two)
            //{
            //    Console.WriteLine("one=two");
            //}
            //else
            //{
            //    Console.WriteLine("one!=two");
            //}
            
            //int[] digits = { 11, 2, 6, 9, 5, 3, 2 };
            //Console.WriteLine(digits.Length);
            //for (int i = 0; i < digits.Length; i++)
            //{
            //    Console.WriteLine(digits[i]);
            //}

            bool data = false; bool exit = false;
            int loopLenght = 1;
            while (!exit)
            {
                Console.WriteLine("Зробiть вибiр:");
                Console.Write("1 - виведення таблиц1\n" +
                              "2 - додати нового користувача\n" +
                              "3 - видалити користувача \n" +
                              "4 - змінити інформацію\n" +
                              "0 - Вихiд\n->");
                int n = Convert.ToInt32(Console.ReadLine()); Console.Clear();
                switch (n)
                {
                    

                    case 1:
                        {

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
                                        Console.Write(reader["id"].ToString());
                                        Console.Write("  ");
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
                            data = true; break;
                        }

                    case 2:
                        {
                            string connString = "server=localhost;port=3306;database=dev_db;user=root;password=2RW4X5lRv;";
                            using (MySqlConnection conn = new MySqlConnection(connString))
                            {
                                try
                                {
                                    conn.Open();

                                    // SQL запит із плейсхолдерами (@name, @email)
                                    //string sql = "INSERT INTO users (full_name, email, password) VALUES (@name, @email, @pass)";
                                    string sql = "INSERT INTO users (name, last_name, position, age) VALUES(@name, @last_name, @position,@age)";

                                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                                    {
                                        // Додаємо реальні дані замість плейсхолдерів
                                        cmd.Parameters.AddWithValue("@name", "Олексій ");
                                        cmd.Parameters.AddWithValue("@last_name", "Катеренчуук");
                                        cmd.Parameters.AddWithValue("@position", "приберальник");
                                        cmd.Parameters.AddWithValue("@age", "11");

                                        // Виконуємо запит
                                        int rowsAffected = cmd.ExecuteNonQuery();

                                        Console.WriteLine($"Успішно додано рядків: {rowsAffected}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Помилка: " + ex.Message);
                                }
                            }
                            data = true; break;
                        }
                    case 3:
                        {
                            Console.WriteLine("вид1ть id користувача якого збараєтесь видалити");
                            int userIdToRemove = Convert.ToInt32(Console.ReadLine()); Console.Clear();

                            string connString = "server=localhost;port=3306;database=dev_db;user=root;password=2RW4X5lRv;";
                            string sqlExpression = "DELETE FROM Users WHERE Id = @id";
                            MySqlConnection connection = new MySqlConnection(connString);
                            using (MySqlConnection connectio = new MySqlConnection(connString))

                            {
                                try
                                {
                                    connection.Open();
                                    MySqlCommand command = new MySqlCommand(sqlExpression, connection);


                                    command.Parameters.AddWithValue("@id", userIdToRemove);

                                    int number = command.ExecuteNonQuery();

                                    if (number > 0)
                                    {
                                        Console.WriteLine($"Успішно видалено записів: {number}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Запис із таким ID не знайдено.");
                                    }
                                }
                                catch (MySqlException ex)
                                {
                                    // Обробка специфічних помилок бази даних (наприклад, невірний пароль)
                                    Console.WriteLine($"Помилка бази даних: {ex.Message}");
                                }
                            }
                            data = true; break;
                        }
                    case 4:
                        {
                            Console.WriteLine("ЩО ТИ ТУТ ЗАБРАВСЯ МІНЯТИ?");
                            data = true; break;
                        }


                    case 0:
                        {
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ДАНI ВВЕДЕНI НЕВIРНО, ПОВТОРIТЬ ВИБIР\n");
                            break;
                        }
                }
            }

            }  }
    }

