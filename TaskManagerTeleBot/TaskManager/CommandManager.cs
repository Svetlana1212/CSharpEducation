using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using InlineKeyboardButton = Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton;
using Update = Telegram.Bot.Types.Update;


namespace TaskManager;

public class CommandManager
{
    // Класс для представления задачи
    //private static readonly TaskManager _taskManager = new TaskManager();

    public static string СurrentStatus;
    /// <summary>
    /// Метод для обработки команды /all
    /// </summary>
    /// <param name="botClient">TG Bot API клиента.</param>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="cancellationToken">Прерывание запроса.</param>
    public static async Task TakeAllTasksCommand(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken, List <WorkTask> tasks)
    {     
        if (!tasks.Any())
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Задач нет",
                cancellationToken: cancellationToken
            );
            return;
        }
        foreach (var task in tasks)
        {
            string message = $"ID: {task.Id}\nЗаголовок: {task.Name}\nОписание: {task.Description}";
            string prefix = (task.Status == "Свободная") ? "work_" : "edit_";
            string buttonText = (task.Status == "Свободная") ? "Взять в работу" : "Изменить статус";
            var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]{
        new[]
        {
            InlineKeyboardButton.WithCallbackData("Комментировать", $"comment_{task.Id}"),
            InlineKeyboardButton.WithCallbackData("Подробнее", $"info_{task.Id}")
        },
        new [] 
        {
            
            InlineKeyboardButton.WithCallbackData(buttonText, $"{prefix}{task.Id}")
            
            
        }
        });
            await botClient.SendMessage(
                chatId: chatId,
                text: message,
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
    }

    /// <summary>
    /// Обрабатывает команду /create, отправляет запрос пользователю.
    /// </summary>
    /// <param name="botClient">Идентификатор чата.</param>
    /// <param name="update"></param>
    /// <returns></returns>
    public static async Task RequestTaskDescriptionAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update)
    {
        var newChatId = update.Message.Chat.Id;
        var message = update.Message;
        string text = update.Message.Text;
        bool createTask = true;
        if (createTask == true)
        {
            if (СurrentStatus == "Id")
            {
                text = "Введите данные задачи. Введите Id задачи, затем команду /newstatus";
                СurrentStatus = "name";
            }
            else if (СurrentStatus == "name")
            {
                text = "Введите наименование задачи, затем команду /newstatus";
                СurrentStatus = "description";
            }
            else if (СurrentStatus == "description")
            {
                text = "Введите описание задачи, затем команду /newstatus";
                СurrentStatus = "priority";
            }
            else
            {
                text = "Введите приоритет задачи, затем команду /stopcreate";
                СurrentStatus = string.Empty;
                createTask = false;

            }
            await botClient.SendMessage(
                chatId: newChatId,
                text: text);
        }   
                
    }

    public static async Task AuthMessageAsync(ITelegramBotClient botClient, Update update)
    {
        var newChatId = update.CallbackQuery.Message.Chat.Id;
        var message = update.Message;
        await botClient.SendMessage(
        chatId: newChatId,
        text: "Введите сообщение в формате логин:<текст>;пароль:<текст>"
        );        
        СurrentStatus = "auth";
    }

    /// <summary>
    /// Обрабатывает команду /create,распарсивает ввод пользователя, вызывает метод создания задачи. 
    /// </summary>
    /// <param name="botClient">Идентификатор чата.</param>
    /// <param name="update"></param>
    /// <returns></returns>
    public static async Task AuthAsync(ITelegramBotClient botClient, Update update)
    {
        var newChatId = update.Message.Chat.Id;
        var message = update.Message;
        try
        {
            
            string login;
            string password;
            string userText = message.Text.ToString();            
            string[] textAuth = userText.Split(";");            
            string[] userLogin = textAuth[0].Split(":");
            string[] userPassword = textAuth[1].Split(":");            
            if ((userLogin[0] == "логин") && (userPassword[0] == "пароль"))
            {
                login = userLogin[1].Trim();
                password = userPassword[1].Trim();
                var currentUser = UpdateHandler.currentUser;
                currentUser=UserManager.Auth(login, password);                
            }
            else
            {
                await botClient.SendMessage(
                chatId: newChatId,
                text: "Неверный формат данных");
            }
        }
        catch (IndexOutOfRangeException)
        {
            await botClient.SendMessage(
            chatId: newChatId,
            text: "Неверный формат данных");
        }
    }
}

