<template>
    <div id="app">
        <h1>Документы и задачи</h1>
        <transition name="fade" mode="out-in">
            <div v-if="!selectedDocument" key="document-list">
                <h2>Список документов</h2>
                <ul>
                    <li v-for="document in documents" :key="document.ID">
                        <button @click="selectDocument(document.ID)">
                            {{ document.ID }} - {{ document.Status }}
                        </button>
                    </li>
                </ul>
                <div>
                    <select v-model="newDocumentStatus">
                        <option disabled value="">Выберите статус</option>
                        <option value="В работе">В работе</option>
                        <option value="В архиве">В архиве</option>
                        <option value="Отменен">Отменен</option>
                    </select>
                    <button @click="createDocument">Создать новый документ</button>
                </div>
            </div>

            <div v-else key="document-details">
                <button @click="backToList">Вернуться к списку</button>
                <h2>Детали документа</h2>
                <p>ID: {{ selectedDocument.ID }}</p>
                <p>Статус: {{ selectedDocument.Status }}</p>

                <h3>Задачи</h3>
                <ul>
                    <li v-for="task in selectedDocument.Tasks" :key="task.ID">
                        <span>{{ task.Name }}</span>
                        <button @click="completeTask(task.ID)">Выполнено</button>
                        <button @click="cancelTask(task.ID)">Отмена</button>
                    </li>
                </ul>

                <!-- Добавляем новое текстовое поле для имени задачи -->
                <input type="text" v-model="newTaskName" placeholder="Введите имя задачи">
                <button @click="addTask">Добавить задачу</button>
            </div>
        </transition>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            documents: [],
            selectedDocument: null,
            newDocumentStatus: '',  // Для хранения выбранного статуса нового документа
            newTaskName: ''  // Для хранения имени новой задачи
        };
    },
    created() {
        this.fetchDocuments();
    },
    methods: {
        async fetchDocuments() {
            try {
                const response = await axios.get('https://localhost:7085/api/v1/Documents?api-version=1.0');
                this.documents = response.data.$values.map(doc => {
                    // Преобразуем задачи
                    if (doc.Tasks && doc.Tasks.$values) {
                        doc.Tasks = doc.Tasks.$values;
                    } else {
                        doc.Tasks = [];
                    }
                    return doc;
                });
            } catch (error) {
                console.error('Ошибка при получении документов:', error);
            }
        },
        async selectDocument(id) {
            try {
                const response = await axios.get(`https://localhost:7085/api/v1/Documents/${id}?api-version=1.0`);
                this.selectedDocument = response.data;
                this.selectedDocument.Tasks = this.selectedDocument.Tasks.$values || [];
            } catch (error) {
                console.error('Ошибка при выборе документа:', error);
            }
        },
        backToList() {
            this.selectedDocument = null;
        },
        async createDocument() {
            if (!this.newDocumentStatus) {
                alert('Пожалуйста, выберите статус для документа');
                return;
            }
            const newDocument = {
                ID: `doc-${Date.now()}`,
                Status: this.newDocumentStatus,
                ActiveTaskID: null,
                ActiveTask: null,
                Tasks: [],
            };
            try {
                await axios.post('https://localhost:7085/api/v1/Documents?api-version=1.0', newDocument);
                this.fetchDocuments();
                this.newDocumentStatus = '';  // Сбросить после создания
            } catch (error) {
                console.error('Ошибка при создании документа:', error);
            }
        },
        async addTask() {
            if (!this.selectedDocument || !this.newTaskName.trim()) {
                alert('Выберите документ и введите имя задачи');
                return;
            }
            const newTask = {
                ID: `task-${Date.now()}`,
                Name: this.newTaskName,
                PreviousTaskID: this.selectedDocument.Tasks.length ? this.selectedDocument.Tasks[this.selectedDocument.Tasks.length - 1].ID : null,
                DocumentID: this.selectedDocument.ID,
            };
            try {
                const response = await axios.post(`https://localhost:7085/api/v1/ProjectTasks`, newTask);
                this.selectedDocument.Tasks.push(response.data);
                this.newTaskName = ''; // Очистить имя задачи после добавления
                this.selectDocument(this.selectedDocument.ID);  // Обновить представление документа
            } catch (error) {
                console.error('Ошибка при добавлении задачи:', error);
            }
        },
        async completeTask(taskId) {
    if (!taskId) {
        console.error('Task ID is undefined');
        alert('Task ID is undefined or incorrect');
        return;
    }

    try {
        const response = await axios.delete(`https://localhost:7085/api/v1/ProjectTasks/${taskId}`);

        if (response.status === 204) {
            this.selectedDocument.Tasks = this.selectedDocument.Tasks.filter(task => task.ID !== taskId);

            if (this.selectedDocument.Tasks.length === 0) {
                await this.updateDocumentStatus(this.selectedDocument.ID, 'В оперативном архиве');
            }

            await this.selectDocument(this.selectedDocument.ID);
        }
    } catch (error) {
        console.error('Ошибка при удалении задачи:', error);
        alert(`Ошибка при удалении задачи: ${error.response?.data || error.message}`);
    }
},

async cancelTask(taskId) {
    if (!taskId) {
        console.error('Task ID is undefined');
        alert('Task ID is undefined or incorrect');
        return;
    }

    try {
        const response = await axios.delete(`https://localhost:7085/api/v1/ProjectTasks/${taskId}`);

        if (response.status === 204) {
            this.selectedDocument.Tasks = this.selectedDocument.Tasks.filter(task => task.ID !== taskId);

            await this.updateDocumentStatus(this.selectedDocument.ID, 'Отмена');

            await this.selectDocument(this.selectedDocument.ID);
        }
    } catch (error) {
        console.error('Ошибка при удалении задачи:', error);
        alert(`Ошибка при удалении задачи: ${error.response?.data || error.message}`);
    }
},

async updateDocumentStatus(documentId, status) {
    try {
        const updateData = { ID: documentId, Status: status };
        await axios.put(`https://localhost:7085/api/v1/Documents/${documentId}`, updateData);
        this.selectedDocument.Status = status;
    } catch (error) {
        console.error('Ошибка при обновлении статуса документа:', error);
        alert(`Ошибка при обновлении статуса документа: ${error.response?.data || error.message}`);
    }
}
    }
};
</script>

<style>
 #app {
    font-family: 'Helvetica Neue', Arial, sans-serif;
    text-align: center;
    color: #333;
    margin-top: 20px;
    max-width: 600px;
    margin-left: auto;
    margin-right: auto;
}

button {
    background-color: #4CAF50;
    color: white;
    border: none;
    padding: 10px 20px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    margin: 4px 2px;
    cursor: pointer;
    border-radius: 5px;
    transition: background-color 0.3s ease, box-shadow 0.3s ease;
}

button:hover {
    background-color: #45a049;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
}

ul {
    list-style-type: none;
    padding: 0;
}

li {
    padding: 8px;
    margin-bottom: 2px;
    background-color: #f9f9f9;
    border: 1px solid #ddd;
}

select, input[type="text"] {
    padding: 10px;
    margin-top: 10px;
    width: 50%;
    display: block;
    margin-left: auto;
    margin-right: auto;
    border-radius: 5px;
    border: 1px solid #ccc;
}

.fade-enter-active, .fade-leave-active {
    transition: opacity 0.5s;
}

.fade-enter, .fade-leave-to {
    opacity: 0;
}
</style>