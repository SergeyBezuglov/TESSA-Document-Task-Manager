# TESSA-Document-Task-Manager
<h1>Реализовал задачу согласно с ТЗ</h1>
<p>Чтобы начать работу нужно запустить проект через Visual Studio </p>
<p>После того как вы запустили проект в Visual Studio.Вам нужно будет установить ваше значение в БД </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/cd1efab3-44a0-4db5-82bb-1082c7604cfd"/>
<p>Заходим в appsettings и appsettings.Development и меняем на значения  </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/5f137186-0cd3-454a-8227-5ca976814f11"/>
<p>Как их получить ?</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/6e7b0a41-1cdd-4d72-a15d-9d215b20abcc"/>
<h2>Если выскочит проблема связанная с миграцией то просто удаляем папку связаную с   миграцией </h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/d43ce111-0012-4510-89dc-bab1ecb6186b"/>
<p>И нужно будет ее установить  для этого: </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/9dd4ea43-ae2b-47c3-ae9f-a15da43814a1"/>
<p>Копируем и вставляем в "Консоль диспетчеров пакетов"</p>
<p>После этого таким же образом копируем код для обновления и вставляем туда же </p>
<h3>Поздравляю у вас получилось запустить проект</h3>
<p>У вас запуститься Swagger , и чтобы посмотреть клиентскую часть </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/42720c60-a420-4532-b6f7-22a0f410613d"/>
<p>Нажмите на кнопку он вам пока выведет : [] так как ваша БД пустая . После этого у вас запуститься клиентская часть которая написана на Vue(я знаю что вы не просили но мне было так проще) </p>
<p>Если вы никогда не работали с Vue , то вам для начала нужно будет установить <a href="https://nodejs.org/en/download/prebuilt-installer"> Node.js</a></p>
<p>После это нужно будет зайти в VS Code и терминале написать npm i </p>
<h3>Я надесь у вас все запуститься !!!!</h3>
<p>После того как у вас запустилось все с "Божьей помощи" то у вас откроется консоль </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/90e809f7-70c7-4aa8-8b98-6d2a0912a103"/>
<p>Просто скопируйте и вставьте в Браузер </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/4c3ea4e7-f494-4d3d-84d7-4a17a3c85526"/>
<p>У вас откроется веб-приложение в котором вы сами можете создать документ и после подцепить к нему задачи.Для этого просто выберите статус документа и нажмите создать</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/5849c48b-296e-4170-9d7a-5d99f9aa0a53"/>
<p>Чтобы добавить задачу просто нажмите на ваш новый документ, и внутри него можно будет заполнить поля создания задач </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/479739fe-d0b3-477e-8c54-2a6c60166a18"/>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/2ed9a7fc-6135-44ac-b3e4-d53c8a30515a"/>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/8ad2d252-b576-42f1-92d3-074e74ff328e"/>
<p>Вот наконец мы создали задачу , и согласно ТЗ у нас есть две кнопки: "Выполнить", "Отмена". После нажатия кнопки выполнить задача пропадает и у документа меняется статус:"В оперативном архиве" </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/ca849246-6abe-4893-b72a-f5fd08b33585"/>
<h1>Урааа теперь самое интересное листнг кода</h1>
<p>Я делал по методолгие Onion и по архитектуре DDD ( да я знаю что это сложная , но мне же надо вас чем-то заинтересовать)</p>
<h2>Начнем с первого как наша Документ и Задача написаны в виде базовых сущностей  </h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/5b351678-7466-455f-b48a-c7f38496f744"/>
<p>Документ как базовая сущность</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/e32006bd-70d3-4ac0-8eaa-7900c3a4541e"/>
<p>Задача как баовая сущность</p>
<h2>Так же реализовал репозиторий Документа </h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/3713dd00-c32d-4cbb-8e72-c63af0efb657"/>
<p>Первая часть репозитория Документа</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/5e071131-fd60-4aac-b25a-e190450f7d90"/>
<p>Вторая часть репозитория Документа</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/4d0139f1-f613-4a25-833c-520c0a5e2765"/>
<p>Интерфейс нашего репозитория </p>
<h2>Репозиторий Задачи</h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/0552cd41-f0d3-401e-9906-5fefc6d8062b"/>
<p>Первая часть репозитория Задачи</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/4c84cbf8-d5bc-47ef-81d3-ffcadde6a300"/>
<p>Вторая часть репозитория Задачи</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/1bd614b2-8377-44cc-a701-dbb0d1f0bb0e"/>
<p>Третья часть репозитория Задачи</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/eb0b2d59-38cf-4b91-b27f-2156ce2817b9"/>
<p>Интерфейс нашего репозитория </p>
<h2>Теперь перейдем к контролеру Документа</h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/d99a58d0-cb82-456a-9e84-6be90e2f5710"/>
<p>Первая часть контролера</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/4de01ee6-438e-4d0e-9945-f21e5f364599"/>
<p>Вторая часть контролера</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/37d8a18a-ab82-4acf-82f7-3f91ae4d5854"/>
<p>Третья часть контролера</p>
<h2>Теперь перейдем к контролеру Задачи</h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/e9499cae-a11b-4dfa-bfb0-9cf35021796c"/>
<p>Первая часть контролера</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/3d0ce28f-b5dc-4d53-819f-525a7ec5a857"/>
<p>Вторая часть контролера</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/c603f803-9bb2-401c-a7c8-42f544d74398"/>
<p>Третья часть контролера</p>
<h1>Вы наверное скажите Сережа это все хорошо , а как ты записываешь в БД , а вот это сейчас покажу</h1>
<p>У меня есть PIMSDbContext через который создается контекст БД и используя конфигурацию  </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/839564db-02c7-4217-a40e-787c4867604d"/>
<p>Листинг кода PIMSDbContext </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/bc535b89-92a8-4873-9ae0-f3d7cbc44ade"/>
<p>Конфигурация нашего Документа</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/a448f8d5-e185-4291-be64-9875bbcf6468"/>
<p>Конфигурая нашей Задачи</p>
<p>С помощью этого собирается наша миграция</p>
<h2>Как хранится это все в БД?</h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/91cb2f72-54bc-4e71-a75d-bac078d332af"/>
<p>Хранение Задачи в БД</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/d1dcc5aa-c2a5-487d-b331-e1d193b37b85"/>
<p>Хранение Документа в БД</p>
<h1>На этом все.Спасибо вам за проявленое терпение чтения моего файла . Надеюсь вам понравиться моя работа!!! </h1>
