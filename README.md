# autopark
Проект Автопарк.
Основной функционал: Учёт автомобилей + трекинг.

Архитектура.
3-х звенная.
Backend: ASP NET Core 5.0 WebApi + Postgres.
Frontend: Blazor

Реализовано развертывание приложения в Docker (Docker Compose).
Для доступа к Api предусмотрен SwaggerUI.

В проекте реализована авторизация и аутентификация для пользователей с ролью Manager, защита от CSRF-атак.

С помощью https://github.com/commandlineparser/commandline реализована консольная утилита генерации данных.


Трекинг: 
1) показ трека машины с помощью Yandex Static API 
![image](https://github.com/kvarlamov/autopark/assets/50484980/96bacd89-d7c9-43a6-9b09-021ba582fe36)

2) Генерация трека в реальном времени с привязкой к реальным дорогам с помощью https://openrouteservice.org/
   ![image](https://github.com/kvarlamov/autopark/assets/50484980/fc15dc19-2485-4182-a06f-6a442ec70ff3)

3) Генерация отчетов по выбранным периодам

4) Telegram-bot для отображения отчета конкретной машины за день/месяц
![9h3XKAJgD-w](https://github.com/kvarlamov/autopark/assets/50484980/99099cc3-f10c-4260-bc92-bdcffeed8608)
