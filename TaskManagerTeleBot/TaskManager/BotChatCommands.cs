using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    /// <summary>
    /// Список команд боту при передаче текстовым сообщением.
    /// </summary>
    public static class BotChatCommands
    {
        /// <summary>
        /// Начать взаимодействие с ботом.
        /// </summary>
        public const string Start = "/start";

        /// <summary>
        /// Создать новую сущность (например, событие).
        /// </summary>
        public const string Create = "/create";

        /// <summary>
        /// Показать все элементы (например, всех участников).
        /// </summary>
        public const string All = "/all";

        /// <summary>
        /// Сменить статус состояния
        /// </summary>
        public const string NewStatus = "/newstatus";
        
        /// <summary>
        /// Остановить создание задачи
        /// </summary>
        public const string StopCreate = "/stopcreate";
    }
}

