using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;
using TaskManager;
using Telegram.BotAPI.AvailableTypes;
using InlineKeyboardButton = Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton;
using Microsoft.Build.Utilities;
using Task = System.Threading.Tasks.Task;
using Microsoft.Build.Framework;


namespace TaskManager
{
    /// <summary>
    /// Обработчик входящих обновлений от Telegram.
    /// </summary>
    internal class UpdateHandler : IUpdateHandler
    {
        #region Поля и свойства
        /// <summary>
        /// Создание клиента для работы с Телеграм ботом.
        /// </summary>
        private readonly ITelegramBotClient _botClient;

        /// <summary>
        /// Настройка сериализации JSON.
        /// </summary>
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = false,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        /// <summary>
        /// Проверяет была ли запущена команда /create
        /// </summary>
        public static bool create;

        /// <summary>
        /// Ассоциативный массив для передачи данных о задаче
        /// </summary>
        //public static Dictionary<string, string> taskData = new Dictionary<string, string>();
        WorkTaskManager newTaskManager = new WorkTaskManager();
        CommentsManager commentsManager = new CommentsManager();
        public static string login;
        public static string password;
        public static User currentUser;

        #endregion

        #region Методы
        /// <summary>
        /// Отправка текстового сообщения.
        /// </summary>
        /// <param name="chatId">Id пользователя.</param>
        /// <param name="text">Техт пользователя.</param>
        /// <param name="cancellationToken">Токен для отмены операции.</param>
        private async Task SendTextMessageAsync(long chatId, string text, string v, CancellationToken cancellationToken, Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup replyMarkup)
        {
            await _botClient.SendMessage(chatId: chatId, text: text, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Логирование входящих обновлений от Telegram.
        /// </summary>
        /// <param name="update">Обновления от Телеграм.</param>
        private void LogUpdate(Update update)
        {
            try
            {
                var json = JsonSerializer.Serialize(update, JsonOptions);
                //Console.WriteLine($"Обновление получено: {json}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сериализации: {ex.Message}");
            }
        }
        #endregion

        #region <IUpdateHandler>
        
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            LogUpdate(update);
            try
            {
                switch (update.Type)
                {
                    case Telegram.Bot.Types.Enums.UpdateType.Message:
                        {
                            if (update.Message is not Telegram.Bot.Types.Message message) return;

                            var chatId = message.Chat.Id;
                            var text = message.Text;
                            if (message.Type == MessageType.Text && !string.IsNullOrEmpty(text))
                            {
                                if ((text == BotChatCommands.Start)&&(currentUser==null))
                                {
                                    var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]{
                                    new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Авторизация", "auth_")

                                        },
                                    new[]
                                        {

                                            InlineKeyboardButton.WithCallbackData("Регистрация", "registration_")
                                        } });

                                    await botClient.SendMessage(
                                        chatId: chatId, 
                                        text:$"🙌🏿 Добро пожаловать!\n\n" +
                                      "Я бот для работы задачами нашей компании😉\n" +
                                      "Благодаря мне ты можешь:\n " +
                                      " * Брать в работу свободные задачи\n" +
                                      " * Просматривать свои задачи\n" +
                                      " * Комментировать свои задачи\n" +
                                      " * Редактировать статус задач\n " +
                                      "Вы сможете пользоваться функционалом после авторизации или регистрации",
                                         replyMarkup: keyboard, 
                                         cancellationToken: cancellationToken);                                          
                                }                          
                                else if (CommandManager.СurrentStatus == "auth")
                                {
                                    CommandManager.AuthAsync(botClient, update);
                                    string authMessage;
                                    if ((currentUser != null)&&(currentUser.Role!="admin"))
                                    {
                                        authMessage = $"Добро пожаловать, {currentUser.Name}";
                                        var userKeyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]{
                                    new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Мои задачи", "taskList_")

                                        },
                                    new[]
                                        {

                                            InlineKeyboardButton.WithCallbackData("Свободные задачи", "freeTask_")
                                        },
                                    new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Показать информацию о задаче", "taskInfo_")

                                        },
                                    new[]
                                        {

                                            InlineKeyboardButton.WithCallbackData("Выйти", "aut_")
                                        }
                                    }
                                   );
                                        await botClient.SendMessage(
                                        chatId: chatId,
                                        text: $"{authMessage}\n Вот список команд для моей работы:\n\n",
                                        replyMarkup: userKeyboard,
                                        cancellationToken: cancellationToken
                                        );
                                        return;
                                    }
                                    else
                                    {
                                        authMessage = "Пользователь не найден";
                                    }
                                    await botClient.SendMessage(
                                        chatId: chatId,
                                        text: authMessage,
                                        cancellationToken: cancellationToken
                                        );

                                }
                                
                            }
                        }
                        return;

                    case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                        {
                            var callbackQuery = update.CallbackQuery;
                            var user = callbackQuery.From;
                            var chat = callbackQuery.Message.Chat;                            
                            var tryBotton = callbackQuery.Data;
                            var parse = tryBotton.Split('_');                            
                            var action = parse[0];
                            Console.WriteLine(action);
                            string listTitle;
                            switch (action)
                            {
                                case "auth":                                    
                                    CommandManager.AuthMessageAsync(botClient, update);                                    
                                    break;
                                case "registration":
                                    break;
                                case "taskList":
                                    listTitle = "Мои задачи:";
                                    List <WorkTask> tasks = newTaskManager.ListCreate(currentUser);                                    
                                    await CommandManager.TakeAllTasksCommand(botClient, chat.Id, cancellationToken, tasks);
                                    break;

                                case "freeTask":
                                    listTitle = "Свободные задачи:";
                                    var freeTasks = newTaskManager.FreeTaskList();                                    
                                    await CommandManager.TakeAllTasksCommand(botClient, chat.Id, cancellationToken, freeTasks);
                                    break;

                                case "taskInfo":
                                    listTitle = "Информация о задаче";
                                    int IdTask = 1;
                                    WorkTask userTask = newTaskManager.Search(IdTask);
                                    Console.WriteLine(userTask.Name);
                                    string taskInfo = $"ID: {userTask.Id}\n" +
                                        $"Заголовок: {userTask.Name}\n" +
                                        $"Описание: {userTask.Description} \n" +
                                        $"Дедлайн: {userTask.Deadline}\n" +
                                        $"Дата создания: {userTask.СreationDate}\n" +
                                        $"Статус: {userTask.Status}\n" +
                                        $"Приоритет: {userTask.Priority}\n";
                                        taskInfo += $"\n Ответственные: \n";
                                        foreach (User item in userTask.Responsible)
                                        {
                                            taskInfo += item.Name +" "+ item.Surname;
                                        }
                                        taskInfo += $"\n Комментарии: \n";

                                        List<Comment> comments = commentsManager.SearchTaskComment(IdTask);
                                        foreach (Comment comment in comments)
                                        {
                                            taskInfo += $"{comment.Author}\n       {comment.Description}\n";
                                        }
                                    await botClient.SendMessage(
                                    chatId: chat.Id,
                                    text: taskInfo,
                                    cancellationToken: cancellationToken
                                    );
                                    break;

                                case "edit":
                                    break;
                                case "work":
                                    break;

                            }                       
                            await botClient.AnswerCallbackQuery(callbackQuery.Id);
                        }
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка при обработке сообщения: {ex.Message}");
            }
        }

        private async Task SendTextMessageAsync(long chatId, string text, Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task SendTextMessageAsync(long chatId, string v, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Конструкторы
        public UpdateHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        #endregion
    }
}
