# TESSA-Document-Task-Manager
<h1>Реализовал задачу согласно с ТЗ</h1>
<h2>Видео моего проекта </h2>
<a href="https://www.youtube.com/watch?v=CD5wrhP6kZo">Видео моего проекта</a>
<h1>Если у вас возникнет проблема с запуском проекта. То просто зайдите в папку Other и в ней будет лежать архив вам нужно будет его скачать </h1>
 <img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/bc872e54-bdfe-42ad-bb3c-b9a671d40c3d"/>
<p>Вам нужно будет распоковать в папку Client(просто чтобы не мучаться с сертификатам и библиотеками)</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/80a556bb-eb48-4bc1-be19-db75fa137806"/>
<p>После этого у вас выскочит при запуске клиента предупреждение с сертификатом вы просто нажмите "OK"</p>
<h1>Урааа теперь самое интересное листинг кода</h1>
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
<h1>Методы для записи в БД</h1>
<p>У меня есть PIMSDbContext через который создается контекст БД и используя конфигурацию  </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/5d33a387-f9f0-4ca6-8a6b-7be40bd88f77"/>
<p>Листинг кода PIMSDbContext </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/d1aeb4a1-3e6b-431d-aa7a-eb5edf425f42"/>
<p>Код в DependencyInjection в каталоге  PIMS.Infrastructure </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/9c6c93bc-2b7a-48c9-8dee-8a506fa9cb98"/>
<p>Код в  appsettings.json</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/bc535b89-92a8-4873-9ae0-f3d7cbc44ade"/>
<p>Конфигурация нашего Документа</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/a448f8d5-e185-4291-be64-9875bbcf6468"/>
<p>Конфигурая нашей Задачи</p>
<p>С помощью этого собирается наша миграция</p>
<h2>Как хранится это все в БД?</h2>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/7c79f8c7-02e6-44c1-ac7f-45006d1c2c68"/>
<p>Хранение Задачи в БД</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/25a98838-9374-4fce-b16b-811e3e80c512"/>
<p>Хранение Документа в БД</p>
<h1>Код на клиентской стороне </h1>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/042c5347-e11c-4437-8d44-a71d1bd00529"/>
<p>Код клиента часть 1</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/cba0ca6c-d4f9-436d-aadd-6113a1eec2eb"/>
<p>Код клиента часть 2. Использую <b>axios</b> для связи методов серверной части </p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/b0b0c2ca-1612-4457-9e1f-8f64b7ef5d15"/>
<p>Код клиента часть 3</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/9434de29-a564-486a-a8fb-1c3c93a90a2f"/>
<p>Код клиента часть 4. Здесь пердставленный методы которые , принимают значения с серверной части , и работают с ними . Если нужно удаляют задачи , или создают документы/задачи</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/098a5fc6-5c7b-49f4-a085-122244a637b0"/>
<p>Код клиента часть 5</p>
<img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/f29148ad-d3ef-44cb-8c98-d07f70e92146"/>
<p>Код клиента часть 6. Здесь пердставлены стили , с помощью которых мой клиент становится  более менее красивым</p>
<h1>На этом все. Спасибо вам за проявленое терпение чтения моего файла . Надеюсь вам понравиться моя работа!!! </h1>
 <img src="https://github.com/SergeyBezuglov/TESSA-Document-Task-Manager/assets/143338316/3974eab5-447e-4bb4-b37a-0d53644f22d2"/>
