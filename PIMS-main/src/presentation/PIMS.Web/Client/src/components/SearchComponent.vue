<template>
    <div id="app">
        <h1>Document Task Manager</h1>
        <div v-if="!selectedDocument">
            <h2>Documents</h2>
            <ul>
                <li v-for="document in documents" :key="document.ID">
                    <button @click="selectDocument(document.ID)">
                        {{ document.ID }} - {{ document.Status }}
                    </button>
                </li>
            </ul>
            <button @click="createDocument">Create New Document</button>
        </div>

        <div v-else>
            <button @click="backToList">Back to List</button>
            <h2>Document Details</h2>
            <p>ID: {{ selectedDocument.ID }}</p>
            <p>Status: {{ selectedDocument.Status }}</p>

            <h3>Tasks</h3>
            <ul>
                <li v-for="task in selectedDocument.Tasks" :key="task.ID">
                    <span>{{ task.Name }}</span>
                    <button @click="completeTask(task.ID)">Complete</button>
                    <button @click="cancelTask(task.ID)">Cancel</button>
                </li>
            </ul>

            <button @click="addTask">Add Task</button>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                documents: [],
                selectedDocument: null,
            };
        },
        created() {
            this.fetchDocuments();
        },
        methods: {
            async fetchDocuments() {
                try {
                    const response = await axios.get('https://localhost:7085/api/v1/Documents?api-version=1.0');
                    this.documents = response.data;
                } catch (error) {
                    console.error('Error fetching documents:', error);
                }
            },
            async selectDocument(id) {
                try {
                    const response = await axios.get(`https://localhost:7085/api/v1/Documents/${id}?api-version=1.0`);
                    this.selectedDocument = response.data;
                } catch (error) {
                    console.error('Error fetching document:', error);
                }
            },
            backToList() {
                this.selectedDocument = null;
            },
            async createDocument() {
                const newDocument = {
                    ID: `doc-${Date.now()}`,
                    Status: 'В работе',
                    ActiveTaskID: null,
                    ActiveTask: null,
                    Tasks: [],
                };
                try {
                    await axios.post('https://localhost:7085/api/v1/Documents?api-version=1.0', newDocument);
                    this.fetchDocuments();
                } catch (error) {
                    console.error('Error creating document:', error);
                }
            },
            async addTask() {
                const newTask = {
                    ID: `task-${Date.now()}`,
                    Name: `Задача ${this.selectedDocument.Tasks.length + 1}`,
                    PreviousTaskID: this.selectedDocument.Tasks.length ? this.selectedDocument.Tasks[this.selectedDocument.Tasks.length - 1].ID : null,
                    DocumentID: this.selectedDocument.ID,
                    Document: null,
                };
                try {
                    this.selectedDocument.Tasks.push(newTask);
                    if (!this.selectedDocument.ActiveTaskID) {
                        this.selectedDocument.ActiveTaskID = newTask.ID;
                        this.selectedDocument.ActiveTask = newTask;
                    }
                    await axios.put(`https://localhost:7085/api/v1/Documents/${this.selectedDocument.ID}?api-version=1.0`, this.selectedDocument);
                    this.fetchDocuments();
                    this.selectDocument(this.selectedDocument.ID);
                } catch (error) {
                    console.error('Error adding task:', error);
                }
            },
            async completeTask(taskId) {
                try {
                    const taskIndex = this.selectedDocument.Tasks.findIndex(task => task.ID === taskId);
                    if (taskIndex !== -1 && taskIndex < this.selectedDocument.Tasks.length - 1) {
                        this.selectedDocument.ActiveTaskID = this.selectedDocument.Tasks[taskIndex + 1].ID;
                        this.selectedDocument.ActiveTask = this.selectedDocument.Tasks[taskIndex + 1];
                    } else {
                        this.selectedDocument.Status = 'В оперативном архиве';
                        this.selectedDocument.ActiveTaskID = null;
                        this.selectedDocument.ActiveTask = null;
                    }
                    await axios.put(`https://localhost:7085/api/v1/Documents/${this.selectedDocument.ID}?api-version=1.0`, this.selectedDocument);
                    this.fetchDocuments();
                    this.selectDocument(this.selectedDocument.ID);
                } catch (error) {
                    console.error('Error completing task:', error);
                }
            },
            async cancelTask(taskId) {
                try {
                    this.selectedDocument.Status = 'Отмена';
                    this.selectedDocument.ActiveTaskID = null;
                    this.selectedDocument.ActiveTask = null;
                    await axios.put(`https://localhost:7085/api/v1/Documents/${this.selectedDocument.ID}?api-version=1.0`, this.selectedDocument);
                    this.fetchDocuments();
                    this.selectDocument(this.selectedDocument.ID);
                } catch (error) {
                    console.error('Error cancelling task:', error);
                }
            }
        }
    };
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }

    button {
        margin: 5px;
    }
</style>