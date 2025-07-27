using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelecomCRM.Application.ResponseModels
{
    public sealed record Error(string Code, string Description, int StatusCode);

    public static class Errors
    {
        // --- Общие ошибки ---
        public static readonly Error Unknown = new("Error.Unknown", "Неизвестная ошибка", 500);
        public static readonly Error InternalServer = new("Server.Internal", "Внутренняя ошибка сервера", 500);
        public static readonly Error Timeout = new("Server.Timeout", "Сервер не ответил вовремя", 504);
        public static readonly Error ServiceUnavailable = new("Server.Unavailable", "Сервис временно недоступен", 503);

        // --- Аутентификация и авторизация ---
        public static readonly Error Unauthorized = new("Auth.Unauthorized", "Пользователь не авторизован", 401);
        public static readonly Error Forbidden = new("Auth.Forbidden", "Доступ запрещен", 403);
        public static readonly Error InvalidCredentials = new("Auth.InvalidCredentials", "Неверный email или пароль", 401);
        public static readonly Error TokenExpired = new("Auth.TokenExpired", "Срок действия токена истёк", 401);
        public static readonly Error TokenInvalid = new("Auth.TokenInvalid", "Токен недействителен", 401);

        // --- Валидация ---
        public static readonly Error Validation = new("Validation.Failed", "Ошибка валидации", 400);
        public static Error FieldRequired(string field) =>
            new($"Validation.{field}.Required", $"Поле '{field}' обязательно", 400);
        public static Error FieldInvalid(string field) =>
            new($"Validation.{field}.Invalid", $"Поле '{field}' содержит недопустимое значение", 400);

        // --- Работа с сущностями ---
        public static Error NotFound(string entity) =>
            new($"{entity}.NotFound", $"{entity} не найден", 404);

        public static Error AlreadyExists(string entity) =>
            new($"{entity}.AlreadyExists", $"{entity} уже существует", 409);

        public static Error Conflict(string entity, string message) =>
            new($"{entity}.Conflict", message, 409);

        // --- База данных ---
        public static readonly Error DatabaseError = new("Database.Error", "Ошибка базы данных", 500);
        public static readonly Error DbUpdateFailed = new("Database.UpdateFailed", "Не удалось сохранить изменения", 500);

        // --- Внешние API ---
        public static readonly Error ExternalServiceUnavailable = new("External.Unavailable", "Внешний сервис недоступен", 503);
        public static readonly Error ExternalServiceError = new("External.Error", "Ошибка внешнего API", 502);
        public static Error ExternalServiceFailed(string serviceName) =>
            new($"External.{serviceName}.Failed", $"Ошибка при обращении к внешнему сервису {serviceName}", 502);

        // --- Логика приложения ---
        public static Error OperationNotAllowed(string reason) =>
            new("Operation.NotAllowed", reason, 400);
    }

}
