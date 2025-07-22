# Telecom CRM Platform with GIS Integration

## Описание проекта

Телекоммуникационная CRM-платформа для управления клиентами, заказами и инфраструктурой телеком-компании. Система включает:

- Регистрацию клиентов, заказ услуг и биллинг
- Управление заявками на подключение
- Интерактивные карты покрытия и оборудования (GIS-модуль)
- Панель администратора с аналитикой и KPI
- Роли: Клиент, Администратор, Техник/Поддержка
- Интеграция с OpenAI ChatGPT для AI-ассистента в клиентском help-центре

---

## Технологии и стек

- Backend: ASP.NET Core Web API, EF Core, MediatR (CQRS)
- Frontend: Blazor Server (или WebAssembly)
- База данных: SQL Server / PostgreSQL
- GIS: Leaflet.js через BlazorInterop / ArcGIS SDK
- Аутентификация: ASP.NET Identity + JWT
- CI/CD: GitHub Actions / Azure Pipelines
- Контейнеризация: Docker и Docker-compose
- UI: Bootstrap, Radzen Blazor components

---

## Структура проекта

/TelecomCRM
├── /TelecomCRM.Application — бизнес-логика, CQRS Handlers
├── /TelecomCRM.Infrastructure — EF Core, Identity, GIS сервисы, OpenAI, Email
├── /TelecomCRM.Api — ASP.NET Core Web API, контроллеры, DTO
├── /TelecomCRM.ClientApp — Blazor UI, компоненты, карты
├── /Docs — ER-диаграммы, спецификации, скриншоты
└── README.md — описание проекта

---

## Запуск проекта локально

1. Склонировать репозиторий:
git clone https://github.com/nikawak/TelecomCRM.git
2. Настроить connection string в `appsettings.json` для базы данных.
3. Выполнить миграции базы данных:
dotnet ef database update --project TelecomCRM.Infrastructure
4. Запустить Backend API:
dotnet run --project TelecomCRM.Api
5. Запустить Frontend Blazor клиент:
dotnet run --project TelecomCRM.ClientApp
6. Открыть в браузере `https://localhost:5001`

---

## Особенности и возможности

- CQRS с MediatR для разделения команд и запросов
- Интерактивные карты с Leaflet.js и BlazorInterop
- Интеграция с OpenAI API (ChatGPT) в help-центр клиента
- Панель аналитики с Radzen Charts
- Многоязычность (ENG / RU)
- Docker-compose для локального деплоя
- Юнит-тесты (xUnit + Moq)

---

## Планы на будущее

- Расширение функционала биллинга
- Автоматизация CI/CD и деплоя в облако
- Добавление аудита изменений (Audit Trail)
- Поддержка мобильных устройств

---

## Контрибьютинг

Буду рад PR и идеям! Пожалуйста, открывайте issues для обсуждения и новых фич.

---

## Лицензия

MIT License © 2025
