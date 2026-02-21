using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySql.Data.MySqlClient; // Додаємо цей простір імен

namespace conditions
{
    internal class Program
    {
        /// <summary>
        /// Зчитує дані з бази даних MySql і виводить їх у консоль у вигляді таблиці.
        /// </summary>
        /// <param name="command">Підготовлена SQL-команда (запит SELECT).</param>
        /// <param name="isId">Чи виводити ID (за замовчуванням true).</param>
        /// <param name="isName">Чи виводити Ім'я (за замовчуванням true).</param>
        /// <param name="isLastname">Чи виводити Прізвище (за замовчуванням true).</param>
        /// <param name="isAge">Чи виводити Вік (за замовчуванням true).</param>
        /// <param name="isposition">Чи виводити Посаду (за замовчуванням true).</param>
        static void Write_table(MySqlCommand command, bool isId = true, bool isName = true, bool isLastname = true, bool isAge = true, bool isposition = true)
        {
            // Створюємо 'reader' для перегляду результатів запиту
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                // Поки в результатах запиту є рядки, які ми ще не прочитали
                while (reader.Read())
                {
                    // Перевіряємо кожен параметр: якщо true — виводимо відповідну колонку
                    if (isId)
                    {
                        Console.Write(reader["id"].ToString() + "  ");
                    }
                    
                    if (isName)
                    {
                        Console.Write(reader["name"].ToString() + "    ");
                    }
        
                    if (isLastname)
                    {
                        Console.Write(reader["last_name"].ToString() + "    ");
                    }
        
                    if (isAge)
                    {
                        Console.Write(reader["age"].ToString() + "    ");
                    }
        
                    if (isposition)
                    {
                        // Використовуємо індексатор ["column_name"], щоб дістати значення
                        Console.Write(reader["position"].ToString());
                    }
        
                    // Після того як надрукували всі колонки одного рядка, 
                    // переходимо на новий рядок для наступного запису.
                    Console.WriteLine();
                }
            }
        }

        static void Main(string[] args)
        {

            bool exit = false;
            string connString = "server=localhost;port=3306;database=dev_db;user=root;password=2RW4X5lRv;";
            
            while (!exit)
            {
                Console.WriteLine("Зробiть вибiр:");
                Console.Write("1 - виведення таблиц1\n" +
                              "2 - додати нового користувача\n" +
                              "3 - видалити користувача \n" +
                              "4 - змінити інформацію\n" +
                              "5 - пошук по імені\n" +
                              "6 - групування по прізвищу, вивести унікальні\n" +
                              "7 - сортування по будь якому полю\n" +
                              "0 - Вихiд\n->");
                int n = Convert.ToInt32(Console.ReadLine()); Console.Clear();
            
                switch (n)
                {
                    case 1:
                        {
                            MySqlConnection connection = new MySqlConnection(connString);

                            try
                            {
                                connection.Open();
                                Console.WriteLine("З'єднання встановлено успішно!");

                                string query = "SELECT * FROM users";
                                MySqlCommand command = new MySqlCommand(query, connection);

                                Write_table(command);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Помилка: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }

                            break;
                        }

                    case 2:
                        {
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

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("вид1ть id користувача якого збараєтесь видалити");
                            int userIdToRemove = Convert.ToInt32(Console.ReadLine()); Console.Clear();

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
                            break;

                        }
                    case 4:
                        {
                            Console.WriteLine("ЩО ТИ ТУТ ЗАБРАВСЯ М1НЯТИ?");
                            // Дані, які ми хочемо змінити
                            
                            Console.WriteLine("ведіть id користувача якого хочити редагувати");
                            int userId = Convert.ToInt32(Console.ReadLine()); Console.Clear();
                            Console.WriteLine("ведінть 1мя на яке хочити змінити");
                            string newName = Convert.ToString(Console.ReadLine()); Console.Clear();
                            Console.WriteLine("ведінть Прізвище на яке хочити змінити");
                            string last_name = Convert.ToString(Console.ReadLine()); Console.Clear();
                            Console.WriteLine("ведінть статус на яке хочити змінити");
                            string position = Convert.ToString(Console.ReadLine()); Console.Clear();
                            // 2. SQL-запит з використанням SET та параметрів
                            string sqlExpression = "UPDATE Users SET name = @name,position = @position,last_name = @last_name WHERE Id = @id";

                            using (MySqlConnection connection = new MySqlConnection(connString))
                            {
                                try
                                {
                                    connection.Open();

                                    MySqlCommand command = new MySqlCommand(sqlExpression, connection);
                                    // 3. Додаємо всі параметри для безпеки
                                    command.Parameters.AddWithValue("@name", newName);
                                    command.Parameters.AddWithValue("@id", userId);
                                    command.Parameters.AddWithValue("@last_name", last_name);
                                    command.Parameters.AddWithValue("@position", position);

                                    // 4. Виконуємо команду
                                    int result = command.ExecuteNonQuery();

                                    if (result > 0)
                                        Console.WriteLine("Дані успішно оновлено!");
                                    else
                                        Console.WriteLine("Користувача з таким ID не знайдено.");
                                }
                                catch (MySqlException ex)
                                {
                                    Console.WriteLine($"Помилка MySQL: {ex.Message}");
                                }
                            }
                             break;
                            
                            
                        }
                    case 5:
                    {

                            MySqlConnection connection = new MySqlConnection(connString);
                            Console.WriteLine("ведіть 1мя користувача");
                            string name= Convert.ToString(Console.ReadLine());
                            Console.Clear();
                            try
                            {
                                connection.Open();
                                Console.WriteLine("З'єднання встановлено успішно!");

                                string query = "SELECT * FROM users WHERE `name` LIKE CONCAT(@name, '%')";
                               
                                MySqlCommand command = new MySqlCommand(query, connection);
                                command.Parameters.AddWithValue("@name",name);

                                Write_table(command);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Помилка: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }
                            break;

                    }
                    case 6:
                        {
                            MySqlConnection connection = new MySqlConnection(connString);
                            try
                            {
                                connection.Open();
                                Console.WriteLine("З'єднання встановлено успішно!");

                                string query = "SELECT name, last_name, age FROM users GROUP BY name, last_name, age;";
                                MySqlCommand command = new MySqlCommand(query, connection);
                                Write_table(command, false,true,true, true,false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Помилка: " + ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }

                            break;
                        }
                    case 7:
                        {
                            // 1. Створюємо об'єкт підключення, передаючи йому рядок з налаштуваннями (адреса, назва БД, логін, пароль)
                            MySqlConnection connection = new MySqlConnection(connString);
                            
                            try 
                            {
                                // 2. Намагаємося відкрити "двері" до бази даних
                                connection.Open();
                                Console.WriteLine("З'єднання встановлено успішно!");
                            
                                // 3. Формуємо текст SQL-запиту (команда базі даних)
                                string query = "SELECT * FROM users ORDER BY age DESC";
                            
                                // 4. Створюємо команду, яка об'єднує текст запиту та наше відкрите з'єднання
                                MySqlCommand command = new MySqlCommand(query, connection);
                            
                                // 5. Передаємо цю команду в наш метод для виводу даних на екран
                                Write_table(command);
                            }
                            catch (Exception ex) 
                            {
                                // 6. Якщо на будь-якому етапі в блоці try сталася помилка (немає інтернету, невірний пароль тощо)
                                // ми "ловимо" її тут і виводимо текст помилки, щоб не "покласти" всю програму
                                Console.WriteLine("Помилка: " + ex.Message);
                            }
                            finally 
                            {
                                // 7. Цей блок виконається ЗАВЖДИ: і якщо все добре, і якщо сталася помилка.
                                // Обов'язково закриваємо з'єднання, щоб звільнити ресурси сервера.
                                connection.Close();
                            }
                            
                            // Вихід з switch/case (якщо цей код всередині меню)
                            break;
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
        }  
    }
}

