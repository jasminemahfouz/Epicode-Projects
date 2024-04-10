
document.addEventListener('DOMContentLoaded', function() {
    const taskInput = document.getElementById('taskInput');
    const addTaskBtn = document.getElementById('addTaskBtn');
    const taskList = document.getElementById('taskList');

    addTaskBtn.addEventListener('click', function() {
        const taskText = taskInput.value.trim();
        if (taskText !== '') {
            addTask(taskText);
            taskInput.value = '';
        }
    });

    function addTask(taskText) {
        const taskItem = document.createElement('li');
        
        // Crea un elemento span per avvolgere il testo del compito
        //preso da google per mettere la linea solo sul testo e non su 
        // intero li
        const taskTextSpan = document.createElement('span');
        taskTextSpan.textContent = taskText;
        
        // Aggiunge il testo del compito all'elemento <span> 
        taskItem.appendChild(taskTextSpan);
        
        taskItem.addEventListener('click', toggleTask);
        
        const deleteBtn = document.createElement('button');
        deleteBtn.textContent = 'Delete';
        deleteBtn.classList.add('delete_btn');
        
        deleteBtn.addEventListener('click', function() {
            taskList.removeChild(taskItem);
        });
        
        taskItem.appendChild(deleteBtn);
        taskList.appendChild(taskItem);
    }
    function toggleTask() {
        const textSpan = this.querySelector('span');
        textSpan.classList.toggle('completed');
    }
    function deleteTask() {
        this.parentElement.remove();
    }
});
