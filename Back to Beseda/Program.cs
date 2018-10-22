using System;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace Back_to_Beseda
{
    class Program
    {
        static VkApi vkapi = new VkApi();

        static void Main(string[] args)
        {
            string Login;
            string Password;
            ulong ID;
            string VKId;

            Console.WriteLine("Возращения пользователя в беседу by serega404");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("Аторизация через Api = 'y', через Login/Pass/Id = 'n'");
                Console.Write("Выберите способ авторизации (по умолчанию y): ");
                if (Console.ReadLine() == "n" )
                {
                    try
                    {
                        Console.WriteLine("Если пропустить ввод ID, то автоматически будет выбрано приложение Back to Beseda (6720120)");
                        Console.Write("Введите Id приложения: ");
                        VKId = Console.ReadLine();

                        if (VKId == "" || VKId == " ")
                        {
                            ID = ulong.Parse("6720120");
                        }
                        else
                        {
                            ID = ulong.Parse(VKId);
                        }

                        Console.Write("Введите логин: ");
                        Login = Console.ReadLine();

                        Console.Write("Введите пароль: ");
                        Password = Console.ReadLine();

                        Console.Clear();

                        if (Auth(Login, Password, ID))
                        {
                            Back();
                        }
                        else
                        {
                            Console.WriteLine("Не удалось авторизоваться!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка ввода данных авторизации приложения");
                    }
                }
                else
                {
                    try
                    {
                        Console.Write("Введите Api токен: ");
                        if (Api_Auth(Console.ReadLine()))
                        {
                            Console.Clear();
                            Back();
                        }
                        else
                        {
                            Console.WriteLine("Не удалось авторизоваться!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка ввода данных авторизации приложения");
                    }
                }
                    
                     

                
            }
        }

        static void Back()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("В ссылке беседы \"...&sel=dXX\", XX = id беседы ");

                    Console.Write("ID беседы: ");
                    string ChatID = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("В ссылке на страницу пользователя \".../idXXXXXXXXX\",где иксы это id пользователя ");
                    Console.WriteLine("Или можно открыть картинку пользователя и там в ссылке \"...photoXXXXXXXXX_...\",где иксы это id пользователя ");

                    Console.Write("ID пользователя: ");
                    long UserID = long.Parse(Console.ReadLine());

                    vkapi.Messages.RemoveChatUser(ulong.Parse(ChatID), UserID);
                    Console.Clear();
                    try
                    {
                        vkapi.Messages.AddChatUser(long.Parse(ChatID), UserID);
                        Console.WriteLine("Пользователь с id " + UserID + " успешно возращён в беседу!");
                        Console.WriteLine();
                    }
                    catch
                    {
                        Console.WriteLine("Не удалось вернуть пользователя!");
                        Console.WriteLine();
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Не удалось исключить пользователя из беседы!");
                    Console.WriteLine();
                }
            }
        }

        static bool Auth(string Login, string Password, ulong ID)
        {
            try
            {
                vkapi.Authorize(new ApiAuthParams
                {
                    Login = Login,
                    Password = Password,
                    Settings = Settings.All,
                    ApplicationId = ID,
                    TwoFactorAuthorization = () =>
                    {
                        Console.WriteLine("Впишите код двух-этапной авторизации или пропустите если не используете её");
                        Console.Write("Enter Code: ");
                        return Console.ReadLine();
                    }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool Api_Auth(string token)
        {
            try
            {
                vkapi.Authorize(new ApiAuthParams
                {
                    AccessToken = token
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
