# TrackingRemoteHostService
Система для отслеживания доступности удаленных хостов

### Документация

Подключен swagger по пути - /swagger
Аутентификация производится с использованием стандарта JWT

------------
### POST /api/Users
##### Добавление пользователя
##### Тело запроса 
```json
{
  "firstName": "Ivan",
  "secondName": "Ivanov",
  "login": "login",
  "password": "12345678"
}
```
##### Результат 
```json
{
  "id": 1,
  "firstName": "Ivan",
  "secondName": "Ivanov"
}
```
------------
### POST /api/Auth/signin
#####  Авторизация
##### Тело запроса  
```json
{
  "login": "login",
  "password": "12345678" //Минимально количество символов 8
}
```
#####  Результат 
```json
{
  "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", //Токен JWT
  "username": "Ivan Ivanov", //Полное имя пользователя
  "id": 1 //Иденитификатор пользователя
}
```
------------
### POST /api/Tracking
##### Добавление адрес и частоты проверки доступности
##### Тело запроса 
```json
{
  "host": "string",
  "interval": 0  //Интервал между проверками, в секундах
}
```
##### Результат 
Возвращает идентификатор добавленного адреса и частоты проверки доступности
------------
### GET /api/Tracking/history
##### Получение истории проверок за заданный промежуток времени
##### Параметры 
startTime (long) - Дата начала
endTime (long) - Дата окончания
#####  Результат 
```json
[
  {
    "host": "google.com",  //Хост
    "date": "2021.09.19 19:00", //Дата проверки
    "available": true   //Доступен ли хост
  }
]
```
------------
### GET /api/Tracking/history/current
##### Получение текущего состояния работоспособности проверяемых адресов
#####  Результат 
```json
[
  {
    "host": "google.com",  //Хост
    "date": "2021.09.19 19:00", //Дата проверки
    "available": true   //Доступен ли хост
  }
]
```
