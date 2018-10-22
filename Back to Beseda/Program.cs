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
            long UserID;
            string ChatID;
            string VKId;

            while (true)
            {
                Console.WriteLine("Возращения пользователя в беседу by serega404");

                Console.WriteLine("Если пропустить ввод ID, то автоматически будет выбрано приложение Back to Beseda");

                try
                {
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
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("В ссылке беседы \"...&sel=dXX\", XX = id беседы ");

                                Console.Write("ID беседы: ");
                                ChatID = Console.ReadLine();

                                Console.WriteLine("В ссылке на страницу пользователя \".../idXXXXXXXXX\",где иксы это id пользователя ");
                                Console.WriteLine("Или можно открыть картинку пользователя и там в ссылке \"...photoXXXXXXXXX_...\",где иксы это id пользователя ");

                                Console.Write("ID пользователя: ");
                                UserID = long.Parse(Console.ReadLine());

                                vkapi.Messages.RemoveChatUser(ulong.Parse(ChatID), UserID);
                                Console.Clear();
                                try
                                {
                                    vkapi.Messages.AddChatUser(long.Parse(ChatID), UserID);
                                    Console.WriteLine("Пользователь с id " + UserID + " успешно возращён в беседу!");
                                }
                                catch
                                {
                                    Console.WriteLine("Не удалось вернуть пользователя!");
                                }
                            }
                            catch
                            {
                                Console.Clear();
                                Console.WriteLine("Не удалось исключить пользователя из беседы!");
                            }
                        }
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

        static bool Auth(string Login, string Password, ulong ID)
        {
            try
            {
                vkapi.Authorize(new ApiAuthParams
                {
                    Login = Login,
                    Password = Password,
                    Settings = Settings.All,
                    ApplicationId = ID
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
